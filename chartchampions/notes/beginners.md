# Module notes: Beginners (Foundation)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 6 lessons
(Apprentice level). The onboarding module — risk management, platform basics, position sizing,
expectations. Almost entirely covered by `basic.md` and the playbook; kept short. These lessons are
mostly video with brief page text.

Vault cheat sheet referenced: **"Position Size and Leverage"** (4).

---

## 1. Risk Management Introduction — `trading-risk-management` ★
The core risk rules (same as `basic.md` §1):
- Define **entry, invalidation, stop** before entering; never random entries. **Risk ≤ 1% per
  trade**; scale up only with experience, **never past 5%** (already aggressive).
- **Position sizing:** size so the stop distance equals the 1% cap. Example: $10,000 account, 1% =
  $100 risk, 2% stop → **$5,000 position** (2% of $5,000 = $100).
- Four tips: **always a hard stop** (in the order book, not mental), **take TP as planned** (no
  greed), **plan the trade / trade the plan**, **journal every trade** (winners and losers both teach).

## 2. TradingView Introduction — `Learn-to-use-tradingview`
Basic TradingView tutorial (interface, drawing tools, layouts). Platform how-to, no strategy content.

## 3. Candlesticks — `candlestick-charts-tradingview`
Reading Japanese candlesticks (OHLC, body vs wick). Foundational; the strategy-relevant candle reads
(extreme-quartile closes, no-wick = unfinished business, COT/COC) live in the Severin core notes and
`fibonacci.md`/`tpo-series.md`.

## 4. Trading Position Size — `trading-position-size`  ·  Vault: "Position Size and Leverage"
Step-by-step position-size formula (90%+ of traders lose, largely from no risk plan). Same formula as
lesson 1 / `basic.md`: **risk $ ÷ stop-loss decimal = position size**. "Get position size and risk
right and you're halfway there." The Vault sheet "Position Size and Leverage" is the reference card.

## 5. Trading With Leverage — `how-to-trade-with-leverage-margin`
Leverage/margin mechanics (mostly crypto exchanges — Bybit/Bitmex). More leverage = more risk per
tick; the real risk is the drawdown, not the notional (see `futures-and-stocks.md` on prop-firm
drawdown). Not directly relevant to ES/NQ micro futures sizing.

## 6. The Journey Ahead + Expectation Management — `trading-expectations`
Mindset: the best traders **play the long game** — stay consistent, keep expectations realistic early,
commit to the process. No mechanical content. (Trading Psychology module goes deeper.)
