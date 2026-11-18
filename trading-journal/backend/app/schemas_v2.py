from __future__ import annotations

from typing import List, Optional

from pydantic import BaseModel


class ExitIn(BaseModel):
    exit_price: float
    quantity: float
    exit_time: Optional[str] = None
    fees: Optional[float] = 0.0
    pnl: Optional[float] = None  # money for this tranche; computed if left empty
    note: Optional[str] = None


class ExitOut(BaseModel):
    id: int
    exit_price: float
    quantity: float
    exit_time: Optional[str]
    fees: Optional[float]
    pnl: Optional[float]
    note: Optional[str]
    r_multiple: Optional[float] = None
    allocation_pct: Optional[float] = None

    model_config = {"from_attributes": True}


class TradeV2Create(BaseModel):
    instrument: str
    direction: str  # "Long" / "Short"
    entry_price: float
    size: float  # total planned/opened quantity
    entry_time: str
    stop_loss: Optional[float] = None
    target: Optional[float] = None
    point_value: Optional[float] = None
    account_balance_at_entry: Optional[float] = None
    strategy: Optional[str] = None
    tags: Optional[str] = None
    signal: Optional[str] = None
    signal_source: Optional[str] = None
    notes: Optional[str] = None
    exits: List[ExitIn] = []


class TradeV2Update(BaseModel):
    instrument: Optional[str] = None
    direction: Optional[str] = None
    entry_price: Optional[float] = None
    size: Optional[float] = None
    entry_time: Optional[str] = None
    stop_loss: Optional[float] = None
    target: Optional[float] = None
    point_value: Optional[float] = None
    account_balance_at_entry: Optional[float] = None
    strategy: Optional[str] = None
    tags: Optional[str] = None
    signal: Optional[str] = None
    signal_source: Optional[str] = None
    notes: Optional[str] = None
    review: Optional[str] = None
    grade: Optional[str] = None
    # When provided, replaces the whole exit ladder. Omit to leave exits as they are.
    exits: Optional[List[ExitIn]] = None


class TradeV2Response(BaseModel):
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
    stop_loss: Optional[float]
    target: Optional[float]
    point_value: Optional[float]
    account_balance_at_entry: Optional[float]
    strategy: Optional[str]
    tags: Optional[str]
    review: Optional[str]
    grade: Optional[str]
    exits: List[ExitOut]
    # Computed, never stored stale
    realized_r: Optional[float]
    total_exited_qty: float
    open_qty: float
    avg_exit_price: Optional[float]
    computed_pnl: Optional[float]
    initial_risk: Optional[float]  # money, if point_value + stop known
    risk_pct: Optional[float]  # initial_risk / account_balance_at_entry
    scaled_out: bool
    session: Optional[str]
    weekday: Optional[str]


# --- Review analytics ---


class RBucket(BaseModel):
    key: str
    trades: int
    total_r: float
    avg_r: float
    win_rate: float
    net_pnl: float


class ReviewResponse(BaseModel):
    trades_with_r: int
    total_realized_r: float
    avg_r: float
    expectancy_r: float  # avg realized R per trade, the honest single number
    scaled_out_share: float  # share of trades that took partials
    by_strategy: List[RBucket]
    by_session: List[RBucket]
    by_weekday: List[RBucket]
    by_hour: List[RBucket]
    by_tag: List[RBucket]
    r_distribution: List[RBucket]  # bucketed realized-R histogram
