# Module notes: Futures and Stocks (Market Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 18 lessons
(Master level). This is the **ES/futures** module and the most directly relevant to Elroy's ES/NQ
(MES/MNQ) trading: contract specs, the NY-session market-profile playbook, Fair Value Gaps, and
trend identification. Taught by several coaches.

**Scope note (cost/value call):** the strategy and ES-mechanics lessons are captured in full below.
Five lessons were intentionally *not* transcribed because they are platform-setup mechanics or
stock-market content with low relevance to ES/NQ futures scalping — slugs are listed at the bottom so
they can be watched directly while the subscription is live.

Vault cheat sheet referenced: **"How to Spot a Trend"** (lesson 16).

---

## ES contract specs (Lessons 1, 7) ★ reference
- **1 ES contract = $50 × the S&P 500 index.** At 4000 → $200,000 notional. The "E-mini" is 1/5 of
  the full S&P contract; **MES micro = 1/10 of the ES** (repo context: Elroy trades the micros).
- **1 tick = $12.50; 4 ticks = 1 point (handle) = $50.** Tight spreads (deep liquidity; one data
  source = CME, so no exchange discrepancies like crypto).
- **Round-trip fee ≈ $4-4.40** per ES contract (range $2-6 by broker).
- **Quarterly contracts / rollover (first letter):** H = March, M = June, U = September, Z = December.
- Scalping maths: 1 point = $50 (−$4 fee = $46 net); **8-12 ticks is substantial; ~20 ticks can pay
  for the day.** Individual traders can't move a market trading in billions.
- Exit strategy = brackets (stop + take-profit), ATAS auto-exit; keep position sizing consistent;
  always verify account + contracts + strategy before entering.

## Value & Auction Market Theory (Lesson 8) ★
- **AMT (J. Peter Steidlmayer):** markets drive directionally on buyer/seller imbalance until price
  stabilises where the most trade is facilitated = **Fair Value**. Three phases: price, time, volume.
- **Value Area = 70% of the day's total volume / TPOs.** (Note: the CC *ATAS* cluster template uses
  68% — see `atas.md`; the theory figure is 70%.)
- **Single print** = one TPO printed during a 30-min auction = price acceleration between levels
  (imbalance). Price tends to **revisit** it to confirm Fair Value. Crypto = 48 auctions/day, 24h, no
  dominant market; traditional markets concentrate volume in the **NY session** (volume ~4x).

## Fair Value Gaps (Lesson 9) ★ indicator-relevant
- **FVG** = a three-candle imbalance; like a single print, price gravitates back to retest it. **Not
  the same as a single print** (don't confuse them).
- **Set rules → fixed R:R:** stop goes at the **origination candle** of the three-candle formation.
  Best on **5-min and 15-min** timeframes.
- Not every gap is equal — gaps are also **price magnets**, not just entries. In a trending one-time-
  framing market, FVGs are better **TP targets** than entries. Identify a swing high/low **first**,
  then look for the FVG forming (gives a proper invalidation). Use PDPA/PDVA (previous-day price/value
  areas) to judge balance; confirm FVG "push" with footprint.

## Profile Trading strategy (Lessons 13-15) ★
The NY-session market-profile playbook (origins: 1860s Chicago pits; TPO = how floor traders tracked).

**Three pillars:** *price advertises* opportunities, *time regulates* them, *volume measures* their
success/failure. Never trade volume profile in isolation — combine volume + time.

- **Three areas of interest:** VAH, VAL, **POC** (volume POC = highest-volume price; time/TPO POC =
  most time at a price — they can differ, both use the 70% value area).
- **DOM** = limit orders only; **Tape (Time & Sales)** = executed market orders filling limits;
  **order flow** = analysis of *completed* executed price action (who controls the move; a lagging but
  near-live read). Institutions provide the liquidity that moves markets.
- **Session letters (EST):** A = 9:30-10:00, B = 10:00-10:30, C = 10:30-11:00 … through M. **Initial
  Balance (IB) = A + B sessions** (first hour); their high/low = **IBH / IBL** (key references, NY
  only). NY opens 9:30, closes 16:00 (futures trade to 16:59), **freeze 17:00-17:59**, reopens 18:00;
  gaps rarely exceed ~100 points.
- **Day-type read from where later sessions open vs IB:** sessions C-G opening **above IBH** = bulls
  in control (one-time framing up); opening **within** IB = inside/symmetrical day (trade IBH↔IBL);
  opening **below IBL** = bears in control. **Wait for the IB (first hour) to form before building
  the plan** — a bearish A session doesn't guarantee a bearish day.
- **Break of balance needs imbalance:** an initial imbalance that fails (no follow-through volume)
  rotates back into balance, toward the opposite value edge. "True imbalance comes with true volume" —
  only NY truly *tests* levels; London/Asia touches don't count. Failed data-release move (volume but
  no imbalance) = reversal signal; look for liquidity the other way.
- **Profile shapes (Lesson 15):** **D / symmetrical** (POC ~50% of range → inside/ping-pong day);
  **elongated P / E / b** (trending, volume clusters stair-step); **squat** (tight range, shelf-like
  edges → a counter-trend rally ending, usually after a big trend day → direction change). **Split
  sessions** to see one-time framing / elongated; **non-split** to see symmetrical/squat shapes.
- **Single prints = efficient, forceful moves** (not "inefficiencies to fill"); use for back-testing
  demand/supply later. **Poor high/low = double TPO** = insufficient pressure to break → market hunts
  liquidity; poor high + bearish NY = target single prints below. In balance, trade VAH↔VAL (where
  most money is made).

## How to Spot a Trend (Lesson 16) ★  ·  Vault: "How to Spot a Trend"
"The trend is your friend" — biggest wins come with the trend, biggest losses against it.
- **Trends begin/end on:** data releases, fundamentals, environmental/systemic risk, breaking an
  accumulation/distribution range, exhaustion (no new highs/lows), and (to end) a **huge volume
  drop-off after multiple large legs**.
- **Trend tools:** trendlines; HH/HL vs LH/LL (market structure); **VWAP**; **prior-bar highs/lows =
  one-time framing** (conviction to continue); moving averages — **LTF 8/24 SMA** (spot reversals),
  **local structure 50 SMA**, **HTF daily EMA 100/200** (clear S/R flip). Don't discredit lagging
  indicators in traditional markets; let trend-indicator combos set the daily bias.

## ES Series logistics (Lessons 6-7)
- **Lesson 6 (prop firms):** access capital via evaluation → funded. **A-book** (trades hit the real
  liquidity pool, firm copy-trades you) vs **B-book** (in-house SIM, your size never shows on the
  footprint). Futures prop firms: **Apex, Topstep, TradeDay**. Drawdown types: static / end-of-day,
  **trailing** (follows highest balance incl. unrealised; on Apex stops $100 in front of start once
  funded). **Apex 30% rule** (no single day > 30% of a withdrawal). Data: **Rithmic → ATAS/
  NinjaTrader/Quantower (not TradingView); CQG → TradingView via Tradovate**; AMP + CQG for live. Your
  real risk is the drawdown, not the account size. "$400/day = $100k/year — consistency over size."
- **Lesson 7 (ATAS/Apex trade management):** ATAS preferred (tick refresh, execution). ⚠️ **Shadow-
  entries bug** on copied/multi-account portfolios: the **Positions Tab is never wrong** — verify flat
  there and scan each account for leftover limits; closing "ghost positions" opens *new* trades.
- **Lesson 2 (Templates & Live Strategies):** live ATAS trade walkthrough — find a key level → evaluate
  R:R → place (DOM limits) → manage (TPs hit, stop to entry). Use templates to find key levels and
  build stats on the most common ones (see Statistics lesson).

## Not transcribed (low relevance to ES/NQ scalping — watch directly if wanted)
- `es-series-exocharts-setup` (3) — ExoCharts platform setup.
- `contenders-session-es-series-tradingview-trade-execution` (4) — TradingView execution setup.
- `es-series-atas-transition` (5) — moving from ExoCharts to ATAS.
- `long-term-stock-market-strategy-part-1` / `-part-2` / `-part-3` (10-12) — long-term stock
  investing, not futures scalping.
- `ai-for-stock-analysis` (17) — using AI for stock research.
- `making-the-move-to-stocks` (18) — transitioning into equities.
