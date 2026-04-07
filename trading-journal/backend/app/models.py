from datetime import datetime, timezone

from sqlalchemy import Column, Float, Integer, Text
from sqlalchemy.orm import DeclarativeBase


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
