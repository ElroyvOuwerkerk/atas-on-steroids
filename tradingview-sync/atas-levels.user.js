// ==UserScript==
// @name         ATAS Levels → TradingView
// @namespace    atas-on-steroids
// @version      3.34
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

  const VERSION = '3.34';
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
  // Double-click the exact element (with coordinates + a real dblclick event) —
  // TradingView opens an indicator's settings when its legend title is dbl-clicked.
  function doubleClick(el) {
    const b = el.getBoundingClientRect();
    const o = { bubbles: true, cancelable: true, clientX: b.left + b.width / 2, clientY: b.top + b.height / 2 };
    for (let n = 1; n <= 2; n++) {
      try { el.dispatchEvent(new PointerEvent('pointerdown', o)); } catch (e) {}
      el.dispatchEvent(new MouseEvent('mousedown', o));
      try { el.dispatchEvent(new PointerEvent('pointerup', o)); } catch (e) {}
      el.dispatchEvent(new MouseEvent('mouseup', o));
      el.dispatchEvent(new MouseEvent('click', { ...o, detail: n }));
    }
    el.dispatchEvent(new MouseEvent('dblclick', { ...o, detail: 2 }));
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
  // A visible leaf element whose exact text is `txt` (used to spot dialog tabs).
  function visLeaf(txt) {
    return [...document.querySelectorAll('button, [role="tab"], span, div')]
      .find((e) => e.childElementCount === 0 && new RegExp('^' + txt + '$', 'i').test((e.textContent || '').trim()) && isVisible(e)) || null;
  }
  // The indicator settings dialog is the only thing with Inputs + Visibility tabs.
  const settingsDialogOpen = () => !!(visLeaf('Visibility') && visLeaf('Inputs'));
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
  // Returns one legend SOURCE-ROW per chart pane whose text reads "ATAS Levels".
  // The user's charts hide the indicator rows (TradingView `hideInvisibleHover`),
  // so the visible title span has 0 size and can't be found by sight — but the
  // row element is still in the DOM as `<div class="item-<hash> …">` whose text
  // is the indicator's name. React delegates events at the document root, so a
  // dblclick dispatched on this row opens its settings even while it's hidden.
  // We therefore match the ROW (not a visible box), keep the tightest-text one
  // per pane (a wrapper holding several sources matches too, but its text is much
  // longer), and dedupe panes by a visible ancestor's position (the row itself
  // can be 0-size). Old data-name / visible-title heuristics are the fallback.
  const LEGEND_NAME = 'ATAS Levels';
  // A visible ancestor's rect — used to place a possibly 0-size hidden row on a pane.
  function paneRectOf(el) {
    for (let a = el, i = 0; a && i < 12; a = a.parentElement, i++) {
      const r = a.getBoundingClientRect();
      if (r.width > 120 && r.height > 4) return r;
    }
    return el.getBoundingClientRect();
  }
  function findLegendTitles(trace) {
    const notOurs = (e) => !e.closest('#atas-levels-widget');
    const hasName = (e) => (e.textContent || '').includes(LEGEND_NAME);
    // Must sit inside a chart LEGEND — excludes the data window / object tree,
    // which also print "ATAS Levels" and created a false 3rd pane.
    const inLegend = (e) => !!e.closest('[class*="legend"]');
    // Source rows — do NOT require visibility (they're hover-hidden here).
    let rows = [...document.querySelectorAll('[class*="item-"]')].filter((e) => notOurs(e) && hasName(e) && inLegend(e));
    // Tightest text first, so the ATAS Levels row beats a multi-source wrapper.
    rows.sort((a, b) => (a.textContent || '').length - (b.textContent || '').length);
    // Fallback: any visible element carrying the name (older/plain DOM).
    let usedFallback = false;
    if (!rows.length) {
      usedFallback = true;
      rows = [...document.querySelectorAll('[class*="title"], div, span')]
        .filter((e) => { const r = e.getBoundingClientRect(); return r.width > 0 && r.height > 0 && r.height <= 80 && notOurs(e) && hasName(e); })
        .sort((a, b) => { const ra = a.getBoundingClientRect(), rb = b.getBoundingClientRect(); return ra.width * ra.height - rb.width * rb.height; });
    }
    const out = [];
    const seen = new Set();
    for (const el of rows) {
      const r = paneRectOf(el);
      const key = Math.round(r.left / 80) + ':' + Math.round(r.top / 40);
      if (seen.has(key)) continue;
      seen.add(key); out.push(el);
    }
    if (trace) {
      trace.push(`rows w/ name: ${rows.length}${usedFallback ? ' (fallback)' : ''}, panes: ${out.length}`);
      trace.push('pane pos: ' + (out.map((e) => { const r = paneRectOf(e); return `${Math.round(r.left)},${Math.round(r.top)}`; }).join(' | ') || 'none'));
    }
    return out;
  }

  function hover(el) {
    for (const tp of ['pointerover', 'pointerenter', 'mouseover', 'mouseenter', 'mousemove']) {
      try { el.dispatchEvent(new MouseEvent(tp, { bubbles: true })); } catch (e) {}
    }
  }

  // Drill from a legend box DOWN to the deepest element still holding the whole
  // name — the actual rendered title node. TradingView's dblclick-to-open handler
  // sits there, and a dblclick dispatched on an ancestor never bubbles THROUGH the
  // title, so it must be fired on the title node (or a descendant) itself.
  function titleLeaf(el) {
    let cur = el;
    for (let depth = 0; depth < 12; depth++) {
      let next = null;
      for (const c of cur.children) {
        if (c.textContent.includes(LEGEND_NAME) && isVisible(c)) { next = c; break; }
      }
      if (!next) break;
      cur = next;
    }
    return cur;
  }
  // Double-click whatever is actually painted at a point (like a real cursor).
  function dblclickAtPoint(x, y) {
    const el = document.elementFromPoint(x, y);
    if (el) doubleClick(el);
    return el;
  }

  // The legend ROW (item-<hash>) that holds a given title element.
  function legendRow(titleEl) {
    let row = titleEl;
    for (let i = 0; i < 8 && row; i++) {
      if (/(^|\s)item-/.test((row.className || '').toString())) return row;
      row = row.parentElement;
    }
    return titleEl.parentElement || titleEl;
  }

  // Close any modal that's up (Escape); used to clear a WRONG dialog (e.g. the
  // chart Settings that the symbol row's ⋯ menu opens) before the next attempt.
  const anyBigDialog = () => [...document.querySelectorAll('[class*="dialog"], [role="dialog"]')]
    .some((e) => { const r = e.getBoundingClientRect(); return r.width > 300 && r.height > 200; });
  async function escapeModal() {
    for (let i = 0; i < 3 && anyBigDialog(); i++) {
      document.body.dispatchEvent(new KeyboardEvent('keydown', { key: 'Escape', keyCode: 27, which: 27, bubbles: true }));
      document.body.dispatchEvent(new KeyboardEvent('keyup', { key: 'Escape', keyCode: 27, which: 27, bubbles: true }));
      await sleep(250);
    }
  }

  // Open the indicator's settings panel. The user's charts HIDE the indicator
  // legend rows (TradingView `hideInvisibleHover` — only the symbol row shows),
  // so there is no per-indicator gear/⋯ to click; the symbol's ⋯ just opens the
  // CHART settings. But the hidden row is still in the DOM, and double-clicking
  // the ROW element opens THAT indicator's settings. We can't know which ancestor
  // of the located title is the row, so we walk outward double-clicking each
  // (skipping oversized containers), validating by the Inputs/Visibility tabs,
  // and Escape-closing any wrong dialog between tries. Returns the textarea or null.
  async function openSettings(titleEl, t) {
    await escapeModal();                       // clear a leftover wrong dialog
    [titleLeaf(titleEl), titleEl, legendRow(titleEl)].forEach(hover);
    await sleep(200);

    // One-time structural dump so the trace shows the real hidden-row layout.
    if (!t._dumped) {
      t._dumped = true;
      const chain = [];
      for (let a = titleEl, i = 0; a && i < 7; a = a.parentElement, i++) {
        const r = a.getBoundingClientRect();
        chain.push(`${((a.className || '').toString().split(' ')[0] || a.tagName).slice(0, 18)}[${Math.round(r.width)}x${Math.round(r.height)}]`);
      }
      t.push('anc: ' + chain.join(' < '));
    }

    const tryOpen = async (label, fn) => {
      try { await fn(); } catch (e) { t.push(`${label} threw: ${e.message}`); }
      const ok = await waitFor(() => (settingsDialogOpen() ? true : null), 1400);
      if (!ok && anyBigDialog()) { t.push(`${label}: wrong-dialog`); await escapeModal(); }
      else t.push(`${label}: ${ok ? 'YES' : 'no'}`);
      return !!ok;
    };

    // Double-click candidates: the ROW itself (may be 0-size while hover-hidden,
    // but React delegates dblclick at the document root so it still fires), its
    // inner name-bearing nodes, and small ancestors. settingsDialogOpen() only
    // trues on Inputs+Visibility, so the chart Settings dialog can't be mistaken
    // for success; a wrong dialog is Escaped between tries.
    const cands = [];
    const add = (e) => { if (e && !cands.includes(e)) cands.push(e); };
    add(titleEl);
    [...titleEl.querySelectorAll('*')].slice(0, 14).forEach((e) => { if ((e.textContent || '').includes(LEGEND_NAME)) add(e); });
    for (let a = titleEl.parentElement, i = 0; a && i < 5; a = a.parentElement, i++) {
      const r = a.getBoundingClientRect();
      if (r.width > 0 && r.width <= 480 && r.height <= 140) add(a);
    }
    let opened = false;
    for (const c of cands) {
      if (opened) break;
      const r = c.getBoundingClientRect();
      const tag = ((c.className || '').toString().split(' ')[0] || c.tagName).slice(0, 16);
      opened = await tryOpen(`dblclick ${tag}[${Math.round(r.width)}x${Math.round(r.height)}]`, () => doubleClick(c));
    }
    if (!opened) { t.push('settings dialog never opened'); return null; }

    // Switch to the Inputs tab, then grab the Levels textarea.
    let ta = findInput();
    if (!ta) {
      const inputsTab = visLeaf('Inputs');
      t.push('Inputs tab: ' + (inputsTab ? 'found' : 'NOT found'));
      if (inputsTab) { realClick(inputsTab); await sleep(200); }
      ta = await waitFor(findInput, 1500);
    }
    // Confirm we opened OUR indicator (its panel mentions ATAS Levels / the Levels
    // label) — not some other indicator's dialog.
    if (ta && !/ATAS Levels|Levels \(paste|Copy all/i.test(ctxAround(ta))) {
      t.push('opened wrong indicator (no ATAS Levels context) — skipping');
      const cancel = [...document.querySelectorAll('button')].filter(isVisible).find((b) => /^cancel$/i.test((b.textContent || '').trim()));
      if (cancel) realClick(cancel);
      return null;
    }
    t.push('textarea: ' + (ta ? 'yes' : 'no'));
    return ta;
  }

  // Coarse on-screen position bucket — identifies a legend row's PANE. The row is
  // hover-hidden (0-size), so bucket by a VISIBLE ancestor's position (same as the
  // finder) — using the row's own 0×0 rect collapses both charts to one pane.
  const paneKey = (el) => {
    const r = paneRectOf(el);
    return Math.round(r.left / 80) + ':' + Math.round(r.top / 40);
  };

  async function applyToChart(csv, trace) {
    const t = trace || [];
    const initial = findLegendTitles(t);
    if (!initial.length) return { ok: false, why: '"ATAS Levels" title not found on page', trace: t };

    // A layout can hold several charts, each with its own ATAS Levels indicator.
    // Enumerate the distinct panes by position (left→right); we re-find each
    // title FRESH just before we open it, so a legend re-render can't leave us
    // holding a stale element (which double-clicks the wrong / left chart).
    const keys = [];
    for (const el of initial) { const k = paneKey(el); if (!keys.includes(k)) keys.push(k); }
    keys.sort((a, b) => (+a.split(':')[0]) - (+b.split(':')[0]));
    t.push(`ATAS Levels panes: ${keys.length} [${keys.join(', ')}]`);

    let applied = 0;
    const done = new Set();
    for (const key of keys) {
      // Never start a pane while a dialog is still open — a leftover dialog makes
      // the detector grab the wrong (stale) textarea in multi-chart layouts.
      if (settingsDialogOpen()) await waitFor(() => (!settingsDialogOpen() ? true : null), 3000);
      // Re-query live titles; pick this pane's, excluding panes already filled.
      const fresh = findLegendTitles().filter((el) => !done.has(paneKey(el)));
      const title = fresh.find((el) => paneKey(el) === key);
      if (!title) { t.push(`pane ${key}: title gone after re-render — skipping`); continue; }
      done.add(key);
      const textarea = await openSettings(title, t);
      if (!textarea) { t.push(`pane ${key}: settings would not open`); continue; }
      fillTextarea(textarea, csv, t);
      await sleep(200);
      const visBtns = [...document.querySelectorAll('button, [role="button"]')].filter(isVisible);
      const ok = document.querySelector('[data-name="submit-button"]')
            || visBtns.find((b) => /^(ok|apply)$/i.test((b.textContent || '').trim()));
      if (!ok) { t.push(`pane ${key}: OK not found`); continue; }
      realClick(ok);
      applied++;
      // Wait for THIS dialog to fully close before touching the next pane.
      await waitFor(() => (!settingsDialogOpen() ? true : null), 3000);
      await sleep(250);
    }
    t.push(`panes applied: ${applied}/${keys.length}`);
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

  // Recreate the indicator's alert(s) so the frozen input snapshot picks up the
  // current zones. One alert per chart pane (each pane's ATAS Levels fires for
  // its own family). Deletes existing ATAS Levels alerts first, then for each
  // pane opens the indicator ROW's own ⋯ menu → "Add alert on ATAS Levels" →
  // submit. The row is the hidden hover-legend row (0-size), same as the settings
  // path; its ⋯ is the INDICATOR's (the symbol row's ⋯ would add a price alert on
  // the symbol — that was the old bug). Notifications inherit the last-used config.
  async function refreshAlert(trace) {
    const t = trace || [];
    await escapeModal();
    const del = await deleteAtasAlerts(t);

    const initial = findLegendTitles(t);
    if (!initial.length) return { ok: false, why: '"ATAS Levels" not on chart', trace: t };
    const keys = [];
    for (const el of initial) { const k = paneKey(el); if (!keys.includes(k)) keys.push(k); }
    keys.sort((a, b) => (+a.split(':')[0]) - (+b.split(':')[0]));
    t.push(`alert panes: ${keys.length}`);

    let created = 0;
    const done = new Set();
    for (const key of keys) {
      await escapeModal();
      const row = findLegendTitles().filter((el) => !done.has(paneKey(el))).find((el) => paneKey(el) === key);
      if (!row) { t.push(`pane ${key}: row gone`); continue; }
      done.add(key);
      hover(row);
      // Diagnostic: dump the row's buttons once (to see the indicator's ⋯).
      if (!t._btns) {
        t._btns = true;
        const bs = [...row.querySelectorAll('button, [role="button"]')].map((b) => (b.getAttribute('aria-label') || b.title || '·'));
        t.push('row buttons: ' + (bs.slice(0, 18).join(', ') || 'none'));
      }
      const more = [...row.querySelectorAll('button, [role="button"]')]
        .find((b) => /^more$/i.test((b.getAttribute('aria-label') || '').trim()));
      t.push(`pane ${key} more: ${more ? 'yes' : 'NO'}`);
      if (!more) continue;
      hover(more); realClick(more);
      const label = await waitFor(() => [...document.querySelectorAll('[class*="label-"], [role="menuitem"]')]
        .find((e) => isVisible(e) && /add alert/i.test((e.textContent || '').trim())) || null, 1600);
      t.push(`pane ${key} add-alert item: ${label ? 'yes' : 'NO'}`);
      if (!label) { await escapeModal(); continue; }
      const clickAddAlert = () => {
        const l = [...document.querySelectorAll('[class*="label-"], [role="menuitem"]')]
          .find((e) => isVisible(e) && /add alert/i.test((e.textContent || '').trim()));
        if (!l) return false;
        let it = l;
        for (let a = l, i = 0; a && i < 5; a = a.parentElement, i++) {
          const rl = a.getAttribute && a.getAttribute('role');
          if (rl === 'menuitem' || /(^|\s)item-/.test((a.className || '').toString())) { it = a; break; }
        }
        const rr = l.getBoundingClientRect();
        hover(it);
        const p = document.elementFromPoint(rr.left + rr.width / 2, rr.top + rr.height / 2);
        realClick(p && it.contains(p) ? p : it);
        return true;
      };
      clickAddAlert();
      // Create-alert dialog → submit (condition pre-filled to the indicator's
      // "Any alert() function call"). Retry the menu click once if it's slow.
      const dialogNow = () => [...document.querySelectorAll('[class*="dialog"], [role="dialog"]')]
        .find((d) => { const r = d.getBoundingClientRect(); return r.width > 300 && r.height > 200 && /condition|alert/i.test(d.textContent); }) || null;
      let dlg = await waitFor(dialogNow, 3500);
      if (!dlg) {
        // Menu may have closed without opening the dialog — reopen ⋯ and retry.
        hover(more); realClick(more); await sleep(400);
        clickAddAlert();
        dlg = await waitFor(dialogNow, 3500);
      }
      t.push(`pane ${key} create dialog: ${dlg ? 'yes' : 'NO'}`);
      if (!dlg) { await escapeModal(); continue; }
      const submit = dlg.querySelector('[data-name="submit-button"], [data-name*="create" i]')
        || [...dlg.querySelectorAll('button')].filter(isVisible).find((b) => /^(create|save)$/i.test((b.textContent || '').trim()));
      t.push(`pane ${key} submit: ${submit ? 'yes' : 'NO'}`);
      if (!submit) { await escapeModal(); continue; }
      realClick(submit);
      await waitFor(() => (!anyBigDialog() ? true : null), 2500);
      created++;
      await sleep(300);
    }
    t.push(`alerts created: ${created}/${keys.length}; deleted: ${del}`);
    if (!created) return { ok: false, why: 'no alert created', trace: t };
    return { ok: true, trace: t };
  }

  // Best-effort delete of existing ATAS Levels alerts (avoids duplicates on
  // refresh). Opens the right-sidebar Alerts panel, then finds each alert row by
  // its text and clicks a delete control — trying a hover trash icon first, then
  // the row's ⋯ menu → "Delete". Dumps structure to the trace so a miss is fixable.
  async function deleteAtasAlerts(t) {
    try {
      // Open the Alerts panel. The sidebar toggle's aria-label is "Alerts";
      // don't match the legend row's "Alerts · N" button.
      const alertsBtn = [...document.querySelectorAll('button')]
        .find((b) => /^alerts$/i.test((b.getAttribute('aria-label') || '').trim()) && isVisible(b));
      if (t) t.push('alerts panel btn: ' + (alertsBtn ? 'yes' : 'NO'));
      if (alertsBtn) { hover(alertsBtn); realClick(alertsBtn); await sleep(900); }

      // Find alert rows: any element naming the indicator / its message. Not
      // restricted to leaves or the right edge (panel width/position varies).
      const nameRe = /ATAS Levels|ATAS level hit/i;
      const hits = [...document.querySelectorAll('div, span')].filter((e) => {
        const tx = (e.textContent || '').trim();
        if (!nameRe.test(tx) || tx.length > 80) return false;   // a row label, not a big container
        if (e.closest('#atas-levels-widget') || e.closest('[class*="legend"]')) return false;  // not the chart legend / our widget
        return isVisible(e);
      });
      // Dedupe to one element per row by vertical position.
      const rowsByY = new Map();
      for (const e of hits) {
        const r = e.getBoundingClientRect();
        const key = Math.round(r.top / 12);
        if (!rowsByY.has(key)) rowsByY.set(key, e);
      }
      const rows = [...rowsByY.values()];
      if (t) {
        t.push('alert rows found: ' + rows.length);
        if (rows[0]) {
          const r0 = rows[0].getBoundingClientRect();
          t.push(`row0: "${(rows[0].textContent || '').trim().slice(0, 30)}" @${Math.round(r0.left)},${Math.round(r0.top)}`);
        }
      }

      let deleted = 0;
      for (const title of rows) {
        // Climb to the clickable alert-row container.
        let row = title;
        for (let i = 0; i < 8 && row.parentElement; i++) {
          const rr = row.getBoundingClientRect();
          if (rr.height >= 24 && rr.width >= 120) break;   // a row-sized box
          row = row.parentElement;
        }
        hover(row);
        for (const el of row.querySelectorAll('*')) hover(el);
        await sleep(200);
        // Diagnostic: dump this row's buttons once, to reveal the delete control.
        if (t && !t._alertBtns) {
          t._alertBtns = true;
          const bs = [...row.querySelectorAll('button, [role="button"]')].map((b) => (b.getAttribute('aria-label') || b.getAttribute('data-name') || b.title || (b.textContent || '').trim() || '·').slice(0, 16));
          t.push('alert row buttons: ' + (bs.slice(0, 16).join(', ') || 'none'));
        }
        // Strategy 1: a direct delete/remove control on the row.
        let del = [...row.querySelectorAll('button, [role="button"]')].find((b) => /delete|remove/i.test((b.getAttribute('aria-label') || b.getAttribute('data-name') || b.title || '')) && isVisible(b));
        if (del) { hover(del); realClick(del); }
        else {
          // Strategy 2: the row's ⋯ menu → "Delete".
          const more = [...row.querySelectorAll('button, [role="button"]')].find((b) => /^more$/i.test((b.getAttribute('aria-label') || '').trim()) && isVisible(b));
          if (more) {
            hover(more); realClick(more);
            const item = await waitFor(() => [...document.querySelectorAll('[class*="label-"], [role="menuitem"]')].find((e) => isVisible(e) && /^(delete|remove)/i.test((e.textContent || '').trim())) || null, 1200);
            if (item) realClick(item);
          }
        }
        await sleep(300);
        const yes = [...document.querySelectorAll('button')].filter(isVisible)
          .find((b) => /^(yes|delete|remove|confirm)$/i.test((b.textContent || '').trim()));
        if (yes) { realClick(yes); await sleep(200); }
        deleted++;
      }
      if (t) t.push('deleted (attempts): ' + deleted);
      return deleted;
    } catch (e) { if (t) t.push('delete step error: ' + e.message); return 0; }
  }

  // Auto-refresh the TV alert after zones change (opt-in checkbox, default on).
  const alertKey = 'atasLevels.autoAlert';
  const isAutoAlert = () => localStorage.getItem(alertKey) !== '0';
  const setAutoAlert = (v) => localStorage.setItem(alertKey, v ? '1' : '0');
  let lastAlertedFor = '';       // the feed the alert was last recreated for
  let alertFailUntil = 0;        // backoff after a failed auto alert refresh

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
        // Re-snapshot the alert onto the new zones (opt-in). Once per change,
        // with its own backoff so a broken refresh can't loop every poll.
        if (isAutoAlert() && combined !== lastAlertedFor && Date.now() >= alertFailUntil) {
          let ar;
          try { ar = await refreshAlert([]); } catch (e) { ar = { ok: false, why: 'exception: ' + (e && e.message) }; }
          if (ar.ok) { lastAlertedFor = combined; flash('✓ levels + alert refreshed'); }
          else { alertFailUntil = Date.now() + 300000; flash(`levels applied; alert refresh failed: ${ar.why}`); }
        }
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
        <div style="display:flex;gap:8px;">
          <label style="display:flex;align-items:center;gap:4px;color:#9a9aa2;cursor:pointer;">
            <input type="checkbox" id="atas-auto" ${isAuto() ? 'checked' : ''}> auto
          </label>
          <label style="display:flex;align-items:center;gap:4px;color:#9a9aa2;cursor:pointer;" title="Auto-recreate the TV alert after each sync so it fires on the new zones">
            <input type="checkbox" id="atas-autoalert" ${isAutoAlert() ? 'checked' : ''}> alerts
          </label>
        </div>
      </div>
      <div style="color:#9a9aa2;margin-bottom:6px;">${countStr} <span style="color:${synced ? '#3ec27a' : '#c9a23e'}">${synced ? '✓ synced' : '… pending'}</span></div>
      <div style="display:flex;gap:6px;flex-wrap:wrap;">
        <button id="atas-copy" style="${BTN}">Copy all</button>
        <button id="atas-push" style="${BTN};background:#3a3a44;">Push now</button>
        <button id="atas-alert" style="${BTN};background:#7a4dd0;">Refresh alert</button>
      </div>
      <div id="atas-status" style="color:#6f6f78;font-size:10px;margin-top:6px;min-height:12px;"></div>`;

    host.querySelector('#atas-auto').onchange = (e) => { setAuto(e.target.checked); render(); };
    host.querySelector('#atas-autoalert').onchange = (e) => { setAutoAlert(e.target.checked); render(); };
    host.querySelector('#atas-copy').onclick = () => { GM_setClipboard(combined); flash('copied — paste into the indicator'); };
    host.querySelector('#atas-alert').onclick = async () => {
      const trace = [];
      let res;
      try { res = await refreshAlert(trace); }
      catch (e) { res = { ok: false, why: 'exception: ' + (e && e.message), trace }; }
      if (res.ok) { lastAlertedFor = combined; flash('✓ alert refreshed'); }
      else flash('alert refresh failed');
      // Always show the trace for now (debugging alert automation); revert to
      // failure-only once it's solid, like the Push button.
      alert('ATAS Levels v' + VERSION + ' — Refresh alert trace\n\n' + (res.trace || trace).join('\n') +
            '\n\nRESULT: ' + (res.ok ? 'OK' : 'FAILED — ' + res.why));
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
        // Only surface the per-pane diagnostic trace when something fails.
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
