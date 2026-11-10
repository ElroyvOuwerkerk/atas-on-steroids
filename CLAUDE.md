# atas-on-steroids: agent working notes

Elroy's trading tooling: ATAS indicators, the ATAS to TradingView level sync, and a trading journal.
It runs against a live trading setup, which is why the first rule is not negotiable. This file loads
on every turn, so it stays short; detail lives in the READMEs and in `RELEASE_NOTES.md`.

## What is where

| Part | Where |
|---|---|
| ATAS indicators (C#). All of them compile into one `AtasOnSteroids.dll` | `indicators/` |
| ATAS to TradingView Level Sync: a backend reads the ATAS workspace file, `atas-levels.pine` draws, the Tampermonkey userscript `atas-levels.user.js` pushes | `tradingview-sync/` (its README explains the flow) |
| Trading journal: Python backend (launchd), React dashboard, ATAS plugin (TradeLogger) | `trading-journal/` |
| Every change and why | `RELEASE_NOTES.md` |

## Hard rules

1. **Never edit an existing script.** `atas-levels.user.js`, `atas-levels.pine`, the ATAS `.cs`
   indicators and every other pre-existing script are off limits, even for a small and obviously
   correct fix (Elroy, 2026-08-01: "You are not allowed to touch any of my other scripts, let that be
   clear."). New work goes in new files. Found a bug? Report the root cause and the suggested change,
   then stop, until he says "go fix it" and names the file.
2. **Update `RELEASE_NOTES.md` on every change,** before the turn ends. Pending work goes under
   Unreleased; on a commit, its bullets move to a dated section with the short SHA. Group under
   Added, Changed, Fixed, Removed. Skip build-artifact churn.
3. **Turn `auto` off in the Level Sync widget before opening TradingView's Pine Editor.** The
   userscript fills whatever element has focus, so it pastes the levels into the script's source. If
   it happens: reload the page without saving; Pine scripts are never autosaved.
4. **Check what the platform already has before building anything.** A Pine volume profile was
   built over three days in August, then retired: TradingView's built-in Session Volume Profile HD had
   been in the chart legend all along. He uses that SVP HD: do not build Pine volume profiles. Audit
   the installed and built-in indicators first.
5. **Commit to `main` and push only when he asks,** and stage explicit paths. Build output (`bin/`, `obj/`) and
   the journal database never go in git: the database holds his real trades.

## ATAS facts that cost time to learn

- **Deploy:** build Release, then
  `cp indicators/bin/Release/net10.0/AtasOnSteroids.dll "$HOME/Library/Application Support/ATAS/Indicators/"`
  and fully restart ATAS, which caches loaded DLLs.
- **No Newtonsoft.Json in indicators:** the indicator then silently fails inside ATAS. Build JSON by
  hand.
- **Comma-decimal locale on this Mac:** serialise numbers with `CultureInfo.InvariantCulture`.
- **Drawings:** the documented API only exposes horizontal lines and channels, but every drawing is
  reachable by reflection (see `indicators/StaleBoxAlertMuter.cs`), and the workspace file
  `~/Library/Application Support/ATAS/Workspaces2/*.aws` is plain JSON with every drawing in full.
  Decompile with `ilspycmd` inside `/Applications/ATAS X.app/Contents/MonoBundle`.
- **Journal backend:** runs under launchd. Restart with
  `launchctl kickstart -k gui/$(id -u)/com.tradingjournal.backend`; logs in `/tmp/tradingjournal-backend.log`.
- **Instruments:** ES/MES and NQ/MNQ. The micros mirror the mini prices 1:1; he trades the micros on
  TradingView.

## The Level Sync userscript is fragile

TradingView changes its DOM often, and the script has been re-fixed many times. Do not guess:
ask him to click **Push now** in the widget and send the popup trace, and inspect the live DOM in the
in-app browser. His charts hide indicator legend rows (they are in the DOM at 0 by 0), so position
panes by a visible ancestor, never by the row itself. The symbol row's "Settings..." opens chart
settings, not the indicator's.

## Working with him

Chat in Dutch; repo docs and code in English. One task per session: at a committed boundary, prefer
a handover and a fresh start over a long session, because every summary drops details this file holds.
