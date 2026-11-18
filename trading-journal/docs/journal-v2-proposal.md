# Journal v2: what to borrow from CC's new Trading Journal

**Status: proposal.** No existing journal code and not the `TradeLogger` plugin has been touched. Per the
repo's hard rule, this documents the root cause and the suggested change, then stops, until Elroy says
"go fix it" and names the file.

## Context

ChartChampions shipped a Trading Journal in the member portal (`portal.chartchampions.com/journal`) around
2026-11-17. Reviewed read-only. This lists which elements are worth adopting for our own journal
(`trading-journal/`), ranked, with the reasoning tied to the finding that matters: in Elroy's real trades
the measurable edge is **management** (de-risk at the first target, trail the rest, hold through an
unrealized drawdown), not the entry. See `chartchampions/live-reads-trades.md`. Our current model cannot
represent that.

## What CC's journal captures (reference)

- **New Trade form:** Long/Short; Journal (multi-journal); Strategy (named setup); Asset/Pair; Trade Date;
  Entry Price; Leverage; Size Input (Base/Quote); Quantity; Value; Tags/Events.
- **Calculator Inputs (optional):** Stop Loss, Target, Account Risk Target %, and computed Actual Risk %,
  Position Size, Risk:Reward, Stop Distance, Risk per trade, Reward per trade, Realized Profit.
- **Exit Price:** multiple TPs, each with Exit Price, Quantity, Fees, Position Allocation % (slider +
  10/20/25/50/100 buttons), Exit Date, per-TP P&L; an "allocated % / remaining" tracker.
- **Per-trade toggles:** Add Metrics, Add Notes, Chart Images.
- **Statistics tab:** filters (journal, date range, type, symbol) + Export Report; Win Rate, Profit Factor,
  Avg Win, Avg Loss; Daily P&L; Asset Distribution; Strategy Performance; Time Analysis by Day of Week, by
  Hour (UTC), by Session, by Tag/Event.
- **Market Data tab:** "Community Market Intelligence", crowdsourced patterns.

## Our journal today

- `Trade` (`trading-journal/backend/app/models.py`): instrument, direction, entry_price, exit_price
  (single), size, pnl, entry_time, exit_time, signal, signal_source, clip_filename, notes, created_at.
- Stats (`trading-journal/backend/app/routes/stats.py`): overall + per-signal win rate, wins/losses,
  avg_pnl, cumulative_pnl.
- Fed by the ATAS `TradeLogger` plugin (auto-logged) and the React dashboard.

## Borrow list, ranked

### 1. Partial exits + stop/target -> R  (the one that matters)

Problem: one `exit_price`/`pnl` and no stop. We cannot measure de-risk-and-trail, hold time per tranche, or
R-multiples. The live-reads already reason in R; the journal should carry R as the unit.

Suggested model change (new work, not an edit mandate):
- Add to `Trade`: `stop_loss`, `target` (optional), `initial_risk` (money) or `risk_pct`, and
  `account_balance_at_entry` (to express risk as %).
- New `TradeExit` table, one-to-many: `trade_id`, `exit_price`, `quantity`, `fees`, `allocation_pct`,
  `exit_time`, derived `pnl` and `r_multiple`. Either migrate the single `exit_price` into one exit row or
  keep it for back-compat and treat exits as the source of truth.
- Derive per trade: realized R (sum of tranche R), hold time per tranche, and MFE/MAE if the candle cache
  can supply it.

What it unlocks: the "stopped-out-then-it-runs" curve already measured, an R-distribution per setup and per
management-rule (the backtest-lab in the vision), and an honest answer to "did holding pay".

Integration note: partial exits are only captured automatically if the logger emits scale-out events. The
`TradeLogger` plugin is an existing `.cs` script and is off limits to edit. Paths: (a) manual exit entry in
the dashboard, (b) a NEW logger that records scale-outs. Decide separately. Do not touch `TradeLogger`.

### 2. Time / session analytics

Add to `stats.py`: net PnL and win rate by session, by hour, by weekday. Convert `entry_time` (UTC) to New
York and to Amsterdam. Answers directly whether the edge concentrates in his hours (London + the first two
New York hours). Low effort, the data is already there.

### 3. Strategy / setup field, distinct from the mechanical signal

We have `signal`/`signal_source`. Add an explicit `strategy`/`setup` tag (pmVAL retest, 3-candle, reclaim,
and so on) and a per-strategy breakdown in stats. Links trades to the per-Champ setups in
`chartchampions/learnings/`.

### 4. Tags / events + per-tag performance

A `tags` field (CPI, FOMC, news, session) and a by-tag stats breakdown. Context filter.

### 5. Multiple journals (optional)

Separate books with their own balance/currency: real trades vs replay/backtests, or ES vs NQ. Nice-to-have.

### 6. Small ones

CSV export of the filtered trades (portability); chart images per trade (we already have `clip_filename`).

## Do not borrow: the Market Data tab

"Community Market Intelligence" aggregates self-reported patterns ("Wednesday Price Reversal, 43%") from a
handful of contributors (7 data points total). Statistically noise. Our candle cache plus the replay
apparatus tests patterns on real data and is stronger. Skip it.

## Suggested order (if and when he says go)

1. Model: stop/target on `Trade` + a `TradeExit` table + an Alembic migration.
2. Stats: R-multiples, session/hour/weekday, per-strategy, per-tag.
3. Dashboard: R-distribution and the hold-vs-cut curve.
4. Decide the exit-capture path (manual vs a new logger). Do not touch `TradeLogger`.
