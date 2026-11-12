# ChartChampions study notes

Personal study notes on Severin's trading method, distilled from the ChartChampions member
portal (`portal.chartchampions.com`) while the subscription is active. Each portal lesson ships
with a written summary and timestamped highlights next to the video; these notes condense that
material into the concrete rules, so the method can be reviewed later and turned into ATAS /
TradingView indicators.

Scope: this is a private knowledge base for one member's own study and for building indicators.
It is not a copy of the course videos and is not for redistribution.

## Layout
- `playbook.md` — the synthesized, actionable daily decision flow (the thing to actually run).
- `notes/` — detailed per-module notes (one file per course module).
- `indicator-backlog.md` — concepts mapped to candidate ATAS / TradingView indicators.

## Capture progress (18 course modules)

Priority is Severin's strategy and anything that maps to an indicator.

| # | Module | Track | Priority | Status |
|---|---|---|---|---|
| 1 | Building Plans & Context (Severin scalping) | Market Mastery | ★★★ | done (`notes/building-plans-and-context.md`) |
| 2 | Indicators and Tools (VWAP, aVWAP, VPVR, OBV, OI) | Technical Mastery | ★★★ | done (`notes/indicators-and-tools.md`) |
| 3 | Orderflow and Footprint | Platform Mastery | ★★★ | done (`notes/orderflow-and-footprint.md`) |
| 4 | ATAS (CC templates) | Platform Mastery | ★★★ | done (`notes/atas.md`) |
| 5 | Entering Trades | Foundation | ★★☆ | done (`notes/entering-trades.md`) |
| 6 | Fibonacci (CC zone 0.618-0.66) | Technical Mastery | ★★☆ | done (`notes/fibonacci.md`) |
| 7 | TPO Series (market profile) | Market Mastery | ★★☆ | done (`notes/tpo-series.md`) |
| 8 | Futures and Stocks (market profile theory) | Market Mastery | ★★☆ | done (`notes/futures-and-stocks.md`) |
| 9 | Advanced Strategies | Market Mastery | ★★☆ | done (`notes/advanced-strategies.md`) |
| 10 | Statistics and Journaling (static levels, playbook) | Market Mastery | ★★☆ | done (`notes/statistics-and-journaling.md`) |
| 11 | Basic (market structure, S/R, routine) | Foundation | ★☆☆ | done (`notes/basic.md`) |
| 12 | Channels and Patterns | Technical Mastery | ★☆☆ | done (`notes/channels-and-patterns.md`) |
| 13 | Beginner Strategies (EMAs, pivots, CCW) | Technical Mastery | ★☆☆ | done (`notes/beginner-strategies.md`) |
| 14 | Intermediate Strategies (fixed range, liquidations) | Technical Mastery | ★☆☆ | done (`notes/intermediate-strategies.md`) |
| 15 | Beginners (risk management, tools) | Foundation | ★☆☆ | done (`notes/beginners.md`) |
| 16 | Trading Psychology | Foundation | ★☆☆ | done (`notes/trading-psychology.md`) |
| 17 | Quantower (Igor's platform) | Platform Mastery | ☆☆☆ | done (`notes/quantower.md`) |
| 18 | Elliott Waves | Market Mastery | ★☆☆ | done (`notes/elliott-waves.md`) |

**All 18 modules harvested.** Vault enumerated in `vault-manifest.md`; the unique files (ATAS
templates + ES/NQ/BTC daily sheets + a few cheat sheets) are pulled to
`~/Downloads/ChartChampions-Vault/`. The 71 cheat-sheet PDFs are condensed versions of the lesson
material already in `notes/`, so they are listed in the manifest (one-click grab) rather than bulk-
downloaded.

Remaining 8 modules (★/☆ priority, not yet harvested): Basic, Channels and Patterns, Beginner
Strategies, Intermediate Strategies, Beginners (risk mgmt partly captured in the playbook), Trading
Psychology, Elliott Waves, Quantower (skip unless asked).

Other portal sections worth harvesting: **Speed Run** (71 short recap videos), **Glossary**
(definitions), **Vault** (downloadable member cheat sheets / templates), Video Library (daily
updates — market commentary, low playbook value).

## Vault (not pulled — awaiting a scope/permission decision)
The Vault holds ~90 downloadable files in three sections. Downloads need Elroy's explicit go-ahead
and land in `~/Downloads`; the templates are the genuinely unique items (the actual workspace files),
while the cheat-sheet PDFs are condensed visual versions of the lesson material these notes already
cover. High-value first:
- **Templates (19)** — Daniel's Daily Volume Profile ATAS Template, **Daniel's ATAS workspace +
  snapshots**, **Severin's ATAS snapshots + workspace**, Fibonacci Settings, Igor Quantower
  Templates + Binds, Igor TradingView Layouts + Indicators, Scott's TradingView Layout. These are the
  actual `.aws` / template files behind the ATAS/TPO modules.
- **Daily Update Sheets (3)** — daily ES, NQ, BTC level sheets (Severin's current levels; perishable,
  updated daily).
- **Cheat Sheets (71)** — high-res PDF summaries. Ones explicitly referenced by lessons so far:
  Trade Entry Checklist, Trading Setup, HTF vs. LTF, Volume Profile Indicators, Case Study Volume
  Profile, VWAP & Anchored VWAP, TPO, Igor Time, How to Spot a Trend, If One then Two, Liquidity,
  Frontruns, Order Blocks, Entry Triggers, How to Record Statistics, Correlations in Trading, plus
  Wedges / Triangles / Trendlines / Types of Traders and others.

## How this feeds the indicators
The repo already builds ATAS indicators (`indicators/`) and a TradingView level sync
(`tradingview-sync/`). The point of these notes is to find the mechanical, rule-based pieces of
Severin's method (extreme-quartile closes, liquidity sweeps, supply/demand box breaks with N
touches, previous-day-high/low guards, value-area relationships) and implement them so a trade
setup is flagged automatically. Candidates live in `indicator-backlog.md`.
