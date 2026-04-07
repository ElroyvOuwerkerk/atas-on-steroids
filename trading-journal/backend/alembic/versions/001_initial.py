"""Initial schema

Revision ID: 001
Revises:
Create Date: 2026-04-02
"""

from alembic import op
import sqlalchemy as sa

revision = "001"
down_revision = None
branch_labels = None
depends_on = None


def upgrade():
    op.create_table(
        "trades",
        sa.Column("id", sa.Integer, primary_key=True, autoincrement=True),
        sa.Column("instrument", sa.Text, nullable=False),
        sa.Column("direction", sa.Text, nullable=False),
        sa.Column("entry_price", sa.Float, nullable=False),
        sa.Column("exit_price", sa.Float, nullable=False),
        sa.Column("size", sa.Float, nullable=False),
        sa.Column("pnl", sa.Float, nullable=False),
        sa.Column("entry_time", sa.Text, nullable=False),
        sa.Column("exit_time", sa.Text, nullable=False),
        sa.Column("signal", sa.Text),
        sa.Column("signal_source", sa.Text),
        sa.Column("clip_filename", sa.Text),
        sa.Column("notes", sa.Text),
        sa.Column("created_at", sa.Text, server_default=sa.text("(datetime('now'))")),
    )


def downgrade():
    op.drop_table("trades")
