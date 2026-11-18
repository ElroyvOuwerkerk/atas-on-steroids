"""Pure helpers for journal v2: R-multiples from price, and session/time buckets.

R is computed from prices only (entry, stop, exit), so it never depends on
contract size or dollar-per-point. Money P&L is a separate concern that needs a
point value. Sessions are bucketed in New York local time, since that is the
frame Elroy's hours sit in (London plus the first two New York hours).
"""

from __future__ import annotations

from datetime import datetime, timezone
from typing import Optional

try:
    from zoneinfo import ZoneInfo

    _NY = ZoneInfo("America/New_York")
    _AMS = ZoneInfo("Europe/Amsterdam")
except Exception:  # pragma: no cover - zoneinfo is stdlib on 3.11+
    _NY = None
    _AMS = None

_WEEKDAYS = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]


def _is_long(direction: Optional[str]) -> bool:
    return bool(direction) and direction.strip().lower().startswith("l")


def r_multiple(
    direction: Optional[str],
    entry: Optional[float],
    stop: Optional[float],
    exit_price: Optional[float],
) -> Optional[float]:
    """R for one exit. None when stop is missing or the stop sits the wrong side."""
    if entry is None or stop is None or exit_price is None:
        return None
    if _is_long(direction):
        risk = entry - stop
        reward = exit_price - entry
    else:
        risk = stop - entry
        reward = entry - exit_price
    if risk <= 0:
        return None
    return reward / risk


def tranche_pnl(
    direction: Optional[str],
    entry: Optional[float],
    exit_price: float,
    quantity: float,
    point_value: Optional[float],
    fees: Optional[float],
    entered_pnl: Optional[float],
) -> Optional[float]:
    """Money for one tranche. Prefer an entered value, else compute from point value."""
    if entered_pnl is not None:
        return entered_pnl
    if point_value is None or entry is None:
        return None
    sign = 1.0 if _is_long(direction) else -1.0
    gross = sign * (exit_price - entry) * quantity * point_value
    return gross - (fees or 0.0)


def parse_dt(ts: Optional[str]) -> Optional[datetime]:
    if not ts:
        return None
    try:
        dt = datetime.fromisoformat(ts)
    except ValueError:
        return None
    if dt.tzinfo is None:
        dt = dt.replace(tzinfo=timezone.utc)
    return dt


def _ny(ts: Optional[str]) -> Optional[datetime]:
    dt = parse_dt(ts)
    if dt is None or _NY is None:
        return dt
    return dt.astimezone(_NY)


def session_of(ts: Optional[str]) -> Optional[str]:
    """Bucket a timestamp into a trading session by New York local hour."""
    dt = _ny(ts)
    if dt is None:
        return None
    h = dt.hour
    if 3 <= h < 8:
        return "London"
    if 8 <= h < 12:
        return "NY AM"
    if 12 <= h < 16:
        return "NY PM"
    if 16 <= h < 18:
        return "Close"
    return "Asia"


def ny_hour(ts: Optional[str]) -> Optional[int]:
    dt = _ny(ts)
    return None if dt is None else dt.hour


def weekday_of(ts: Optional[str]) -> Optional[str]:
    dt = _ny(ts)
    return None if dt is None else _WEEKDAYS[dt.weekday()]
