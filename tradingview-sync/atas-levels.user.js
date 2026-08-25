// ==UserScript==
// @name         ATAS Levels → TradingView
// @namespace    atas-on-steroids
// @version      3.19
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

  const VERSION = '3.19';
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
  function ctxAround(ta) {
    const dlg = ta.closest('[role="dialog"]') || ta.closest('[data-name*="dialog" i]');
    if (dlg) return dlg.textContent || '';
    let p = ta;
    for (let i = 0; i < 10 && p; i++) p = p.parentElement;
    return (p || document).textContent || '';
  }
  function isVisible(el) {
    const r = el.getBoundingClientRect();
    return r.width > 0 && r.height > 0;
  }
  function findInput() {
    const tas = [...document.querySelectorAll('textarea')].filter(isVisible);
    if (!tas.length) return null;
    // Prefer the textarea whose panel clearly belongs to our indicator.
    for (const ta of tas) {
      if (/ATAS Levels|Levels \(paste|Copy all/i.test(ctxAround(ta))) return ta;
    }
    // Fallback: any visible textarea that is NOT the timeframe/interval dialog.
    for (const ta of tas) {
      if (!/add custom interval|interval settings|\b30s\b|\b1m\b\s*\b5m\b/i.test(ctxAround(ta))) return ta;
    }
    return tas[0];
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

    // Detect the indicator settings dialog by its visible tabs — NOT [role=dialog]
    // (TradingView dropped that). Only the indicator settings has a "Visibility" tab.
    const visLeaf = (txt) => [...document.querySelectorAll('button, [role="tab"], span, div')]
      .find((e) => e.childElementCount === 0 && new RegExp('^' + txt + '$', 'i').test((e.textContent || '').trim()) && isVisible(e)) || null;
    const settingsOpen = () => (visLeaf('Visibility') && visLeaf('Inputs')) ? true : null;

    const tryOpen = async (label, fn) => {
      try { await fn(); } catch (e) { t.push(`${label} threw: ${e.message}`); return null; }
      const ok = await waitFor(settingsOpen, 1500);
      t.push(`${label}: dialog=${ok ? 'yes' : 'no'}`);
      return ok;
    };

    let opened = null;
    if (gear) opened = await tryOpen('gear', () => { hover(gear); realClick(gear); });
    if (!opened) opened = await tryOpen('title dbl', () => { realClick(titleEl); realClick(titleEl); });
    if (!opened) { t.push('settings dialog never opened'); return null; }

    // TradingView remembers the last tab, which may not be Inputs — the Levels
    // textarea only exists on the Inputs tab, so switch to it first.
    let ta = findInput();
    if (!ta) {
      const inputsTab = visLeaf('Inputs');
      t.push('Inputs tab: ' + (inputsTab ? 'found' : 'NOT found'));
      if (inputsTab) { realClick(inputsTab); await sleep(200); }
      ta = await waitFor(findInput, 1500);
    }
    t.push('textarea: ' + (ta ? 'yes' : 'no'));
    return ta;
  }

  async function applyToChart(csv, trace) {
    const t = trace || [];
    const all = findLegendTitles(t);
    if (!all.length) return { ok: false, why: '"ATAS Levels" title not found on page', trace: t };

    // A layout can hold several charts, each with its own ATAS Levels indicator.
    // Dedupe to one title per pane (by on-screen position) and fill them all.
    const seen = new Set();
    const titles = [];
    for (const el of all) {
      const r = el.getBoundingClientRect();
      const key = Math.round(r.left / 60) + ':' + Math.round(r.top / 60);
      if (seen.has(key)) continue;
      seen.add(key); titles.push(el);
    }
    t.push(`ATAS Levels panes: ${titles.length}`);

    let applied = 0;
    for (const title of titles) {
      const textarea = await openSettings(title, t);
      if (!textarea) { t.push('pane: settings would not open'); continue; }
      fillTextarea(textarea, csv, t);
      await sleep(150);
      const visBtns = [...document.querySelectorAll('button, [role="button"]')].filter(isVisible);
      const ok = document.querySelector('[data-name="submit-button"]')
            || visBtns.find((b) => /^(ok|apply)$/i.test((b.textContent || '').trim()));
      if (!ok) { t.push('pane: OK not found'); continue; }
      realClick(ok);
      await sleep(400);   // let the dialog close before the next pane
      applied++;
    }
    t.push(`panes applied: ${applied}/${titles.length}`);
    if (!applied) return { ok: false, why: 'no ATAS Levels indicator could be filled', trace: t };
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

  // Recreate the indicator's alert so its frozen input snapshot picks up the
  // current zones. Deletes existing "ATAS Levels" alerts, then creates a fresh
  // one via the legend's More menu → "Add alert on ATAS Levels" → Create.
  // Notifications default to the last-used config (set your sound once manually).
  async function refreshAlert(trace) {
    const t = trace || [];

    // 1. Best-effort delete of existing ATAS Levels alerts (avoid duplicates).
    await deleteAtasAlerts(t);

    // 2. Locate the legend row's "More" (⋯) button.
    const titles = findLegendTitles(t);
    if (!titles.length) return { ok: false, why: '"ATAS Levels" not on chart', trace: t };
    const isMore = (b) => /^(more|more actions|⋯|\.\.\.)$/i.test((b.getAttribute('aria-label') || b.title || '').trim())
                       || /more/i.test(b.getAttribute('data-name') || '');
    let row = titles[0], more = null;
    for (let i = 0; i < 12 && row.parentElement && !more; i++) {
      row = row.parentElement;
      hover(row);
      more = [...row.querySelectorAll('button, [role="button"]')].find(isMore) || null;
    }
    t.push('more button: ' + (more ? 'found' : 'NONE'));
    if (!more) {
      const labels = [...row.querySelectorAll('button,[role="button"]')].map((b) => b.getAttribute('aria-label') || b.title || '·');
      t.push('row buttons: ' + labels.slice(0, 16).join(', '));
      return { ok: false, why: 'legend More button not found', trace: t };
    }

    // 3. Open the menu and click "Add alert…".
    hover(more); realClick(more);
    const menu = await waitFor(() => document.querySelector('[role="menu"], [data-name="menu-inner"], [class*="menuBox" i]'), 1500);
    t.push('menu: ' + (menu ? 'open' : 'no'));
    if (!menu) return { ok: false, why: 'legend menu did not open', trace: t };
    const items = [...menu.querySelectorAll('[role="menuitem"], [class*="item" i], [data-role="menuitem"]')];
    t.push('menu items: ' + items.map((x) => x.textContent.trim()).filter(Boolean).slice(0, 20).join(' | '));
    const addAlert = items.find((x) => /add alert/i.test(x.textContent));
    if (!addAlert) return { ok: false, why: 'no "Add alert" menu item', trace: t };
    realClick(addAlert);

    // 4. Create-alert dialog opens with the condition pre-filled → click Create.
    const dlg = await waitFor(() => [...document.querySelectorAll('[role="dialog"]')].find((d) => /create alert|condition/i.test(d.textContent)), 2500);
    t.push('create dialog: ' + (dlg ? 'open' : 'no'));
    if (!dlg) return { ok: false, why: 'create-alert dialog did not open', trace: t };
    const btns = [...dlg.querySelectorAll('button')];
    t.push('dialog buttons: ' + btns.map((b) => b.textContent.trim()).filter(Boolean).slice(0, 12).join(', '));
    const createBtn = dlg.querySelector('[data-name="submit-button"], [data-name="alerts-create-edit-button"]')
                   || btns.find((b) => /^(create|save)$/i.test(b.textContent.trim()));
    if (!createBtn) return { ok: false, why: 'Create button not found', trace: t };
    realClick(createBtn);
    await sleep(400);
    return { ok: true, trace: t };
  }

  // Delete existing "ATAS Levels" alerts (right-sidebar Alerts panel only, so we
  // never touch the chart legend / indicator-remove button). In diagnostic mode
  // it dumps the alert-row structure instead of clicking, to find the delete control.
  async function deleteAtasAlerts(t) {
    try {
      const W = window.innerWidth;
      // Leaf title elements that read "ATAS Levels" and sit on the right sidebar.
      const titles = [...document.querySelectorAll('div, span')].filter((e) => {
        if (e.childElementCount !== 0) return false;
        if (!/^ATAS Levels/.test((e.textContent || '').trim())) return false;
        const r = e.getBoundingClientRect();
        return r.width > 0 && r.left > W * 0.6;   // right sidebar = alerts panel
      });
      if (t) t.push('ATAS alert rows (right side): ' + titles.length);
      if (!titles.length) return 0;

      let deleted = 0;
      for (const title of titles) {
        let r = title;
        for (let i = 0; i < 6 && r.parentElement; i++) r = r.parentElement;
        hover(r);
        for (const el of r.querySelectorAll('*')) hover(el);
        await sleep(150);
        const del = r.querySelector('[data-name*="delete" i], button[aria-label*="delete" i]');
        if (del) { realClick(del); deleted++; await sleep(250); }
      }
      if (t) t.push('deleted: ' + deleted);
      return deleted;
    } catch (e) { if (t) t.push('delete step error: ' + e.message); return 0; }
  }

  let autoFailUntil = 0;   // backoff: don't auto-retry until this time (ms epoch)
  async function autoTick() {
    if (!isAuto() || applying) return;
    if (!combined || combined === lastApplied) return;     // nothing new
    if (Date.now() < autoFailUntil) return;                // backing off after a failure
    if (anyDialogOpen() || userIsTyping()) return;          // be polite; retry next tick
    applying = true;
    try {
      const res = await applyToChart(combined);
      if (res.ok) {
        lastApplied = combined; autoFailUntil = 0;
        flash('✓ levels applied');
      } else {
        // Back off 2 min so a broken apply can't reopen the dialog every poll.
        autoFailUntil = Date.now() + 120000;
        flash(`auto-apply failed (backing off): ${res.why}`);
      }
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
      <div style="display:flex;gap:6px;flex-wrap:wrap;">
        <button id="atas-copy" style="${BTN}">Copy all</button>
        <button id="atas-push" style="${BTN};background:#3a3a44;">Push now</button>
        <button id="atas-alert" style="${BTN};background:#7a4dd0;">Refresh alert</button>
      </div>
      <div id="atas-status" style="color:#6f6f78;font-size:10px;margin-top:6px;min-height:12px;"></div>`;

    host.querySelector('#atas-auto').onchange = (e) => { setAuto(e.target.checked); render(); };
    host.querySelector('#atas-copy').onclick = () => { GM_setClipboard(combined); flash('copied — paste into the indicator'); };
    host.querySelector('#atas-alert').onclick = async () => {
      const trace = [];
      let res;
      try { res = await refreshAlert(trace); }
      catch (e) { res = { ok: false, why: 'exception: ' + (e && e.message), trace }; }
      if (res.ok) flash('✓ alert refreshed');
      else {
        flash('alert refresh failed');
        alert('ATAS Levels v' + VERSION + ' — Refresh alert trace:\n\n' + (res.trace || trace).join('\n') +
              '\n\nRESULT: ' + (res.ok ? 'OK' : 'FAILED — ' + res.why));
      }
      render();
    };
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
