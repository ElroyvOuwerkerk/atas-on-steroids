// ==UserScript==
// @name         ATAS Levels → TradingView
// @namespace    atas-on-steroids
// @version      2.0
// @description  Auto-syncs ATAS-drawn levels onto TradingView: detects the chart's symbol, polls the local backend, and when levels change it fills the "ATAS Levels" Pine indicator by itself (manual Copy buttons as fallback).
// @match        https://www.tradingview.com/chart/*
// @match        https://*.tradingview.com/chart/*
// @grant        GM_xmlhttpRequest
// @grant        GM_setClipboard
// @connect      localhost
// @run-at       document-idle
// ==/UserScript==

(function () {
  'use strict';

  const BACKEND = 'http://localhost:3001';
  const POLL_MS = 4000;
  const FAMILIES = ['ES', 'NQ'];

  // ── Symbol → contract family (mirror of the backend's _group) ──
  const FAMILY_MAP = { MES: 'ES', MNQ: 'NQ', MGC: 'GC', M2K: 'RTY', MYM: 'YM' };
  const FUT_SUFFIX = /[FGHJKMNQUVXZ]\d{1,4}$/;

  function familyOf(sym) {
    let s = (sym || '').trim().toUpperCase();
    s = s.split('@')[0].split('.')[0].replace('1!', '').replace('2!', '').replace(/!+$/, '');
    s = s.replace(FUT_SUFFIX, '');
    return FAMILY_MAP[s] || s;
  }

  function detectChartFamily() {
    const el = document.getElementById('header-toolbar-symbol-search')
            || document.querySelector('[data-name="symbol-search-button"]');
    let sym = el ? el.textContent.trim() : '';
    if (!sym) sym = (document.title || '').split(/[\s:]/)[0];
    return familyOf(sym);
  }

  // ── Backend ──
  const state = {};   // family -> { csv, count, ts }
  FAMILIES.forEach((f) => (state[f] = { csv: '', count: 0, ts: '' }));

  const ep = (iso) => { if (!iso) return 0; const d = new Date(iso); return isNaN(d) ? 0 : Math.floor(d.getTime() / 1000); };

  function fetchFamily(family) {
    return new Promise((resolve) => {
      GM_xmlhttpRequest({
        method: 'GET',
        url: `${BACKEND}/api/levels/${family}`,
        onload: (r) => {
          try {
            const data = JSON.parse(r.responseText);
            const levels = data.levels || [];
            const csv = levels.map((l) =>
              `${l.kind || 'L'}|${l.price}|${l.price2 || 0}|${(l.color || '#FFFFFF').replace('#', '').toUpperCase()}` +
              `|${l.fill || ''}|${l.name || ''}|${ep(l.start)}|${ep(l.end)}`).join(';');
            resolve({ csv, count: levels.length, ts: data.updated_at || '' });
          } catch (e) { resolve({ csv: '', count: 0, ts: '' }); }
        },
        onerror: () => resolve({ csv: '', count: 0, ts: '' }),
      });
    });
  }

  // ── Auto-apply machinery ──
  const appliedKey = (f) => `atasLevels.applied.${f}`;
  const autoKey = 'atasLevels.auto';
  let applying = false;

  function isAuto() { return localStorage.getItem(autoKey) !== '0'; }
  function setAuto(v) { localStorage.setItem(autoKey, v ? '1' : '0'); }

  function sleep(ms) { return new Promise((r) => setTimeout(r, ms)); }

  async function waitFor(fn, timeoutMs) {
    const t0 = Date.now();
    while (Date.now() - t0 < timeoutMs) {
      const v = fn();
      if (v) return v;
      await sleep(120);
    }
    return null;
  }

  function findDialog() {
    return document.querySelector('[data-name="indicator-properties-dialog"]')
        || [...document.querySelectorAll('[role="dialog"]')].find((d) => d.querySelector('textarea'));
  }

  function anyDialogOpen() {
    return !!document.querySelector('[role="dialog"], [data-dialog-name]');
  }

  function userIsTyping() {
    const a = document.activeElement;
    return a && (a.tagName === 'INPUT' || a.tagName === 'TEXTAREA' || a.isContentEditable);
  }

  function findLegendTitle() {
    const titles = document.querySelectorAll('[data-name="legend-source-title"]');
    for (const t of titles) {
      if (t.textContent.includes('ATAS Levels')) return t;
    }
    return null;
  }

  async function applyToChart(csv) {
    // 1. Open the indicator's settings dialog (double-click its legend title)
    const title = findLegendTitle();
    if (!title) return { ok: false, why: 'indicator "ATAS Levels" not found on this chart' };
    title.dispatchEvent(new MouseEvent('dblclick', { bubbles: true, cancelable: true, view: window }));

    const dialog = await waitFor(findDialog, 2500);
    if (!dialog) return { ok: false, why: 'settings dialog did not open' };

    const textarea = await waitFor(() => dialog.querySelector('textarea'), 1500);
    if (!textarea) return { ok: false, why: 'Levels input not found in dialog' };

    // 2. Fill the React-controlled textarea via the native setter
    const setter = Object.getOwnPropertyDescriptor(window.HTMLTextAreaElement.prototype, 'value').set;
    setter.call(textarea, csv);
    textarea.dispatchEvent(new Event('input', { bubbles: true }));
    textarea.dispatchEvent(new Event('change', { bubbles: true }));
    await sleep(150);

    // 3. Confirm
    const ok = dialog.querySelector('[data-name="submit-button"]')
          || [...dialog.querySelectorAll('button')].find((b) => /^(ok|apply)$/i.test(b.textContent.trim()));
    if (!ok) return { ok: false, why: 'OK button not found' };
    ok.click();
    return { ok: true };
  }

  async function autoTick() {
    if (!isAuto() || applying) return;
    const family = detectChartFamily();
    if (!FAMILIES.includes(family)) return;
    const { csv } = state[family];
    if (!csv) return;
    if (localStorage.getItem(appliedKey(family)) === csv) return;   // already applied
    if (anyDialogOpen() || userIsTyping()) return;                  // don't interrupt — retry next tick

    applying = true;
    try {
      const res = await applyToChart(csv);
      if (res.ok) {
        localStorage.setItem(appliedKey(family), csv);
        flash(`✓ ${family} levels applied (${state[family].count})`);
      } else {
        flash(`auto-apply failed: ${res.why}`);
      }
    } finally {
      applying = false;
      render();
    }
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
      'box-shadow:0 6px 24px rgba(0,0,0,.4)', 'min-width:230px',
    ].join(';');
    document.body.appendChild(host);
    render();
  }

  function fmtTs(ts) {
    if (!ts) return '—';
    try { return new Date(ts).toLocaleTimeString(); } catch (e) { return '—'; }
  }

  function render() {
    if (!host) return;
    const chartFam = detectChartFamily();
    const rows = FAMILIES.map((f) => {
      const s = state[f];
      const applied = localStorage.getItem(appliedKey(f)) === s.csv && s.csv;
      const mark = f === chartFam ? '▸ ' : '';
      const status = applied ? '✓' : (s.csv ? '…' : '–');
      return `
        <div style="display:flex;align-items:center;gap:6px;margin:6px 0;">
          <b style="width:38px;">${mark}${f}</b>
          <span style="color:#9a9aa2;width:64px;">${s.count} obj ${status}</span>
          <button data-copy="${f}" style="${BTN}">Copy</button>
          <button data-push="${f}" style="${BTN};background:#3a3a44;">Push</button>
        </div>`;
    }).join('');
    host.innerHTML = `
      <div style="display:flex;justify-content:space-between;align-items:center;margin-bottom:4px;">
        <b>ATAS Levels</b>
        <label style="display:flex;align-items:center;gap:4px;color:#9a9aa2;cursor:pointer;">
          <input type="checkbox" id="atas-auto" ${isAuto() ? 'checked' : ''}> auto
        </label>
      </div>
      ${rows}
      <div id="atas-status" style="color:#6f6f78;font-size:10px;margin-top:4px;min-height:12px;"></div>`;

    host.querySelector('#atas-auto').onchange = (e) => { setAuto(e.target.checked); render(); };
    host.querySelectorAll('[data-copy]').forEach((b) =>
      b.onclick = () => {
        const f = b.getAttribute('data-copy');
        GM_setClipboard(state[f].csv);
        flash(`copied ${state[f].count} ${f} objects`);
      });
    host.querySelectorAll('[data-push]').forEach((b) =>
      b.onclick = async () => {
        const f = b.getAttribute('data-push');
        const res = await applyToChart(state[f].csv);
        if (res.ok) { localStorage.setItem(appliedKey(f), state[f].csv); flash(`✓ ${f} pushed`); }
        else flash(`push failed: ${res.why}`);
        render();
      });
  }

  let flashTimer;
  function flash(msg) {
    const el = document.getElementById('atas-status');
    if (!el) return;
    el.textContent = msg;
    clearTimeout(flashTimer);
    flashTimer = setTimeout(() => { el.textContent = ''; }, 6000);
  }

  // ── Main loop ──
  async function refresh() {
    for (const f of FAMILIES) state[f] = await fetchFamily(f);
    render();
    await autoTick();
  }

  buildWidget();
  refresh();
  setInterval(refresh, POLL_MS);
})();
