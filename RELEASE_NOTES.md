# Release Notes

All notable changes to **atas-on-steroids** are documented here. This file is updated with every development change to the project (new features, refactors, bug fixes, tooling, and in-progress work).

Format: reverse-chronological. Newest entries at the top.

---

## Unreleased

Work currently in progress on the working tree (not yet committed).

### Changed — Pine labels
- `tradingview-sync/atas-levels.pine` — labels (price-value chips) now render **only for horizontal lines**. Zones (HighlightY) and rectangles are drawn without labels for a cleaner chart.

### Added — TradingView alerts on zones/boxes (Pine)
- `tradingview-sync/atas-levels.pine` — the indicator now fires `alert()` when price **enters** a highlight (Z) or rectangle (R) — once on entry, tracked per-object via persisted inside/outside state, `alert.freq_once_per_bar`. New **Alerts** settings group: toggle highlights / rectangles / lines (only highlights/zones on by default; rectangles and lines off). **Rectangle alerts are time-gated** — a box only fires while its time window is still active (end epoch ≥ `timenow`); once the chart passes the box's end it's ignored. Zones are timeless (end 0) so always active. **Per-zone cooldown** (default 15 min, `0` = off): after a zone alerts it won't re-fire for the cooldown window, tracked per-object in a `var` array — avoids repeat pings while price hovers at an edge. Alert targets are parsed once into `var` arrays for the chart's family (re-parses on input/symbol change). Setup: create one alert on the indicator with condition "Any alert() function call"; the message carries the level name, price range, and symbol. Independent of the ATAS-side Telegram alerts.

### Changed — Level Sync userscript (v3.x → v3.12)
- `tradingview-sync/atas-levels.user.js` — hardened the unattended apply against the live TradingView DOM and added server-side persistence:
  - **Native-paste fill**: sets the Levels input via `document.execCommand('insertText')` (a trusted input event TradingView's editor accepts) instead of a programmatic `.value` set, which TV ignored — that bug silently dropped boxes/zones on auto-apply while manual paste worked.
  - **Correct settings gear**: scopes to the ATAS Levels legend row and clicks the button whose label is exactly "Settings" (earlier it matched "Show interval settings" and opened the timeframe dialog); opens via a full pointer sequence (`pointerdown/mousedown/up/click`), since a bare `.click()`/synthetic `dblclick` don't register.
  - **Right dialog detection**: identifies the indicator's panel by its "ATAS Levels"/"Levels (paste…)" text, ignoring stray dialogs.
  - **Layout persistence**: after applying, saves the TradingView layout (so the change reaches the **desktop app / other devices**, which the browser-only userscript can't touch directly — they pick it up on layout reload). Clicks the header Save control; synthetic Cmd+S kept as a cheap first attempt.
  - Poll interval relaxed to 15s; widget redraws only on change; version stamped in the widget + popup; **Push now** only shows a diagnostic on failure.

### Changed — CC Suite Levels
- `indicators/CCSuiteLevels.cs` — absorbed the **Division Candle Overlay (75% momentum)** feature from CC Suite, so one indicator now covers all three strategies (COT/COC at level, Moonshot, momentum bar colouring): new master toggle + settings group "1) Division Candle Overlay" (divisions count, upper/lower zone toggles and colours), `PaintbarsDataSeries` bar colouring in `OnCalculate`; existing groups renumbered (Cherry → 2, COC → 3, Probability Table → 4). CC Suite can be removed from charts.
- `indicators/CCSuiteLevels.cs` — marker-to-bar distance halved (COT gap `(size+15)/2`, stacked COC gap halved proportionally) after feedback that markers sat too far from the candles.

### Added — ATAS → TradingView Level Sync (new self-contained system)
- `trading-journal/backend/app/routes/levels.py` — new FastAPI router (`/api/levels`). **Parses the ATAS workspace file directly** (`~/Library/Application Support/ATAS/Workspaces2/*.aws`, plain JSON; most recently saved one wins, mtime-keyed cache). Extracts `HorizontalRay`/`HLine` objects with **true price, colour, the user's label text, and the ray's anchor time** — all things the ATAS indicator API does not expose (`HorizontalLine` via `DrawingObjectsListInfo` carries only Price/Width/Style/Color — verified by property dump). Levels are grouped by contract family (ESM6/MES1!/ES@CME_Ind → `ES`; NQ family → `NQ`) so minis and micros share one set. Endpoints: `GET /api/levels`, `GET /api/levels/{symbol}`, `GET /api/levels/{symbol}/csv` (paste format `price|RRGGBB|name|startEpochSec;…`), `GET /api/levels/view/page` (copy page with swatches/labels/anchor dates). Registered additively in `app/main.py` — no change to trades/stats/clips.
- `tradingview-sync/atas-levels.pine` — Pine v6 indicator, **symbol-aware**. The input holds *both* families (ES + NQ); the indicator computes the chart's family from `syminfo.root` (micro→mini: MES→ES, MNQ→NQ; `Chart family` override input as fallback) and draws only matching records. So one config works on ES/MES/NQ/MNQ and **switching the ticker instantly redraws the right set with no re-paste/flash**. Draws **rays** anchored at their ATAS start time extending right (full-width fallback), **rectangles** between their two time anchors (honouring ATAS `ExtendRight`/`ExtendLeft`), and **HighlightY zones** as full-width boxes — all in ATAS colours with translucent fills (hand-rolled hex→`color.rgb` incl. alpha). Labels are colour-filled chips at the price axis showing the price value (`6078.75 · key (support)`; ranges for named boxes), black/white text by luminance, prices toggleable. Full redraw on the last bar → adds/moves/deletes all sync.
- Record format (family-tagged): `family|kind|price|price2|borderRRGGBB|fillRRGGBBAA|name|startEpoch|endEpoch` (kind L/R/Z), `;`-separated. Backend serves the combined feed at `GET /api/levels/all/csv` and on the copy page as a single **Copy all** button that works on every chart.
- `tradingview-sync/atas-levels.user.js` — Tampermonkey userscript (v3). Polls `/api/levels` every 4 s, builds the combined family-tagged string, and **re-fills the "ATAS Levels" indicator only when the feed changes** (i.e. on workspace save — not on ticker switches, since the indicator self-selects). Fills via double-click legend → native React setter on the textarea → OK. Guards: skips while typing or any dialog open; `auto` toggle; **Copy all** / **Push now** manual fallback. Replaces v2's per-family symbol-detection-and-apply model, which re-pushed (and flashed the dialog) on every ticker switch and could leave stale values when switching back.
- `tradingview-sync/README.md` — architecture, data format, setup, daily flow.

**Background:** investigated whether ATAS levels can appear on TradingView. There is no real-time inbound drawing API on tradingview.com (Pine can't fetch external data; `request.seed()` is EOD + TradingView-scheduled; webhooks are outbound only). Chosen design: ATAS workspace save → local backend parse → Pine via copy/paste or userscript auto-fill; the user's "analysis done" signal is saving the ATAS workspace. An earlier intermediate (`indicators/LevelSync.cs`, an ATAS indicator POSTing API-visible levels) was built and then retired the same session once the workspace file proved to carry the full drawing objects.

---

## 2026-04-23 — `v0.8`

### Added — ATAS Indicators
- `indicators/OpeningRangeBreakout.cs` (210 lines) — new ATAS indicator: Opening Range Breakout. Marks the high/low of the first 15 min of RTH as yellow dotted lines (`orb high`/`orb low`) and draws Fibonacci extension targets on both sides (T1 at 100%, T2 at 150% of the range) in cyan. All colors, widths, labels, extension percentages, RTH open time, and ORB duration are configurable.
- `indicators/TheDailyPlan.cs` (1036 lines) — new ATAS indicator that draws 5 previous-day levels (Settlement, Equilibrium, A-session POC, B-session POC, A+B combined POC) and renders a textual analysis block (OTF direction, prev-RTH value relation, gap, quartile close, excess, session open, overnight inventory). Uses exact footprint values only — no OHLCV approximations, no fallbacks.
- `indicators/ZoneAlertNotifier.cs` (344 lines) — new ATAS indicator that sends Telegram alerts when price enters Highlight Y zones (read via `HorizontalChannels` from `DrawingObjectsListInfo`). Direct Telegram Bot API integration over HTTPS with manually-constructed JSON (no `Newtonsoft.Json` dependency to avoid runtime load issues). Features:
  - Startup confirmation message per chart on first live bar.
  - Live-mode gating: skips the initial historical replay so old touches never alert.
  - Current-bar guard (`bar == CurrentBar − 1`): suppresses false alerts during recalculations triggered by drawing a new zone.
  - Both-bound validation (`firstPrice > 0 && secondPrice > 0`): ignores zones mid-draw where one bound is still `0`.
  - Background color → human-readable name with emoji (🔴 Red, 🟢 Green, 🔵 Blue, 🟡 Yellow, 🟠 Orange, 🟣 Purple, ⚪ White, Cyan, Magenta, fallback `RGB(r,g,b)`).
  - Optional zone-color filter for selective alerts.
  - Per-zone cooldown (configurable, default 30 min) keyed by `topPrice_bottomPrice` so the same zone doesn't spam.
  - Approach direction in alert text (above / below / inside).
  - Settings: bot token, chat ID, cooldown, color filter toggle, target zone color.

### Changed
- `indicators/CCSuiteLevels.cs` — reworked (~70 lines touched, +48 / −24).
- `trading-journal/start.sh` — switched backend invocation to use the dedicated venv Python (`backend/.venv/bin/python3`) so launchd-managed runs don't pick up the broken system Python 3.9.
- `trading-journal/plugin/TradeLogger.cs` — refactored from `ChartStrategy` to `Indicator`. ATAS X (macOS) does not surface custom `ChartStrategy` plugins in any UI, so it now inherits from `Indicator` and uses the `OnNewMyTrade` / `OnPositionChanged` overrides exposed on the indicator base. Net position is tracked from fill deltas with `Position.CurrentBuy − CurrentSell` as authoritative fallback. Now appears under right-click → Indicators → Custom → "CC Trade Logger".

### Added — Tooling / Infrastructure
- `trading-journal/backend/.venv/` — local Python 3.13 venv with FastAPI/uvicorn/SQLAlchemy/etc. so the backend can run under launchd (system Python 3.9 was blocked by sandboxing and missing deps).
- `~/Library/LaunchAgents/com.tradingjournal.backend.plist` & `com.tradingjournal.dashboard.plist` — auto-start the backend (port 3001) and dashboard (port 5173/5174) at login with `KeepAlive`. Logs to `/tmp/tradingjournal-backend.log` and `/tmp/tradingjournal-dashboard.log`.

---

## 2026-04-08 — `f740ac8` journal

### Added
- `trading-journal/start.sh` — launcher script for the trading journal stack (backend + dashboard).
- `trading-journal/stop.sh` — teardown counterpart to `start.sh`.

---

## 2026-04-07 — `e90920b` updates

### Changed
- `trading-journal/plugin/TradeLogger.cs` — simplified (−11 net lines); removed redundant logic paths.
- `trading-journal/dashboard/src/components/FilterBar.tsx` — small refinements.
- `trading-journal/dashboard/src/utils/colors.ts` — palette adjustments.
- ATAS indicator + TradeLogger plugin binaries rebuilt (Release artifacts refreshed).

---

## 2026-04-07 — `67a093a` updates (initial substantive commit)

First large drop establishing the two major components of the project.

### Added — ATAS Indicators (`indicators/`)
- `AtasOnSteroids.cs` (844 lines) — primary custom indicator.
- `CCSuiteLevels.cs` (986 lines) — suite of level-drawing logic.
- `AtasOnSteroids.csproj` targeting `net10.0`.
- Design notes: `change-of-context.txt`, `cherry-on-top.txt`, `momentum.txt`.
- Debug + Release build artifacts.

### Added — Trading Journal Backend (`trading-journal/backend/`)
- FastAPI application (`app/main.py`, `app/config.py`, `app/database.py`).
- SQLAlchemy models (`app/models.py`) and Pydantic schemas (`app/schemas.py`).
- Routes: `trades.py`, `stats.py`, `clips.py`.
- Alembic migration setup with initial migration `001_initial.py`.
- `pyproject.toml` for dependency management.

### Added — Trading Journal Dashboard (`trading-journal/dashboard/`)
- React + TypeScript + Vite front-end.
- API clients: `api/client.ts`, `api/trades.ts`, `api/stats.ts`.
- Components: `FilterBar`, `NotesEditor`, `SignalBadge`, `TradeDetail`, `TradeTable`, `VideoPlayer`.
- Pages: `Trades`, `TradeView`, `Stats`.
- Shared utilities: `utils/colors.ts`, `utils/format.ts`, `types/trade.ts`.
- Tooling: ESLint config, tsconfig files, Vite config, favicon/icon SVGs, hero asset.

### Added — ATAS TradeLogger Plugin (`trading-journal/plugin/`)
- `TradeLogger.cs` (300 lines) — logs live trades.
- `ObsClient.cs` (162 lines) — OBS integration for clip capture.
- `TradeLogger.csproj` targeting `net10.0`.
- Debug + Release build artifacts.

### Added — Tooling
- `.claude/launch.json` — debugger launch configuration.

---

## Guidelines for future entries

- Group changes under **Added / Changed / Fixed / Removed / Deprecated / Security**.
- Reference files with backticks and short commit SHAs where available.
- Keep bullets focused on user-observable or architecturally meaningful changes; skip pure rebuild-artifact churn.
- When in-progress work lands, move bullets from **Unreleased** into a dated section with the new commit SHA.
