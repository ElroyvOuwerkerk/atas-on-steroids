from fastapi import APIRouter, Depends
from sqlalchemy import select
from sqlalchemy.ext.asyncio import AsyncSession

from app.database import get_db
from app.models import Trade
from app.schemas import SignalStats, StatsResponse

router = APIRouter(prefix="/api/stats", tags=["stats"])


def _compute_stats(trades: list[Trade], label: str) -> SignalStats:
    total = len(trades)
    if total == 0:
        return SignalStats(
            signal=label,
            total=0,
            wins=0,
            losses=0,
            win_rate=0.0,
            avg_pnl=0.0,
            cumulative_pnl=0.0,
        )

    wins = sum(1 for t in trades if t.pnl > 0)
    losses = total - wins
    cumulative = sum(t.pnl for t in trades)

    return SignalStats(
        signal=label,
        total=total,
        wins=wins,
        losses=losses,
        win_rate=round(wins / total * 100, 1),
        avg_pnl=round(cumulative / total, 2),
        cumulative_pnl=round(cumulative, 2),
    )


@router.get("", response_model=StatsResponse)
async def get_stats(db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Trade).order_by(Trade.exit_time.asc()))
    all_trades = list(result.scalars().all())

    overall = _compute_stats(all_trades, "Overall")

    by_signal: dict[str, list[Trade]] = {}
    for t in all_trades:
        key = t.signal or "Unknown"
        by_signal.setdefault(key, []).append(t)

    signal_stats = [_compute_stats(trades, sig) for sig, trades in sorted(by_signal.items())]

    return StatsResponse(overall=overall, by_signal=signal_stats)
