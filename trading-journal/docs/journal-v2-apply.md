# Journal v2: applying it to the live setup

The code is built and tested against a copy of the database. Nothing live has been
touched yet: the live backend has not been restarted and the live database has not
been migrated. Apply it yourself when you are flat and out of a live session.

The migration is additive and non-destructive (it only adds columns and one table),
idempotent (safe to run twice), and it backs the database up first.

## Steps

1. Migrate the database (this backs it up to `journal.db.bak-<timestamp>` first):

```bash
cd /Users/lroy/Documents/GitHub/atas-on-steroids/trading-journal/backend && .venv/bin/python3 -I scripts/migrate_v2.py
```

2. Restart the backend so it loads the new code (do this AFTER the migration, never
   before, or it would query columns the table does not have yet):

```bash
launchctl kickstart -k gui/$(id -u)/com.tradingjournal.backend
```

3. Reload the dashboard in the browser. It runs as a vite dev server, so it picks up
   the new pages on reload; no rebuild needed.

That is it. The new tabs are **Journal** (manual entry with partial exits, R per
trade), **Review** (R by session, weekday, strategy, hour, tag, plus the R
histogram), and **Auto-log** (the old TradeLogger table, unchanged).

## If something looks wrong

Restore the backup and restart:

```bash
cd /Users/lroy/Documents/GitHub/atas-on-steroids/trading-journal/backend && cp journal.db.bak-<timestamp> journal.db && launchctl kickstart -k gui/$(id -u)/com.tradingjournal.backend
```

## What changed, and what did not

- New: `trade_exits` table; `stop_loss`, `target`, `point_value`,
  `account_balance_at_entry`, `strategy`, `tags`, `review`, `grade` on `trades`.
- The existing `exit_price`, `pnl` and `exit_time` on `trades` stay as the aggregate
  (avg exit, summed P&L, last exit), so the old table and the old stats keep working.
- The `TradeLogger` ATAS plugin was NOT touched. It still auto-logs single-exit
  trades. Partial exits are entered by hand in the dashboard for now. A separate
  scale-out logger is a later decision.

## How R is computed

R is a price ratio, so it needs no contract size and no dollar value:

- long: `R = (exit - entry) / (entry - stop)`
- short: `R = (entry - exit) / (stop - entry)`

A trade's realized R is the quantity-weighted average across its exits. A trade with
no stop has no R and sits out of the R buckets, but its money P&L still counts. Money
P&L per tranche is computed from `point_value` ($/point, suggested per instrument:
MES 5, MNQ 2, ES 50, NQ 20) when you do not type it in.
