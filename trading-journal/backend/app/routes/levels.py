"""
Level sync — mirrors ATAS-drawn horizontal levels onto TradingView.

Source of truth: the ATAS workspace file (~/Library/Application Support/
ATAS/Workspaces2/*.aws). It's plain JSON and — unlike the ATAS indicator
API, which strips drawing objects down to Price/Width/Style/Color — it
contains the full objects: user labels (`Text`), the ray's start time
(`_firstTime`), real prices (`TruePrice`), colours, and the instrument.

ATAS writes the file when the workspace is saved, so the user's "analysis
done" signal is simply saving the workspace in ATAS. We re-parse lazily:
every read checks the file's mtime and re-parses only when it changed.

Levels are grouped by contract family (ESM6/MES → "ES"), because minis and
micros trade at identical prices — one level set serves both charts.
"""
import json
import re
from datetime import datetime
from pathlib import Path

from fastapi import APIRouter
from fastapi.responses import HTMLResponse, PlainTextResponse
from pydantic import BaseModel, Field

router = APIRouter(prefix="/api/levels", tags=["levels"])

WORKSPACES_DIR = Path.home() / "Library/Application Support/ATAS/Workspaces2"

# Drawing types we mirror, mapped to a compact kind code:
#   L = horizontal ray / line, R = rectangle, Z = HighlightY zone (full-width)
_KIND_BY_TYPE = {
    "DrawingObjects.HorizontalRay": "L",
    "DrawingObjects.HLine": "L",
    "DrawingObjects.RectangleObj": "R",
    "DrawingObjects.HighlightY": "Z",
}

# Futures month code + year suffix, e.g. "M6" / "M26" / "M2026" in "ESM6".
_FUT_SUFFIX = re.compile(r"[FGHJKMNQUVXZ]\d{1,4}$")

# Micro contracts share their mini's levels (same index price).
_FAMILY = {"MES": "ES", "MNQ": "NQ", "MGC": "GC", "M2K": "RTY", "MYM": "YM"}

# Named colours ATAS may store instead of an RGB triple.
_NAMED_COLORS = {
    "WHITE": "#FFFFFF", "BLACK": "#000000", "RED": "#FF0000", "GREEN": "#008000",
    "BLUE": "#0000FF", "YELLOW": "#FFFF00", "CYAN": "#00FFFF", "MAGENTA": "#FF00FF",
    "ORANGE": "#FFA500", "PURPLE": "#800080", "GRAY": "#808080", "LIME": "#00FF00",
}


class Level(BaseModel):
    kind: str = "L"      # L = line/ray, R = rectangle, Z = zone (full-width)
    price: float         # line price, or box top/bottom edge 1
    price2: float = 0.0  # box edge 2 (0 for lines)
    color: str = "#FFFFFF"   # line / border colour
    fill: str = ""           # box fill as RRGGBBAA ("" for lines)
    name: str = ""
    start: str = ""  # ISO anchor time ("" if none → full width)
    end: str = ""    # ISO second anchor for rectangles ("" → extends right)


class LevelSet(BaseModel):
    symbol: str
    levels: list[Level] = Field(default_factory=list)
    updated_at: str | None = None


def _group(symbol: str) -> str:
    """ES, MES, ESM6, MES1!, ES@CME_Ind … → "ES" (and NQ family → "NQ")."""
    s = (symbol or "").strip().upper()
    s = s.split("@")[0].split(".")[0]
    s = s.replace("1!", "").replace("2!", "").rstrip("!")
    s = _FUT_SUFFIX.sub("", s)
    return _FAMILY.get(s, s)


def _color_parts(value) -> tuple[int, int, int, int] | None:
    """ATAS colour → (a, r, g, b). Accepts 'R, G, B', 'A, R, G, B', or names."""
    if not value or not isinstance(value, str):
        return None
    v = value.strip()
    named = _NAMED_COLORS.get(v.upper())
    if named:
        return (255, int(named[1:3], 16), int(named[3:5], 16), int(named[5:7], 16))
    parts = [p.strip() for p in v.split(",")]
    try:
        nums = [max(0, min(255, int(p))) for p in parts]
    except ValueError:
        return None
    if len(nums) == 3:
        return (255, *nums)
    if len(nums) == 4:
        return tuple(nums)  # A, R, G, B
    return None


def _color_hex(value) -> str:
    """ATAS colour → '#RRGGBB' (alpha dropped)."""
    p = _color_parts(value)
    if p is None:
        return "#FFFFFF"
    _, r, g, b = p
    return f"#{r:02X}{g:02X}{b:02X}"


def _fill_hex(value) -> str:
    """ATAS area colour → 'RRGGBBAA' (alpha kept, for box fills)."""
    p = _color_parts(value)
    if p is None:
        return ""
    a, r, g, b = p
    return f"{r:02X}{g:02X}{b:02X}{a:02X}"


def _sanitize(name: str) -> str:
    """Labels travel in a ';'/'|'-delimited format — strip those chars."""
    return (name or "").replace(";", " ").replace("|", " ").strip()


def _latest_workspace() -> Path | None:
    try:
        files = list(WORKSPACES_DIR.glob("*.aws"))
        return max(files, key=lambda p: p.stat().st_mtime) if files else None
    except OSError:
        return None


def _parse_workspace(path: Path) -> dict[str, LevelSet]:
    """Extract horizontal levels from a workspace file, grouped by family."""
    doc = json.loads(path.read_text(encoding="utf-8"))
    updated = datetime.fromtimestamp(path.stat().st_mtime).isoformat()

    sets: dict[str, LevelSet] = {}
    for obj in doc.get("GlobalObjects", []):
        if not isinstance(obj, dict):
            continue
        otype = obj.get("$type", "")
        kind = next((k for t, k in _KIND_BY_TYPE.items() if t in otype), None)
        if kind is None:
            continue
        if obj.get("Visible") is False:
            continue

        sym = _group(obj.get("InstrumentName", ""))
        if not sym:
            continue

        if kind == "L":
            price = obj.get("TruePrice", obj.get("Price"))
            price2 = 0.0
        else:
            price = obj.get("TrueFirstPrice", obj.get("FirstPrice"))
            price2 = obj.get("TrueSecondPrice", obj.get("SecondPrice")) or 0.0
        if not isinstance(price, (int, float)) or price <= 0:
            continue

        # Rectangle time anchors; ExtendRight overrides the second anchor.
        start = obj.get("_firstTime", "") or ""
        end = ""
        if kind == "R":
            end = obj.get("_secondTime", "") or ""
            if obj.get("ExtendRight") is True:
                end = ""
            if obj.get("ExtendLeft") is True:
                start = ""

        level = Level(
            kind=kind,
            price=float(price),
            price2=float(price2),
            color=_color_hex(obj.get("Color")),
            fill=_fill_hex(obj.get("AreaColor")) if kind in ("R", "Z") else "",
            name=_sanitize(obj.get("Text", "")),
            start=start,
            end=end,
        )
        sets.setdefault(sym, LevelSet(symbol=sym, updated_at=updated)).levels.append(level)

    for s in sets.values():
        s.levels.sort(key=lambda lv: lv.price)
    return sets


# ── mtime-keyed cache ──
_cache: dict[str, LevelSet] = {}
_cache_key: tuple[str, float] | None = None


def _levels() -> dict[str, LevelSet]:
    global _cache, _cache_key
    path = _latest_workspace()
    if path is None:
        return {}
    key = (str(path), path.stat().st_mtime)
    if key != _cache_key:
        try:
            _cache = _parse_workspace(path)
            _cache_key = key
        except (json.JSONDecodeError, OSError):
            # Mid-write or unreadable — keep serving the previous parse.
            pass
    return _cache


def _epoch(iso: str) -> int:
    if not iso:
        return 0
    try:
        return int(datetime.fromisoformat(iso).timestamp())
    except ValueError:
        return 0


def _csv(s: LevelSet | None) -> str:
    """
    Paste format for the Pine indicator. One record per object:
        kind|price|price2|borderRRGGBB|fillRRGGBBAA|name|startEpoch|endEpoch
    records ';'-separated. kind: L=line/ray, R=rectangle, Z=full-width zone.
    0 epoch = no anchor (L/Z: full width; R end: extends right).
    """
    if not s or not s.levels:
        return ""
    out = []
    for lv in s.levels:
        out.append(
            f"{lv.kind}|{lv.price}|{lv.price2}|{lv.color.lstrip('#')}|{lv.fill}"
            f"|{lv.name}|{_epoch(lv.start)}|{_epoch(lv.end)}"
        )
    return ";".join(out)


@router.get("")
async def list_levels():
    sets = _levels()
    ws = _latest_workspace()
    return {
        "workspace": ws.name if ws else None,
        "symbols": sorted(sets.keys()),
        "sets": list(sets.values()),
    }


@router.get("/{symbol}", response_model=LevelSet)
async def get_levels(symbol: str):
    sym = _group(symbol)
    return _levels().get(sym, LevelSet(symbol=sym, levels=[]))


@router.get("/{symbol}/csv", response_class=PlainTextResponse)
async def get_levels_csv(symbol: str):
    return _csv(_levels().get(_group(symbol)))


@router.get("/view/page", response_class=HTMLResponse)
async def view_page():
    return _VIEW_HTML


_VIEW_HTML = """<!doctype html>
<html><head><meta charset="utf-8"><title>ATAS → TradingView Levels</title>
<style>
  body { font-family: -apple-system, system-ui, sans-serif; background:#0e0e12; color:#e8e8ea;
         margin:0; padding:24px; }
  h1 { font-size:18px; font-weight:600; margin:0 0 4px; }
  .ws { color:#7a7a82; font-size:12px; margin-bottom:16px; }
  .card { background:#17171d; border:1px solid #26262e; border-radius:10px;
          padding:16px; margin-bottom:14px; }
  .sym { font-size:15px; font-weight:600; margin-bottom:8px; }
  .count { color:#9a9aa2; font-weight:400; font-size:13px; }
  textarea { width:100%; box-sizing:border-box; height:64px; background:#0e0e12; color:#cfe;
             border:1px solid #2c2c36; border-radius:8px; padding:10px; font-family:Consolas,monospace;
             font-size:12px; resize:vertical; }
  button { margin-top:8px; background:#2d6cdf; color:#fff; border:0; border-radius:8px;
           padding:8px 14px; font-size:13px; cursor:pointer; }
  button:active { transform:translateY(1px); }
  .muted { color:#7a7a82; font-size:12px; margin-top:18px; }
  .ts { color:#7a7a82; font-size:11px; margin-top:6px; }
  .rows { margin-bottom:10px; max-height:300px; overflow-y:auto; }
  .lvl { display:flex; align-items:center; gap:8px; padding:2px 0; font-size:13px; }
  .sw { width:12px; height:12px; border-radius:3px; border:1px solid #3a3a44; flex:none; }
  .px { font-family:Consolas,monospace; color:#cfe; min-width:80px; }
  .nm { color:#b8b8c0; }
  .st { color:#6f6f78; font-size:11px; margin-left:auto; }
</style></head>
<body>
  <h1>ATAS → TradingView Levels</h1>
  <div class="ws" id="ws"></div>
  <div id="root"></div>
  <div class="muted"><b>Save your workspace in ATAS</b> to refresh this list, then paste a
     symbol's string into the "ATAS Levels" Pine indicator. ES levels apply to ES &amp; MES;
     NQ levels apply to NQ &amp; MNQ.</div>
<script>
async function load() {
  let data;
  try { data = await (await fetch('/api/levels')).json(); }
  catch (e) { return; }
  document.getElementById('ws').textContent = data.workspace
    ? 'workspace: ' + data.workspace : 'no workspace file found';
  const root = document.getElementById('root');
  const sets = (data.sets || []).sort((a,b)=> a.symbol.localeCompare(b.symbol));
  if (!sets.length) { root.innerHTML = '<div class="card">No levels found.</div>'; return; }
  root.innerHTML = sets.map(s => {
    const lv = s.levels || [];
    const ep = (iso) => { if (!iso) return 0; const d = new Date(iso); return isNaN(d) ? 0 : Math.floor(d.getTime()/1000); };
    const csv = lv.map(l =>
      (l.kind||'L') + '|' + l.price + '|' + (l.price2||0) + '|' + (l.color||'#FFFFFF').replace('#','')
      + '|' + (l.fill||'') + '|' + (l.name||'') + '|' + ep(l.start) + '|' + ep(l.end)
    ).join(';');
    const rows = lv.map(l => {
      const st = l.start ? new Date(l.start).toLocaleDateString() : '';
      const px = (l.kind==='L') ? l.price : (l.price + ' – ' + l.price2);
      const badge = l.kind==='R' ? '▭ ' : l.kind==='Z' ? '▤ ' : '';
      return `<div class="lvl"><span class="sw" style="background:${l.color}"></span>`
           + `<span class="px">${badge}${px}</span>`
           + `<span class="nm">${(l.name||'').replace(/</g,'&lt;')}</span>`
           + `<span class="st">${st}</span></div>`;
    }).join('');
    const id = 'ta_' + s.symbol;
    const ts = s.updated_at ? new Date(s.updated_at).toLocaleString() : '';
    return `<div class="card">
      <div class="sym">${s.symbol} <span class="count">(${lv.length} levels)</span></div>
      <div class="rows">${rows}</div>
      <textarea id="${id}" readonly>${csv}</textarea>
      <button onclick="copy('${id}')">Copy ${s.symbol}</button>
      <div class="ts">workspace saved ${ts}</div>
    </div>`;
  }).join('');
}
function copy(id){ const t=document.getElementById(id); t.select();
  navigator.clipboard.writeText(t.value); }
load(); setInterval(load, 3000);
</script>
</body></html>
"""
