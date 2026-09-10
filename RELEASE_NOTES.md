# Release Notes

All notable changes to **atas-on-steroids** are documented here. This file is updated with every development change to the project (new features, refactors, bug fixes, tooling, and in-progress work).

Format: reverse-chronological. Newest entries at the top.

---

## Unreleased

Work currently in progress on the working tree (not yet committed).

### Added — ATAS indicator: Stale Box Alert Muter (turns off a box's alert once it is fully in the past)
- `indicators/StaleBoxAlertMuter.cs` (new) — ATAS fires a rectangle's approximation alert purely on **price**; it never consults the box's time anchors, so a box drawn around an old session keeps pinging every time price revisits that band. This indicator clears `UseApproximationAlert` on any `RectangleObj` whose right edge is behind the reference clock.
  - **Reaching the boxes.** The documented API is a dead end: `IDrawingObjectsListInfo` declares only `HorizontalLines` and `HorizontalChannels` (which is why `ZoneAlertNotifier` only ever handled HighlightY zones). The concrete object behind it is `ChartData+DrawingObjectsListCollection`, holding a private field of type `DrawingObjectsCollection`, whose **public** `GetAllObjects(Instrument)` / `GetObjectsForSnapshot(bool)` return every drawing object on the chart. Both bottom out in `GlobalTradingObjectsStore`, which copies **references** into a fresh `List` with no cloning, so the objects handed back are the live ones the alert engine ticks (verified by decompiling `OFT.Platform.Core`, ATAS X 8.0.15.640). `RectangleObj.OnNewTicks()` opens with `if (!UseApproximationAlert) return;`, so clearing that one bool kills the alert from the next tick.
  - **Obfuscation-proofing.** Field names in `OFT.Platform.Core` are obfuscated (the collection field is literally `#=zb0NzAVH7_Gph`), so the field is located by **field type name**, never by name; public method/property names are not obfuscated and are matched normally. Every hop fails soft: a future ATAS refactor makes the indicator inert and prints the reason on the chart instead of throwing.
  - **Dry run on by default** — reports which boxes it *would* mute (on-chart list + log file) and changes nothing until you turn it off.
  - **Write-back verification**: after muting, the flag is read back. If ATAS ever starts returning copies, the status block says `write did not stick` rather than silently no-op'ing.
  - Settings: Enabled, Dry run, Grace (minutes), Re-enable when box is current again (only for boxes this session muted, so hand-muted boxes are never switched back on), Time reference (Last candle / Local now / UTC — last candle avoids any timezone mismatch since anchors and candles share one time axis), Scan interval, status block position, log file path (defaults to `~/Library/Application Support/ATAS/StaleBoxAlertMuter.log`).
  - Boxes with `ExtendRight` never expire and are skipped; half-drawn boxes (unset anchor) are skipped.
  - **Side effect worth knowing**: muting edits the box's own setting, so it persists once the workspace is saved. Reversible by ticking "Use alert" back on in the box properties.
  - This is the ATAS-side counterpart to the time-gate `atas-levels.pine` already applies to TradingView rectangle alerts.

### Changed — Level Sync userscript: multi-chart sync working; quiet the Push popup (v3.32)
- `tradingview-sync/atas-levels.user.js` — both charts now sync (`panes applied: 2/2`, each filled 28434 chars and the layout saved). Reverted the temporary "Push always shows the trace" debug behavior back to **failure-only**: a successful Push just flashes `✓ pushed & saved` with no popup; the full per-pane diagnostic (ancestor chain, `dblclick …: YES/no/wrong-dialog`, fill counts) still pops if something fails. Auto-apply remains silent. Net result of v3.19→v3.32: ATAS levels mirror to **every** chart pane in a multi-chart TradingView layout, even with the indicator legends hidden.

### Fixed — Level Sync userscript: pane dedupe used the hidden row's 0×0 rect (v3.31)
- `tradingview-sync/atas-levels.user.js` — v3.30 got the double-click working (a hidden row's settings opened and filled), but only one of the two charts applied. The finder dedupes panes by a visible ancestor's position, but `applyToChart` had its own `paneKey` reading the row's own rect — which is `0×0` for a hover-hidden row, so both charts hashed to `0:0` and merged into one pane. `paneKey` now buckets by the same visible-ancestor rect (`paneRectOf`), so the two charts get distinct keys and both are filled.

### Fixed — Level Sync userscript: target the hidden source ROW, not the sources wrapper (v3.30)
- `tradingview-sync/atas-levels.user.js` — v3.29's ancestor dump revealed the finder was returning `sourcesWrapper-…` (607×31, the container of *all* indicator sources) as the "title", because the real ATAS Levels title span is 0-size while hover-hidden, so the tightest *visible* element carrying the text was the wide wrapper — and every ancestor was a 600px+ container the size filter skipped, so no double-click ever fired. Rewrote `findLegendTitles` to match the source **row** itself — `[class*="item-"]` whose text includes the indicator name, *without* requiring visibility — keeping the tightest-text row per pane and deduping panes by a visible ancestor's position. `openSettings` now double-clicks that row (and its inner name-bearing nodes / small ancestors) directly; React delegates dblclick at the document root, so it fires even on a 0-size hidden row.

### Fixed — Level Sync userscript: hidden indicator legends → double-click the row to open settings (v3.29)
- `tradingview-sync/atas-levels.user.js` — root cause found by inspecting the user's actual chart (video frames): the charts **hide the indicator legend rows** (TradingView `hideInvisibleHover` — only the symbol row is shown). So there is no per-indicator ⋯/gear to click; the ⋯ that v3.27/3.28 found was the *symbol's*, whose "Settings…" opens the **chart** Settings dialog (Symbol/Status line/Scales/…), which is why `after settings-click` reported `bigDialog=true tabs=[none]`. The hidden indicator row is still in the DOM though, and double-clicking the ROW element opens that indicator's settings. `openSettings` rewritten to walk outward from the located title, double-clicking each reasonably-sized ancestor (the row is among them), validating success by the Inputs/Visibility tabs, and pressing Escape to close any wrong dialog (the chart Settings) between tries so it can't block the next attempt or the next pane. Also dumps the ancestor chain to the trace for confirmation.

### Changed — Level Sync userscript: harden the ⋯-menu Settings click + diagnostics (v3.28)
- `tradingview-sync/atas-levels.user.js` — v3.27 found the "More" button and the "Settings…" item but the click didn't open the dialog (`more→settings: dialog=no`). Verified in a real browser that the clickable menuitem is an `item-<hash> interactive…` row wrapping the `label-<hash>` text span, so clicking the bare label was unreliable. Now climbs to that interactive ancestor and fires a real-cursor click on whatever is painted at the label's centre (confirmed to open the dialog live). Added a diagnostic line — `after settings-click: bigDialog=… tabs=[Inputs/Style/Visibility]` and a menu-item count — so a remaining failure says whether *no* dialog opened, or one opened that isn't being recognised as the settings dialog.

### Fixed — Level Sync userscript: open settings via the row's ⋯ menu (v3.27)
- `tradingview-sync/atas-levels.user.js` — with both panes correctly detected (v3.26), the double-click no longer opened the dialog: double-clicking the title *button* triggers its own action, and TradingView's current legend has no discoverable "Settings" gear (`same-row gear: NONE`). Rewrote `openSettings` to use the legend row's **⋯ "More" button → "Settings…" menu item** as the primary path — verified against live TradingView: every legend row has an `aria-label="More"` button whose menu contains a `<div class="label-…">Settings…</div>`. This targets that exact row's indicator unambiguously (no click-aiming), so it can't wander onto a neighbouring indicator the way the v3.25 point-click landed on CC Suite. Double-click on the row (then a point double-click) remain as fallbacks. Verified end to end in a real browser: hover row → click More → menu opens → "Settings…" found → click → dialog opens.

### Fixed — Level Sync userscript: target the real legend-title element (v3.26)
- `tradingview-sync/atas-levels.user.js` — inspected the current TradingView legend DOM directly and rewrote `findLegendTitles` around what it actually is now. TradingView dropped `data-name="legend-source-title"` entirely; a legend source title is now `<span class="title-<hash> accessible-<hash>">` whose text is the indicator name — visible, tightly sized, one per source per pane. The `-<hash>` suffix rotates per TradingView release but the readable `title`/`accessible` prefixes are stable. The finder now matches `[class*="title-"][class*="accessible-"]` containing "ATAS Levels" (with height-capped fallbacks for future churn), which pinpoints the actual title in each pane instead of a wrapper whose centre sat over a neighbouring indicator (that mis-targeting in v3.25 opened *CC Suite*'s settings instead of ATAS Levels). Each pane's title now has a distinct real position, so both charts are detected and filled.

### Fixed — Level Sync userscript: click the title NODE, ignore our own widget (v3.25)
- `tradingview-sync/atas-levels.user.js` — with both chart legends now detected (v3.24), the double-click stopped opening settings (`title dblclick: dialog=no` on every pane). Cause: v3.24 targeted the enclosing legend *box*, but TradingView's dblclick-to-open handler lives on the title text node — a dblclick dispatched on an ancestor never bubbles *through* the title, so nothing fired. Fix: `titleLeaf()` drills from the box down to the deepest visible element still holding the whole name (the real title node) and double-clicks that; a second attempt double-clicks whatever is painted at the title's centre point (`elementFromPoint`, real-cursor style) before falling back to the gear. Also: the finder now excludes our own `#atas-levels-widget` (it prints "ATAS Levels v…" and was being picked up as a phantom third pane).

### Fixed — Level Sync userscript: legend detection collapsed both charts into one (v3.24)
- `tradingview-sync/atas-levels.user.js` — the real reason only one chart of a two-chart layout ever filled. A TradingView update made all three legend-title selectors return **0** elements, so the finder fell through to its exact-text-leaf last resort — which matched **2** hidden/detached duplicate nodes (the copies TV keeps in menus/templates), both reporting screen position `(0,0)`. The pane-dedupe then merged them into a single pane, so the loop only ever applied to one chart. `findLegendTitles` rewritten to not trust class/`data-name` selectors or exact-text leaves: it now gathers every element containing the name, keeps only **short visible boxes** (height ≤ 80px — excludes the tall pane/chart/body wrappers and the hidden 0,0 dupes), and takes the tightest box per pane by real screen position. The trace now prints `legend candidates / rows(<=80px) / panes` and each pane's `left,top`, so pane detection is verifiable at a glance.

### Fixed — Level Sync userscript: right chart not filling in multi-chart layouts (v3.23)
- `tradingview-sync/atas-levels.user.js` — in a two-chart layout the left chart's indicator updated but the right one didn't. Cause: `applyToChart` captured every legend title up front, but opening/closing the first pane's settings makes TradingView **re-render the legend**, leaving the second pane's captured element detached (stale). A double-click on a detached node with a zeroed rect effectively re-targets the top-left = the left chart, so the left filled twice and the right never did. Fix: enumerate panes by on-screen position, then **re-find each pane's title fresh** immediately before opening it (excluding panes already filled), so every click acts on a live, correctly-placed element. The trace now lists the detected pane keys (`panes: 2 [8:1, 700:1]`) and, for this debug cycle, **Push always shows the trace** (auto-apply stays silent) so a miss is diagnosable at a glance.

### Fixed — Pine: RE10020 runtime error on very-far-future boxes (halted the whole chart)
- `tradingview-sync/atas-levels.pine` — a box whose start projected onto the far-future edge could error the entire script with `RE10020: Objects positioned using xloc.bar_index cannot be drawn further than 500 bars into the future`, which **halts the indicator on that chart** (this, not the multi-pane userscript logic, was why a chart with such a box "wasn't updating" — the NQ chart had a far-future box and its indicator crashed). Cause: the left edge was clamped to `bar_index + 500`, then the right edge became `left + 1` = `+501`, one past TradingView's hard limit. Fix: clamp the **left** edge at `bar_index + 499` so the right edge (`left + 1`) can never exceed `+500`. Very-far-future boxes now pin harmlessly to the right edge instead of crashing.

### Changed — Pine: zone transparency + box positioning accuracy
- `tradingview-sync/atas-levels.pine` — added a **"Zone/box fill transparency (%)"** input (default 92) that overrides the ATAS-derived fill opacity, so highlights are see-through enough to watch price enter them. Box time-anchoring reworked: boxes now use exact `xloc.bar_time` (pixel-accurate, matches ATAS) unless the box is **entirely** in the future (`start > last bar`), which alone falls back to `bar_index` projection. Previously any box with a future *end* used projection, which mis-placed past/spanning boxes closer to the current bar.

### Fixed — Pine: boxes drawn on future candles
- `tradingview-sync/atas-levels.pine` — rectangles that extend past the last candle now render into the future. `xloc.bar_time` clamps any future timestamp to the last historical bar (TradingView limitation), so a box drawn on future candles in ATAS collapsed back to the latest bar. When a box's end is beyond the last bar, it's now projected onto `xloc.bar_index` (bars ahead computed from `timeframe.in_seconds()`, clamped to TV's 500-bar future limit); historical boxes still use `bar_time` for accuracy. Far-future/across-weekend projection is approximate (calendar-time based).

### Changed — Pine labels
- `tradingview-sync/atas-levels.pine` — labels (price-value chips) now render **only for horizontal lines**. Zones (HighlightY) and rectangles are drawn without labels for a cleaner chart.

### Fixed — Level Sync userscript: multi-chart layouts (v3.19)
- `tradingview-sync/atas-levels.user.js` — in a layout with more than one chart, only the first chart's "ATAS Levels" indicator was filled (the apply used `titles[0]`). Now it dedupes the found legend titles to one per pane (by on-screen position) and fills **every** pane's indicator in sequence, then saves the layout once. Each pane self-selects its family, so ES/MES and NQ/MNQ charts in the same layout all update.

### Fixed — Level Sync userscript vs a TradingView UI update (v3.16–3.18)
- `tradingview-sync/atas-levels.user.js` — a TradingView update broke the auto-apply: the settings dialog lost its `role="dialog"` attribute and now reopens on the last-used tab (often Style), which has no Levels textarea. Fixes: detect the settings dialog by its visible **Inputs/Style/Visibility** tabs (not `role="dialog"`); **click the Inputs tab** before reading the textarea; detect the textarea by visibility (not surrounding label text); find the OK button among visible `Ok`/`Apply` buttons. Added a **2-minute failure backoff** in `autoTick` so a broken apply can't reopen the dialog every 15s (the repeated-popup symptom). Manual **Copy all → paste** remains the fallback.

### Added — Level Sync userscript: Refresh alert button (v3.15)
- `tradingview-sync/atas-levels.user.js` — new **Refresh alert** button in the widget. Because TradingView freezes an indicator alert's input at creation, the button recreates the alert against the current zones: best-effort deletes existing "ATAS Levels" alerts (scoped to the right-sidebar alerts panel so it never touches the chart legend/indicator-remove), then creates a fresh one via the legend ⋯ menu → *Add alert* → Create (inherits last-used notification settings). Semi-auto by design — the create is reliable; the delete is best-effort (stale duplicates may need a manual remove). Uses the same `realClick` pointer-sequence + `waitFor` helpers as the settings automation.

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
