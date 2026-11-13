#!/usr/bin/env python3
"""
Build the ChartChampions knowledge library: a local, offline, searchable web interface
that combines the module notes, the glossary, the setup diagrams, and the Vault cheat-sheet
decks (shown as a gallery, full PDFs one click away).

Input  : chartchampions/notes/*.md, playbook.md, indicator-backlog.md, vault-manifest.md
         + ~/Downloads/ChartChampions-Vault/cheat-sheets/*.pdf
Output : ~/Downloads/ChartChampions-Vault/library/  (double-click index.html — nothing is uploaded)

Usage  : python3 chartchampions/library/build.py [--force]
         --force regenerates every PDF thumbnail (otherwise cached).
"""
import os, re, sys, json, shutil, subprocess, unicodedata

HERE = os.path.dirname(os.path.abspath(__file__))
CC   = os.path.dirname(HERE)                      # chartchampions/
NOTES= os.path.join(CC, "notes")
ASSETS = os.path.join(HERE, "assets")
VAULT = os.path.expanduser("~/Downloads/ChartChampions-Vault")
CHEATS = os.path.join(VAULT, "cheat-sheets")
OUT = os.path.join(VAULT, "library")
THUMBS = os.path.join(OUT, "thumbs")
FORCE = "--force" in sys.argv
TODAY = "2026-11-12"

TRACKS = [
    ("Foundation", ["basic", "entering-trades", "beginners", "trading-psychology"]),
    ("Technical Mastery", ["indicators-and-tools", "fibonacci", "channels-and-patterns",
                           "beginner-strategies", "intermediate-strategies"]),
    ("Platform Mastery", ["orderflow-and-footprint", "atas", "quantower"]),
    ("Market Mastery", ["building-plans-and-context", "tpo-series", "futures-and-stocks",
                        "advanced-strategies", "statistics-and-journaling", "elliott-waves"]),
]
TRACK_OF = {s: name for name, slugs in TRACKS for s in slugs}

DIAGRAMS = [
    ("severin-flow", "Severin — daily decision flow",
     "The order to run every session; any failed step means stand aside."),
    ("three-candle-entry", "3-candle entry: COT → COC → Momentum",
     "The near-mechanical long trigger (mirror for shorts)."),
    ("range-tool-025", "0.25 Range Tool — where you may trade",
     "Quarter the range; never chase the extreme quarter; 0.5 is no-man's-land."),
    ("value-area", "Value area — VAH / POC / VAL rules",
     "Short the high, long the low, never the middle; naked-POC and the CCV 80% rule."),
    ("day-types", "Six market-profile day types",
     "Read the shape to know whether to fade edges, follow the trend, or expect a range."),
]

CATEGORIES = [
    "Severin & core method", "Market structure & levels", "Volume & order flow",
    "VWAP & moving averages", "Fibonacci & patterns", "Strategies & setups",
    "Futures, stocks & crypto", "Foundation & meta",
]
# keyword -> category  (checked in order; first hit wins)
CAT_RULES = [
    (["severin", "scalping", "trade entry checklist", "trading setup", "build your own",
      "four box", "4 box", "profile trading", "master instruction"], "Severin & core method"),
    (["market structure", "support", "resistance", "level flip", "pdhigh", "pdlow",
      "daily open", "consolidation", "s/r indicator"], "Market structure & levels"),
    (["volume profile", "case study", "tpo", "single prints", "fair value", "footprint",
      "delta", "cvd", "imbalance", "open interest", "obv", "divergence", "igor time"], "Volume & order flow"),
    (["vwap", "50 sma", "ema", "log scale"], "VWAP & moving averages"),
    (["fibonacci", "harmonic", "three drives", "pitchfork", "channel", "bull and bear",
      "wedge", "triangle", "trendline", "head and shoulder", "bump", "pivot", "elliot"], "Fibonacci & patterns"),
    (["entry trigger", "liquidity", "if one then two", "frontrun", "order block", "sequence",
      "cctr", "ccss", "ccv", "orb", "opening range", "all time high", "ath", "accumulation",
      "distribution", "supply and demand", "hedging"], "Strategies & setups"),
    (["futures", "es/nq", "swing", "correlation", "altcoin"], "Futures, stocks & crypto"),
]
# keyword -> related note slug
NOTE_RULES = [
    (["severin", "scalping", "trade entry", "trading setup", "build your own", "four box", "4 box", "master instruction"], "building-plans-and-context"),
    (["tpo", "single prints", "fair value", "ledge", "rotation", "igor time", "case study"], "tpo-series"),
    (["footprint", "delta", "cvd", "imbalance"], "orderflow-and-footprint"),
    (["volume profile", "vwap", "obv", "open interest", "divergence", "log scale"], "indicators-and-tools"),
    (["fibonacci", "harmonic", "three drives"], "fibonacci"),
    (["channel", "wedge", "triangle", "trendline", "head and shoulder", "bump", "pitchfork", "flag", "pivot"], "channels-and-patterns"),
    (["elliot"], "elliott-waves"),
    (["cctr", "order block", "frontrun", "if one then two", "failed auction", "entry trigger",
      "sequence", "accumulation", "distribution", "supply and demand", "level 2", "liquidity"], "advanced-strategies"),
    (["ccw", "50 sma", "ema", "daily open"], "beginner-strategies"),
    (["ccss", "ccv", "fixed range"], "intermediate-strategies"),
    (["orb", "opening range", "all time high", "ath", "how to start trading", "futures", "es/nq", "profile trading"], "futures-and-stocks"),
    (["market structure", "support", "resistance", "level flip", "consolidation", "candlestick", "pdhigh", "pdlow", "s/r"], "basic"),
    (["statistics", "how to record", "correlation", "swing"], "statistics-and-journaling"),
    (["position size", "leverage", "types of traders", "how to spot a trend"], "beginners"),
]

ACRONYMS = {"cc", "ccv", "cctr", "ccss", "ccw", "tpo", "cvd", "obv", "vwap", "ema", "sma",
            "ath", "atl", "es", "nq", "orb", "htf", "ltf", "sfp", "oi", "dd", "s/r", "atas",
            "dsf", "dom", "poc", "vah", "val", "rf", "ib", "atr", "rsi", "macd", "amt"}

def slugify(s):
    s = unicodedata.normalize("NFKD", s).encode("ascii", "ignore").decode()
    s = re.sub(r"[^a-zA-Z0-9]+", "-", s).strip("-").lower()
    return re.sub(r"-+", "-", s)

def clean_title(fn):
    t = fn[:-4] if fn.lower().endswith(".pdf") else fn
    # fix mojibake / smart quotes
    t = unicodedata.normalize("NFKD", t)
    t = t.replace("’", "'").replace("‘", "'")
    t = t.encode("ascii", "ignore").decode()
    # strip known prefixes/suffixes
    for p in ["CC Cheat Sheets -", "CC Cheatsheets -", "CC_Cheatsheets_-_", "cc-cheatsheets-", "cc_cheatsheets_-_"]:
        if t.lower().startswith(p.lower()):
            t = t[len(p):]
            break
    t = t.replace("_", " ").replace("-", " ")
    t = re.sub(r"\bES NQ\b", "ES/NQ", t)
    t = re.sub(r"\bS R\b", "S/R", t)
    t = re.sub(r"(?i)\bcheat ?sheets?\b", "", t)
    t = re.sub(r"\s+b\s*$", "", t)          # trailing "-b"
    t = re.sub(r"\s+", " ", t).strip()
    # capitalise words, keep acronyms
    out = []
    for w in t.split(" "):
        lw = w.lower()
        if lw in ACRONYMS:
            out.append("S/R" if lw == "s/r" else w.upper())
        elif w.isupper() and len(w) <= 4:
            out.append(w)
        else:
            out.append(w[:1].upper() + w[1:] if w else w)
    return " ".join(out) or fn

def categorize(title):
    low = title.lower()
    for kws, cat in CAT_RULES:
        if any(k in low for k in kws):
            return cat
    return "Foundation & meta"

def related_note(title):
    low = title.lower()
    for kws, slug in NOTE_RULES:
        if any(k in low for k in kws):
            return slug
    return None

def note_title(markdown, slug):
    m = re.search(r"^#\s+(.+)$", markdown, re.M)
    t = m.group(1).strip() if m else slug
    t = re.sub(r"^Module notes:\s*", "", t)
    return t

def read(path):
    with open(path, encoding="utf-8") as f:
        return f.read()

def main():
    if not os.path.isdir(CHEATS):
        sys.exit("Cheat-sheet folder not found: " + CHEATS)
    os.makedirs(THUMBS, exist_ok=True)

    # ---- notes ----
    notes = {}
    for name, slugs in TRACKS:
        for slug in slugs:
            p = os.path.join(NOTES, slug + ".md")
            if not os.path.exists(p):
                print("  ! missing note:", slug); continue
            mdtext = read(p)
            title = note_title(mdtext, slug)
            nav = re.sub(r"\s*\(.*?\)\s*$", "", title).strip()
            notes[slug] = {"title": title, "nav": nav, "track": TRACK_OF.get(slug, ""),
                           "markdown": mdtext, "cheats": []}

    # ---- cheat sheets + thumbnails ----
    pdfs = sorted(f for f in os.listdir(CHEATS) if f.lower().endswith(".pdf"))
    cheatsheets, made, cached = [], 0, 0
    for fn in pdfs:
        title = clean_title(fn)
        sid = slugify(title) or slugify(fn)
        thumb = sid + ".png"
        tp = os.path.join(THUMBS, thumb)
        if FORCE or not os.path.exists(tp):
            try:
                subprocess.run(["pdftoppm", "-png", "-singlefile", "-f", "1", "-l", "1",
                                "-scale-to", "520", os.path.join(CHEATS, fn), tp[:-4]],
                               check=True, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
                made += 1
            except Exception as e:
                print("  ! thumb failed:", fn, e); continue
        else:
            cached += 1
        rel = related_note(title)
        cheatsheets.append({"id": sid, "title": title, "file": fn, "thumb": thumb,
                            "category": categorize(title), "note": rel})
        if rel and rel in notes:
            notes[rel]["cheats"].append(sid)

    # order cheat sheets by category, then title
    cheatsheets.sort(key=lambda c: (CATEGORIES.index(c["category"]) if c["category"] in CATEGORIES else 99,
                                    c["title"].lower()))

    # ---- diagrams (inline svg) ----
    diagrams = []
    for did, dtitle, ddesc in DIAGRAMS:
        sp = os.path.join(ASSETS, "diagrams", did + ".svg")
        if os.path.exists(sp):
            diagrams.append({"id": did, "title": dtitle, "desc": ddesc, "svg": read(sp)})

    # ---- pages ----
    def page(path, title, desc=""):
        if not os.path.exists(path):
            return None
        return {"title": title, "desc": desc, "markdown": read(path)}

    pages = {
        "playbook": page(os.path.join(CC, "playbook.md"), "Playbook",
                         "The synthesized daily decision flow — the thing to actually run."),
        "backlog": page(os.path.join(CC, "indicator-backlog.md"), "Indicator backlog",
                        "Severin's method mapped to candidate ATAS / TradingView indicators."),
        "glossary": page(os.path.join(NOTES, "glossary.md"), "Glossary",
                         "Every ChartChampions term and abbreviation, with the lesson it is taught in."),
        "vault": page(os.path.join(CC, "vault-manifest.md"), "Vault index",
                      "Every downloadable template, workspace and daily sheet in the member Vault."),
    }

    data = {
        "generated": TODAY,
        "tracks": [{"name": n, "modules": [s for s in slugs if s in notes]} for n, slugs in TRACKS],
        "notes": notes,
        "pages": pages,
        "cheatsheets": cheatsheets,
        "categories": CATEGORIES,
        "diagrams": diagrams,
    }

    # ---- write output ----
    for f in ["index.html", "style.css", "app.js", "marked.min.js"]:
        shutil.copyfile(os.path.join(ASSETS, f), os.path.join(OUT, f))
    with open(os.path.join(OUT, "data.js"), "w", encoding="utf-8") as f:
        f.write("window.CCDATA = " + json.dumps(data, ensure_ascii=False) + ";\n")

    print("Library built -> %s" % OUT)
    print("  modules: %d   cheat sheets: %d (thumbs: %d new, %d cached)   diagrams: %d"
          % (len(notes), len(cheatsheets), made, cached, len(diagrams)))
    # category tally
    from collections import Counter
    for cat, n in Counter(c["category"] for c in cheatsheets).most_common():
        print("    %-26s %d" % (cat, n))

if __name__ == "__main__":
    main()
