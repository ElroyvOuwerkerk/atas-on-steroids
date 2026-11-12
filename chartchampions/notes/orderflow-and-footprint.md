# Module notes: Orderflow and Footprint (Platform Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-11. 9 lessons.
This module defines the order-flow confirmation that Severin's execution relies on, and in
particular what a "buy/sell block" is (a stacked-imbalance zone). Taught on ExoCharts; the same
reads apply in ATAS.

Cross-link: the "buy block / sell block" reference levels in `building-plans-and-context.md`
(lessons 7-9) = the stacked imbalance zones defined here (lessons 5-6).

---

## 1. ExoCharts (order-flow tools) — `exocharts-tutorial`
- Non-time charts (tick / volume / range) print a new candle per transactions or volume, filtering
  sideways noise (e.g. a 10M-volume chart prints only when 10M traded).
- Tick/box sizing: 1 tick = $1 price unit; low tick (1) for scalp precision, higher (10-20) for
  HTF.
- Delta clusters (green = market buys, red/orange = market sells); volume clusters (total,
  direction-agnostic); darker = more activity. Value-area opacity isolates the value area.
- Delta profile = same POC/VAH/VAL as volume profile + buy/sell direction (shows if a naked POC was
  built on buying or selling).
- Imbalances read diagonally with bid/ask multiples (e.g. 300x); higher multiple = stronger filter.
- Liquidation markers (RECT): circle size = relative liquidation size; green = shorts liquidated,
  red = longs. Footprint bar shows total volume, delta, CVD, POC, range per candle.
- Delta bars confirm trend: price breaks a prior high → want increasing delta above it; divergence
  = weakness. OI + net longs/shorts: rising OI + rising net longs in an uptrend = healthy.

## 2. Footprint charts — `footprint-tutorial`
See inside a candle: volume per price. Candle close does not mark where volume occurred.
- Bid/ask view: sell volume (left) vs buy volume (right) per price row. POC at a candle low with
  heavy buying after a support break = likely reversal up.
- **Acceptance** above/below a level = continued willingness to buy higher / sell lower; lack of
  acceptance is one of the strongest failed-breakout signals.
- Bullish breakout signature: rising volume on the breakout candle, increasing positive delta, POC
  climbing on successive candles, P-shaped bid/ask profile, rising net longs.
- SFP (swing failure): breakout on strong volume reverses; pullback on declining volume; buyers
  step in at support; net longs rebuild. Mirror for failed breakouts / shorts.
- Volume clusters: dark = HVN (S/R), light = LVN (price travels through fast). Compare breakout
  volumes (1B vs 350M = weak retest). High volume → trade with the break; low volume → fade.

## 3. Delta — `delta-tutorial`
Passive = limit orders (may not fill); aggressive = market orders (move price). Delta = aggressive
buys − sells.
- Two questions before any trade: (1) where is the HTF market trying to go (market structure, HH/HL
  vs LH/LL)? (2) is it doing a good job (delta + volume)?
- Healthy: price up + rising positive delta/volume. Price up + falling delta = bearish divergence.
- **Delta clusters = trapped traders**: heavy green at highs = trapped buyers → expect rejection on
  revisit; heavy selling at lows = trapped sellers → expect bounce. Only valid on UNTESTED clusters
  (once price passes through repeatedly, participants have exited).
- Breakouts: healthy = rising positive delta + volume; low delta/volume break = fakeout/SFP.
- Range delta: weak negative delta at highs → range likely breaks up; weak positive delta at lows →
  breaks down. Strong buying at lows + weak selling at highs = bullish range bias.
- Breakout checklist: breakout candle delta should exceed the largest candles inside the range,
  with delta concentrated at the breakout zone, not the midpoint.

## 4. CVD (Cumulative Volume Delta) — `cvd-tutorial`
Running total of delta. Thesis: bigger money enters with limit orders; divergences reveal them
absorbing opposing market orders. Use "Full data" (no reset) for divergences.
- Bullish CVD divergence = price higher low, CVD lower low (aggressive selling not pushing price
  down = limit buyers absorbing). Bearish = price lower high, CVD higher high.
- Avoid single-wick (V-shaped) divergences; require absorption built over multiple candles.
- Possible vs confirmed: confirmed when price actually moves away from the level (trapped traders).
- Confirm with a market-structure break (break the low that led to the high → short the retrace);
  larger confirmation = breaking the whole range low. Faster: a failing-break candle with large
  opposing delta is itself confirmation.

## 5. Imbalances Part 1 — `imbalances-part1`
Price moves until one side is overwhelmed. Imbalance = disproportion of buyers vs sellers at a
price; threshold **Fibonacci 618 ratio (~400%+ difference)**. Read **diagonally / stepped**, not
horizontally.
- **Stacked imbalances = 3+ imbalances on top of each other** = strong aggression; the more stacked
  and clustered, the stronger. These become future S/R when revisited (= buy/sell blocks).
- Context to use them: grinding markets and breaks of important levels. Break down WITH heavy
  selling imbalances = continuation; break WITHOUT them = fade candidate.
- Reclaim after a break with buying imbalances = trapped shorts → SFP reversal.
- Absorption at range lows: heavy selling imbalances but price barely drops = limit buyer absorbing;
  wait for a market-structure break up to confirm. Mirror at highs (wait for MSB before shorts).
- Four long triggers: (1) slow grind down with aggressive shorts then buying imbalances appear;
  (2) key liquidity level + buying imbalances; (3) wick through a level immediately bought with
  stacked buying imbalances; (4) local MSB triggering a short squeeze on a bleed day.

## 6. Imbalances Part 2 — `imbalances-part2`
Trade imbalances ONLY at pre-identified reference levels; everything between confluence zones is a
no-trade zone. Imbalances mid-range give no edge.
- Two execution styles: aggressive (enter on absorption evidence) vs confirmation-based (wait for
  a structural signal after absorption).
- Absorption = rounded bottom with heavy negative delta + strong selling imbalances into the lows
  while price fails to break down. Thresholds ~400% or 61.8%.
- Distinguish absorption from low-volume drift: volume tapering (900M → 700M → 400M) into the lows =
  low participation, NOT absorption → no edge.
- Reclaim plan: wait for reclaim of the reference level, require a volume increase + buying
  imbalances + strong positive delta; target the highs via trapped-short liquidation.

## 7-9. Chart types — `chart-types-part1..3`
- Range chart: new candle per N ticks of high-low movement (6-range = 6 ticks = $3; 1 tick =
  $0.50). Range Y: measured from the candle open instead of high-low (cleaner). Tick chart: new
  candle per N transactions (89-tick = every 89 trades) — closest view to raw order flow; large
  volume in few trades = big players.
- Trend reversal chart: trending candles (large, move N ticks with trend) + reversal candles
  (small, move N ticks against); wicks always point against the candle direction (where price tried
  and failed). Good for separating trend vs range. (This is the CCTR chart base.)
- Volume chart: new candle per fixed volume (momentum/liquidity). Delta chart: new candle per fixed
  delta — most accurate for monitoring incoming market orders. Combine with OI, CVD. No single best
  chart; personal preference + experience.
