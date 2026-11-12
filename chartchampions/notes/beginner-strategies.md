# Module notes: Beginner Strategies (Technical Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 9 lessons
(Adept level). The named CC mechanical setups (CCW, CCSS, CCV, Daily Open, EMA) — several are
indicator-relevant and formalise rules seen elsewhere (CCV = the 80% value rule). The standard-tool
lessons (pivots, trendlines, pitchforks) are noted briefly.

---

## 1. CCW Setup (fib ladder) — `ccw-tutorial`
A mechanical fib-ladder strategy with predefined stops and TPs (emotion-free; combine with
confluence). Draw the fib low→high (long); **4 laddered entries at the golden retracement levels,
25% each** (weight by confluence). **Stop a few dollars below the swing-low anchor** (covers double-
bottom SFP sweeps). Scaled TPs at successive extensions; **TP1 at the -0.28** (testing showed -0.28
often aligns with the 0.618 of the next high-low leg). Move stop to BE at TP1/TP2. Use a structural
reference (POC) to tighten an otherwise-wide stop (e.g. 6% → 3.5%).

## 2. EMA 2.0 Strategy — `EMA-strategy`
4H+ (daily preferred). **26 (orange) / 55 (yellow) / 100 (white) / 200 (green) EMAs.** Only tradable
when EMAs **fan out** (compression = no signal). **Buy signal: 55 crosses below 26** (uptrend);
**exit/reverse: 55 crosses back above 26.** In an uptrend (HH/HL) **buy dips at the 55/100/200 EMAs**
with horizontal + fib confluence (**0.382-0.618 golden pocket**, SR flip at 0.5/0.618); 200 EMA guides
the stop. RSI bearish divergence = early warning only. Shorts mirror.

## 3. CCSS / CCS — Chart Champion Sculpting (scalp) — `CCSS-strategy` ★
A high-frequency **counter-trend** scalp (not for swing traders). **Bollinger Bands + 8/12/26 EMA +
Elliott + horizontals + fib** on the **3-min** (5-min in extreme volatility).
- **Entry: a candle closes outside the lower BB → long (upper BB → short).** Limit at the **candle
  body close** (not the wick), a few cents inside to get filled. A wick-only poke of the band is not a
  signal.
- **Exit: scale 33% at the 8 EMA, 33% at 12, 33% at 26.** Move stop to BE after the 8 EMA.
- **Stops by volatility:** low vol ~0.2% stop for ~0.5%; normal/high ~1% stop for ~2% (to the 26 EMA);
  keep **≥2:1**. Best in **high volatility** (5-10% daily). Add RSI/MACD divergence; highest
  conviction = a BB close at a **monthly** level (+ fib). Don't fade strong momentum breaks unless RSI
  is extreme (single digits / 90s) with divergence.

## 4. CCV Setup — Chart Champions Value "80% setup" — `ccv-tutorial` ★ = the 80% rule
The **80%-probability** value-area play (same mechanic as the TPO 80% rule): market **opens outside
the prior day's VAH/VAL**, then gets **accepted back inside via two consecutive 30-min closes** within
the value area → price tends to **traverse to the opposite side** of the prior value area (a broken
VAH/VAL flips to S/R). Volume helps, not required.
- **Context gate (essential):** prior day range-bound (good) vs a fresh breakout on volume + rising OI
  (skip — V-reversals are rare; BTC builds a new range first). Check distance from daily open to the
  value area, VA width, absorption/CVD against your direction, and whether a daily-open retest is
  likely. Example: short just below VAH, target just inside VAL (front-run the fill).

## 5. Daily Open Strategy — 3 open types — `daily-open-strategy` ★
Use the **Initial Balance (first hour after the midnight-UTC daily open)** to predict the day. Three
open types, each with a defined trigger + invalidation:
- **Open Drive:** opens **outside** prior value, immediate strong volume-backed impulse → **trending
  day**; enter on the initiative volume, invalidation = back above the open (but stop on nearby
  structure / order block, smaller size if far).
- **Open Test Drive:** slow choppy start, loses the open, **retests it, then a volume injection** →
  enter the injection candle off the retest; invalidation = the test candle's low/high (not the open).
- **Open Rejection Reversal:** weak open, drifts to a **key level** (VA / S/R / fib / weekly-monthly),
  **strong rejection drives price back through the open on volume** → enter on that; invalidation =
  back above the key level (or enter earlier at the key level for a tighter stop).
- Strong impulsive IB + volume → trend day; weak IB → range day or wait for a retest.

## 6-9. Standard tools (summarised)
- `pivot-points-strategy` (6) — classic floor/Camarilla pivots (P, R1-3, S1-3) as intraday S/R;
  CC references the Composite Camarilla POC in the Frontrun lesson.
- `trendline-trading` (7) — 2 touches to draw, 3rd = speculative; break = early trend-change sign
  (Elliott). Covered more in `basic.md` / `channels-and-patterns.md`.
- `pitchforks-part1` / `pitchforks-part2` (8-9) — Andrews' pitchfork (median line + the 1.618
  extension used in the confluence case study); anchor on three pivots, trade reactions at the median
  and outer lines.
