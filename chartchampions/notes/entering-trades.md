# Module notes: Entering Trades (Foundation)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 9 lessons.
This is the execution module: the exact entry/management mechanics (SFP, consolidation, TP%,
stop-loss movement, compounding/laddering, HTF/LTF). It directly enriches `playbook.md` sections
4-6. Taught mostly by coach Daniel; crypto examples, but the rules are instrument-agnostic.

Vault cheat sheets referenced here (member downloads): **"Trading Setup"** (lessons 6-8) and
**"HTF vs. LTF"** (lesson 9).

---

## 1. Consolidation — `consolidation`
A period of the market cooling off: drawn-out sideways movement after a strong move, generally
followed by another strong move.
- **Directional read:** consolidation **above support = bearish**; **below resistance = bullish**;
  inside a larger sideways range = neutral.
- **When it breaks:** use **Fibonacci time** (ends often align after Elliott impulse waves 1/3/5)
  and **order-flow CVD divergence** during the consolidation to judge direction (where limits are
  absorbing).
- **Trade it:** longs at range low, shorts at range high (failed auction / SFP on the outer
  boundaries). Breakout trade = retest of the old range high with **decreasing volume + bullish
  divergence**. Can enter in anticipation during the consolidation rather than waiting for the break.
- **Caution:** breakouts on low volume / poor delta are likely a failed auction or SFP. Great R:R:
  shorts off consolidation-below-resistance, longs off consolidation-above-support.

## 2. Confluence — `confluence`
Case study (BTC bottom) stacking many tools; the lesson is *why* confluence raises probability.
- **CC (0.618) zone** repeatedly reacts (high→low and low→high retracements land there). A real
  entry example stacked 0.618 + weekly structure + SFP.
- **Support that is retested without flipping to resistance often remains support.** POC (highest-
  volume node) acts as precise support because large players execute at historical value regions.
- Tool confluence shown: Wyckoff accumulation schematic, Fib speed fan (reject at .75/.618/.5/.382/
  .25), Gann fan, pitchfork + 1.618 extension, Three Drives (1.272-1.618 ext = reversal zone), Fib
  time zones (2.0 / 2.618 / 3.0 timed major pivots), Fib channel on **log scale**.
- **Principle:** don't try to time the exact top; wait for confirmation (short the next rejection).
  Sacrificing a little profit buys much higher trade security.

## 3. Compounding (advanced trade management) — `compounding`
Three advanced techniques (add complexity + risk; paper-trade first):
- **Compounding** = add to a winner only when the new setup would qualify as a **standalone trade**
  (SR flip, S/R break). Move the original stop up to the new entry's stop → locks profit on the
  original, maximises gains. Risk: a stop-out turns the added position into a loss.
- **Averaging down** = add to a loser only at a **stronger** support (e.g. 0.382 fib). Strict rules:
  **max 1-2 averages then enforce the stop**; aim to exit at break-even on the bounce, review, re-
  enter fresh; consider sizing the averaged buy ~2x so a smaller bounce reaches break-even. Beginners
  should just take the stop.
- **Laddering** = spread entries across a range (fib 0.5 / 0.618 / 0.65 / 0.786) for a better average
  price; weight sizing by conviction; the **0.5 of an order block holds the heaviest size** (beyond
  it = order-block red flag). Cancel lower ladder orders after a confirmed SR flip + market-structure
  change (HH/HL), then market-buy the rest. Unfilled lower orders = your bias was right.
- **Not a compounding opportunity** if the required stop gives poor R:R (e.g. 1:1), even if already
  in the trade.

## 4. Swing Failure Pattern + Liquidity — `SFP-liquidity`
- **Why SFPs exist:** large traders can't fill size in thin markets; stops cluster predictably below
  significant lows / above significant highs, so those zones become **liquidity pools** they harvest.
- **Definition:** breach a key high/low, trigger stops, grab liquidity, **close back on the other
  side** (a break alone is NOT an SFP — wait for the close). Uptrend SFP dips below a key low and
  closes back above; downtrend SFP pokes a key high and closes back below. Aka stop run / liquidity
  grab.
- **Execution:** enter on the reclaim candle close; stop just beyond the SFP wick (tight). Take
  partials early (local S/R, session VWAP, value area of the range); let runners go if structure
  shifts.
- **Rules:** works on every timeframe (HTF = bigger rotation; daily >> 5-min). **Obvious levels are
  best** (more stops). High volume at the breach strengthens; thin volume = caution. Trade *with* the
  big players using those levels, not against them.

## 5. Swing Failure Pattern + Orderflow — `sfp-orderflow`
Order flow is confluence on top of the SFP, not a requirement. Three steps:
1. **Trap traders** on the footprint: activity picks up in the liquidity zone, longs get stopped,
   **new shorts open = negative delta at the low + rising open interest**. Perfect trigger = OI
   increase + correct delta + candle closes back across the swept level. (Flip for SFPs at highs.)
2. **Demand/supply responds:** buying imbalances where aggressive buying overpowers selling; OI up +
   positive delta = new longs. (Selling imbalances + new shorts for highs.)
3. **Market-structure change:** after the SFP, price makes HL/HH on the LTF → hold / compound with
   confidence.
- Steps 1-2 = order-flow confirmation, step 3 = price-action confirmation. Always ask "can I see
  trapped participants at the level?" Take TP1 early, then adjust by structure.

## 6. Trade Entry (order types) — `trade-entry-strategy`  ·  Vault: "Trading Setup"
- **Limit** = preset at strong confluence; cheaper, guarantees price but not fill; good for swing
  traders who can't watch. **Market** = real-time; more expensive (~0.1% fee), guarantees fill,
  slippage rare; scalpers' default. **Alerts** = mark an area, then market-enter only if the reaction
  at the level is good (avoids instantly-losing entries).
- Large orders: mix limit + market. Fees are a cost of doing business — over-optimising fees causes
  missed trades.

## 7. How much TP % to take — `how-much-percentage-profit-to-take`  ·  Vault: "Trading Setup"
- Plan TP locations **and percentages ahead of time**, with entry and stop. Set hard **TP1, TP2,
  then a trailing stop**.
- **TP1 = protection to minimise loss** if the stop later hits; take it at an intraday level (VWAP or
  a mini-range POC). **TP2** = a swing-trader high-probability level (overall range high / HTF strong
  confluence).
- There is no right/wrong % (90% at TP1 is fine if that's the plan; context decides — sometimes 50%,
  sometimes 5%). **Biggest mistake = not taking enough profit.** "Profit is profit" — never regret a
  TP because price continued.

## 8. Moving Stop Loss — `moving-stop-loss`  ·  Vault: "Trading Setup"
- **Only move the stop when the actual invalidation level changes** — not to break-even as soon as
  possible (that gets you stopped and misses the move).
- Take a small TP (5-10%) at the VWAP to reduce risk, but do **not** move the stop to entry there.
  Move it once price reclaims the range POC (invalidation genuinely shifted).
- Mental check: view current price action as a fresh setup — where would you put the stop now? Stop
  chasing "risk-free" trades; keep stops at invalidation.

## 9. HTF vs LTF — `htf-vs-ltf`  ·  Vault: "HTF vs. LTF"
- **LTF = below the 4H chart; HTF = 4H and above; MTF = an LTF view lasting ≤ 1 week.**
- **Trade HTF setups with LTF entry triggers** → more precise entries, smaller invalidation, higher
  probability. Best longs: HTF bullish while LTF is bearish (and mirror for shorts).
- The main addition on the LTF is **order flow** (necessary for trading BTC on LTF).
- Daniel's timeframes: **30-min = go-to intraday** (correlates with the TPO chart); 15-min earlier in
  the day with less data; **3-min for entry triggers** when action is live.
