# Module notes: ATAS (Platform Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-11. 10 lessons.
Mostly platform setup, but it pins down the exact CC template settings (value area %, imbalance
threshold, session/TPO config) that matter when building ATAS indicators and matching Severin's /
the CC workspace. Elroy already trades ATAS; the repo builds ATAS indicators in `indicators/`.

Vault cheat sheet: "Daniel's New ATAS Templates" (lessons 9-10). Member perk mentioned across these
lessons: a one-time free month of ATAS Ultra study license (apply link in the portal).

---

## Key settings the CC templates use (collected across lessons)
- **Value area / buffer: change the default 70% to 68%.** (Lessons 1, 2.)
- **Imbalance ratio: 350%** in cluster view (purple = bid imbalance, blue = ask); red-bordered
  volume POCs flag trapped traders. (Lesson 4.) ExoCharts examples use 300-400% / Fib 618.
- **Fixed TPO Profile: Ctrl+F3**; configure previous day / week / month profiles (D/W/M labels).
  **F3** to check volume at a price level. (Lessons 2, 4.)
- **Look-back: at least 15 days for weekly levels; 30/60/90 days for monthly.** Too little
  look-back = wrong levels. (Lessons 2, 4.)
- **Fibonacci: hotkey F8; add the 66% level and save as default** (the "CC zone" is 0.618-0.66).
  (Lesson 1.)
- **Volume-based candles** 500k / 1M / 5M / 10M depending on volatility; tick size scales with
  volatility (crypto 10-20, up to 50; stocks/futures tick 1 for precision or 10 for a cleaner
  look). (Lessons 1, 2, 4.)
- Data feed: **Rithmic** (preferred, deeper DOM) or CQG via AMP, ~$20-25/month for real-time CME.
- Hotkeys: B = buy market, S = sell market. DOM depth = 500 levels. Padlock on live accounts to
  prevent accidental clicks.

## 1. Introduction + ExoCharts Desktop — `atas-introduction`
Multi-account trading: subaccounts for swing vs scalp; portfolio groups execute one click across
accounts; ratio multiplier tiers size per account; exits auto as reduce-only limits. Auto-exit
strategy (SL/TP/BE + trailing). TPO + volume profile combined tool. DOM design mode for preset
sizes; observe stacking/pulled orders/absorption. Top-Most feature pins a HTF chart (e.g. weekly
TPO) above the workspace. Dynamic profile tool on custom ranges: open line as intraday reference;
volume POC + TPO POC alignment = stronger confluence.
- **Range selection for a profile: downside range = first red candle to last red candle; upside =
  first to last green candle; extend slightly forward to reveal the naked POC.**

## 2. Stocks — `atas-stocks`
One-stop platform (crypto, ES/NQ, CFDs); data feed usable without broker. Label each account
(Swing Short/Long, Scalps). Smart Tape (price/volume/bid-ask, Freeze, min-volume filter e.g. 10).
Multi-level exit: split size evenly across 3 protections, **TP at 15 / 30 / 45 ticks, single SL**,
save per instrument. OCO orders. Depth-changes column: pulled orders at support = less likely to
hold; stacked sell orders = level price may push through. Fractals as liquidity lines (overlapping
H/L = liquidity pools). Single prints act as magnets.

## 3. Stocks Live Analysis — `atas-stocks-live-analysis`
Live practice. VPOC (volume point of control), not just the 50% fib, is often the true reaction
level. Pull fixed-range + fib across major swings. Trade BTCUSD (inverse) for better order
flow/CVD while executing on BTCUSDT (linear). Layered take-profits, uniform stop. Place TPs just
ahead of large resting DOM orders to avoid being queued behind them. If the platform is closed,
local strategies stop — set TP/SL on the exchange before stepping away.

## 4. Templates Part 1 — `atas-templates-part-1`
**The six condensed CC templates:**
1. **Order Flow** — daily TPO + volume profile; time POC (orange), volume POC (gray); VWAP closes
   (daily white, weekly yellow) as soft flip levels; single prints (blue) = inefficiencies price
   revisits; daily open (green) = equilibrium; prev-day 50%; DOM, CVD, OI, footprint stats; cluster
   delta profile with 350% imbalances; red-bordered VPOCs = trapped traders.
2. **Sessions** — session opens/closes (yellow); session delta (hidden-candle mode) for directional
   control; dynamic current session POC (shifting) vs previous session POC (true S/R).
3. **Classic TPO** — HTF daily/weekly/monthly TPO + volume profiles, color-coded D orange / W
   yellow / M purple; single prints as dots.
4. **Liquidity Pivots + VWAPs** (see Part 3).
5. **CCTR** (Trend Reversal + Clusters; see Part 3).
6. **Stacking Balances** with daily/weekly/monthly levels (see Part 3).
- **VWAP close = a "star" level**: short if price above, long if below; historically 1-2% reactive
  moves. Naked POCs all stacked to one side = directional liquidity bias (price seeks unfinished
  business / resting orders).

## 5. Templates Part 2 — `atas-templates-part-2`
Live Q&A on Order Flow / Sessions / Classic TPO. Introduces **ACR (average candle range)** for
setting profit targets. F3 to read volume at a level; save settings as default; save workspaces.

## 6. Templates Part 3 — `atas-templates-part-3`
- **Liquidity + Pivots template**: finds best swing highs/lows using the **fractal indicator**;
  shows daily/weekly/monthly VWAP closes + pivots + daily open.
- **CCTR (trend reversal) template**: for lower-timeframe scalps and sniper day-trade entries;
  session volume delta, single prints, previous-day levels. (CCTR = the trend-reversal chart from
  `orderflow-and-footprint.md` lesson 8, wired into a ready template.)
- **Stacked imbalances template**: daily/weekly/monthly naked profile levels + clusters; adjust
  settings per market/timeframe.

## 7. Templates Part 4 — `atas-templates-part-4`
Igor's pure **TPO template** (time POC, single prints, VAH/VAL) across ES/NQ/BTC; expand letters
with Tab. Two snapshots: Clean Chart (key daily levels) and Previous Levels (historical D/W/M
levels). Level 2 order book template.

## 8. Install and Setup — `atas-install-setup`
Install latest ATAS; AMP broker + CQG or Rithmic feed; import templates/snapshots/level-two tape;
build and save the workspace by closing the main window; keep the main ATAS window on a separate
screen; separate workspaces per market/style; snapshots save charts with drawings/levels; AMP feed
is accurate even when paper trading.

## 9-10. Workspace walkthrough (Daniel's new templates) — `atas-workspace-walkthrough-part1/2`
Reasons to switch ExoCharts → ATAS: ExoCharts glitches; ATAS is the most-used CC platform. Paste the
workspace into the ATAS workspaces folder and import. Daily/weekly/monthly volume profiles; weekly
naked POC, monthly VAH/VAL; **Igor Sessions 1-4** for lower-timeframe scalps; expand TPO with Tab;
merge/unmerge profiles with + / -; POCs act as magnets.
- Order-flow section (Part 2): a **time-based template** (CVD, delta, volume, OI; absorption and
  imbalances live; entry triggers; trap shorts at lows; CVD divergences on retests) and a
  **volume-based template** (each candle = 10M volume; high/low liquidity; aggressive clusters;
  pair with trade count for big vs small players). Use templates only at key levels or for trend
  verification, not in the noise.
