# Module notes: Advanced Strategies (Market Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 19 lessons
(Master level). The most strategy-dense module — concrete, mostly mechanical setups that map almost
one-to-one onto indicator ideas. Directly feeds `playbook.md` section 4 and `indicator-backlog.md`.

**Scope note:** the strategy setups are captured in full. Three lessons not transcribed (lower
relevance to ES/NQ futures): `order-blocks-live-analysis` (7, a walkthrough of lesson 6),
`hedging-part-1` / `hedging-part-2` (11-12, crypto spot+perp hedging), `spot-vs-derivatives` (16,
crypto spot vs perps). Slugs are in the sidebar if wanted.

Vault cheat sheets referenced: **"If One then Two"** (2), **"Liquidity"** (4, 3rd touch), **"Frontruns"**
(5), **"Order Blocks"** (6), **"Entry Triggers"** (17). (Earlier modules also cite "Trade Entry
Checklist", "TPO", "Igor Time", "How to Spot a Trend", "Trading Setup", "HTF vs. LTF".)

---

## Level 2 + Time & Sales (Lesson 1)
- **Level 2 / order book** = all bids/asks + sizes at each price (vs Level 1 = best bid/ask only).
  **Can be spoofed by bots** (orders pulled fast). **Time & Sales (the tape) cannot be manipulated** —
  executed trades are permanent. Depth-changes column shows volume added/removed per level.
- Use: confirm S/R with large resting orders, find where orders stack or get pulled, **filter the
  tape for large orders to follow big money**. Scalp tool, not for swing traders. Trades shown used
  **10-tick risk / 20-tick target** and **4-tick/4-tick** scalps; after a loss, **increase the TP** on
  the recovery trade.

## "If 1, Then 2" (Lesson 2) — liquidity targeting
Price reaches one liquidity pool, then moves to the next in the opposite direction. Steps: find
daily/weekly bias from key swing highs/lows → mark where **buy stops and sell stops** sit on both
sides of the internal range → plan from where price came, where it is, likely direction, invalidation.
In a downtrend take only shorts (uptrend only longs); use the daily High/Low indicator for bias and
the ATAS liquidity template for untapped swing highs/lows.

## Failed Auctions (Lesson 3) ★
Like an SFP but price **spends more time beyond the level** before reclaiming → traps more traders →
bigger follow-through. Bearish setup: sideways range → break above range high (ideally rising OI +
volume) → **close back into the range** → short with stop above the new high. **TP1** = last low
before the high, or CC fib; **TP2** = range POC / range low / next monthly level. Confirm with
**bullish CVD divergence + OI increase at the lows** (mirror for bearish). Preferred **30-min / 4-h**.
**Market orders** after confirmation (the move is quick; limits miss).

## 3rd Touch setup (Lesson 4) ★  ·  Vault: "Liquidity"
Three-pivot reversal: **Pivot 1** = first important high/low; **Pivot 2** = SFP / failed auction of
pivot 1 (look for trapped traders + high volume); **Pivot 3** = first retest of pivot 1 (look for
CVD divergence on the retest). Stop beyond the highest high / lowest low of the three pivots (pivot 2).
TP = fixed range / range POC / last major high-low / CC / session VWAP. Best at key S/R; wait for an
LTF market-structure change to confirm. If invalidated, look for a Three Drives or another SFP.

## Frontrun (Lesson 5) ★  ·  Vault: "Frontruns"
A key level is **just missed** (front-run). Statistic (bear market): a front-run of **support = 92%**
probability of being hit within 7 days; of **resistance = 81%**. Trade the opposite direction
expecting the level to still fill; confirm with order flow (trapped traders after the front-run
candle). Probabilities are high, not guaranteed — read context per level.

## Order Blocks (Lesson 6) ★  ·  Vault: "Order Blocks"
ICT-style. **Bullish order block = the last down candle before a strong impulse up near a key
support**, confirmed when the next candle trades above its high (best when followed by an impulsive
move with imbalances + single prints). Needs a proper swing low/high — lows too close together raise
stop-hunt risk. **0.786 fib** confluence + daily/weekly levels = higher probability. Fractal (any TF
if all conditions met). Valid window ~**7-14 days** for the immediate range; HTF blocks get more time.
Manage retests with the 0.25 range tool and order flow.

## Volume Specifics (Lesson 8) ★
Read **liquidity environment from volume-candle size**: large candles on a volume chart = **low
liquidity / thin book** (fade candidates — fakeouts, liquidation runs beyond POC / VAH-VAL / monthly /
0.618); many small candles = **high liquidity** (larger traders absorbing).
- **CVD divergences are reliable only in high-liquidity environments** (why CVD signals fail in thin
  conditions). **Trade count:** low trade count + large candle = one big player → follow the big
  participants, not retail. Signals must land at meaningful levels with confluence.

## Day Trading Strategy (Lesson 9) ★
Reaction-based (no pre-set limits), **1-4 trades/day**, patience over forcing. Components: TPO + open
interest + **volume momentum** (how fast volume enters, candle size) + Fibonacci confluence.
- **Fib levels:** **0.66 (CC)** primary, esp. ranging; **0.796** low-volume/ranging; **0.28** high-
  momentum/trending. Entries from value areas, POCs, VWAPs, single prints, high-time nodes.
- OI tells aggressiveness (new longs/shorts vs closures). **Declining OI on an up-move = short
  squeeze**, not strength. Best high-prob confluence: **pullback into VWAP + daily POC** (often weekly
  POC too). Exit discipline: close full position at the CC/0.66 target, no greed; bullish CVD
  divergence warns against re-entry.

## CCTR = Candle Close Trend Reversal (Lesson 10) ★★ maps to indicator A
A 2-candle reversal **scalp** (BTC $100-300 moves). **Signal anatomy:**
- **Bullish CCTR:** the reversal candle's **tail is on the OPEN side** (bottom); **enter with a market
  order at the OPEN of the second trending candle** after the signal; **stop just below the low**
  (very tight).
- **Bearish CCTR:** tail **above** the candle; stop above the wick; entry on the **second candle's
  open** down.
- **Exits:** TP1 = 50% at the **5-min ACR** (~$100-150 BTC); TP2 = **10-min ACR** (~$200-300); always
  collapse at TP2. If an HTF level (daily open, prev-day H/L, EQ) is near, use it as TP (small buffer
  past prev-day H/L — price takes liquidity first). **Move stop to break-even immediately after TP1 —
  non-negotiable.** On a bigger level, let it run as a day trade but keep the stop below the wick.
- **ATAS:** the trend-reversal chart is called a **"range" chart**; produces truer trending candles
  than ExoCharts. **Keep the chart open — OI resets when you zoom/refresh.** OI up + price down = new
  shorts. Very advanced; requires order flow / OI / CVD / delta mastery first. (This is the mechanical
  core of the repo's CCTR template references and a prime indicator candidate.)

## Accumulation / Distribution (Wyckoff) (Lessons 13-15)
**80% of market time is range-bound** — pre-anticipate breakouts from structure + volume + delta + OI.
Four schematics: **Accumulation** (reversal at bottoms), **Distribution** (reversal at tops),
**Reaccumulation** (uptrend continuation), **Redistribution** (downtrend continuation). Fractal, all
markets/TFs; market structure > volume in crypto.
- **Accumulation phases (A-E):** PS (preliminary support) → SC (selling climax, range low, CVD
  divergence on high volume) → AR (automatic rally, range high, OI flat/down) → ST (secondary test,
  no new supply) → **Spring** (low-volume poke below range low then reclaim = the favourite setup;
  stop below the wick; **low-vol spring = accumulation, high-vol break = redistribution**) → Test →
  LPS (last point of support, S/R flip) → SOS (sign of strength, break range high on volume+OI) →
  Backup (final buy, may be skipped) → **Markup**.
- **Distribution** mirrors but is **faster** (HTF accumulation 1-2 yrs vs distribution 3-6 mo). PSY →
  BC (buying climax, often no huge volume spike) → AR → ST → **SOW** (sign of weakness, often a
  **higher low at 0.786/0.886**, not a range-low break) → **UT** (upthrust = SFP of a new high) →
  **UTAD** (upthrust after distribution, final push above range high; **42% of the time it shows
  rising vol+OI = trapped longs** → one of the best shorts when price returns to range) → Test → LPS →
  **MSOW** (major sign of weakness, final range-low loss; **58% crash with no retest**; if VAL
  reclaimed = sign of strength, flip to longs).
- **Reaccumulation / redistribution (Part 3):** full cycle accumulation → markup → reaccumulation →
  distribution → markdown → redistribution. Hold the range bias but **trade both ways inside the
  range** (longs at range low, shorts at range high) until breakout; don't micro-label. Invalidation:
  reaccumulation dies if range low lost with high negative delta + bearish structure; redistribution
  dies if range high reclaimed with high positive delta. Short rallies/lower-highs (tight
  invalidation), never short into support after a sharp drop. **Market structure trumps everything** —
  a weak-volume range-low break that keeps making LL/LH is still not a long.

## Entry Triggers (Lesson 17) ★ clean summary — feeds indicator A
Five entry triggers to fire a trade after an HTF zone is reached: **SFP, failed auction, mini-range
(SFP of an LTF range), retest (CVD divergence + trapped traders), market-structure change (LH/LL or
HL/HH on 5-min — least preferred, wider stop).** Process: at the zone, drop to **1-/5-min**, check
order flow (volume/delta/OI), wait for a structure shift or CVD divergence on the retest. Each trigger
gives both the entry reason and the invalidation. Don't trade a retest/structure-change if OI rises +
high positive delta + new longs on the pullback (= continuation). Front-run a key level + mini-range
fakeout = high-prob. Be okay missing trades.

## Locking Profits vs Flipping (Lesson 18)
Have a **hard TP rule** (personal TP1 = 2-15% by context) — most traders fail by never taking profit
and "round-tripping" back to entry/stop. **Flipping** = open a short at resistance while keeping the
long (fully hedged, relaxed through a pullback); best for day-swing with an entry from the day's low.
Turn scalps into swings by locking a short at the TP level. Trail the long's stop on **market
structure (two higher lows back on a medium TF)**.

## Sequence by Igor = TD Sequential (Lesson 19) ★ indicator candidate
A rule-based **market-timing** framework (times exhaustion, does **not** give direction/signals).
- **Price flip** (core trigger) = current close vs the close **4 bars ago**. Bullish flip: a close
  below 4-bars-ago immediately followed by a close above 4-bars-ago (mirror for bearish).
- **Phase 1 Setup (1-9):** 9 consecutive closes lower (buy setup) / higher (sell setup) than 4 bars
  earlier, after the opposite price flip. Interrupted before 9 = cancelled, restart. **Perfected buy
  setup:** low of bar 8 or 9 ≤ lows of bars 6 and 7 (perfected sell: high of 8/9 ≥ highs of 6/7) —
  waits for the true extreme.
- **Phase 2 Countdown (1-13):** starts after a complete setup; bars need not be consecutive. Buy: each
  bar closes ≤ the low 2 bars earlier; **bar-13 rule:** low of bar 13 ≤ close of bar 8. Cancellation
  (opposite setup / structure break) and recycling (new same-direction setup before countdown ends).
- **Phase 3 Structural validation** = dynamic S/R "line in the sand"; respected → trend valid, broken
  → abandon thesis. **Execution:** a **perfected 9 or 13 at a key area of interest** (VAH/VAL, single
  prints, open/high/low, EQ, halfback) + opposing price-flip trigger + risk line. "The indicator times
  the move; **location defines the trade**." Highly reliable on daily/weekly. (TD Sequential exists on
  most platforms — audit before building.)
