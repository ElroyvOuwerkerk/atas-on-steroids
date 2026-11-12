# Module notes: Indicators and Tools (Technical Mastery)

Personal study notes distilled from the ChartChampions portal (lesson summaries + timestamped
highlights). Captured 2026-11-11. 14 lessons. This module defines the tools behind the playbook
(VWAP, value areas, naked POCs, OI, divergences) and is the main source for indicator logic.

Vault cheat sheets referenced here: "Volume Profile Indicators" (lesson 8), "Case Study Volume
Profile" (lesson 14).

---

## 1. Indicators (overview) — `indicators-introduction`
Minimalist philosophy: the goal is reading price action without indicators; tools are confluence
aids only. Most indicators lag.
- MACD crossovers: bad for entries (the move is mostly done by the cross). Fine for trend
  confirmation (e.g. death cross), not timing.
- 200 daily MA: can act as a leading level (price anticipates rejection at it).
- ATR (14): place stops outside the ATR range; use it to size targets (a $100 ATR informs a $100
  target).
- Ultimate RSI Multi-Timeframe (CM_Ultimate_RSI_Multi_Time_Frame, free): show two RSIs (e.g. 1H +
  Daily) at once.
- ZigZag: swing highs/lows, good for anchoring fibs.
- Stochastic RSI: K/D crosses inside RSI; faster than RSI, better for scalping with tuned settings.
- Fibonacci Pivot Points: standard pivot + R1/S1; some wait for R1 break + retest to go long to R2
  (mirror for S1). Confluence only, never in isolation.
- Volume Profile: HVN/LVN + POC; high-volume node = stronger S/R, low/zero = weak. Needs TV Pro.

## 2. Divergences — `divergences-tutorial`
Confluence tool, never a standalone signal. Always from candle closes, not wicks.
- Regular bullish = price lower low, oscillator higher low → reversal (only after extended
  downtrend into support).
- Regular bearish = price higher high, oscillator lower high → reversal (at HTF resistance after
  uptrend).
- Hidden bullish = price higher low, oscillator lower low → continuation (buy the dip in uptrend).
- Hidden bearish = price lower high, oscillator higher high → continuation (short rally in
  downtrend), best at SR flip + monthly resistance.
- Classes: A = strongest (standard HH + lower high on oscillator), the only class worth trading.
  B = double top, weaker. C = new high, oscillator flat, weakest — avoid.

## 3. Open Interest and Funding — `open-interest-funding`
OI = health of macro trend. The four-quadrant rule (core, mechanical):
- Rising OI + rising price = new longs opening (bullish).
- Falling OI + rising price = short covering / closing (bearish, weak demand).
- Rising OI + falling price = new shorts opening (bearish).
- Falling OI + falling price = long liquidation / exhaustion (potential bullish reversal).
Healthy uptrend = price up + volume up + OI up. New high on falling volume + falling OI =
exhaustion.
Funding (perps): positive = longs pay shorts; negative = shorts pay longs. Reference levels:
0.01% normal, 0.02%+ slightly skewed, 0.04%+ moderate pressure, 0.07%+ exceeds fees, 0.075%+
extreme (often precedes volatility). Falling price + positive funding = crowded longs. Never trade
funding alone.

## 4. VWAP — `VWAP-tutorial`
Dynamic S/R combining price + volume. Below VWAP expect resistance, above expect support.
- Timeframes: session (scalps), daily/weekly/monthly/quarterly/yearly, and anchored. Weekly VWAP
  is actively interacted and should always be monitored; yearly reacts strongly but rarely.
- Strong confluence when VWAP aligns with POC, fib, or weekly VAH. On time charts works well on
  3/5/15m. Never trade VWAP alone — require at least two confluences + context.
- Daily VWAP = fair value for swing retrace entries while structure (HH/HL) holds.

## 5. Anchored VWAP — `anchored-vwap`
Manual anchor from a chosen pivot (session VWAP resets automatically; anchored does not).
- Anchor at: major highs/lows, start of a trend, range-breakout candle. Wait for a reaction.
- Minor wicks through it do not invalidate. Losing the anchored VWAP of an uptrend and flipping it
  to resistance = early weakness.
- Stack multiple AVWAPs (trend start, range start, last range high); alignment = stronger zone.
  Combine with a fractal. Prioritize the AVWAP with most confluence. Works any timeframe.

## 6-10. Volume series — `volume-part-1..5`
### Part 1 (volume basics)
Volume = contracts in a timeframe. Prefer tight spread + high volume (wide spread = low liquidity,
execution risk). Volume MA period 30, histogram style.
- Rising price + rising volume = healthy up; rising price + falling volume + sell spikes = weak.
- Breakout needs a volume increase, else likely a fakeout back into range. Bull flag = breakout on
  volume, flag on decreasing volume, continuation on rising volume.
- Screen assets by 24h volume (suggested 200+ BTC), combine with directional movement. Avoid big
  moves on low volume (pump-and-dump).

### Part 2 — OBV (`volume-part2-obv`)
Volume leads price; OBV can break S/R before price. Absolute number meaningless, trade patterns +
divergences (same method as RSI, often more informative). Trendlines/patterns on OBV can precede
price. Confluence only.

### Part 3 — VPVR / Visible Range (`volume-part-3`)
VAH, VAL, POC. **Short the value area high, long the value area low, never enter new positions at
the POC.** POC = most volume transacted = magnet (traps mid-range entrants). SR flip on break of
VAH/VAL. Take profit at POC within a range even if it leaves some on the table. Weekly POC
important.

### Part 4 — Fixed Range (`volume-part4`)
Untested range POC is a very strong level. Price often enters the range and rejects from the POC,
not from the wick. Rules repeat: short VAH, long VAL, never at POC; SR flips on break. Selecting
boundaries: start at the first wick of the range, end at the candle just before the break, to
capture the consolidation. Concentrated volume at one level > volume spread across many.

### Part 5 — Session Volume + Naked POCs (`volume-part5`)
Pre-session checklist: write down previous day VAL, POC, VAH (the three critical intraday levels);
also previous week VAL/POC/VAH.
- Naked POC = a prior-session POC not yet retested. First touch usually bounces (~95% of traders
  get the direction wrong after a POC break; underwater traders want out at break-even + new
  traders enter → "double strength"). **First touch only** — not 2nd/3rd.
- Favorite setup: naked POC + CC confluence on weekly/monthly (roughly once a week intraday).
- **80% rule:** if price trades above a value area then accepts back into the VAH, ~80% probability
  of trading back down to the VAL.
- ~90% of entries are taken after waiting for a reaction (alert at level, wait for rejection),
  not pre-set bids. ExoCharts auto-marks developing/previous day POC/VAH/VAL.

## 11. Log Scale Charts — `log-scale-charts`
Use log scale for: very large % moves (100%+), old multi-year data, HTF perspective. Channels and
fibs can only be valid on log for such assets. TradingView: enable "Fib levels based on log scale"
on the fib tool, or levels plot wrong. Check both scales for confluence.

## 12. Fractals — `fractals`
"History rhymes." Draw with TradingView Bars Pattern tool, HL Bars setting (no mirror/flip), from
the origin to the end of the prior move. Confluence only; 4H+ more reliable. Time alignment: a 4H
fractal compares to 4H action. Forecast/roadmap, not a standalone entry; take entries from lower
TF. Invalidated when price breaks the prior high the fractal projected against.
(Note: this is the classic-TA fractal/Bars-Pattern, distinct from the Williams Fractals entry tool
in `building-plans-and-context.md`.)

## 13. TradingView Know How — `tradingview-know-how`
Platform tutorial (Igor): saving/loading shared charts, indicator templates, layouts, DOM/trade
panel, tick size, desktop app. The lesson lists ~24 shared TradingView chart layout links
(public tradingview.com/chart/... URLs) — CC's template charts, worth saving as references.

## 14. Volume Profile Shapes — `volume-profile-shapes`
Auction Market Theory: market auctions to find a two-sided trade; balance/imbalance/value
migration. Profile shapes = footprints of the auction (inventory story). Terms: POC, VAH, VAL,
HVN, LVN, excess.
- **D-shape** = balanced day. **P-shape** = short covering. **b-shape** = long liquidation.
  **Trend (elongated)** = trend day. **Double distribution** = two distributions with single
  prints between (needs TPO).
- Ledges, LVN retests, poor highs/lows each have implications/playbook.
- Open types: Open Auction in Range (OAR), Open Auction Out of Range (AOR), Open Drive (OD),
  Test Drive (TD).
- Multi-day composites (5-20 day) for HTF structure; track POC + value-area migration for bias.
- Confluence with VWAP/TPO/options; best trades layer confluence (e.g. LVN + VWAP pullback in a
  trend). Risk: location over signal, risk behind structure, scale at HVN/POC/value edges, avoid
  midpoint chop.
