# Severin trade log (from #severin-trading)

Machine-readable log of **Severin's** posted trades, to line up against the cached 1-minute candles.
Times are **UTC** (the Discord `datetime`); CET = UTC+1. Prices marked `chart` are in the posted
screenshot, not in the text. NFA — these are the coach's trades, not Elroy's. Source: #severin-trading.

Known limitation: exact entry/stop/exit prices live in the chart screenshots, so the table carries the
**level, direction, R:R and result** from the text and marks prices `chart`. A tick/footprint export
around these timestamps turns the levels into exact fills.

| date | time_utc | instrument | dir | setup / level | entry | stop | exit / result | r_r | hold | with_trend | source_note |
|------|----------|------------|-----|---------------|-------|------|---------------|-----|------|------------|-------------|
| 2026-11-03 | morning→13:32 | **NQ (NQZ6, confirmed)** | long | morning level; retest → trail | **28100.22** (60 contracts) | no stop line shown; "almost stopped to the tick" near ~ONL 28038.25 | **+161,740** open @12:01 → locked 150k → 250k → **+325k** | large | all day | yes (strong bounce) | "another generational long … keep it and risk the unrealized" |
| 2026-11-06 | 08:12 (plan) | NQ1! | long (planned) | major support ~26k, wait for reaction | chart | — | thesis; by 2026-11-09 "**+5%** from major buy zone" | — | multi-day | yes | planned at major support; played out into Nov 9 |
| 2026-11-11 | **morning entry (Elroy confirmed 2026-11-14)** → 16:46 | **NQ (NQZ6)** | long | **pmVAL retest** | **28253.62** (30 contracts) | trailed to **28372.25** by 16:45 (risk 21k → implied initial ~28218.62) | **+113,030** open @16:45 (≈5.4R), locked 100k | **5:1** | held all the way (new-high context) | yes | "1 trade for a 5:1 R:R … held it all the way"; 6-figure day |

Related (not a Severin post): the **2026-11-12 NQ pmVAL rejection → flush** case (from Elroy's own
footprint screenshot) is written up in `cases/2026-11-12-nq-pmval-rejection.md` with a per-candle
delta/volume table — the counter-example where no entry was armed.

## Chart-image reads (2026-11-13) — from the posted ATAS/dxFeed screenshots
These position screenshots show the **average entry price, size and open PnL** on a right-axis order
line, but **no entry time and no execution-fill markers** — so entry timing is not directly legible from
them (same on both days).

**2026-11-11 NQ long** (post 16:46 CET):
- Instrument **NQ (NQZ6)** — high confidence (ticker + levels).
- Open position **30 contracts, avg entry 28253.62, open PnL +$113,030** — high confidence (green
  position line). Cross-check: (28441 − 28253.62) × 30 × $20 ≈ $112k ✓; 113,030 / 21,000 risk ≈ **5.4R**,
  matching his stated "5:1".
- **Trailed stop (red SELL STP) 28372.25** by 16:45 — above entry, locking profit — high confidence.
- **Entry TIME: not legible.** No time axis, no fill markers. The avg entry 28253.62 sits ~29 pts above
  the morning return low (28224.50) and ~55 pts above the cash-open low (28199.00); he entered on the
  bounce, above both lows, not at either low.
- Derived (NOT read — flag): risk 21k on 30 × $20 = 35 pts → **implied initial stop ≈ 28218.62**, which
  is just below the morning low (28224.50) and **above** the cash-open low (28199.00). If that 21k was on
  the full 30 lots this leans **morning** (stop under the morning low); if he scaled in, it was on a
  smaller start and this does not hold. **I cannot confirm morning vs cash-open from the picture** — the
  candle data plus this implied-stop number have to settle it.
- **2026-11-14 — Elroy confirmed: MORNING entry.** So he held through the cash-open dip (28199.00, below
  both his 28253.62 entry and the ~28218.62 implied stop): the +5.4R came from **sitting through an
  unrealized drawdown** (discretionary management), not from the trail rule — which would have stopped
  the morning entry early (~09:33 per the build reconstruction). For this trade the edge was the
  **hold**, not the entry or a mechanical exit.

**2026-11-03 NQ long** (post 12:01 CET, "almost stopped to the tick"):
- Instrument **NQ (NQZ6)** — **confirms** the earlier "NQ/ES index" guess. High confidence.
- Open position **60 contracts, avg entry 28100.22, open PnL +$161,740** at 12:01 — high confidence.
- **Stop: no stop line in frame.** "Almost got stopped to the tick … from the morning" implies the
  initial stop sat just under the morning low, near the **ONL 28038.25**. Not a readable figure.
- Chart levels: ONL 28038.25, nwPOC 28170.00, 28201.00, pdLow 28223.75, pdClose 28235.70, ONH 28284.50.

## Field notes for the build session
- Every winning trade in this window is a **long held to a context target** in a strong up-regime, with
  the **stop trailed by structure** after a confirming retest — not a 2R take-profit. Lining Nov 3 and
  Nov 11 up against the 1-minute candles (entry level + the retest + the trail points) is the way to
  measure the real R, which is far above 2R.
- Instrument on Nov 3 is **confirmed NQ (NQZ6)** from the chart. Entry times are not in the position
  screenshots, so a tick export is still the only way to the exact entry fills.
