# Severin Scalping Playbook (working synthesis)

A condensed, actionable version of Severin's method, built from the ChartChampions "Building
Plans & Context" module. This is the decision flow to run daily. It will be enriched as the
other modules (Indicators & Tools, Orderflow/Footprint, ATAS, TPO, Fibonacci, Entering Trades)
are captured. Detail and sources live in `notes/`.

Instruments here: ES/NQ (trade the micros MES/MNQ, prices mirror 1:1).

---

## 0. The one-line thesis
Price travels from one liquidity region to the next (a "liquidity auction"). Build a daily plan
of predefined levels, wait for price to react at a level, and only take entries that (a) trade
*with* the active auction and (b) are confirmed by candle structure + order flow. Context first,
confluence second. The market must convince you; you never convince the market.

## 1. Build the daily plan (before the session)
1. **Market structure** — Daily High/Low indicator: trending or ranging? Sets risk approach.
   - Never plan a long above a previous day high, or a short below a previous day low (those are
     TP / stop zones).
2. **Daily open vs previous day range** — directional read, trend-day vs range-day.
3. **Daily open vs previous day value area** — in balance or out of balance?
4. **HTF levels** (TradingView) — mark previous month/week/day levels, opens, SFPs, CC S/R, value
   areas (VAH/VAL/POC), single prints, naked/untested POCs.
5. **Active liquidity auction** — classify it and set the target:
   - Supply→Demand flip → target previous demand or previous high.
   - Demand→Supply flip → target previous supply or previous low.
   - SFP of low → target previous demand or previous high.
   - SFP of high → target previous supply or previous low.
6. **Shortlist levels** = untested values that coincide with liquidity grabs, on the auction path.
   Mark both long and short scenarios. Note where price can move fast (single prints, low-volume
   nodes, triple tops).

## 2. Bias filter (keep it simple)
- Trade with trend while ~60-70% of bars close in the trend direction.
- Hold bias on fractals: 2 swing lows back (bull) / 2 swing highs back (bear). Flip only when
  that swing breaks, or when one bar breaks multiple prior swings (confirmed shift).
- A quick poke of a prior day low/high that closes back inside (15m) = liquidity grab, not a
  shift. First real weakness in an uptrend = first previous-day-low break; don't short it, wait
  for a retrace to >= 50% fib.

## 3. Is this level tradeable? (the context gate)
Ask, in order:
- Where did price come from? What is the active auction? Where is the next liquidity region?
- Does this level trade **with** the auction? If it trades against the active auction, skip it,
  even with pretty confluence. (Trades between an auction's start and its target are low-prob.)
- Would I size up here as a large trader? If not, it is at most a reduced-size scalp.
- What invalidates the thesis?

## 4. Entry trigger (3-candle system, order flow confirmed)
Priority order: **COT → COC → Momentum**. Skip inside candles (indecision). Levels are zones.

- **COT (Cherry on Top)** — highest conviction:
  - Reversal candle takes the **liquidity** of the previous candle (prev low for long, prev high
    for short) AND **closes in the extreme quartile** (upper 25% long / lower 25% short).
  - ~88% follow-through stat. Large wick on the sweep is ideal. Can be a 2-candle version.
- **COC (Change of Context)** — second:
  - Candle takes out the **high** of the previous candle (long) / **low** (short). Standard
    3-candle. Combine with COT for best conviction.
- **Momentum** — only when no COT/COC:
  - Break of a **supply** zone + upper-quartile close (long), or **demand** zone + lower-quartile
    close (short). Zone needs **3+ touches**. Enter on the **retest** candle, local stop.
  - Red flag: never long into a previous demand zone / short into previous supply.
- Order-flow confirmation at the level (delta ladder, footprint stats, CVD, OI, buy/sell blocks):
  falling selling pressure into support = first sign of strength (and mirror for shorts).

## 5. Risk and targets (mechanical)
- Risk **< 1%** per trade. Minimum **2:1** R:R (momentum entries aim 10:1 on the tight local stop).
- **2 losses in a row → size -50%. 3 losses in a row → stop for the day.**
- Default stop/TP (adjust to context):
  - ES: 12-tick stop → TP 20 / 25 / 30.
  - NQ: 26-tick stop → TP 40 / 50 / 60.
- TP allocation: with-trend **50/25/25**; counter-trend **75/15/10**; compounding split
  **34/33/33**.

## 6. Trade management
- De-risk at the first logical liquidity target; move stop to break-even there.
- Then trail the stop on each **new** liquidity auction that forms (not fixed partials when a
  strong contextual move is running).
- Don't place stops directly on visible resting liquidity (FIFO queue). Use minor structure
  (lower highs / higher lows).
- Read context, not P&L: "how long can I stay while the market still justifies it?" Exit promptly
  if order flow conflicts after TP1.

## 7. When to stand aside (no-trade is a position)
- Data-driven change only; never revenge/ego adjust. Stand aside when volatility spikes, key
  levels break with conviction against the plan, correlations disagree, or CPI/FOMC/NFP is near
  (wait for post-event structure). Missing a trade beats an avoidable loss.

---

## Level and order-flow reference (confirmed from Indicators / Orderflow / ATAS modules)

Value areas (VPVR, fixed range, session):
- **Short the value area high, long the value area low, never enter new positions at the POC.** POC
  is a magnet. Break of VAH/VAL = support/resistance flip on retest.
- **Naked POC** (untested prior-session POC): first touch usually bounces; trade the FIRST touch
  only, ideally with CC confluence. Pre-session, write down previous day VAL / POC / VAH.
- **80% rule:** price trades above a value area, then accepts back into the VAH → ~80% probability
  of trading back down to the VAL.

VWAP:
- Below VWAP expect resistance, above expect support. Never alone — need 2 confluences + context.
- **VWAP close = a "star" level**: short if price is above it, long if below; ~1-2% reactive moves.
  Session VWAP for scalps; always watch weekly VWAP.

Open interest (health of a move):
- Rising OI + rising price = new longs (bullish). Falling OI + rising price = short covering
  (bearish). Rising OI + falling price = new shorts (bearish). Falling OI + falling price = long
  liquidation (possible reversal). Healthy trend = price + volume + OI all rising.

Order-flow confirmation at a level (what "confirmed by order flow" means in section 4):
- **Buy/sell block = a stacked-imbalance zone** (3+ diagonal imbalances, Fib 618 / ~400%+ ratio,
  350% in the CC ATAS cluster template). These act as reference S/R; price reclaiming a block with
  opposing imbalances = trapped traders = the SFP/reclaim entry.
- **Delta clusters = trapped traders** (heavy green at highs → rejection; heavy sell at lows →
  bounce); only on untested clusters.
- **Absorption** = heavy imbalances into a level while price fails to move; distinguish from
  low-volume drift (tapering volume = not absorption, no edge). Confirm with a market-structure
  break or CVD divergence before entering.

CCTR = the ATAS/CC "trend reversal + clusters" template (lower-timeframe scalp / sniper entries:
trend-reversal chart + session volume delta + single prints + previous-day levels). The "CC zone"
= the 0.618-0.66 fib retracement (ATAS fib F8 with the 66% level added).

---

## Added from the ★★ modules (2026-11-12)

**The five entry triggers** (`notes/entering-trades.md`, `notes/advanced-strategies.md` §17) — at a
predetermined HTF zone, drop to 1-5m, check order flow, and wait for one of: **SFP**, **failed
auction** (spends time beyond the level then reclaims — traps more than an SFP, bigger follow-through,
use a market order), **mini-range** (SFP of an LTF range), **retest** (CVD divergence + trapped
traders), or **market-structure change** (HL/HH or LH/LL — widest stop, least preferred). Each gives
the entry reason and the invalidation.

**CCTR = Candle Close Trend Reversal** (`notes/advanced-strategies.md` §10) — a tight 2-candle scalp:
bullish = reversal candle with the **tail on the open (bottom) side**, enter at the **open of the 2nd
trending candle**, stop just below the low; bearish mirrors. Exits at the **5-min ACR then 10-min
ACR**; **move stop to break-even immediately after TP1**. In ATAS it is the **"range" chart**; keep it
open (OI resets on zoom/refresh). This is the mechanical core of the CCTR template.

**0.25 Range Tool** (`notes/fibonacci.md` §11) — anchor on prev day/week/month H-L or fractal (period
2) swings; **no longs 0.75-1.0, no shorts 0.25-0, 0.5 = stay out / take profit**. Replaces RSI. Longs
above 0.75 / shorts below 0.25 get swept. Candle with no lower wick = unfinished business below (and
mirror).

**Fibonacci** (`notes/fibonacci.md`) — extension targets: **corrective → 1:1, impulsive → 1.618**
(failure to reach 1:1 = weakness). Negative-fib TP ladder -0.236 / -0.618 / -1. Touch-count: 1st
bounces, **3rd breaks**. ⚠️ **TradingView's fib fan labels are one step off from ATAS/pro** (TV
"0.382" = ATAS "0.618").

**Day types & profile** (`notes/tpo-series.md`, `notes/futures-and-stocks.md`) — read the day from
where sessions open vs the IB (A+B): above IBH = trend up, inside = inside/symmetrical day, below IBL
= trend down; **wait for the first hour before building the plan**. Six TPO rules, single prints
(trade like CME gaps), poor highs/lows = continuation, double distribution = never trade counter to
the gap fill. ES 30-min volume: **>100 healthy, <50 low, <25 drying**. **IB equilibrium / halfback**
is the most reliable intraday pullback entry; **settlement rule** = above lean long, below lean short.

**Statistics** (`notes/statistics-and-journaling.md`) — an edge = a measurable repeatable pattern;
want **60-65% win rate**, **min 200 observations**. Severin backtests static levels (dOpen, pdHigh,
pdLow; first-touch stats per session) and playbooks separately, **excludes holidays + DST** (NY open
shifts 3:30↔2:30 CET), and tracks the 3-candle COT/COC/momentum entry per session. Journal the trade
**before** entering — it is the plan.

## Still open (remaining ★/☆ modules + Vault)
- ★/☆ modules not yet harvested: Basic, Channels & Patterns, Beginner/Intermediate Strategies,
  Beginners, Trading Psychology, Elliott Waves (Elroy ~33% in), Quantower.
- DSF (depth-of-market liquidity map) exact ATAS setup (DOM + depth-changes + cluster) vs ExoCharts —
  the Futures module confirmed the ES DOM/tape mechanics but not a one-to-one DSF rebuild.
- Vault: the actual CC ATAS workspace files (Daniel's / Severin's) not yet pulled — see `README.md`.
