# Module notes: Fibonacci (Technical Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 11 lessons.
Pins down the exact fib levels CC trades, the "Champions Channel" (0.618-0.66), and — most useful
for an indicator — the **0.25 Range Tool** quadrant rules. Several lessons (4, 5, 8) are livestream
walkthroughs; the mechanical rules are below.

Repeated rule across every lesson: **fib is a confluence tool, never standalone** — stack 2-3
reasons (horizontal level, structure, channel, order flow) before acting, and place stops on market
structure, not arbitrarily below a fib level.

---

## Core levels and the Champions Channel
- Retracement levels that matter: **0.382, 0.5, 0.618**, plus **0.66**. **CC / "Champions Channel"
  = 0.618-0.66** (an extra 0.66 added, backed by CC's data testing). Scale entries across it:
  front-run 0.618 → midpoint → 0.66.
- **Pull direction: always left to right.** Resistance = high (left) → low (right); support = low
  (left) → high (right).
- Trending-market rules: in strong macro uptrends the **0.382 holds as support**; if 0.382 breaks,
  expect a move to the **0.618**; **losing the 0.618 is an early warning the trend may be over**.
  Don't time exact tops/bottoms — wait for the **0.236 to be reclaimed as support (or lost as
  resistance)** to confirm a reversal.
- A second swing retracing into *its own* Champions Channel after a macro-CC bounce = high-prob long.

## 1. Fibonacci Retracement — `Fibonacci-retracement`
The core lesson (all of the above). Confluence = multiple fib pulls (overall swing / impulsive move /
local swing) whose levels **cluster tightly** = stronger S/R. Fib + channel-top alignment = strong
short zone.

## 2. Negative Fibonacci — `negative-fibonacci`
External targets once price leaves the retracement pool. Levels to add: **-0.236, -0.618, -1,
-1.618** (stocks often prefer **-0.272**). Use as take-profit ladder: **TP1 -0.236, TP2 -0.618,
final -1 (a 1:1 extension)**; horizontals stay the primary target, fib adds confluence. Internal vs
external retracement: internal = price stays within the swing hi/lo; external = breaks the range (use
extensions). Negative levels can also define parallel-channel boundaries.

## 3. Fibonacci Expansion — `Fibonacci-expansion`
For projecting targets with **no horizontal levels** (all-time highs/lows). Distinct from the
extension (expansion = external projection of the pool; extension = 3-click Elliott tool). Levels:
enable **2.0**; focus **1.272, 1.133, 1.414, 1.618** (tie into harmonics). Flip the pool after a
reaction (old point 2 becomes new point 1). Buying all-time lows with falling-wedge / oversold /
bullish-divergence confluence is lower-risk than shorting all-time highs.

## 4. Fibonacci Practice & Risk Management — `fibonacci-practice`
Livestream. Themes: consistency and patience over being right, strict risk management, "you don't
always have a setup — wait." Integrating retracement + extension + negative fib on live charts; use
the correct exchange data; log trades. No new mechanical rules beyond the above.

## 5. Fibonacci Charting — `fibonacci-charting`
Livestream walkthrough across BTC + alts of retracements / extensions / expansions together. Reminder
to use **log scale on volatile assets**. Practice material, not new rules.

## 6. Fibonacci Extension — `Fibonacci-extension`
TradingView **Trend-Based Fib Extension** (3 pivots). Structure: bullish low→high→higher-low; bearish
high→low→lower-high. Levels **0.618, 1.0, 1.618**, then 2.618 / 3.618 / 4.618 once 1.618 breaks.
- **Corrective / choppy move → target the 1:1 (1.0).** **Impulsive / trending → target 1.618.**
  **Failure to reach the 1:1 = sign of weakness.** Lock partials at 1:1 even in strong moves.
- The 1:1 often produces an **SR flip** (buyers from a prior bounce get trapped once it breaks, exit
  at BE on retest → reinforces resistance), stronger with a psychological round number.

## 7. .5 Fibonacci (algo / bot footprints) — `05-fibonacci`
Bots trade programmable fib levels (buy 0.5, sell -0.618, hard stop below 0.786), cleaner on **low-
market-cap coins** (less human noise). Map full range: negative retraces (0, -0.5, -1, -1.5, -2…)
and positive expansions (1, 1.5, 2, 2.5, 3).
- **Touch-count rule (indicator-relevant):** 1st touch bounces, 2nd often still holds, **3rd touch =
  high probability of break-through** (the level's resting liquidity is depleted). Stop-loss cascades
  can skip a level (e.g. jump -1.5, hit -2).

## 8. Gann Retracement Levels — `Gann-levels`
Livestream. **Gann retracement = eighths and thirds** (1/8, 1/4, 3/8… and 1/3, 2/3), used alongside
fib to find confluence. Pivot selection is crucial — consider **intraday pivots, not just the obvious
overall hi/lo**. Also covers timeframe focus per style (scalp/day/swing/position) and staying
reactive, not fixed-bias.

## 9. Fibonacci Time — `Fibonacci-time`
Times likely reaction points on the x-axis (most traders ignore time). **Trend-Based Fib Time** tool:
click low → high → double-click; key levels **0.618, 1.0, 1.618, 2.0, 2.618** (a 0.5-based template
for stocks).
- **Corrections take longer than impulses** (often ~2:1 time), so **prefer 1.618 / 2.0 over 0.618**
  (0.618 is usually too short). A correction that is too quick is likely just the first leg of a
  bigger, deeper correction.
- Being right on direction but wrong on timing = stop-outs. Strongest combined with harmonics /
  Elliott count; works all TFs but more reliable on 4H+.

## 10. Fibonacci (Speed) Fan — `Fibonacci-fan`
Macro-timeframe (4H+) tool: 2 clicks (like a retracement), produces horizontal levels + time
intervals + angled fan lines. Focus **0.382, 0.5, 0.618; 0.25 = last level before a trend
extension**. Strongest where a fan angle intersects a horizontal fib + fib-time. Lose a fan level →
target the next fib below.
- **Important ATAS/TV note: TradingView's Fib Speed Resistance Fan labels angles differently from
  pro platforms (ATAS, TT, Sierra). What TV labels "0.382" = "0.618" on ATAS/pro software.** The
  levels still work; mind the label mismatch when matching CC templates.
- Works poorly on fast impulsive dumps; resets at the 1:1 extension if price breaks all levels.

## 11. 0.25 Range Tool — `25-Range-tool`  ★ indicator-grade
A probability tool dividing a range into quadrants (0 / 0.25 / 0.5 / 0.75 / 1.0). Replaces RSI /
oscillators — structure shows overbought/oversold without lag.
- **Anchor points:** previous day / week / month high-low, or clearly defined swing highs/lows (use
  the **fractal indicator, period 2**, to find valid swings).
- **Core quadrant rules:**
  - **0.75 → 1.0: no longs** (look for shorts / reversals).
  - **0.25 → 0: no shorts** (look for longs / reversals).
  - **0.5 midpoint: stay out or take profit** (treated as 50/50; the midpoint is a natural TP zone).
  - One-line rule: **"no more longs at 0.75, no more shorts at 0.25."**
- Longing a breakout above 0.75 / shorting a breakdown below 0.25 = getting caught in liquidity
  sweeps. **Sub-range technique:** apply a second 0.25 fib inside the upper/lower quadrant for refined
  breakout entries. Compound at 0.5 only with order-flow / OI confluence.
- Candle read: **no lower wick = unfinished business below; no upper wick = unfinished business
  above.** Confirm swing lows with the fractal 3-bar pattern + 4th-bar confirmation. Fractals: period
  2, small triangle icons; 15m/1h/4h/D/W most useful for swings.
