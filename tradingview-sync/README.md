# ATAS → TradingView Level Sync

Mirrors your ATAS chart markup onto TradingView — **horizontal rays/lines,
rectangles (boxes), and HighlightY zones** — with their original colours,
fills, your labels, and time anchors (rays and boxes start where you drew
them, just like in ATAS). Works for the micros (MES/MNQ) as well as the
minis: same prices, same levels.

## How it works

```
ATAS workspace file (~/Library/Application Support/ATAS/Workspaces2/*.aws)
        │  (ATAS writes it when you SAVE the workspace)
        ▼
backend parses it on change ──→ /api/levels/{ES|NQ}  ─┬─→ copy page → paste into Pine
                                                      └─→ Tampermonkey widget → copy/push
```

The backend reads the **ATAS workspace file directly** — plain JSON containing
every drawing object in full (label text, anchor time, true price, colour,
instrument). This bypasses the ATAS indicator API, which exposes only
`Price/Width/Style/Color` and would lose the labels and anchors.

Sync is **full-state**: every save re-exports the complete set and TradingView
redraws from scratch — so adds, moves, and **deletes** all propagate.

**Your trigger = saving the workspace in ATAS** (when your analysis is done).
The backend notices the file change within seconds.

## Components

| File | Where it runs |
|------|---------------|
| `trading-journal/backend/app/routes/levels.py` | FastAPI backend — parses the workspace, serves levels |
| `atas-levels.pine` | TradingView — draws the levels |
| `atas-levels.user.js` | Browser (Tampermonkey) — copy/auto-fill helper |

No ATAS-side indicator is needed.

## Data format

`GET /api/levels/all/csv` → the combined feed the indicator wants: one record
per object, family-tagged, `;`-separated:

```
family|kind|price|price2|borderRRGGBB|fillRRGGBBAA|name|startEpochSec|endEpochSec
ES|L|6078.75|0.0|FF733B||key (support)|1750073400|0
ES|R|6307.25|6322.0|FFFFFF|FFFFFF1A||1775793600|1776312600
NQ|Z|25490.0|25456.0|FFFFFF|FFFFFF2C|strong level|0|0
```

The indicator draws only records whose **family** matches the chart, so the one
combined string works on every chart. `kind`: **L** = ray/line, **R** =
rectangle, **Z** = HighlightY zone (full width). Epoch `0` = no anchor (L/Z:
full width; R end: extends right). Fill keeps ATAS's alpha so boxes stay
translucent.

Symbols are grouped by contract family: `ESM6`, `MES1!`, `ES@CME_Ind` → `ES`;
the NQ family → `NQ`. Hidden objects (`Visible: false`) are skipped.
(`GET /api/levels/{symbol}/csv` still returns just one family, for reference.)

## Setup

### 1. Backend
Already part of the trading-journal backend (auto-starts via launchd). Check
<http://localhost:3001/api/levels/view/page> — you should see your ES/NQ levels
with colour swatches, labels, and anchor dates.

### 2. TradingView (Pine)
1. Open **Pine Editor** → paste `atas-levels.pine` → **Save** as "ATAS Levels"
   (Save is required — "Add to chart" alone does **not** put it in *My scripts*).
2. **Add to chart**, and on any other chart add it via **Indicators → My
   scripts → ATAS Levels**.
3. The indicator is **symbol-aware** — one config works on ES, MES, NQ and MNQ.
   Switch the ticker on a chart and it instantly redraws the right family. If
   detection ever misses, set the indicator's **Chart family** input from Auto
   to ES or NQ.

### 3. Browser helper (Tampermonkey — required for full automation)
1. Install the [Tampermonkey](https://www.tampermonkey.net/) extension.
2. Tampermonkey icon → **Create a new script** → replace the template with
   `atas-levels.user.js` → save (Cmd+S).
3. Open a TradingView chart — an **ATAS Levels** widget appears bottom-right.
   Allow the localhost connection when Tampermonkey asks.
4. Leave the **auto** checkbox on. The widget shows per-family object counts and
   `✓ synced` / `… pending`. It re-fills the indicator only when the feed
   changes (i.e. when you save the ATAS workspace) — not on ticker switches.

## Daily use

**Fully automatic (with the Tampermonkey userscript, "auto" checked):**

1. Finish your analysis in ATAS → **save the workspace**.
2. Within ~15 seconds the userscript fills the *ATAS Levels* indicator on the
   open browser chart, clicks OK, and then **saves the TradingView layout**
   (so the change persists server-side). Because the indicator self-selects by
   symbol, switching the ticker afterwards needs no action — it redraws instantly.

The auto-apply waits politely: it won't fire while you're typing or while any
TradingView dialog is open (it retries on the next poll).

### Desktop app / other devices

The userscript only runs in a **web browser** — not the TradingView desktop app.
So one browser tab (with the userscript) acts as the **engine**: it applies the
levels and saves the layout to your TradingView account. The desktop app (or
phone, or another browser) then shows the levels **after it reloads that layout**
— TradingView does not live-sync an already-open layout. In practice:

1. Keep a browser tab open with the userscript (it can sit in the background).
2. Save in ATAS → the browser tab applies + saves the layout.
3. In the desktop app, **reload/re-open the layout** to pull the update.

**Manual fallback (if auto ever breaks after a TradingView UI update):**

- Click **Copy all** in the widget (or on the copy page) → open *ATAS Levels*
  settings → paste into *Levels* → OK, then **Cmd/Ctrl+S** to save the layout.
  The **Push now** button does the fill + OK + layout-save for you on demand
  (it only pops a diagnostic if something fails).

## Alerts (price entering a zone/box)

The indicator can fire a TradingView alert when price **enters** a highlight or
rectangle (once on entry, not while it sits inside). Set it up once:

1. In the indicator settings → **Alerts** group, pick what should alert
   (only highlights/zones on by default; rectangles and lines off).
2. On the chart, **Alerts → Create alert** → Condition: **ATAS Levels** →
   **"Any alert() function call"** → choose how you're notified (app push,
   popup, sound, email) → Create.

One alert covers **all** zones/boxes on that chart, and automatically includes
new ones you add later (they flow through the same indicator). The alert message
includes the level's name, price range, and symbol. Alerts fire on the chart's
family, so an alert on the MNQ chart triggers on your NQ zones.

**Time rule for rectangles:** highlights are timeless (full-width), so they
always alert when price reaches them. Rectangles, however, only alert **while
their time window is still active** — once the chart has passed a box's end
time, it no longer fires (matching the ATAS-side rule). So you can safely enable
rectangle alerts without getting pinged on stale, historical boxes.

**Per-zone cooldown:** after a zone alerts, it won't re-fire for the cooldown
window (default 15 min; `0` disables) — avoids repeat pings while price hovers
at a zone edge.

**Important — the snapshot rule:** a TradingView alert **freezes** the
indicator's input the moment you create it, and never updates it afterward
([TradingView docs](https://www.tradingview.com/support/solutions/43000597494-alerts-on-alert-function/)).
Since the userscript continuously rewrites the input with your latest zones,
an alert created earlier keeps firing on **old** zones and ignores new ones. So
create the alert **after** finalizing your zones (widget shows ✓ synced), and
recreate it whenever your zones change. This is independent of the ATAS-side
Telegram zone alerts.

## Notes

- The backend always parses the **most recently saved** `.aws` workspace.
- Mirrored types: `HorizontalRay`, `HLine`, `RectangleObj`, `HighlightY`.
- **Only horizontal lines** get a chip — a colour-filled label just right of the
  last candle, hugging the price axis, showing the level's **price value** (and
  name) — e.g. `6078.75 · key (support)`. Zones and rectangles are drawn
  without labels (kept clean). Chip text auto-switches black/white based on the
  colour's luminance; "Show price values in labels" toggles the prices off.
- Anchor times are interpreted in the machine's local timezone; a small offset
  in where a ray/box *starts* doesn't affect its price.
- TradingView caps: 500 lines + 500 boxes + 500 labels per indicator — ES
  (~185 objects) and NQ (~233) fit comfortably.
