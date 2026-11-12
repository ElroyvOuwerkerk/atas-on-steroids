# Module notes: Building Plans & Context (Market Mastery)

Personal study notes distilled from the ChartChampions portal lessons (text summaries +
timestamped highlights that CC publishes alongside each video). Captured 2026-11-11. This is
the core module for Severin's method. Numbers and rules are kept verbatim where the lesson
states them, so they can be turned into indicator logic.

Source course: `portal.chartchampions.com/lesson/...` (15 lessons). Downloadable cheat sheet
referenced by lessons 3-5: Vault > "Trade Entry Checklist" (PDF, still to pull).

---

## 1. Trading Bias (Part 1) — `trading-bias-part-1`

Bias = a predisposition toward the higher-probability direction. The real question: where is
price trading *away from* (a key level, swing high/low, liquidity pool, gap, or broken
structure)?

Reading a single bar (open/high/low/close):
- Close above open = bullish bar; close below open = bearish bar.
- Large open-to-close distance = conviction/volatility; close near open = indecision.
- Open far from previous close = a gap = "unfinished business".

Core rules:
- **Never long above a previous day high; never short below a previous day low.** Those are
  profit-take zones (limit orders) and stop-loss zones.
- Hold bias with **fractals**: pick 2 swing lows (bullish) or 2 swing highs (bearish) back.
  Only flip bias when that key swing level breaks; until then trade with trend.
- Liquidity grab vs trend shift: a quick poke of a prior day low that closes back inside
  (ideally on 15m) = bullish liquidity collection, not a bearish shift.
- First sign of weakness in a long uptrend = first time a previous day low breaks. Do **not**
  short immediately; wait for a retrace to at least the 50% fib (re-anchor the fib to each new
  pivot low until confirmed).
- Confirmed trend shift = a single bar breaking multiple prior swing lows/highs.

Williams Fractals:
- Bullish fractal = a low with 2 higher-low bars each side (down-arrow).
- Bearish fractal = a high with 2 lower-high bars each side (up-arrow).
- Entry at the **open of the 3rd bar after the arrow** forms.
- High-probability combo = key level + fractal break + market-structure shift.
- Enter on retrace, not the breakout high: draw fib low→high, enter the **0.618–0.66 "CC zone"**,
  stop just beyond.

Confirmation tools to stack: CVD, open interest, volume. Liquidity principle: "if there, then
below; if below, then above." Stay with trend while ~60-70% of bars close in that direction.

## 2. Trading Bias (Part 2) — `trading-bias-part-2` (Q&A)

- Compounding an 8-day trend: take profit at each previous day high, compound on pullbacks,
  trail stop, watch for exhaustion.
- High/Low indicator also useful in ranges (gauge liquidity build above/below, breakout
  strength).
- Fractals work on all timeframes, preference 15m+.
- Take-profit split when compounding: **34% / 33% / 33%**; compound with slightly less than
  realized profit.
- Crypto: bar open always equals previous bar close (no gaps, unlike stocks/forex).

## 3-5. Traders Checklist (Part 1, Part 2, Live) — `traders-checklist-*`

Top-down process. Cheat sheet: Vault > "Trade Entry Checklist".

Higher-timeframe pass (monthly/weekly → down):
- Identify weekly trend, key S/R, trend vs range.
- Volume, delta, open interest to judge trend strength.
- Locate position within range using fixed-range tool + value areas.
- Spot trapped traders: large OI increase then reversal, ideally with CVD divergence.
- "Where is the market trying to go, and is it doing a good job getting there?"
- Harmonic patterns on HTF; note obvious retail patterns/sentiment; decide who is in control.

Lower-timeframe pass (4h → 15m) for entry triggers once the HTF location is set:
- Low TF = 1h and below, medium = 4h, high = above 4h.
- Find impulsive waves off a base; proximity to key levels.
- Mark where price can move fast (through triple tops, single prints / low-volume nodes).
- Pre-plan re-entry if stopped, or compounding on strength.
- Separate "right on the idea" from "stopped on the trade".
- Entry-trigger examples: bullish CVD divergence, trapped shorts at support.

## 6. Context / Volume tips — `context-volume-tips`

- Recognize short squeezes via open interest + volume.
- Trade deviations from established ranges; context + confluence before acting.
- Use clear invalidation; appropriate stops.

---

## 7-9. Severin Scalping Strategy (the core) — `severin-scalping-strategy*`

### 7. Foundation (the ExoCharts workspace)
Strategy = react to levels (do not pre-decide), confirmed with order flow. Works on stocks,
crypto, forex. Three entry types: **aggressive, backtest/retest, reclaim**.

ExoCharts workspace grid (S&P and Nasdaq), split context (top) vs execution (bottom):
- Right screen: daily volume profile, time + volume POCs, single prints (context); order flow
  (precise entries).
- Left screen: session POCs, TPO profile (context); CCTR template (precise entries).
- Templates: developing + previous day volume profile (VAH/VAL, POCs, single prints); 5m order
  flow (delta ladder profile, footprint bar stats, developing day volume profile); session
  volume template (session volume/time POC, session value area); CCTR for low-TF precise entries.
- Needs a DXFeed subscription for real-time CME data.

### 8. Formulating a daily plan
A daily plan = roadmap; keeps you objective. Five steps:
1. Assess market structure with the **Daily High/Low indicator** (TradingView) → trending vs ranging → risk approach.
2. Daily open vs previous day **range** → directional read, trend vs range day.
3. Daily open vs previous day **value area** → state of balance, potential setups.
4. Update **HTF levels** in TV: previous month/week/day levels, opens, SFPs, CC S/R, value areas.
5. Final plan in ExoCharts: focus on **untested values that coincide with liquidity grabs**;
   mark long and short scenarios.
Adapt intraday on new info; backtest to build intuition for well-respected levels; keep flexible.

### 9. Execution (the rule set)
Three entry types: **aggressive**, **reclaim (of orders and of levels)**, **backtest/retest**.
Always confirmed with order flow (delta ladder, footprint stats) against reference levels
(VAH/VAL, VWAP, POC).

Risk rules (mechanical):
- Risk < **1%** of account per trade.
- Minimum **2:1** reward-to-risk.
- After **2 consecutive losses**, cut position size by **50%**.
- After **3 consecutive losses**, pause trading.

Take-profit (context-dependent, adjustable):
- **ES**: 12-tick stop → TP1 20 / TP2 25 / TP3 30 ticks.
- **NQ**: 26-tick stop → TP1 40 / TP2 50 / TP3 60 ticks.
- TP allocation by structure: with-trend conservative **50/25/25**; counter-trend aggressive **75/15/10**.

Entry mechanics:
- Aggressive = swing-failure patterns and first touches of key S/R, confirmed by order flow.
- Reclaim of orders = use buy/sell blocks as reference; enter on confirmed breach/hold.
- Reclaim of levels = retake a level (e.g. VAL) with positive delta, then backtest it as support.
- Backtest/retest = for impulsive moves or where big volume transacted; compare delta into the
  move vs on the retest; weak retest + opposing orders = trigger.
- Treat levels as **zones, not exact prices**. Scale out, move stop to break-even, trail.
- Do not rely on order flow alone; follow the complete rule set. Exit promptly if signals
  conflict after TP1.

---

## 10-12. Severin's 3-Candle Entry Strategy (+ refinement) — `severin-scalping-strategy-addition-edit`, `contenders-session-3-candle-strategy-refinement-part1/2`

### 10. The base 3-candle entry ("1-Minute AutoFlow")
Addition to the scalping strategy, claimed +9% win rate; gives a near-automated yes/no entry.
- Core: 5m order flow for execution; sell blocks at support, buy blocks at resistance.
- Volatile-market fix: apply on the 1m, but raw 1m gets stopped too often, so combine 1m order
  flow with candle structure.
- Three candles: **(1) arrival/bearish-or-bullish candle, (2) reversal candle (confirmation),
  (3) entry candle**. The 2nd candle is the confirmation, NOT a fib retrace.

### 11. Refinement part 1 — COT and COC (+8% win rate)
Priority of entries: **COT → COC → Momentum**. Context first, always. Inside candles (fully
inside previous range) = low conviction = skip.

- **Cherry on Top (COT)** — highest probability:
  - Must take liquidity of previous candle (its low for bullish, high for bearish).
  - Must close in the **extreme quartile** (upper 25% bullish / lower 25% bearish).
  - Stat: **88%** probability of follow-through on an extreme-quartile close.
  - Look for large wicks on the liquidity take. COT can merge arrival+reversal into one candle
    (2-candle entry).
- **Change of Context (COC)** — second best:
  - Bullish: take out the **high** of the previous candle. Bearish: take out the **low**.
  - Standard 3-candle formation; can combine with COT for highest conviction.
- Mindset: "I am not convincing the market to take a trade. The market has to convince me."
- Works on any timeframe (universal stats); best with level confluence. Don't mix methods.

### 12. Refinement part 2 — Momentum entry (10:1 R:R)
Use only when no COT/COC is available (priority COT → COC → Momentum). Momentum is fueled by
**trapped traders**.
- Long: break of a **supply** zone + close in **upper** quartile. Short: break of a **demand**
  zone + close in **lower** quartile.
- Zone must have **3+ touches** (examples use 4-5). Entry on the **retest** candle. Stop local
  (just beyond the retest candle).
- Draw supply/demand boxes to include the most wicks while staying as small as possible.
- Red flag: do not long into a previous demand zone (or short into previous supply).
- Tight local stop → very high R:R (10:1+), allows larger size. Compound: take COT/COC first,
  add on the momentum break, trail or take partials.

---

## 13. How/when to modify the daily plan — `when-to-adjust-the-trading`

Modify on **new data, not emotion**. Three trade types: long, short, **no-trade (protect capital)**.
Step away when:
- Unexpected impulses / news moves; invalidations became too extreme; plan and correlations disagree.
- Major releases (CPI, FOMC, NFP) incoming — don't open new positions into them; wait for structure after.

Adjust checklist (any "yes" = valid to stand aside):
- Is the change data-driven or emotional?
- Has volatility increased significantly?
- Are key levels broken with conviction (high volume)?
- Is a major event (CPI/FOMC/NFP) incoming?

Psychological traps: ego ("I'm right"), confirmation bias / revenge trading disguised as
"following the plan". Missing a trade is fine; the next setup always comes.

## 14. DSF + Market Profile — `market-profile-and-dsf`

Combine **Market Profile (TPO)** = where value develops, with **DSF (depth-of-market surface /
liquidity map)** = live resting liquidity + intent. "Market profile = where; DSF = where and when."

- TPO terms: POC, VAH/VAL, unfinished business, unfinished auctions, poor high/low (auction
  without excess → likely revisited).
- Session letters A-M: A = cash open 9:30 NY, M = cash close.
- Overlapping value across sessions raises odds of rotation back inside range; check prior
  sessions before continuation trades.
- Overnight (Globex) inventory vs previous settlement anticipates counter-auctions at the RTH
  open. Only yesterday's overnight profile matters.
- DSF "dark zones" (no resting orders) = price travels through; concentrated liquidity validates
  areas. Watch for icebergs (hidden replenishing orders).
- Low participation at auction extremes (e.g. < 10 lots on NQ at a profile extreme) = exhaustion
  / rotation.
- Gap logic at the open: overnight above yesterday's RTH value and gap holds = bullish; full gap
  fill = rebalancing; gap holds above settlement = long continuation.
- Workflow: context (profile) → participation (30m volume) → confirmation (DSF) → execution with
  defined risk. Don't place stops directly on visible liquidity (FIFO queue). Prefer confirmation
  entries over picking tops/bottoms.
- Data note: Rithmic feed generally gives deeper DOM than CQG for order flow.

## 15. Context Mastery — Liquidity Auctions — `context-mastery-liquidity-auctions`

Framework: price moves from one meaningful **liquidity region** to the next; align trades with
the active **auction**. Context first, confluence second.

Liquidity regions = supply/demand zones + significant previous highs/lows grouped together.
**Finished-auction targets:**
- Supply→Demand flip → targets previous demand or previous high (whichever first).
- Demand→Supply flip → targets previous supply or previous low.
- SFP of a low → targets previous demand or previous high.
- SFP of a high → targets previous supply or previous low.

Trades *between* the start and target of an active auction are lower probability; fading an
active auction with retrace entries badly reduces win rate. Auctions chain: when one completes,
a new one often starts. Works fractally (30m context, 5m/1m for scalps and management).

Daily-plan checklist:
- Where did price come from?
- What is the active liquidity auction?
- Where is the next meaningful liquidity region?
- Does this level trade with or against the auction?
- What invalidates the contextual thesis?

Scalp filter: an entry setup alone is not enough; the lower-TF auction must support the trade
direction. Systematically avoid retests that fade a higher-TF auction.

Sizing: predefined high-conviction trades get larger size; in-between scalps get reduced size.
Management: de-risk at the first logical liquidity target, then trail the stop on each new
auction that forms. Read context, not P&L: "how long can I stay while the market still justifies
it?" Supply/demand zones do not expire while still clean; liquidity auctions override conflicting
static levels.
