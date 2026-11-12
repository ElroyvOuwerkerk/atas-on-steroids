# Indicator backlog (Severin method → ATAS / TradingView)

Mechanical pieces of Severin's method that can be flagged automatically, so a setup announces
itself instead of being eyeballed. Ordered roughly by value-to-effort. Each item notes the
platform that fits best. This is a backlog for discussion, not a commitment; we build after the
notes are fuller and Elroy picks the first one.

> **Items A-Q are candidate FEATURES of one signal indicator, not seventeen indicators to build.**
> Elroy wants the single strongest signal, scored by probabilities/statistics — not dozens of
> overlays (CC | Indicators, 2026-11-12). Most of these fold into **Level Trigger** (`indicators/
> LevelTrigger.cs`) as gates/stages that earn their place only if the scoreboard shows they help.

Legend: [ATAS] C# indicator in `indicators/`; [TV] Pine; [either].

---

## A. 3-Candle Entry detector (COT / COC / Momentum) — highest value
The most mechanical part of the whole method and the closest to "don't think, just react".
- **Inputs:** timeframe (works fractally), optional level list (from the TV level sync) to require
  confluence, quartile threshold (default 25%), min touches for momentum zones (default 3).
- **Logic per bar:**
  - Quartile of close within the bar's range (track upper/lower 25%).
  - COT = bar sweeps previous bar's low (long) / high (short) AND closes in extreme quartile.
  - COC = bar takes out previous bar's high (long) / low (short).
  - Momentum = break of a supply/demand zone (3+ touches) + extreme-quartile close, then arm a
    retest entry on the next bar; local stop just beyond the retest candle.
  - Suppress inside candles (fully within previous range).
- **Output:** arrow/label with entry type, plus suggested stop (local) and the 88% tag on COT.
- **Platform:** [either]. TV first (fast to iterate, and the alerts can fire on his micros chart).
  ATAS version later to combine with real order flow confirmation.
- **Open:** exact "close in extreme quartile" vs wick handling; whether to require order-flow
  confirmation (ATAS) before flagging.

## B. Previous-day / week / month High-Low + Value guard — foundational
Severin's bias spine. Likely overlaps with built-ins and the existing level sync; audit first.
- Plot previous day/week/month high, low, and open; shade "never long above PDH / never short
  below PDL" zones; flag when price pokes and closes back inside (liquidity grab).
- Value-area relationship of the daily open vs previous day value area (in/out of balance).
- **Platform:** [TV] (ties into the level sync) with an ATAS counterpart if needed.
- **Check first:** TradingView built-ins and the "Daily High/Low indicator" Severin uses may
  already cover most of this (per the hard rule: audit built-ins before building).

## C. Extreme-quartile / unfinished-auction candle stat — small, reusable
Standalone building block for A. Colors bars that close in the extreme quartile and tracks the
realized follow-through rate (to verify the 88% claim on his instruments).
- **Platform:** [either]. Cheap, good first deliverable, feeds the win-rate question.

## D. Supply/Demand zone tracker (N-touch) — feeds A (momentum)
Auto-draw supply/demand boxes, count touches, mark "valid" at 3+ touches, flag break + retest.
Box-drawing rule: include the most wicks while staying as small as possible.
- **Platform:** [ATAS] fits order flow; [TV] also viable. Non-trivial (zone bookkeeping).

## E. Liquidity-auction state machine — context automation (ambitious)
Classify the active auction (Supply→Demand flip, Demand→Supply flip, SFP of high/low) and show
its target region, so entries can be filtered to "with auction only". This is the context gate
in `playbook.md` section 3.
- **Platform:** [either], significant logic. Later, once supply/demand zones (D) exist.

## F. Risk/position-size + loss-streak helper — journal-adjacent
Given account size and the per-instrument tick stop (ES 12 / NQ 26), output position size at 1%,
enforce 2:1 min, and surface the 2-loss (size -50%) / 3-loss (stop) rules. Fits the trading
journal more than a chart indicator.
- **Platform:** trading-journal dashboard, or a small ATAS panel.

---

## G. Stacked-imbalance / buy-sell block detector — feeds A, D, E [ATAS]
Now well defined by the Orderflow module: an imbalance is a diagonal bid/ask disproportion at the
Fib 618 / ~400%+ ratio (CC ATAS template uses 350%). A **buy/sell block = 3+ stacked imbalances**.
- Auto-mark blocks, keep them as S/R zones until price trades cleanly through, and flag a reclaim
  (price retakes a block with opposing imbalances = trapped traders / SFP).
- Reads ATAS footprint (`candle.GetAllPriceLevels()` bid/ask per price, as `StaleBoxAlertMuter.cs`
  and `AbsorptionSqueeze.cs` already do). This is the concrete definition of the "buy/sell block"
  Severin references. High value.

## H. Value-area + naked-POC level tool — foundational [ATAS or TV]
Plot session/previous-day VAH, VAL, POC; mark untested (naked) POCs; flag the first touch; alert
the 80% rule (price accepts back above a VAH → watch for the move to VAL). Encodes "short VAH, long
VAL, never at POC".
- **Audit first:** SVP HD (built-in, already used) and ATAS built-ins cover much of this. Build
  only the naked-POC-first-touch + 80%-rule alerting that the built-ins don't flag.

## I. OI four-quadrant classifier — small, high clarity [either]
Label each bar/zone from price + OI direction: new longs / short covering / new shorts / long
liquidation, and tag "healthy" when price + volume + OI align. Crypto has OI; for futures use
the CME OI series. Good small standalone that sharpens bias reads.

## J. Delta-cluster trapped-trader + VWAP-close star flags — small [ATAS]
Flag untested delta clusters (heavy green at highs / heavy sell at lows) as likely rejection/bounce
zones, and mark VWAP-close "star" levels (short above / long below). Both are cheap reads on data
ATAS already exposes.

---

## From the ★★ modules (2026-11-12)

## K. CCTR detector (Candle Close Trend Reversal) — refines A [ATAS]
The most precisely specified entry in the whole course (`notes/advanced-strategies.md` §10). Bullish:
a reversal candle whose **tail is on the open (bottom) side**, then arm an entry at the **open of the
next trending candle**, stop just below the low; bearish mirrors. Add order-flow confirm (OI up +
delta). Outputs: arrow at the 2nd-candle open, stop line, and 5-/10-min ACR take-profit lines.
Overlaps item A — likely the first thing to build, since the rule is unambiguous. ATAS "range" chart.

## L. 0.25 Range Tool / quadrant classifier — small, high value [either]
Clean and mechanical (`notes/fibonacci.md` §11). Anchor on prev day/week/month H-L (or fractal
period-2 swings), split into quartiles, and shade: **0.75-1.0 = no longs, 0-0.25 = no shorts, ~0.5 =
neutral/TP**. Flag when price enters a "no-trade" quadrant and when a candle prints no wick on one
side (unfinished business). Overlaps item B (PDH/PDL guard) — could be one tool. Replaces RSI.

## M. Fair Value Gap detector — small [either]
3-candle imbalance (`notes/futures-and-stocks.md` §9). Mark the gap, keep it as a magnet/target, stop
at the **origination candle**; best on 5/15m. Many FVG indicators already exist — **audit built-ins
first**; the CC-specific part is pairing it with a prior swing high/low for invalidation and footprint
"push" confirmation.

## N. Failed-auction / 3rd-touch / frontrun setup flags — feed A, E [ATAS]
Three concrete setups from `notes/advanced-strategies.md`: **failed auction** (break a range, spend
time beyond, close back in → fade with stop past the extreme), **3rd touch** (pivot1 → SFP/failed
auction pivot2 → retest pivot3, CVD divergence), **frontrun** (key level just missed → high prob it
fills). All reuse the same trapped-trader / CVD-divergence order-flow reads as G.

## O. Order-block detector — feeds D [ATAS or TV]
Last down candle before an impulsive up move near support (confirmed when the next candle trades above
its high), ideally at the **0.786 fib**; mark as a zone, valid ~7-14 days (`notes/advanced-strategies.md`
§6). Related to the supply/demand tracker (D).

## P. Trend / bias state — Level Trigger STAGE 2 (not a separate indicator) ★ Elroy's request (2026-11-12)
**What he wants:** on the chart it should read e.g. "trending up today" / "down" so that when he has
levels marked but the trend is strong, he does not chase shorts into a strong uptrend.
**How it's built (per CC | Indicators):** NOT a standalone `TrendState.cs`. It is **stage 2 of Level
Trigger** — the trend state shown in plain words, **logged per level touch**, with the scoreboard split
into **with-trend / against-trend / range**. That turns "no shorts in a strong uptrend" into a
*measured* statement about his own level entries, not an unscored banner. One signal indicator, scored
by probabilities. Code lives in the "Level Trigger v0" session; this file only carries requirements.

**Start minimal** (Severin's statistics lesson: fewest variables first) — two rules:
1. **30-min one-time framing.** Trend is up while each new 30-min bar does not take out the prior bar's
   low (down = prior bar's high); the OTF break ends it, and a break wants **2 ticks of confirmation**
   (quick re-entry = stop hunt). Refs: `notes/tpo-series.md:26`, `futures-and-stocks.md:86`,
   `tpo-series.md:130`.
2. **Share of recent bars closing in one direction** — stay with trend while **~60-70% of bars close
   in that direction**. Ref: `notes/building-plans-and-context.md:45`.

**Candidate variables to log and test ONE at a time** (keep only what separates winners from losers in
the with-/against-trend split — a "4/5 filters agree" count is a count, not a probability):
- **Rotation Factor** — each 30-min period scored **+1 / 0 / -1** by directional progression; cumulative
  = which way the market is trying to go; strongly one-sided RF while price fails to follow = failed
  attempt. Ref: `notes/tpo-series.md:63`. *Gap:* the notes say "directional progression" but not the
  exact +1 rule — the pulled Vault "TPO" cheat sheet likely pins it; confirm before coding.
- **Six day types from the IB** (A+B = first hour; high/low = IBH/IBL): sessions C-G opening **above
  IBH** = bulls / OTF-up, **within IB** = inside/symmetrical, **below IBL** = bears. Types: Normal,
  Normal Variation, Neutral, Neutral Variation, Trend, Double Distribution. Refs:
  `notes/futures-and-stocks.md:60-65`, `notes/tpo-series.md:41-53`.
- **ES volume at the auction extreme** (~100 healthy / <50 low / <25 drying). ⚠️ **NOT** the 30-min
  bar's total volume — a full ES 30-min bar trades tens of thousands of contracts, so these describe
  the volume printing at the probing extreme (worked example: 2000 → 660 → 79 at the low = drying).
  The lesson's wording is loose, so the exact per-level unit is **unverified** — confirm against the
  Vault "TPO" / "Case Study Volume Profile" sheets before coding. Ref: `notes/tpo-series.md:130-131`.
- **"How to Spot a Trend" filters:** HH/HL vs LH/LL, VWAP side, one-time framing, and MAs **LTF 8/24
  SMA** (reversals), **50 SMA** (local structure), **daily EMA 100/200** (S/R flip). Ref:
  `notes/futures-and-stocks.md:85-87`. The daily routine also uses **4h 55 & 200 EMAs** (confluence
  only, `notes/basic.md:45`).
- **"Is it doing a good job?"** (Severin MS part 2): grade the attempt by volume in the trend direction
  + value-area migration (value clearly higher/lower vs overlapping/inside-day). Ref:
  `notes/basic.md` §6-7.
- Structure spine for all of it: 3-pivot trend confirmation via swing H/L (fractal, period 2),
  `notes/basic.md` §6.

**TradingView: yes, but DEFERRED — do NOT start it yet (Elroy, 2026-11-12).** Sequence: first run
Level Trigger **stage 2 in ATAS** and let its scoreboard show **which trend rule actually helps**;
**then port exactly that one proven rule** to a new small Pine file. No Pine file is created now, by any
session, until the ATAS scoreboard has spoken. When that time comes: a **new Pine file** (NOT touching
`tradingview-sync/atas-levels.pine`, hard rule 1), on his MES/MNQ chart (prices mirror ES/NQ 1:1).
- **What Pine CAN do (the price-structure subset — the two starting rules live here):** 30-min
  one-time framing, % of recent bars closing in the trend direction, HH/HL structure (pivots/
  fractals), EMAs (8/24 SMA, 50 SMA, daily EMA 100/200), VWAP side, IB/day-types via session logic,
  and Rotation Factor (per-30-min range progression is computable). So the TV version can start with
  the exact same two rules.
- **What Pine CANNOT do:** real order flow — delta, footprint, CVD, OI, and volume-at-the-extreme all
  need ATAS data. Those candidate variables are **ATAS-only**; the TV readout is the structural part.
- **Install hazard (hard rule 3):** turn the Level Sync widget's `auto` OFF before opening TradingView's
  Pine Editor, or the userscript pastes levels into the Pine source.

## Q. TD Sequential (Sequence) — audit first [either]
`notes/advanced-strategies.md` §19 is TD Sequential (setup 1-9, countdown 1-13, perfected rules).
**Already exists on most platforms** (TradingView has it built in) — audit before building; the CC
value-add is only firing at a key area of interest, not the count itself.

## Notes / constraints (from repo rules)
- Audit installed + built-in indicators before building anything (SVP HD already covers session
  volume profile; do not rebuild volume profiles in Pine).
- New work goes in new files only; never edit `atas-levels.pine`, `atas-levels.user.js`, or
  existing `.cs` indicators.
- ATAS build notes (deploy path, no Newtonsoft, InvariantCulture) live in the root `CLAUDE.md`.
