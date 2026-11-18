"""Journal v2 endpoints: manual trades with a partial-exit ladder, and R.

Lives beside the existing /api/trades router, which stays untouched and keeps
serving the TradeLogger auto-logging path. Every write here also refreshes the
aggregate columns on `trades` (exit_price, exit_time, pnl) so the old table and
the old stats keep reading correctly.
"""

from __future__ import annotations

from typing import List, Optional

from fastapi import APIRouter, Depends, HTTPException, Query
from sqlalchemy import select
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy.orm import selectinload

from app.database import get_db
from app.metrics import (
    r_multiple,
    session_of,
    tranche_pnl,
    weekday_of,
)
from app.models import Trade, TradeExit
from app.schemas_v2 import (
    ExitIn,
    ExitOut,
    TradeV2Create,
    TradeV2Response,
    TradeV2Update,
)

try:
    from app.routes.trades import _find_clip
except Exception:  # pragma: no cover - clip matching is optional
    def _find_clip(_exit_time: str):  # type: ignore
        return None

router = APIRouter(prefix="/api/v2/trades", tags=["trades-v2"])


# --- helpers -------------------------------------------------------------


async def _get_with_exits(db: AsyncSession, trade_id: int) -> Optional[Trade]:
    stmt = (
        select(Trade)
        .options(selectinload(Trade.exits))
        .where(Trade.id == trade_id)
    )
    return (await db.execute(stmt)).scalar_one_or_none()


def _raw_exits(trade: Trade) -> List[dict]:
    """Real tranches, or a single synthetic one for a legacy single-exit trade."""
    if trade.exits:
        return [
            dict(
                id=e.id,
                exit_price=e.exit_price,
                quantity=e.quantity,
                exit_time=e.exit_time,
                fees=e.fees,
                pnl=e.pnl,
                note=e.note,
            )
            for e in trade.exits
        ]
    return [
        dict(
            id=0,
            exit_price=trade.exit_price,
            quantity=trade.size,
            exit_time=trade.exit_time,
            fees=0.0,
            pnl=trade.pnl,
            note=None,
        )
    ]


def _apply_aggregates(trade: Trade) -> None:
    """Fold the exit ladder back into the legacy aggregate columns."""
    exits = list(trade.exits)
    if not exits:
        return
    total_qty = sum(e.quantity for e in exits) or trade.size or 1.0
    trade.exit_price = sum(e.exit_price * e.quantity for e in exits) / total_qty
    times = [e.exit_time for e in exits if e.exit_time]
    if times:
        trade.exit_time = max(times)
    pnls = [
        tranche_pnl(
            trade.direction,
            trade.entry_price,
            e.exit_price,
            e.quantity,
            trade.point_value,
            e.fees,
            e.pnl,
        )
        for e in exits
    ]
    trade.pnl = sum(p for p in pnls if p is not None)


def build_trade_response(trade: Trade) -> TradeV2Response:
    raw = _raw_exits(trade)
    total_qty = sum(e["quantity"] for e in raw) or 0.0

    out_exits: List[ExitOut] = []
    r_num = 0.0
    r_den = 0.0
    computed_pnl = 0.0
    any_pnl = False

    for e in raw:
        r = r_multiple(trade.direction, trade.entry_price, trade.stop_loss, e["exit_price"])
        alloc = (e["quantity"] / total_qty * 100.0) if total_qty else None
        p = tranche_pnl(
            trade.direction,
            trade.entry_price,
            e["exit_price"],
            e["quantity"],
            trade.point_value,
            e["fees"],
            e["pnl"],
        )
        if p is not None:
            computed_pnl += p
            any_pnl = True
        if r is not None:
            r_num += r * e["quantity"]
            r_den += e["quantity"]
        out_exits.append(
            ExitOut(
                id=e["id"],
                exit_price=e["exit_price"],
                quantity=e["quantity"],
                exit_time=e["exit_time"],
                fees=e["fees"],
                pnl=round(p, 2) if p is not None else None,
                note=e["note"],
                r_multiple=round(r, 3) if r is not None else None,
                allocation_pct=round(alloc, 1) if alloc is not None else None,
            )
        )

    realized_r = round(r_num / r_den, 3) if r_den else None
    avg_exit = (
        round(sum(e["exit_price"] * e["quantity"] for e in raw) / total_qty, 4)
        if total_qty
        else None
    )

    initial_risk: Optional[float] = None
    risk_pct: Optional[float] = None
    if trade.stop_loss is not None and trade.point_value:
        risk_money = abs(trade.entry_price - trade.stop_loss) * (trade.size or 0.0) * trade.point_value
        initial_risk = round(risk_money, 2)
        if trade.account_balance_at_entry:
            risk_pct = round(risk_money / trade.account_balance_at_entry * 100.0, 2)

    return TradeV2Response(
        id=trade.id,
        instrument=trade.instrument,
        direction=trade.direction,
        entry_price=trade.entry_price,
        exit_price=trade.exit_price,
        size=trade.size,
        pnl=trade.pnl,
        entry_time=trade.entry_time,
        exit_time=trade.exit_time,
        signal=trade.signal,
        signal_source=trade.signal_source,
        clip_filename=trade.clip_filename,
        notes=trade.notes,
        created_at=trade.created_at,
        stop_loss=trade.stop_loss,
        target=trade.target,
        point_value=trade.point_value,
        account_balance_at_entry=trade.account_balance_at_entry,
        strategy=trade.strategy,
        tags=trade.tags,
        review=trade.review,
        grade=trade.grade,
        exits=out_exits,
        realized_r=realized_r,
        total_exited_qty=round(total_qty, 4),
        open_qty=round((trade.size or 0.0) - total_qty, 4),
        avg_exit_price=avg_exit,
        computed_pnl=round(computed_pnl, 2) if any_pnl else None,
        initial_risk=initial_risk,
        risk_pct=risk_pct,
        scaled_out=len(trade.exits) > 1,
        session=session_of(trade.entry_time),
        weekday=weekday_of(trade.entry_time),
    )


def _new_exit(e: ExitIn) -> TradeExit:
    return TradeExit(
        exit_price=e.exit_price,
        quantity=e.quantity,
        exit_time=e.exit_time,
        fees=e.fees or 0.0,
        pnl=e.pnl,
        note=e.note,
    )


# --- endpoints -----------------------------------------------------------


@router.post("", response_model=TradeV2Response, status_code=201)
async def create_trade_v2(body: TradeV2Create, db: AsyncSession = Depends(get_db)):
    trade = Trade(
        instrument=body.instrument,
        direction=body.direction,
        entry_price=body.entry_price,
        size=body.size,
        entry_time=body.entry_time,
        exit_price=body.entry_price,  # placeholder until aggregates fold exits in
        exit_time=body.entry_time,
        pnl=0.0,
        stop_loss=body.stop_loss,
        target=body.target,
        point_value=body.point_value,
        account_balance_at_entry=body.account_balance_at_entry,
        strategy=body.strategy,
        tags=body.tags,
        signal=body.signal,
        signal_source=body.signal_source,
        notes=body.notes,
    )
    trade.exits = [_new_exit(e) for e in body.exits]
    _apply_aggregates(trade)
    trade.clip_filename = _find_clip(trade.exit_time)
    db.add(trade)
    await db.commit()
    fresh = await _get_with_exits(db, trade.id)
    return build_trade_response(fresh)


@router.get("", response_model=list[TradeV2Response])
async def list_trades_v2(
    strategy: Optional[str] = Query(None),
    instrument: Optional[str] = Query(None),
    direction: Optional[str] = Query(None),
    from_date: Optional[str] = Query(None, alias="from"),
    to_date: Optional[str] = Query(None, alias="to"),
    db: AsyncSession = Depends(get_db),
):
    stmt = select(Trade).options(selectinload(Trade.exits)).order_by(Trade.exit_time.desc())
    if strategy:
        stmt = stmt.where(Trade.strategy == strategy)
    if instrument:
        stmt = stmt.where(Trade.instrument == instrument)
    if direction:
        stmt = stmt.where(Trade.direction == direction)
    if from_date:
        stmt = stmt.where(Trade.exit_time >= from_date)
    if to_date:
        stmt = stmt.where(Trade.exit_time <= to_date)
    rows = (await db.execute(stmt)).scalars().all()
    return [build_trade_response(t) for t in rows]


@router.get("/{trade_id}", response_model=TradeV2Response)
async def get_trade_v2(trade_id: int, db: AsyncSession = Depends(get_db)):
    trade = await _get_with_exits(db, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")
    return build_trade_response(trade)


@router.put("/{trade_id}", response_model=TradeV2Response)
async def update_trade_v2(
    trade_id: int, body: TradeV2Update, db: AsyncSession = Depends(get_db)
):
    trade = await _get_with_exits(db, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")

    data = body.model_dump(exclude_unset=True)
    new_exits = data.pop("exits", None)
    for key, value in data.items():
        setattr(trade, key, value)

    if new_exits is not None:
        # delete-orphan cascade removes the old rows when we reassign
        trade.exits = [_new_exit(ExitIn(**e)) for e in new_exits]

    _apply_aggregates(trade)
    await db.commit()
    fresh = await _get_with_exits(db, trade_id)
    return build_trade_response(fresh)


@router.post("/{trade_id}/exits", response_model=TradeV2Response, status_code=201)
async def add_exit(trade_id: int, body: ExitIn, db: AsyncSession = Depends(get_db)):
    trade = await _get_with_exits(db, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")
    trade.exits.append(_new_exit(body))
    _apply_aggregates(trade)
    await db.commit()
    fresh = await _get_with_exits(db, trade_id)
    return build_trade_response(fresh)


@router.delete("/{trade_id}/exits/{exit_id}", response_model=TradeV2Response)
async def delete_exit(
    trade_id: int, exit_id: int, db: AsyncSession = Depends(get_db)
):
    trade = await _get_with_exits(db, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")
    remaining = [e for e in trade.exits if e.id != exit_id]
    if len(remaining) == len(trade.exits):
        raise HTTPException(status_code=404, detail="Exit not found")
    trade.exits = remaining
    _apply_aggregates(trade)
    await db.commit()
    fresh = await _get_with_exits(db, trade_id)
    return build_trade_response(fresh)


@router.delete("/{trade_id}", status_code=204)
async def delete_trade_v2(trade_id: int, db: AsyncSession = Depends(get_db)):
    trade = await db.get(Trade, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")
    await db.delete(trade)
    await db.commit()
    return None
