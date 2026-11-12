# Module notes: Quantower (Platform Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 2 lessons
(Expert level, Igor). Quantower is Igor's order-flow platform — **Elroy trades ATAS**, so this is
reference only. Captured mainly because **lesson 2 (DSF) resolves the long-open "DSF liquidity map"
question** in `playbook.md`.

Vault: "Igor Quantower Templates + Binds" (templates + binds folder).

---

## 1. Quantower Setup & Know-How — `quantower-setup-and-know-how`
Igor's platform intro. Quantower ≈ **ATAS for features, ExoCharts for visuals/UI**; its draw is the
**Auto Volume** feature and a sleek modern UI. Not a required switch — "good options are beneficial."
- Setup: Connections → pick broker → connect. Import the CC **templates + binds** (close Quantower,
  copy the vault "templates" folder into the Quantower install folder, reopen → load from the
  templates panel).
- Covers: Sessions Manager, default accounts/symbols, data-latency limit, loading/saving
  templates+binds, linking charts by color, tick size / clusters / abbreviations, saving order-entry
  (exit) strategies.
- Tips: color-synced binds + watchlist (select an asset → all charts switch together); pre-save an
  exit strategy per asset; name/save fibs/drawings as **global** objects for quick level marking;
  **copy trader** across accounts (two clicks); **Futures Rollover tool** rolls all assets to the new
  contract month by volume on expiry.

## 2. DOM Surface (DSF) Liquidity Heatmap — `dom-surface-liquidity-heatmap` ★ resolves the DSF question
**DSF = DOM Surface** = Quantower's real-time **liquidity heatmap**. This is the "DSF" referenced in
Igor's method and the open question in the playbook.
- Collects and **visually displays live Level 2 orders + depth of market + historical data**, showing
  the **supply/demand ratio in real time**. **Brighter colours = bigger resting limit orders**;
  **market orders show as circles** (bigger circle = bigger order).
- Use it to read where resting liquidity sits: e.g. a large sell order causing a 40-tick drop to the
  next bid support; **iceberg orders** (hidden size) revealed as they absorb market buys. Set trade
  entries, targets and **invalidation** on the real resting liquidity, in conjunction with the chart
  and plan.
- **ATAS equivalent (for the repo's indicator work):** ATAS exposes the live book via
  `OnlineDataProvider.GetMarketDepthSnapshot()` and recorded depth via
  `GetMarketDepthSnapshotsAsync(From,To,Period)`, plus the DOM **Depth-Changes** column and the ATAS
  **Heatmap** panel — see `chartchampions/atas-data-feasibility.md`. So a DSF-style read is reachable
  in ATAS, with the caveat that depth *history* only exists if it was recorded (the local Heatmap
  folder was empty). This closes the "confirm the exact ATAS setup for the liquidity-map step" item.
