"""Review analytics for journal v2: realized R sliced the ways that matter.

R is the unit, because the edge in Elroy's real trades is management, not the
entry. Trades without a stop have no R and are left out of the R buckets, but
their money P&L still counts.
"""

from __future__ import annotations

from typing import Callable, List, Optional

from fastapi import APIRouter, Depends
from sqlalchemy import select
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy.orm import selectinload

from app.database import get_db
from app.metrics import ny_hour, session_of, weekday_of
from app.models import Trade
from app.routes.trades_v2 import build_trade_response
from app.schemas_v2 import RBucket, ReviewResponse

router = APIRouter(prefix="/api/v2/review", tags=["review"])

_WEEKDAY_ORDER = {
    "Monday": 0,
    "Tuesday": 1,
    "Wednesday": 2,
    "Thursday": 3,
    "Friday": 4,
    "Saturday": 5,
    "Sunday": 6,
}
_SESSION_ORDER = {"Asia": 0, "London": 1, "NY AM": 2, "NY PM": 3, "Close": 4}


class _Row:
    __slots__ = ("r", "pnl")

    def __init__(self, r: Optional[float], pnl: float):
        self.r = r
        self.pnl = pnl


def _bucket(key: str, rows: List[_Row]) -> RBucket:
    with_r = [row for row in rows if row.r is not None]
    total_r = sum(row.r for row in with_r)
    wins = sum(1 for row in with_r if row.r > 0)
    net_pnl = sum(row.pnl for row in rows)
    return RBucket(
        key=key,
        trades=len(rows),
        total_r=round(total_r, 2),
        avg_r=round(total_r / len(with_r), 3) if with_r else 0.0,
        win_rate=round(wins / len(with_r) * 100.0, 1) if with_r else 0.0,
        net_pnl=round(net_pnl, 2),
    )


def _group(
    rows: List[_Row],
    keys: List[Optional[str]],
    order: Optional[dict] = None,
) -> List[RBucket]:
    groups: dict[str, List[_Row]] = {}
    for row, key in zip(rows, keys):
        if key is None:
            key = "Unknown"
        groups.setdefault(key, []).append(row)
    buckets = [_bucket(k, v) for k, v in groups.items()]
    if order is not None:
        buckets.sort(key=lambda b: order.get(b.key, 99))
    else:
        buckets.sort(key=lambda b: b.key)
    return buckets


def _r_histogram(rows: List[_Row]) -> List[RBucket]:
    edges = [
        ("< -1R", lambda r: r < -1),
        ("-1R..0", lambda r: -1 <= r < 0),
        ("0..1R", lambda r: 0 <= r < 1),
        ("1R..2R", lambda r: 1 <= r < 2),
        ("2R..3R", lambda r: 2 <= r < 3),
        ("3R+", lambda r: r >= 3),
    ]
    with_r = [row for row in rows if row.r is not None]
    out: List[RBucket] = []
    for label, test in edges:
        sel = [row for row in with_r if test(row.r)]
        out.append(_bucket(label, sel))
    return out


@router.get("", response_model=ReviewResponse)
async def get_review(db: AsyncSession = Depends(get_db)):
    stmt = select(Trade).options(selectinload(Trade.exits))
    trades = list((await db.execute(stmt)).scalars().all())

    rows: List[_Row] = []
    strategies: List[Optional[str]] = []
    sessions: List[Optional[str]] = []
    weekdays: List[Optional[str]] = []
    hours: List[Optional[str]] = []
    tag_rows: List[_Row] = []
    tag_keys: List[Optional[str]] = []

    for t in trades:
        resp = build_trade_response(t)
        net = resp.computed_pnl if resp.computed_pnl is not None else resp.pnl
        row = _Row(resp.realized_r, net or 0.0)
        rows.append(row)
        strategies.append(t.strategy or t.signal or "Unknown")
        sessions.append(session_of(t.entry_time))
        weekdays.append(weekday_of(t.entry_time))
        h = ny_hour(t.entry_time)
        hours.append(f"{h:02d}:00 NY" if h is not None else None)
        if t.tags:
            for tag in [x.strip() for x in t.tags.split(",") if x.strip()]:
                tag_rows.append(row)
                tag_keys.append(tag)

    with_r = [row for row in rows if row.r is not None]
    total_r = sum(row.r for row in with_r)
    scaled = sum(1 for t in trades if len(t.exits) > 1)

    return ReviewResponse(
        trades_with_r=len(with_r),
        total_realized_r=round(total_r, 2),
        avg_r=round(total_r / len(with_r), 3) if with_r else 0.0,
        expectancy_r=round(total_r / len(with_r), 3) if with_r else 0.0,
        scaled_out_share=round(scaled / len(trades) * 100.0, 1) if trades else 0.0,
        by_strategy=_group(rows, strategies),
        by_session=_group(rows, sessions, _SESSION_ORDER),
        by_weekday=_group(rows, weekdays, _WEEKDAY_ORDER),
        by_hour=_group(rows, hours),
        by_tag=_group(tag_rows, tag_keys),
        r_distribution=_r_histogram(rows),
    )
