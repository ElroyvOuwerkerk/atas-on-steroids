# Module notes: Statistics and Journaling (Market Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 8 lessons
(Master level). The "prove your edge" module: how to backtest static levels and playbooks, journal
trades, and — most relevant — **Severin's own statistics-building framework**, which explicitly
tracks the 3-candle entry (COT/COC/momentum) and the 0.25 quartile tool. Feeds the risk/journal side
of `playbook.md` and the trading-journal in `trading-journal/`.

Vault cheat sheets referenced: **"How to Record Statistics"** (1), **"Correlations in Trading"** (6).

---

## Core targets (Lessons 1, 7)
- **Edge = a repeatable market pattern with a measurable statistical advantage** — not a win streak.
  A loss doesn't invalidate an edge with positive expectancy over a large sample.
- **Win-rate vs R:R:** a 20% win rate is profitable at 5:1; CC prefers playbook setups with a
  **60-65%+ win rate**, because high-probability setups allow flexible management (even 1:1 stays
  profitable). Target a **minimum 1:1, 2:1 preferred**; risk the same equity per trade.
- **Static level = where; playbook = when/why/how.** Static level = a fixed reference (daily open,
  pdHigh, pdLow). Playbook = a structured setup tied to specific market conditions.
- **Minimum 200 observations** before a statistic is meaningful. Do the backtesting **yourself** (it
  builds pattern recognition); keep it simple and honest.

## How to record statistics (Lesson 1, Igor) ·  Vault: "How to Record Statistics"
Track **win rate, R:R, drawdown, number of trades**. Tick-based method: pick a level (e.g. daily
open), record ticks of the move **away** from the naked level (green) and ticks **through** the stop
(red); build average ticks / win% over time. Second method: log **weekly highs/lows** per day of week
(Excel highlights the week's highest high / lowest low) to find which weekday tends to print the
high/low. Separate stats per tick size; record session highs/lows with the High/Low indicator on 15m;
allow time between naked levels for higher-probability trades.

## Static level vs playbook (Lesson 2, Severin) ★
Expands Igor's lesson. Backtest both:
- **Static level** (unchanged intraday: dOpen, pdHigh, pdLow) — keep separate stats for the **first
  touch** in the Overnight vs the New York session, plus a combined stat.
- **Playbook** (dynamic relationships + conditions, needs defined rules). Examples:
  - **dOpen quartile play:** split the previous-day range into 4 quartiles; test the relationship
    between a dOpen in the **upper/lower quartile** and a move above pdHigh / below pdLow.
  - **Overnight session Supply/Demand flip:** S&D zone qualifies only if **≥3x** supply or demand;
    entry = **15-min close above/below the overnight S&D zone**, based on **5-min order flow**;
    invalidation = a 15-min close back through it (that is the invalidation, not the stop).
- Playbook ideas emerge *during* backtesting; there's no limit to what you can test.

## Severin's backtest preparation (Lesson 8) ★★ most method-specific
Clean data → reliable probabilities. Prepare: a documented backtesting plan, an Excel/notebook, a
dedicated charting workspace per statistic, and **marked holidays + session-time changes**.
- **Exclude holidays** (thin liquidity distorts price) and **adjust for daylight-saving:** the **NY
  open shifts between 3:30 and 2:30 CET** — not adjusting wrecks session-based stats. (Directly
  relevant: Elroy trades ES from CET.)
- **PDVAL study rules (example):** include only days where the **daily open is inside the previous
  day's value area**; track only the **first touch** of the PDVAL next session; **exclude days where
  the 30-min opening candle already touches the level** (no valid entry); later touches = separate
  studies.
- **Excel fields:** observation #, asset, level/playbook, timeframe, market context (up/down/range),
  session, win/loss, deviation above/below (ticks), move away (ticks). **Win = a move ≈ 2:1 away from
  the level.**
- **Optional entry tracking:** log the **3-candle entry on 1m/5m (COT / COC / momentum / no entry)**
  with stop + TP in ticks → auto win/loss per session and entry type. (This is the statistical hook
  for indicator A.)
- **dOpen quartile study:** use a **Fibonacci 0.25 tool** to mark the previous-day quartiles; include
  only days with the dOpen in the upper/lower quartile; record whether price trades above pdHigh /
  below pdLow during ETH — a pure contextual probability (no session/entry/R:R tracked).
- Principles: define structure before collecting; apply the same framework to every observation;
  start each study neutral; keep variables minimal, layer complexity later.

## Trading journal for CCTR (Lesson 3) ★
Journal for CCTR / day / swing trades; **fill it out BEFORE entering — it IS your trading plan.**
Keys: have a strategy, journal, accept mistakes, **risk 1 to make ≥2**, be consistent. Auto-calc
position size (balance × leverage), stop (e.g. 40 below entry), BE, TP1/TP2, projections. Log entry
time + session (4 color-coded sessions), SBOC presence, **5m/10m ACR (150/300)**, local order flow,
CVD, fib level, CCW confirmation. **Don't take every CCTR candle** — wait for high-prob confluence
(time + level + indicators → 80%+). Add fees on **both** entry and exit. Separate journals/sub-
accounts per style. Journaling puts you in the top 5% who know their own stats.

## Playbook / tier framework (Lessons 4-5, Rivalry) ★
**Roles + Plays + Game Plan = Playbook.** Work the tiers **in order; Tier One always takes
precedence**; tiers are "areas of interest for possible reversal" — combine with "How to Spot a Trend"
+ management.
- **Tier 1:** PDPA / PDVA (futures indexes), PWVA (crypto/BTC). Always ask: *is price rotating within
  value or in an imbalance searching?*
- **Tier 2:** ONH/ONL (overnight high/low, indexes), monthly/weekly/daily opens, S/R (monthly —Tier 1
  in crypto— weekly, daily, nPOCs), VWAP, developing value (post-IB, midday).
- **Tier 3:** Fibonacci (all forms), IB (indexes), supply/demand flips (subjective).
- **Tier 4:** footprint, delta trend (indexes), bullish/bearish divergences (crypto).
- **Honorable mention:** previous-week profile (indexes) / previous-month profile (crypto) for overall
  trend.
- **Value Retrace Play:** a 30-min candle closing **within balance** of a PDPA/PDVA level → rotation
  to the inverse level is more likely; closing **outside balance** → imbalance searching for next
  value. (Crypto: use the Weekly profile vs the overall Monthly.)
- **Part 2 plays:** **Divergence plays** (reversal scalps with a runner; PDPA/PDVA + one-time framing;
  initiative vs responsive entries by delta trend; mantra **"no buys no highs, no sells no lows"**;
  spot the reversal, don't time it). **VWAP trap** (bearish play on ES/NASDAQ/crypto, read on the 5m
  with market structure + delta trend). 30-min highs/lows are crucial for runners. (Rivalry uses the
  1m + delta trend on the lowest TF rather than CCTR.)

## Correlations (Lesson 6) ·  Vault: "Correlations in Trading"
Use asset correlations as **added confluence/confirmation** (not a leading indicator alone), esp. in
the NY session, and for risk management (avoid over-leveraging correlated positions).
- **DXY is inversely correlated to the S&P 500 / Nasdaq** (fluctuates in phases, not a reliable
  predictor). Top-weighted S&P/Nasdaq stocks (AAPL, MSFT…) are **positively correlated** to their
  index. Use TradingView's **Correlation Coefficient (CC)** indicator (0→1 positive, 0→-1 inverse,
  0 none). Correlations change over time — monitor and adapt.
