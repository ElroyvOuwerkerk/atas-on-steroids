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

`GET /api/levels/{symbol}/csv` → one record per object, `;`-separated:

```
kind|price|price2|borderRRGGBB|fillRRGGBBAA|name|startEpochSec|endEpochSec
L|6078.75|0.0|FF733B||key (support)|1750073400|0
R|6307.25|6322.0|FFFFFF|FFFFFF1A||1775793600|1776312600
Z|6903.5|6886.75|FFFFFF|FFFFFF2C|strong level|0|0
```

`kind`: **L** = horizontal ray/line, **R** = rectangle, **Z** = HighlightY zone
(full chart width). Epoch `0` = no anchor (L/Z: full width; R end: extends
right). Fill keeps ATAS's alpha so boxes stay translucent.

Symbols are grouped by contract family: `ESM6`, `MES1!`, `ES@CME_Ind` → `ES`;
the NQ family → `NQ`. Hidden objects (`Visible: false`) are skipped.

## Setup

### 1. Backend
Already part of the trading-journal backend (auto-starts via launchd). Check
<http://localhost:3001/api/levels/view/page> — you should see your ES/NQ levels
with colour swatches, labels, and anchor dates.

### 2. TradingView (Pine)
1. Open **Pine Editor** → paste `atas-levels.pine` → **Save** as "ATAS Levels" → **Add to chart**.
2. On each other chart: **Indicators → My scripts → ATAS Levels**.
3. Apply to the charts you trade — e.g. **MES and MNQ**.

### 3. Browser helper (Tampermonkey — required for full automation)
1. Install the [Tampermonkey](https://www.tampermonkey.net/) extension.
2. Tampermonkey icon → **Create a new script** → replace the template with
   `atas-levels.user.js` → save (Cmd+S).
3. Open a TradingView chart — an **ATAS Levels** widget appears bottom-right.
   Allow the localhost connection when Tampermonkey asks.
4. Leave the **auto** checkbox on. The widget shows per family: object count,
   `✓` = chart in sync, `…` = update pending, `▸` = family of the open chart.

## Daily use

**Fully automatic (with the Tampermonkey userscript, "auto" checked):**

1. Finish your analysis in ATAS → **save the workspace**.
2. That's it. Within ~5–10 seconds the userscript notices the change, detects
   which family the open TradingView chart belongs to (MES → ES, MNQ → NQ),
   opens the *ATAS Levels* settings, fills the input, and clicks OK by itself.
   You'll see the settings dialog flash briefly, then the chart redraws.

The auto-apply waits politely: it won't fire while you're typing or while any
TradingView dialog is open (it retries on the next poll). Each chart tab syncs
the symbol it's showing; switch tabs and they catch up on their own.

**Manual fallback (if auto ever breaks after a TradingView UI update):**

- Click **Copy ES/NQ** in the widget (or on the copy page) → open *ATAS
  Levels* settings → paste into *Levels* → OK. The **Push** button does the
  fill+OK for you on demand.

## Notes

- The backend always parses the **most recently saved** `.aws` workspace.
- Mirrored types: `HorizontalRay`, `HLine`, `RectangleObj`, `HighlightY`.
- Every line gets a colour-filled chip just right of the last candle, hugging
  the price axis and showing the level's **price value** — e.g.
  `6078.75 · key (support)` — the closest Pine allows to a price-scale mark
  (only fixed-count `plot`s get true axis labels, and levels are dynamic).
  Named boxes/zones get a price-range chip (`6886.75–6903.5 · strong level`).
  Chip text auto-switches black/white based on the colour's luminance;
  "Show price values in labels" toggles the prices off.
- Anchor times are interpreted in the machine's local timezone; a small offset
  in where a ray/box *starts* doesn't affect its price.
- TradingView caps: 500 lines + 500 boxes + 500 labels per indicator — ES
  (~185 objects) and NQ (~233) fit comfortably.
