#!/usr/bin/env python3
"""Idempotent, non-destructive journal v2 migration for the live SQLite DB.

Backs the database up first, then adds the new trades columns and the
trade_exits table only if they are missing. Safe to run more than once. Run it
BEFORE restarting the backend on the new code, so the model never queries a
column the table does not have yet.

Usage:
    python3 -I scripts/migrate_v2.py [path/to/journal.db]

Default path: the journal.db next to the backend (../journal.db from here),
or $DATABASE_URL if it is a sqlite URL.
"""

from __future__ import annotations

import os
import shutil
import sqlite3
import sys
from datetime import datetime
from pathlib import Path

NEW_COLUMNS = [
    ("stop_loss", "REAL"),
    ("target", "REAL"),
    ("point_value", "REAL"),
    ("account_balance_at_entry", "REAL"),
    ("strategy", "TEXT"),
    ("tags", "TEXT"),
    ("review", "TEXT"),
    ("grade", "TEXT"),
]

TRADE_EXITS_DDL = """
CREATE TABLE IF NOT EXISTS trade_exits (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    trade_id INTEGER NOT NULL REFERENCES trades(id),
    exit_price REAL NOT NULL,
    quantity REAL NOT NULL,
    exit_time TEXT,
    fees REAL DEFAULT 0,
    pnl REAL,
    note TEXT
)
"""


def _resolve_db_path() -> Path:
    if len(sys.argv) > 1:
        return Path(sys.argv[1]).expanduser().resolve()
    url = os.getenv("DATABASE_URL", "")
    if url.startswith("sqlite"):
        # sqlite+aiosqlite:///abs/path or sqlite:///rel/path
        tail = url.split(":///", 1)[-1]
        if tail and tail != url:
            return Path(tail).expanduser().resolve()
    return (Path(__file__).resolve().parent.parent / "journal.db").resolve()


def main() -> int:
    db = _resolve_db_path()
    if not db.exists():
        print(f"No database at {db}. Nothing to migrate (a fresh DB is built by the app).")
        return 0

    backup = db.with_name(f"{db.name}.bak-{datetime.now():%Y%m%d-%H%M%S}")
    shutil.copy2(db, backup)
    print(f"Backup: {backup}")

    con = sqlite3.connect(str(db))
    try:
        cur = con.cursor()
        existing = {row[1] for row in cur.execute("PRAGMA table_info(trades)")}
        added = []
        for name, coltype in NEW_COLUMNS:
            if name not in existing:
                cur.execute(f"ALTER TABLE trades ADD COLUMN {name} {coltype}")
                added.append(name)

        had_exits = bool(
            cur.execute(
                "SELECT name FROM sqlite_master WHERE type='table' AND name='trade_exits'"
            ).fetchone()
        )
        cur.execute(TRADE_EXITS_DDL)
        cur.execute(
            "CREATE INDEX IF NOT EXISTS ix_trade_exits_trade_id ON trade_exits(trade_id)"
        )
        con.commit()
    finally:
        con.close()

    print(f"Columns added to trades: {', '.join(added) if added else 'none (already present)'}")
    print(f"trade_exits table: {'created' if not had_exits else 'already present'}")
    print("Done. Now restart the backend:")
    print("  launchctl kickstart -k gui/$(id -u)/com.tradingjournal.backend")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
