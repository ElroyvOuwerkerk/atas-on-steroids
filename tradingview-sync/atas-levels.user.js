// ==UserScript==
// @name         ATAS Levels → TradingView
// @namespace    atas-on-steroids
// @version      3.12
// @description  Auto-syncs ATAS-drawn levels onto TradingView. The Pine indicator is symbol-aware (one config holds both ES and NQ and self-selects), so this only fills it when you save the workspace in ATAS — no action on ticker switches.
// @match        https://www.tradingview.com/chart/*
// @match        https://*.tradingview.com/chart/*
// @grant        GM_xmlhttpRequest
// @grant        GM_setClipboard
// @connect      localhost
// @run-at       document-idle
// ==/UserScript==

(function () {
  'use strict';

  const VERSION = '3.12';
  const BACKEND = 'http://localhost:3001';
  const POLL_MS = 15000;   // levels change only on workspace save; 15s is plenty

  let combined = '';   // current backend feed (all families)
  let counts = {};     // family -> object count, for the widget
  let lastApplied = '';
  let applying = false;

  const autoKey = 'atasLevels.auto';
  const isAuto = () => localStorage.getItem(autoKey) !== '0';
  const setAuto = (v) => localStorage.setItem(autoKey, v ? '1' : '0');

  const ep = (iso) => { if (!iso) return 0; const d = new Date(iso); return isNaN(d) ? 0 : Math.floor(d.getTime() / 1000); };

  function fetchAll() {
    return new Promise((resolve) => {
      GM_xmlhttpRequest({
        method: 'GET',
        url: `${BACKEND}/api/levels`,
        onload: (r) => {
          try {
            const data = JSON.parse(r.responseText);
            const sets = (data.sets || []);
            const recs = [];
            const cnt = {};
            for (const s of sets) {
              cnt[s.symbol] = (s.levels || []).length;
              for (const l of s.levels || []) {
                recs.push(
                  `${s.symbol}|${l.kind || 'L'}|${l.price}|${l.price2 || 0}` +
                  `|${(l.color || '#FFFFFF').replace('#', '').toUpperCase()}|${l.fill || ''}` +
                  `|${l.name || ''}|${ep(l.start)}|${ep(l.end)}`);
              }
            }
            resolve({ csv: recs.join(';'), counts: cnt });
          } catch (e) { resolve({ csv: '', counts: {} }); }
        },
        onerror: () => resolve({ csv: '', counts: {} }),
      });
    });
  }

  // ── Drive TradingView's UI to fill the indicator input ──
  const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
  async function waitFor(fn, timeoutMs) {
    const t0 = Date.now();
    while (Date.now() - t0 < timeoutMs) { const v = fn(); if (v) return v; await sleep(120); }
    return null;
  }
  // Full pointer+mouse sequence — TradingView ignores a bare .click().
  function realClick(el) {
    const o = { bubbles: true, cancelable: true };
    try { el.dispatchEvent(new PointerEvent('pointerdown', o)); } catch (e) { /* older browsers */ }
    el.dispatchEvent(new MouseEvent('mousedown', o));
    try { el.dispatchEvent(new PointerEvent('pointerup', o)); } catch (e) {}
    el.dispatchEvent(new MouseEvent('mouseup', o));
    el.dispatchEvent(new MouseEvent('click', o));
  }
  // Fill a textarea the way a real paste does. TradingView's editor ignores a
  // programmatically-set .value, so use the browser's native insertText (fires
  // a trusted input event the editor accepts); fall back to the React setter.
  function fillTextarea(ta, value, t) {
    ta.focus();
    try { ta.select(); } catch (e) {}
    let ok = false;
    try { ok = document.execCommand('insertText', false, value); } catch (e) {}
    if (!ok || ta.value !== value) {
      const setter = Object.getOwnPropertyDescriptor(window.HTMLTextAreaElement.prototype, 'value').set;
      setter.call(ta, value);
      ta.dispatchEvent(new InputEvent('input', { bubbles: true, inputType: 'insertText', data: value }));
    }
    ta.dispatchEvent(new Event('change', { bubbles: true }));
    if (t) t.push(`fill: ${ta.value.length}/${value.length} chars` + (ta.value === value ? ' ok' : ' MISMATCH'));
  }

  // Find the textarea that belongs to OUR indicator's settings dialog — the one
  // whose surrounding panel mentions "ATAS Levels" / the Levels input label.
  // (Avoids latching onto a stray dialog, e.g. the timeframe settings.)
  function findInput() {
    for (const ta of document.querySelectorAll('textarea')) {
      const dlg = ta.closest('[role="dialog"]') || ta.closest('[data-name*="dialog" i]');
      let ctx = dlg ? dlg.textContent || '' : '';
      if (!dlg) { let p = ta; for (let i = 0; i < 10 && p; i++) p = p.parentElement; ctx = (p || document).textContent || ''; }
      if (ctx.includes('ATAS Levels') || ctx.includes('Levels (paste')) return ta;
    }
    return null;
  }
  const anyDialogOpen = () => !!document.querySelector('[role="dialog"], [data-dialog-name]');
  const userIsTyping = () => {
    const a = document.activeElement;
    return a && (a.tagName === 'INPUT' || a.tagName === 'TEXTAREA' || a.isContentEditable);
  };
  // Returns all candidate legend-title elements that read "ATAS Levels",
  // trying several selectors since TradingView's DOM names drift.
  function findLegendTitles(trace) {
    const out = [];
    const sel = [
      '[data-name="legend-source-title"]',
      '[class*="title-"][class*="legend"]',
      '[data-name="legend"] [class*="title"]',
    ];
    for (const s of sel) {
      const hits = [...document.querySelectorAll(s)];
      if (trace) trace.push(`selector ${s}: ${hits.length} els`);
      for (const el of hits) if (el.textContent.includes('ATAS Levels')) out.push(el);
    }
    if (!out.length) {
      // last resort: any leaf element whose exact text is the title
      const leaves = [...document.querySelectorAll('span,div')]
        .filter((e) => e.childElementCount === 0 && e.textContent.trim() === 'ATAS Levels');
      if (trace) trace.push(`leaf exact-text "ATAS Levels": ${leaves.length}`);
      out.push(...leaves);
    }
    return out;
  }

  function hover(el) {
    for (const tp of ['pointerover', 'pointerenter', 'mouseover', 'mouseenter', 'mousemove']) {
      try { el.dispatchEvent(new MouseEvent(tp, { bubbles: true })); } catch (e) {}
    }
  }

  // Open the indicator's settings panel by any means that works; returns the
  // textarea once the panel is up, else null. Logs which method succeeded.
  async function openSettings(titleEl, t) {
    // The indicator's own settings gear is labelled exactly "Settings"
    // (NOT "Show interval settings", which is the chart timeframe control).
    const isGear = (b) => /^settings$/i.test((b.getAttribute('aria-label') || b.title || '').trim());

    // Climb to the SMALLEST ancestor that contains this row's own gear — that
    // scopes us to the ATAS Levels legend item, not the whole legend.
    let row = titleEl;
    let gear = null;
    for (let i = 0; i < 12 && row.parentElement && !gear; i++) {
      row = row.parentElement;
      gear = [...row.querySelectorAll('button, [role="button"]')].find(isGear) || null;
    }
    hover(row);
    if (gear) hover(gear);
    await sleep(200);
    t.push('gear (exact "Settings"): ' + (gear ? 'found' : 'NONE'));
    if (!gear) {
      // diagnostic if the exact match failed
      const labels = [...row.querySelectorAll('button,[role="button"]')]
        .map((b) => b.getAttribute('aria-label') || b.title || '·');
      t.push('row buttons: ' + (labels.slice(0, 16).join(', ') || '(none)'));
    }

    const tryOpen = async (label, fn) => {
      try { await fn(); } catch (e) { t.push(`${label} threw: ${e.message}`); return null; }
      const ta = await waitFor(findInput, 1400);
      t.push(`${label}: ${ta ? 'OPENED' : 'no'}`);
      return ta;
    };

    let ta = null;
    if (gear) {
      ta = ta || await tryOpen('gear realClick', () => { hover(gear); realClick(gear); });
      const inner = gear.querySelector('svg, span, div');
      if (!ta && inner) ta = await tryOpen('gear inner realClick', () => realClick(inner));
    }
    if (!ta) ta = await tryOpen('title dbl-realClick', () => { realClick(titleEl); realClick(titleEl); });
    if (!ta) ta = await tryOpen('row dbl-realClick', () => { realClick(row); realClick(row); });
    return ta;
  }

  async function applyToChart(csv, trace) {
    const t = trace || [];
    const titles = findLegendTitles(t);
    t.push(`title matches: ${titles.length}`);
    if (!titles.length) return { ok: false, why: '"ATAS Levels" title not found on page', trace: t };

    const textarea = await openSettings(titles[0], t);
    if (!textarea) return { ok: false, why: 'settings panel would not open (see methods above)', trace: t };

    fillTextarea(textarea, csv, t);
    await sleep(150);

    // OK button: scope to the panel containing the textarea, else whole document.
    const panel = textarea.closest('[role="dialog"]') || textarea.closest('[data-name*="dialog" i]') || document;
    const buttons = [...panel.querySelectorAll('button')];
    const ok = panel.querySelector('[data-name="submit-button"]')
          || buttons.find((b) => /^(ok|apply)$/i.test(b.textContent.trim()));
    t.push(`buttons: [${buttons.map((b) => b.textContent.trim()).filter(Boolean).slice(0, 12).join(', ')}]`);
    t.push(`OK button: ${ok ? 'yes' : 'no'}`);
    if (!ok) return { ok: false, why: 'OK button not found', trace: t };
    realClick(ok);
    await sleep(400);
    saveLayout(t);   // persist into the chart layout so it survives reloads / shows everywhere
    return { ok: true, trace: t };
  }

  // Persist the chart layout server-side so the desktop app / other devices
  // pick it up. A synthetic Cmd+S doesn't trigger TradingView's save, so click
  // the real layout Save control in the top header. Logs candidates so we can
  // lock the exact selector.
  function saveLayout(t) {
    // cheap shortcut attempt first (harmless if ignored)
    for (const mods of [{ metaKey: true }, { ctrlKey: true }]) {
      const init = { key: 's', code: 'KeyS', keyCode: 83, which: 83, bubbles: true, cancelable: true, ...mods };
      document.dispatchEvent(new KeyboardEvent('keydown', init));
      document.dispatchEvent(new KeyboardEvent('keyup', init));
    }

    const header = document.querySelector('#header-toolbar, [class*="headerToolbar" i], [class*="header-toolbar" i]') || document;
    const labelOf = (e) => ((e.getAttribute('aria-label') || '') + ' ' + (e.getAttribute('data-name') || '') + ' ' + (e.title || '')).trim();
    const cands = [...header.querySelectorAll('button, [role="button"], [data-name]')]
      .filter((e) => /save/i.test(labelOf(e)));
    if (t) t.push('save candidates: ' + (cands.map((e) => e.getAttribute('data-name') || e.getAttribute('aria-label') || e.title).filter(Boolean).slice(0, 8).join(' | ') || 'NONE'));

    // Prefer a direct layout-save control; avoid "save as", template, "save-load" menu.
    const btn = cands.find((e) => /(^|[^-])save-?(chart-?)?layout/i.test(e.getAttribute('data-name') || ''))
             || cands.find((e) => /^save$/i.test((e.getAttribute('aria-label') || '').trim()) && !/template|as/i.test(labelOf(e)))
             || cands.find((e) => /save/i.test(e.getAttribute('data-name') || '') && !/load|as|template/i.test(e.getAttribute('data-name') || ''))
             || cands[0];
    if (btn) {
      try { realClick(btn); if (t) t.push('clicked save: ' + (btn.getAttribute('data-name') || btn.getAttribute('aria-label') || btn.title || '?')); }
      catch (e) { if (t) t.push('save click threw: ' + e.message); }
    } else if (t) {
      t.push('no save button matched');
    }
  }

  async function autoTick() {
    if (!isAuto() || applying) return;
    if (!combined || combined === lastApplied) return;     // nothing new
    if (anyDialogOpen() || userIsTyping()) return;          // be polite; retry next tick
    applying = true;
    try {
      const res = await applyToChart(combined);
      if (res.ok) { lastApplied = combined; flash(`✓ levels applied`); }
      else flash(`auto-apply failed: ${res.why}`);
    } finally { applying = false; render(); }
  }

  // ── Widget ──
  let host;
  const BTN = 'background:#2d6cdf;color:#fff;border:0;border-radius:6px;padding:4px 10px;cursor:pointer;font-size:12px';

  function buildWidget() {
    host = document.createElement('div');
    host.id = 'atas-levels-widget';
    host.style.cssText = [
      'position:fixed', 'right:12px', 'bottom:12px', 'z-index:2147483647',
      'background:#17171d', 'color:#e8e8ea', 'border:1px solid #2c2c36',
      'border-radius:10px', 'padding:10px 12px', 'font:12px -apple-system,system-ui,sans-serif',
      'box-shadow:0 6px 24px rgba(0,0,0,.4)', 'min-width:220px',
    ].join(';');
    document.body.appendChild(host);
    render();
  }

  function render() {
    if (!host) return;
    const synced = combined && combined === lastApplied;
    const families = Object.keys(counts).sort();
    const countStr = families.length ? families.map((f) => `${f} ${counts[f]}`).join('  ') : 'no data';
    host.innerHTML = `
      <div style="display:flex;justify-content:space-between;align-items:center;margin-bottom:6px;">
        <b>ATAS Levels <span style="color:#6f6f78;font-weight:400;">v${VERSION}</span></b>
        <label style="display:flex;align-items:center;gap:4px;color:#9a9aa2;cursor:pointer;">
          <input type="checkbox" id="atas-auto" ${isAuto() ? 'checked' : ''}> auto
        </label>
      </div>
      <div style="color:#9a9aa2;margin-bottom:6px;">${countStr} <span style="color:${synced ? '#3ec27a' : '#c9a23e'}">${synced ? '✓ synced' : '… pending'}</span></div>
      <div style="display:flex;gap:6px;">
        <button id="atas-copy" style="${BTN}">Copy all</button>
        <button id="atas-push" style="${BTN};background:#3a3a44;">Push now</button>
      </div>
      <div id="atas-status" style="color:#6f6f78;font-size:10px;margin-top:6px;min-height:12px;"></div>`;

    host.querySelector('#atas-auto').onchange = (e) => { setAuto(e.target.checked); render(); };
    host.querySelector('#atas-copy').onclick = () => { GM_setClipboard(combined); flash('copied — paste into the indicator'); };
    host.querySelector('#atas-push').onclick = async () => {
      const trace = [];
      let res;
      try {
        res = await applyToChart(combined, trace);
      } catch (e) {
        res = { ok: false, why: 'exception: ' + (e && e.message), trace };
      }
      if (res.ok) {
        lastApplied = combined;
        flash('✓ pushed & saved');
      } else {
        // Only surface the full diagnostic when something actually fails.
        flash(`push failed: ${res.why}`);
        alert('ATAS Levels v' + VERSION + ' — Push failed\n\n' + (res.trace || trace).join('\n') +
              '\n\nRESULT: ' + res.why);
      }
      render();
    };
  }

  let flashTimer;
  function flash(msg) {
    const el = document.getElementById('atas-status');
    if (!el) return;
    el.textContent = msg;
    clearTimeout(flashTimer);
    flashTimer = setTimeout(() => { el.textContent = ''; }, 6000);
  }

  async function refresh() {
    const { csv, counts: c } = await fetchAll();
    const changed = csv !== combined || JSON.stringify(c) !== JSON.stringify(counts);
    combined = csv; counts = c;
    if (changed) render();   // only redraw the widget when something actually changed
    await autoTick();        // autoTick is a no-op unless the feed differs from what's applied
  }

  buildWidget();
  refresh();
  setInterval(refresh, POLL_MS);
})();
