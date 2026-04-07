from __future__ import annotations

import os
from datetime import datetime, timedelta, timezone
from typing import Optional

from fastapi import APIRouter, Depends, HTTPException, Query
from sqlalchemy import select
from sqlalchemy.ext.asyncio import AsyncSession

from app.config import CLIP_MATCH_WINDOW_SECONDS, OBS_CLIPS_DIR
from app.database import get_db
from app.models import Trade
from app.schemas import TradeCreate, TradeNotes, TradeResponse

router = APIRouter(prefix="/api/trades", tags=["trades"])


def _find_clip(exit_time_str: str) -> Optional[str]:
    """Find the OBS replay clip closest to the trade exit time."""
    if not OBS_CLIPS_DIR.exists():
        return None

    try:
        exit_time = datetime.fromisoformat(exit_time_str)
    except ValueError:
        return None

    if exit_time.tzinfo is None:
        exit_time = exit_time.replace(tzinfo=timezone.utc)

    window = timedelta(seconds=CLIP_MATCH_WINDOW_SECONDS)
    best_match = None
    best_delta = None

    for f in OBS_CLIPS_DIR.iterdir():
        if not f.is_file():
            continue
        mtime = datetime.fromtimestamp(f.stat().st_mtime, tz=timezone.utc)
        delta = abs(mtime - exit_time)
        if delta <= window and (best_delta is None or delta < best_delta):
            best_match = f.name
            best_delta = delta

    return best_match


@router.post("", response_model=TradeResponse, status_code=201)
async def create_trade(trade: TradeCreate, db: AsyncSession = Depends(get_db)):
    clip = _find_clip(trade.exit_time)
    row = Trade(
        **trade.model_dump(),
        clip_filename=clip,
    )
    db.add(row)
    await db.commit()
    await db.refresh(row)
    return row


@router.get("", response_model=list[TradeResponse])
async def list_trades(
    signal: Optional[str] = Query(None),
    instrument: Optional[str] = Query(None),
    direction: Optional[str] = Query(None),
    from_date: Optional[str] = Query(None, alias="from"),
    to_date: Optional[str] = Query(None, alias="to"),
    db: AsyncSession = Depends(get_db),
):
    stmt = select(Trade).order_by(Trade.exit_time.desc())

    if signal:
        stmt = stmt.where(Trade.signal == signal)
    if instrument:
        stmt = stmt.where(Trade.instrument == instrument)
    if direction:
        stmt = stmt.where(Trade.direction == direction)
    if from_date:
        stmt = stmt.where(Trade.exit_time >= from_date)
    if to_date:
        stmt = stmt.where(Trade.exit_time <= to_date)

    result = await db.execute(stmt)
    return result.scalars().all()


@router.get("/{trade_id}", response_model=TradeResponse)
async def get_trade(trade_id: int, db: AsyncSession = Depends(get_db)):
    trade = await db.get(Trade, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")
    return trade


@router.put("/{trade_id}/notes", response_model=TradeResponse)
async def update_notes(
    trade_id: int, body: TradeNotes, db: AsyncSession = Depends(get_db)
):
    trade = await db.get(Trade, trade_id)
    if not trade:
        raise HTTPException(status_code=404, detail="Trade not found")
    trade.notes = body.notes
    await db.commit()
    await db.refresh(trade)
    return trade
