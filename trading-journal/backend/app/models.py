from datetime import datetime, timezone

from sqlalchemy import Column, Float, ForeignKey, Integer, Text
from sqlalchemy.orm import DeclarativeBase, relationship


class Base(DeclarativeBase):
    pass


class Trade(Base):
    __tablename__ = "trades"

    id = Column(Integer, primary_key=True, autoincrement=True)
    instrument = Column(Text, nullable=False)
    direction = Column(Text, nullable=False)
    entry_price = Column(Float, nullable=False)
    exit_price = Column(Float, nullable=False)
    size = Column(Float, nullable=False)
    pnl = Column(Float, nullable=False)
    entry_time = Column(Text, nullable=False)
    exit_time = Column(Text, nullable=False)
    signal = Column(Text)
    signal_source = Column(Text)
    clip_filename = Column(Text)
    notes = Column(Text)
    created_at = Column(Text, default=lambda: datetime.now(timezone.utc).isoformat())

    # Journal v2. All nullable and additive: legacy rows and the TradeLogger
    # auto-logging path keep working untouched. exit_price / pnl / exit_time stay
    # the aggregate (avg exit, summed pnl, last exit) so the existing table and
    # stats still read correctly; the per-tranche truth lives in trade_exits.
    stop_loss = Column(Float)
    target = Column(Float)
    point_value = Column(Float)  # dollars per point, only for money P&L (MES=5, MNQ=2)
    account_balance_at_entry = Column(Float)
    strategy = Column(Text)  # named setup, distinct from the mechanical signal
    tags = Column(Text)  # comma-separated events/context (CPI, FOMC, news, session)
    review = Column(Text)  # the journaling verdict, written by Claude or Elroy
    grade = Column(Text)  # A / B / C

    exits = relationship(
        "TradeExit",
        back_populates="trade",
        cascade="all, delete-orphan",
        order_by="TradeExit.id",
    )


class TradeExit(Base):
    __tablename__ = "trade_exits"

    id = Column(Integer, primary_key=True, autoincrement=True)
    trade_id = Column(
        Integer, ForeignKey("trades.id"), nullable=False, index=True
    )
    exit_price = Column(Float, nullable=False)
    quantity = Column(Float, nullable=False)
    exit_time = Column(Text)
    fees = Column(Float, default=0.0)
    pnl = Column(Float)  # money for this tranche, entered or computed from point_value
    note = Column(Text)

    trade = relationship("Trade", back_populates="exits")
