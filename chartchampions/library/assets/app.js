/* ChartChampions Knowledge Library — local, offline SPA */
(function () {
  "use strict";
  var D = window.CCDATA || {};
  var content = document.getElementById("content");
  var nav = document.getElementById("nav");
  var searchEl = document.getElementById("search");
  var sideFoot = document.getElementById("side-foot");

  if (window.marked) marked.setOptions({ gfm: true, breaks: false, headerIds: false, mangle: false });
  var md = function (s) { try { return marked.parse(s || ""); } catch (e) { return "<pre>" + esc(s) + "</pre>"; } };
  function esc(s) { return (s || "").replace(/[&<>"]/g, function (c) { return ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;" })[c]; }); }
  function pdfPath(file) { return "../cheat-sheets/" + encodeURIComponent(file); }

  /* ---------- navigation ---------- */
  function navItem(label, route, count) {
    return '<button class="nav-item" data-route="' + route + '"><span class="dot"></span><span>' + esc(label) + "</span>" +
      (count != null ? '<span class="nav-count">' + count + "</span>" : "") + "</button>";
  }
  function buildNav() {
    var h = "";
    h += '<div class="nav-group">' + navItem("Home", "/") + "</div>";
    h += '<div class="nav-group"><div class="nav-label">Start here</div>' +
      navItem("Playbook", "/playbook") + navItem("Setup diagrams", "/diagrams", (D.diagrams || []).length) + "</div>";
    (D.tracks || []).forEach(function (tr) {
      h += '<div class="nav-group"><div class="nav-label">' + esc(tr.name) + '</div>';
      tr.modules.forEach(function (slug) {
        var n = D.notes[slug]; if (n) h += navItem(n.nav, "/note/" + slug);
      });
      h += "</div>";
    });
    h += '<div class="nav-group"><div class="nav-label">Reference</div>' +
      navItem("Cheat sheets", "/cheats", (D.cheatsheets || []).length) +
      navItem("Glossary", "/glossary") +
      navItem("Indicator backlog", "/backlog") +
      navItem("Vault index", "/vault") +
      navItem("About this library", "/about") + "</div>";
    nav.innerHTML = h;
    nav.querySelectorAll(".nav-item").forEach(function (b) {
      b.addEventListener("click", function () { go(b.dataset.route); });
    });
    sideFoot.innerHTML = "Generated " + esc(D.generated || "") + " · " + (D.cheatsheets || []).length +
      " cheat sheets · " + Object.keys(D.notes || {}).length + " modules<br>Private study — not for redistribution.";
  }
  function markActive(route) {
    nav.querySelectorAll(".nav-item").forEach(function (b) {
      b.classList.toggle("active", b.dataset.route === route);
    });
  }

  /* ---------- routing ---------- */
  function go(route) { location.hash = "#" + route; }
  function currentRoute() {
    var h = location.hash.replace(/^#/, "");
    return h || "/";
  }
  window.addEventListener("hashchange", render);

  function head(eyebrow, title, desc) {
    return '<div class="page-head">' +
      (eyebrow ? '<div class="eyebrow">' + esc(eyebrow) + "</div>" : "") +
      '<div class="page-title">' + esc(title) + "</div>" +
      (desc ? '<div class="page-desc">' + esc(desc) + "</div>" : "") + "</div>";
  }

  function render() {
    if (searchEl.value.trim().length >= 2) return renderSearch(searchEl.value.trim());
    var r = currentRoute();
    markActive(r);
    content.scrollTop = 0;
    content.focus({ preventScroll: true });
    document.body.classList.remove("nav-open");

    if (r === "/") return renderHome();
    if (r === "/playbook") return renderDoc("Start here", D.pages.playbook);
    if (r === "/backlog") return renderDoc("Reference", D.pages.backlog);
    if (r === "/glossary") return renderDoc("Reference", D.pages.glossary);
    if (r === "/vault") return renderDoc("Reference", D.pages.vault);
    if (r === "/about") return renderAbout();
    if (r === "/diagrams") return renderDiagrams();
    if (r === "/cheats") return renderCheats();
    if (r.indexOf("/note/") === 0) return renderNote(r.slice(6));
    content.innerHTML = '<div class="wrap">' + head("", "Not found", "That page doesn't exist.") + "</div>";
  }

  /* ---------- views ---------- */
  function renderHome() {
    var tiles = [
      ["Playbook", "The daily decision flow to actually run.", "/playbook"],
      ["Setup diagrams", (D.diagrams || []).length + " quick-reference visuals of the core setups.", "/diagrams"],
      ["Cheat sheets", (D.cheatsheets || []).length + " of Severin's own slide decks.", "/cheats"],
      ["Glossary", "Every CC term and abbreviation, defined.", "/glossary"],
      ["Indicator backlog", "The method mapped to ATAS / TradingView indicators.", "/backlog"],
      ["Vault index", "Templates, workspaces and daily level sheets.", "/vault"]
    ];
    var tracksHtml = (D.tracks || []).map(function (tr) {
      return '<div class="related"><h3>' + esc(tr.name) + "</h3><div class=\"tiles\">" +
        tr.modules.map(function (slug) {
          var n = D.notes[slug]; if (!n) return "";
          return '<a class="tile" data-route="/note/' + slug + '"><div class="ti">' + esc(n.nav) +
            '</div><div class="tn">' + (n.cheats ? n.cheats.length + " cheat sheets" : "") + "</div></a>";
        }).join("") + "</div></div>";
    }).join("");
    content.innerHTML = '<div class="wrap">' +
      head("ChartChampions", "Knowledge Library",
        "Severin's trading method, distilled from the member portal into searchable notes, his own cheat-sheet decks, and quick-reference diagrams. Everything stays on this Mac — nothing is uploaded.") +
      '<div class="tiles">' + tiles.map(function (t) {
        return '<a class="tile" data-route="' + t[2] + '"><div class="ti">' + esc(t[0]) +
          '</div><div class="td">' + esc(t[1]) + "</div></a>";
      }).join("") + "</div>" +
      '<h2 style="margin-top:36px;font-size:18px;border-bottom:1px solid var(--line);padding-bottom:8px">All modules</h2>' +
      tracksHtml + "</div>";
    wireRoutes();
  }

  function renderDoc(eyebrow, doc) {
    if (!doc) { content.innerHTML = '<div class="wrap">' + head("", "Empty", "") + "</div>"; return; }
    content.innerHTML = '<div class="wrap">' + head(eyebrow, doc.title, doc.desc || "") +
      '<div class="md">' + md(doc.markdown) + "</div></div>";
    enhanceMd();
  }

  function renderNote(slug) {
    var n = D.notes[slug];
    if (!n) { content.innerHTML = '<div class="wrap">' + head("", "Not found", "") + "</div>"; return; }
    var related = "";
    if (n.cheats && n.cheats.length) {
      related = '<div class="related"><h3>Related cheat sheets</h3>' + cardGrid(n.cheats.map(csById)) + "</div>";
    }
    // strip the leading H1 from markdown (shown as page title)
    var body = (n.markdown || "").replace(/^#\s+.*\n/, "");
    content.innerHTML = '<div class="wrap">' + head(n.track, n.title, "") +
      related + '<div class="md">' + md(body) + "</div></div>";
    enhanceMd();
    wireCards();
  }

  function renderDiagrams() {
    var h = '<div class="wrap">' + head("Start here", "Setup diagrams",
      "Hand-drawn quick-reference cards for the signature mechanical setups. For the full visual decks, see the cheat sheets.");
    (D.diagrams || []).forEach(function (d) {
      h += '<div class="diagram" id="' + esc(d.id) + '"><div style="font-weight:700;margin-bottom:10px">' + esc(d.title) + "</div>" +
        d.svg + (d.desc ? '<div class="dc">' + esc(d.desc) + "</div>" : "") + "</div>";
    });
    content.innerHTML = h + "</div>";
  }

  function renderCheats() {
    var byCat = {};
    (D.cheatsheets || []).forEach(function (c) { (byCat[c.category] = byCat[c.category] || []).push(c); });
    var h = '<div class="wrap">' + head("Reference", "Cheat sheets",
      "Severin's own slide decks — click any card to page through the full PDF. These are your member downloads, kept local.");
    (D.categories || Object.keys(byCat)).forEach(function (cat) {
      var list = byCat[cat]; if (!list || !list.length) return;
      h += '<div class="cat-head"><h2>' + esc(cat) + '</h2><span class="c">' + list.length + "</span></div>" + cardGrid(list);
    });
    content.innerHTML = h + "</div>";
    wireCards();
  }

  function renderAbout() {
    var m = "This is a private, offline knowledge base built from a ChartChampions membership for one member's own study. " +
      "It is **not** a copy of the course videos and is **not for redistribution**.\n\n" +
      "## What's inside\n" +
      "- **" + Object.keys(D.notes || {}).length + " module notes** — the concrete rules distilled from every course module, plus the glossary.\n" +
      "- **" + (D.cheatsheets || []).length + " cheat-sheet decks** — Severin's own PDF slide decks (your Vault downloads), shown as a gallery.\n" +
      "- **" + (D.diagrams || []).length + " setup diagrams** — hand-drawn quick-reference cards for the signature setups.\n\n" +
      "## Where things live\n" +
      "- This library and the cheat-sheet PDFs sit in `~/Downloads/ChartChampions-Vault/` — **local only, never uploaded**.\n" +
      "- The notes, the playbook and this generator live in the git repo at `chartchampions/`.\n" +
      "- Regenerate anytime with `python3 chartchampions/library/build.py` after the notes change.\n\n" +
      "## How to use it\n" +
      "- Press **/** anywhere to jump to search. It covers notes, glossary terms, cheat sheets and diagrams.\n" +
      "- Start at the **Playbook** for the daily flow, the **diagrams** for the setups at a glance, then drill into a module.\n\n" +
      "_Generated " + (D.generated || "") + "._";
    content.innerHTML = '<div class="wrap">' + head("", "About this library", "") + '<div class="md">' + md(m) + "</div></div>";
  }

  /* ---------- cards + lightbox ---------- */
  function csById(id) { return (D.cheatsheets || []).find(function (c) { return c.id === id; }); }
  function cardGrid(list) {
    return '<div class="grid">' + list.filter(Boolean).map(function (c) {
      return '<div class="card" data-cs="' + esc(c.id) + '">' +
        '<img class="thumb" loading="lazy" src="thumbs/' + esc(c.thumb) + '" alt="">' +
        '<div class="cap"><div class="t">' + esc(c.title) + '</div><div class="m">' + esc(c.category) + "</div></div></div>";
    }).join("") + "</div>";
  }
  function wireCards() {
    content.querySelectorAll(".card").forEach(function (el) {
      el.addEventListener("click", function () { openLightbox(csById(el.dataset.cs)); });
    });
  }
  var lb = document.getElementById("lightbox"),
    lbFrame = document.getElementById("lb-frame"),
    lbTitle = document.getElementById("lb-title"),
    lbOpen = document.getElementById("lb-open");
  function openLightbox(c) {
    if (!c) return;
    lbTitle.textContent = c.title;
    lbOpen.href = pdfPath(c.file);
    lbFrame.src = pdfPath(c.file);
    lb.hidden = false;
  }
  function closeLightbox() { lb.hidden = true; lbFrame.src = "about:blank"; }
  document.getElementById("lb-close").addEventListener("click", closeLightbox);
  lb.addEventListener("click", function (e) { if (e.target === lb) closeLightbox(); });

  /* ---------- search ---------- */
  var index = null;
  function buildIndex() {
    index = [];
    Object.keys(D.notes || {}).forEach(function (slug) {
      var n = D.notes[slug];
      index.push({ kind: "Module", title: n.title, route: "/note/" + slug, text: stripMd(n.markdown) });
    });
    [["Playbook", "/playbook", D.pages.playbook], ["Indicator backlog", "/backlog", D.pages.backlog],
     ["Vault index", "/vault", D.pages.vault]].forEach(function (p) {
      if (p[2]) index.push({ kind: p[0], title: p[2].title, route: p[1], text: stripMd(p[2].markdown) });
    });
    // glossary terms as individual entries
    if (D.pages.glossary) {
      var re = /^\*\*(.+?)\*\*\s*[—-]\s*([\s\S]*?)(?=\n\n|\n\*\*|$)/gm, m;
      while ((m = re.exec(D.pages.glossary.markdown))) {
        index.push({ kind: "Glossary", title: m[1].replace(/\*/g, ""), route: "/glossary", text: m[2].replace(/\s+/g, " ").trim() });
      }
    }
    (D.cheatsheets || []).forEach(function (c) {
      index.push({ kind: "Cheat sheet", title: c.title, cs: c.id, text: c.category });
    });
    (D.diagrams || []).forEach(function (d) {
      index.push({ kind: "Diagram", title: d.title, route: "/diagrams", text: d.desc || "" });
    });
  }
  function stripMd(s) {
    return (s || "").replace(/`{1,3}[^`]*`{1,3}/g, " ").replace(/[#*_>|\-]/g, " ")
      .replace(/\[([^\]]*)\]\([^)]*\)/g, "$1").replace(/\s+/g, " ").trim();
  }
  function renderSearch(q) {
    if (!index) buildIndex();
    markActive(null);
    var ql = q.toLowerCase(), terms = ql.split(/\s+/).filter(Boolean);
    var hits = [];
    index.forEach(function (it) {
      var hay = (it.title + " " + it.text).toLowerCase();
      var ok = terms.every(function (t) { return hay.indexOf(t) >= 0; });
      if (!ok) return;
      var score = 0;
      terms.forEach(function (t) { if (it.title.toLowerCase().indexOf(t) >= 0) score += 10; });
      if (it.title.toLowerCase() === ql) score += 50;
      hits.push({ it: it, score: score });
    });
    hits.sort(function (a, b) { return b.score - a.score; });
    var h = '<div class="wrap">' + head("Search", '"' + q + '"', hits.length + " result" + (hits.length === 1 ? "" : "s"));
    if (!hits.length) h += '<div class="empty">Nothing matched. Try fewer or different words.</div>';
    hits.slice(0, 80).forEach(function (r, i) {
      var it = r.it;
      h += '<button class="result" data-i="' + i + '"><span class="rk">' + esc(it.kind) + "</span>" +
        '<div class="rt">' + esc(it.title) + "</div>" +
        '<div class="rx">' + snippet(it.text, terms) + "</div></button>";
    });
    content.innerHTML = h + "</div>";
    var shown = hits.slice(0, 80);
    content.querySelectorAll(".result").forEach(function (el) {
      el.addEventListener("click", function () {
        var it = shown[+el.dataset.i].it;
        searchEl.value = "";
        if (it.cs) { openLightbox(csById(it.cs)); render(); }
        else go(it.route);
      });
    });
  }
  function snippet(text, terms) {
    if (!text) return "";
    var low = text.toLowerCase(), pos = -1;
    for (var i = 0; i < terms.length; i++) { pos = low.indexOf(terms[i]); if (pos >= 0) break; }
    if (pos < 0) pos = 0;
    var start = Math.max(0, pos - 60), s = (start > 0 ? "… " : "") + text.slice(start, start + 180) + "…";
    s = esc(s);
    terms.forEach(function (t) {
      s = s.replace(new RegExp("(" + t.replace(/[.*+?^${}()|[\]\\]/g, "\\$&") + ")", "ig"), "<mark>$1</mark>");
    });
    return s;
  }

  /* ---------- helpers ---------- */
  function wireRoutes() {
    content.querySelectorAll("[data-route]").forEach(function (el) {
      el.addEventListener("click", function (e) { e.preventDefault(); go(el.dataset.route); });
    });
  }
  function enhanceMd() {
    // open external links in new tab; keep internal anchors
    content.querySelectorAll(".md a").forEach(function (a) {
      var href = a.getAttribute("href") || "";
      if (/^https?:/.test(href)) { a.target = "_blank"; a.rel = "noopener"; }
    });
  }
  function wireCardsAndRoutes() { wireCards(); wireRoutes(); }

  /* ---------- keyboard + menu ---------- */
  var debounce;
  searchEl.addEventListener("input", function () {
    clearTimeout(debounce);
    debounce = setTimeout(render, 120);
  });
  document.addEventListener("keydown", function (e) {
    if (e.key === "/" && document.activeElement !== searchEl) { e.preventDefault(); searchEl.focus(); searchEl.select(); }
    else if (e.key === "Escape") {
      if (!lb.hidden) closeLightbox();
      else if (searchEl.value) { searchEl.value = ""; render(); searchEl.blur(); }
    }
  });
  document.getElementById("menu-toggle").addEventListener("click", function () {
    document.body.classList.toggle("nav-open");
  });

  /* ---------- boot ---------- */
  buildNav();
  render();
})();
