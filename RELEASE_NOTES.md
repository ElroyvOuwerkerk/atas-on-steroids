# Release Notes

All notable changes to **atas-on-steroids** are documented here. This file is updated with every development change to the project (new features, refactors, bug fixes, tooling, and in-progress work).

Format: reverse-chronological. Newest entries at the top.

---

## Unreleased

Work currently in progress on the working tree (not yet committed).

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
