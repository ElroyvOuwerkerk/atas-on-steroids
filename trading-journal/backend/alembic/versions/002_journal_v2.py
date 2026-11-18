"""Journal v2: stop/target/strategy fields on trades + a trade_exits ladder

Revision ID: 002
Revises: 001
Create Date: 2026-11-18

Additive only. Existing trades keep their single exit_price/pnl, which stay the
aggregate; the per-tranche truth lives in the new trade_exits table.
"""

from alembic import op
import sqlalchemy as sa

revision = "002"
down_revision = "001"
branch_labels = None
depends_on = None


_NEW_COLUMNS = [
    ("stop_loss", sa.Float),
    ("target", sa.Float),
    ("point_value", sa.Float),
    ("account_balance_at_entry", sa.Float),
    ("strategy", sa.Text),
    ("tags", sa.Text),
    ("review", sa.Text),
    ("grade", sa.Text),
]


def upgrade():
    for name, coltype in _NEW_COLUMNS:
        op.add_column("trades", sa.Column(name, coltype, nullable=True))

    op.create_table(
        "trade_exits",
        sa.Column("id", sa.Integer, primary_key=True, autoincrement=True),
        sa.Column("trade_id", sa.Integer, sa.ForeignKey("trades.id"), nullable=False),
        sa.Column("exit_price", sa.Float, nullable=False),
        sa.Column("quantity", sa.Float, nullable=False),
        sa.Column("exit_time", sa.Text),
        sa.Column("fees", sa.Float, server_default="0"),
        sa.Column("pnl", sa.Float),
        sa.Column("note", sa.Text),
    )
    op.create_index("ix_trade_exits_trade_id", "trade_exits", ["trade_id"])


def downgrade():
    op.drop_index("ix_trade_exits_trade_id", table_name="trade_exits")
    op.drop_table("trade_exits")
    for name, _ in reversed(_NEW_COLUMNS):
        op.drop_column("trades", name)
