#!/usr/bin/env python3
"""Bundle the multi-file CC library into ONE self-contained HTML file.
Inlines css + marked + data + app + all thumbnails (as data URIs).
PDFs stay as relative links (cheat-sheets/...), so put the output at the Vault root.
Leaves the original library/ untouched.
"""
import base64, pathlib, mimetypes, sys

VAULT = pathlib.Path("/Users/lroy/Downloads/ChartChampions-Vault")
LIB = VAULT / "library"
OUT = VAULT / "ChartChampions-Library.html"

def read(p): return (LIB / p).read_text(encoding="utf-8")

css   = read("style.css")
marked= read("marked.min.js")
data  = read("data.js")
app   = read("app.js")
index = read("index.html")

# --- build thumbnail data-URI map ---
thumbs = {}
for png in sorted((LIB / "thumbs").glob("*.png")):
    b64 = base64.b64encode(png.read_bytes()).decode("ascii")
    thumbs[png.name] = "data:image/png;base64," + b64
thumbs_js = "window.CC_THUMBS = {\n" + ",\n".join(
    f'  {k!r}: "{v}"' for k, v in thumbs.items()) + "\n};"

# --- patch app.js: PDFs relative to Vault root, thumbs from the inlined map ---
app = app.replace('"../cheat-sheets/"', '"cheat-sheets/"')
old_img = '''src="thumbs/' + esc(c.thumb) + '"'''
new_img = '''src="' + ((window.CC_THUMBS && window.CC_THUMBS[c.thumb]) || ("thumbs/" + esc(c.thumb))) + '"'''
assert old_img in app, "thumb src pattern not found in app.js"
app = app.replace(old_img, new_img)

def safe(js):  # avoid breaking out of the inline <script>
    return js.replace("</script", "<\\/script")

# --- assemble the single file from index.html ---
assert '<link rel="stylesheet" href="style.css">' in index
index = index.replace('<link rel="stylesheet" href="style.css">',
                      "<style>\n" + css + "\n</style>")

scripts_old = ('<script src="marked.min.js"></script>\n'
               '<script src="data.js"></script>\n'
               '<script src="app.js"></script>')
assert scripts_old in index, "script block not found in index.html"
scripts_new = (
    "<script>\n" + safe(thumbs_js) + "\n</script>\n"
    "<script>\n" + safe(marked)   + "\n</script>\n"
    "<script>\n" + safe(data)     + "\n</script>\n"
    "<script>\n" + safe(app)      + "\n</script>"
)
index = index.replace(scripts_old, scripts_new)

OUT.write_text(index, encoding="utf-8")
mb = OUT.stat().st_size / 1_048_576
print(f"wrote {OUT}  ({mb:.2f} MB, {len(thumbs)} thumbs inlined)")
print("sanity:",
      "CC_THUMBS" in index,
      "window.CCDATA" in index,
      "<link" not in index,
      'src="data.js"' not in index)
