from __future__ import annotations

from typing import List, Optional

from pydantic import BaseModel


class TradeCreate(BaseModel):
    instrument: str
    direction: str
    entry_price: float
    exit_price: float
    size: float
    pnl: float
    entry_time: str
    exit_time: str
    signal: Optional[str] = None
    signal_source: Optional[str] = None


class TradeNotes(BaseModel):
    notes: str


class TradeResponse(BaseModel):
    id: int
    instrument: str
    direction: str
    entry_price: float
    exit_price: float
    size: float
    pnl: float
    entry_time: str
    exit_time: str
    signal: Optional[str]
    signal_source: Optional[str]
    clip_filename: Optional[str]
    notes: Optional[str]
    created_at: Optional[str]

    model_config = {"from_attributes": True}


class SignalStats(BaseModel):
    signal: str
    total: int
    wins: int
    losses: int
    win_rate: float
    avg_pnl: float
    cumulative_pnl: float


class StatsResponse(BaseModel):
    overall: SignalStats
    by_signal: List[SignalStats]
