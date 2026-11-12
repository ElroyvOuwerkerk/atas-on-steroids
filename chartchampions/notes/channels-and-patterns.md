# Module notes: Channels and Patterns (Technical Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 13 lessons
(Adept level). Classic chart patterns with CC's Fibonacci specifics. The strategy-relevant ones
(parallel channels, supply/demand, harmonics, three drives, flags, fib channels) are captured in
full; the textbook single-pattern lessons (wedges, triangles, H&S, bump-and-run, channels 2-3) are
summarised briefly with their slugs — standard TA covered in any course, CC adds only fib levels +
volume rules.

Vault cheat sheets referenced: **"Three Drives Pattern"** (12); Vault also has Wedges / Triangles /
Trendlines sheets.

---

## 1-3. Parallel Channels — `Channels-part1` / `channels-part2` / `Channels-part3` ★
Channels remove directional bias (trade the chart, not a view).
- **Confirmed channel = 4 touches** (2 top, 2 bottom); can start trading on **3 points** once the 3rd
  touch reacts. Draw boundaries from **candle closes** (not wicks) when closes gave the rejections.
- **Deviations:** price rarely hits the exact extreme — a **partial rise** (fails to reach the top) =
  weakness; a **partial decline** (fails to reach the bottom) = strength (early break clue).
- **Volume filter:** volume contracts inside the channel; a **low-volume break that reclaims =
  deviation/fakeout** (trade back to the other side); a **high-volume close outside = valid breakout**
  (enter the retest of the flipped level, stop back inside).
- **Management:** day-trade = enter the extreme, **TP 50% at the midpoint**, stop to BE there, compound
  ~30% of booked profit on a midpoint reclaim with a higher low. Swing/long channels: the **midpoint
  becomes a full exit**, trail aggressively. **Measured move = 1:1 of the channel height** from the
  break (cross-check horizontals/fib). R:R inside a confirmed range can be acceptable at ~1.4-1.5:1.
  Up/down-sloping channels = bull/bear flags (continuation favoured).

## 4. Fibonacci Channels — `Fibonacci-channels` (Daniel)
Three types: **standard** (parallel channel with fib levels as S/R), **extension** (targets after a
channel breakout), **long-term** (judge when a macro trend is over / reversal is due). Higher
timeframes, always with other confluence. (The 0.618/0.66 "CC zone" and log scale carry over from
`fibonacci.md`.)

## 5. Flags + Fibonacci — `bull-flags-bear-flags` ★
Bull flag = impulse up → low-volume retrace down → continue; bear flag mirrors.
- **Volume contracts during the flag**; the flag should last **~25-30% of the pole's duration** (a
  much longer consolidation is a different pattern).
- **Fib retrace of the pole:** **majors (BTC) → stay above/retrace to 0.382**; **low-cap coins that
  already pumped 100%+ → wait for 0.618-0.786** (don't take an aggressive 0.382 on a pumped coin).
- **Three entries:** aggressive (at 0.382 inside the flag), conservative (close outside the flag),
  retest (breakout then pullback). Stop back inside the flag. **Measured move = 1:1 of the pole** from
  the break. Accept ~4% losers; winners ~3x losers.

## 10-11. Harmonic Patterns — `harmonics-part1` / `Harmonics-part2` ★ exact ratios
Fibonacci + correct visual **symmetry** (time symmetry too — ties to fib time). **Never trade on fib
alone**; enter at **D** (or anticipate in the C wave with strong confluence); **1H minimum, 4H+
preferred**. Stops just beyond the pattern invalidation.
- **AB=CD:** C between 0.618-0.786 of AB; C at 0.618 → D at 1.618 ext; C at 0.786 → D at 1.272 ext.
- **Bat:** B = 0.382-0.5 of XA (perfect = 0.5); C = 0.382-0.886 of AB; **D = 0.886 of XA** + 1.618-
  2.618 of BC. TP1 = 0.618 of CD, TP2 = high of A.
- **Gartley:** B = 0.618 of XA; C = 0.382-0.886 of AB (ideal 0.618); **D = 0.786 of XA** + 1.272-1.618
  of BC. TP1 = 0.618 of CD, alt = 1:1 of XA from D.
- **Butterfly:** B = **0.786 of XA** (deeper); D = **1.272-1.618 ext of XA** + 1.618-2.618 of BC. Has
  a volume signature (bullish: XA up on falling volume, AB down on rising bear volume, BC up on low
  volume, CD bear volume should fade toward D). TP1 = B, TP2 = 0.618 of AD.
- **Crab:** B = 0.382-0.618 of XA; **D = 2.24-3.618 ext of BC** + 1.618 ext of XA/AB. TP1 = 0.618 of
  CD, TP2 = high of A.
- **Part 2 adds Cipher, Shark, and 5-0** (5-0 extends the shark); same fib-extension + confluence
  approach (SFP + key fib levels), BTC examples.

## 12. Three Drives — `three-drives-tutorial` ★  ·  Vault: "Three Drives Pattern"
**Three attempts at a level before reversal, each drive extending to the 1.272-1.618 fib.** Related
**valleys & peaks** = three attempts to break a high/low, each on **lower volume with divergences**.
Context + confluence over blindly following the shape. (Referenced in the playbook/confluence notes.)

## 13. Supply and Demand — `supply-and-demand` ★ feeds the S/D tracker indicator
- **Supply zone** = cluster of price action then a sharp decline → resistance; **demand zone** =
  cluster then a sharp rise → support. Valid zone needs **≥3 tests**. Sell at supply, buy at demand,
  target the opposite zone.
- **Trend confirmation:** uptrend = demand holds + supply breaks; downtrend = supply holds + demand
  breaks. Ties to accumulation (demand) / distribution (supply) — institutions can't fill size at once.
- **S/D flips:** supply→demand (former resistance becomes support) / demand→supply. **Valid flip:**
  prior zone had ≥3 tests, price breaks and retests the opposite role on **high volume** with closes
  beyond + value built on the other side. Backtest across TFs (D/4H/1H/30m/5m/1m).

## 6-9. Standard single patterns (summarised — textbook TA) 
- `wedge-patterns` (6) — rising/falling wedges; converging trendlines, reversal bias against the
  slope, break + measured move. Vault: "Wedges".
- `triangle-patterns` (7) — ascending / descending / symmetrical; once a triangle line breaks price
  should **not** re-enter (flip the line to S/R); low-volume break = suspect. Vault: "Triangles".
- `head-and-shoulders-pattern` (8) — reversal; neckline break, measured move = head-to-neckline
  projected from the break; right-shoulder volume lower.
- `bump-run-pattern` (9) — lead-in trendline → "bump" (overextension) → break back below the trendline
  (run); referenced in the `confluence` case study (BTC measured target).
