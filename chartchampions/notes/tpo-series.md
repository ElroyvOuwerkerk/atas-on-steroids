# Module notes: TPO Series (Market Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 16 lessons
(Master level, taught largely by Igor). This is the deep market-profile module and the richest
source of mechanical, indicator-ready rules: day types, the six TPO rules, single prints / poor
highs-lows, ledges, double distribution, Rotation Factor, Eagle sessions/time, and an ES-specific
RTH replay. Directly feeds the context gate and level work in `playbook.md`.

Vault cheat sheets referenced: **"TPO"** (parts 2, 7, 8, 9, 14, candle-structure) and **"Igor
Time"** (parts 12-13).

TPO = Time Price Opportunity. Each letter = a 30-min period; **48 letters/day** (A-X caps = first 12h,
a-x lowercase = next 12h), from **UTC midnight** in crypto. Fixed-range volume tools measure *volume*;
TPO measures *time*. TPO alone is data organisation, **not a strategy** — the edge is price + time +
volume agreeing. **No volume, no auction.**

---

## Core auction principle (Parts 1, 8)
- Market cycles **balance ↔ imbalance**. Balanced range = fair value; a volume increase = acceptance
  / breakout; a **low-volume breakout fails**.
- **Time vs volume discrepancy** = the key read: price spends lots of *time* at a level but little
  *volume* → weak. **At highs = bearish, at lows = bullish** (fade back toward the volume POC). Always
  check **delta + open interest** at the volume POC for context.
- Discrepancy between **time POC and volume POC** signals a potential reversal / fade.
- **One-time framing** = a strong trend where each prior higher-low (or lower-high) is not violated,
  with rising volume/delta/OI; **never fade it**. Mark levels from the furthest-out price.

## Six TPO trading rules (Part 2)
1. Acceptance into an **old balance** likely leads to testing the **other side** of it.
2. Inside a balance, expect rejection of the **value area high** and bounce from the **value area
   low**.
3. Price moving **out of balance** generally travels to an **old balance range**.
4. Rejection from an **old range POC** can invalidate rule 1.
5. **Time + delta + OI building at the VAH/VAL** → likely breakout.
6. (Igor's personal rule) Trade an **SFP on the first attempt** to break a balance **with no increase
   in delta or OI**.
- Keep **old ranges and naked POCs** marked on the chart for reference.

## Six day types (Part 3)
**Initial Balance (IB)** = the first hour (from UTC midnight in crypto). Four range-bound, two
trending:
- **Normal** — wide IB, price stays inside it all session (more a stock-market type).
- **Normal Variation** — smaller IB, range extension within the first ~6h (Eagle Session 1); P- or
  b-shape.
- **Neutral** — wider IB, value in the middle, both extremes rejected (buying + selling tails). The
  **next day is very likely range-bound** (strong continuity edge). Trade it: SFPs at the prior
  neutral range high/low — longs at lows, shorts at highs.
- **Neutral Variation** — like Neutral but closes at the session high/low (momentum continuation).
- **Trend** — one-directional, rising volume/delta/OI, rows of single prints between short
  consolidations, one-time framing. Exhaustion = those increases fade. **Don't fade a strong trend**
  regardless of pre-marked S/R.
- **Double Distribution (DD)** — two balances joined by one column of single prints (balance →
  impulse → new balance).
- Framework: trend → trade with direction; range → short highs / long lows; a range becomes a trend
  when broken with rising volume + OI + delta.

## The all-in-one template + Rotation Factor (Part 4)
Combines TPO + volume profile + candlesticks with a daily-stats block:
- **A/B TPO %** (time above/below time POC), **Above/Below volume %**, **Total Delta**, **RF**
  (Rotation Factor), **IBH/IBL**, **ACR** (Average Candle Range), **V** (total volume). Per-row stats
  (TPO count, volume, net delta) flag high-volume/high-delta **friction zones** for scalps.
- **Rotation Factor:** each 30-min period scored **+1 / 0 / -1** by directional progression;
  cumulative = which way the market is *trying* to go. Strongly negative RF but price not falling
  (stuck in range) → market trying down and failing → **probability favours an upside resolution**.
- Preferred settings: **30-min interval, 50-tick increments** (instrument-dependent). Heat-map TPO
  colouring red→yellow→green→blue by time spent.

## Single prints, tails, poor highs/lows (Part 5) ★
- **Single prints** = a TPO "wick" in the *middle* of the profile; trade like **CME gaps**: top =
  support if above, bottom = resistance if below. **Acceptance into the row → expect the row to
  fill** before any bounce. Strength by print count (bracketed): **≥5 = strong**, min 3 to count.
- **Buying/selling tails** form at the *extremes* = larger-participant rejection. **Poor high / poor
  low** = a profile *without* a tail → big traders didn't defend → **continuation likely** (don't
  short a poor high; wait for continuation or an SFP that builds a selling tail).
- **Trapped-participant read (confirm tails with volume/OI/delta):**
  - **Selling tail + rising OI + positive delta = trapped longs (bearish).**
  - **Buying tail + rising OI + negative delta = trapped shorts (bullish).**
  - Tails with no volume/OI/delta = weak signals unless at a major pivot.

## Ledges + Double Distribution setups (Part 6)
- **Ledge** = 2+ TPO blocks stacked at one price **inside** the profile (a poor high/low in the
  middle) → intraday S/R (day-trade tool). A ledge of **4+ blocks** that first rejects is more likely
  to break on a retest. Breakout-retest signature: breakout = rising OI + volume + positive delta;
  retest = declining OI + volume, neutral delta. First choice at a ledge is always the **SFP**; if it
  breaks, long the backtest.
- **DD setup:** distribution → quick move → **single-print row** → second distribution. Acceptance
  into the single-print row → target the **opposite end of the row** (gap fill). Two variants: SFP
  (wick into the prints, rejected) or acceptance (sustained, target the far side). **Directional
  discipline: never short an upside DD acceptance, never long a downside one.** DD's main value is
  knowing **when NOT to trade** counter-trend ("Bart" patterns = short/long squeezes because the fast
  move built no volume S/R).

## Merging profiles (Part 7)
Merge daily profiles **within a visually-recognised range** into one bigger profile → the merged
POC/VAH/VAL are **stronger than single-day levels**. Best in ranges, not trends. No hard rules on how
many to merge (experience). Merge a *potential* range's profiles ahead of time to get the levels
before the crowd.

## Eagle Sessions (Part 11) + Eagle Time (Parts 12-13) ★
- **Eagle Sessions** = a day-trade profile tool. Each day has **four sessions**, each with its own
  VAH / VAL / POC. Core play: trade **naked session POCs** (untested) from the *previous* session =
  resting liquidity that traps the wrong side. Unlike TPO charts, Eagle shows naked *session* POCs.
  - Favourite setup: **naked session POC + VWAP + SFP** (triple-confluence scalp). Price often trades
    between two naked session POCs (one support, one resistance).
  - **Double session POC** (current naked POC aligns with a prior session POC at ~same price) = much
    stronger; more stacked POCs = stronger level. Merge sessions when POCs/value areas overlap.
  - Trend day: each new session POC should rise and support the next; **losing the prior session POC
    = the trend may be over** → expect consolidation/retrace.
- **Eagle Time** = four daily sessions start at fixed times when volatility/volume injections occur
  (different from London/NY/Asia opens; UTC+1 times in the "Igor Time" Vault sheet). Expect major
  pivots or key-level breaks at each session start. Eagle Time marks **when** (intraday volatility);
  context gives **direction**. Contrast with Fibonacci Time (weeks/months/years). **Sessions 2 and 3
  usually carry the most volume** (exceptions happen). Prefer **fresh, untested** levels over already-
  tested ones.

## Candle Structure Formation / TPO letters (Part 15)
Read a forming 30-min candle via its TPO letters (OHLC by colour, split mode) and wait for a
mechanical sequence before entering. Combine with poor highs/lows, buying/selling tails, value area,
time POC — one screen for a quick decision. **No volume, no auction.** Tips: build stats for **volume
drying out at extremes** (signals an auction ending), focus on stats as levels (prev day H/L, value
area) are taken, and add the day's delta + volume profiles next to the 30-min profile to follow the
big-money trail.

## ES / futures RTH replay (Part 16) ★ most futures-specific
Market replay of the RTH (Real Trading Hours) session; focus shifts entirely to RTH once the open
occurs (vs ETH, electronic hours).
- **Pre-open (mark ~7-8am so London H/L are set):** overnight high/low, previous-day VAH/VAL/POC,
  poor highs, excess highs/lows, single prints.
- **Volume at the auction extreme on ES (~100 = healthy / <50 = low / <25 = very low / drying):** these
  are **not** the 30-min bar's total volume (a full ES 30-min bar trades tens of thousands of
  contracts) — they describe the volume *printing at the probing extreme* as the auction tries to
  extend. Low volume at an extreme = exhausted auction; worked example: **2000 → 660 → 79 contracts at
  the low = the downside auction drying up** → long on return above the first TPO period. ⚠️ The lesson
  phrased this loosely as "30-min volume thresholds", so the exact per-level unit is **unverified** —
  confirm against the Vault "TPO" / "Case Study Volume Profile" sheets before using the raw numbers.
- **Rotation thesis:** once price accepts inside one side of yesterday's value (e.g. VAL), it
  typically rotates to the other side (VAH).
- **IB equilibrium / "halfback" (EQ, midpoint of a period's range) is one of the most reliable
  intraday pullback entries** — respected to the tick. On a strong-volume rip, buy the EQ pullback
  (50% retrace of the prior period's range) for low-risk continuation targeting the prior high.
- **Scalp confluence highs/lows:** overnight high + yesterday's VAH = strong magnet; preset limits a
  tick inside, **12-20 tick stops**, target the reference level, close before it's breached.
- **Breakout on high volume = continuation** (buy pullbacks to EQ); on low volume = suspect.
- **Settlement price rule: above settlement lean long, below lean short** (with value context).
- **Skinny profiles produce no meaningful POC** → reference the prior RTH POC instead.
- **One-time-framing break needs 2 ticks of confirmation**; quick re-entry into the range = treat as
  a stop hunt, not a reversal. ES/NQ **open interest is only released after session close** (crypto is
  real-time). Combine volume profile (finished auctions) with delta/DOM (aggression) for conviction.

## Practice walkthroughs (Parts 9, 10, 14)
Live BTC sessions applying the framework (merge profiles, naked POC, SFP, VWAP + fib confluence,
define entry/stop/target, wait for confirmation, treat each trade as probability). Part 14 "Finale":
**context is king** — even when confluences line up, skip the trade when context (e.g. bearish CVD
divergence, no bullish divergence) disagrees. No new mechanical rules beyond the above.
