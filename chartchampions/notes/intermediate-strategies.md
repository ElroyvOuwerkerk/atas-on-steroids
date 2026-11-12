# Module notes: Intermediate Strategies (Technical Mastery)

Personal study notes distilled from the ChartChampions portal. Captured 2026-11-12. 13 lessons
(Adept level). A grab-bag of named setups; several are ES/NQ-relevant and statistically backed. The
futures-relevant ones are captured in full; the crypto/altcoin-specific lessons are summarised (incl.
Severin's altcoin workflow, which is useful as a top-down process even though it's crypto).

Vault cheat sheets referenced: **"Volume Profile Indicators"** (1), **"Liquidity"** (4), **"50 SMA
Strategy"** (2), **"CCV 2.0 Setup"** (12), **"Altcoin Trading Strategy"** (7).

---

## 1. Fixed Range 2.0 — `fixed-range-2.0` ★  ·  Vault: "Volume Profile Indicators"
Mark the most significant **VAH / POC / VAL** across **five** fixed-range pools within one timeframe
range: (1) lower-TF range, (2) medium-TF range, (3) latest major low → current price, (4) latest major
high → current price, (5) a **new untapped price-action range**. The **untapped POC** is a key trade
target. Pick the levels that matter by context/intuition (improves with months of practice). Daniel's
favourite way to mark volume levels for swing trading.

## 2. 50 SMA Strategy — `50-sma-strategy` ★ (Igor)  ·  Vault: "50 SMA Strategy"
The **50 SMA** as dynamic S/R + momentum-direction guide (lagging, so it confirms where price came
from). **Enter on the first pullback to the 50 SMA after a momentum change** (the SMA rolling over /
curling up). **On the ES you typically get >10 ticks average from the first touch.** Trade in the
SMA's direction (up → buyer, down → seller); when unclear, hide price and read the SMA slope. Any TF
(1-min for scalps; ES on TR 3/1). Backtest the first-touch-after-a-break stat.

## 3. Reclaims and Rejections — `market-replay-reclaims-and-rejections`
Market-replay walkthrough: trading **level reclaims** (price loses then reclaims a level = strength →
long) and **rejections** (fails and rejects = short), confirmed with order flow. Same mechanic as the
S/R-flip + failed-auction work; a replay practice session rather than new rules.

## 4. Swing Failure Pattern 2.0 — `swing-failure-pattern-2` ★ stats  ·  Vault: "Liquidity"
Statistical layer on the SFP, for **TP and management** (1-30 min TFs). Key stats (bear market):
- **52% probability price takes out the last high before the SFP** before continuing down.
- **64% probability of a reaction** at the last low/high made before the SFP; **52% probability of a
  reversal** from the last high (26% from the last low).
- Use the **last low/high before the SFP as a take-profit** (resting liquidity there). A debatable
  last low: use the higher one for a safer TP, the lower (more confluence) for a reversal trade. Can
  hold both a long and short, waiting for one to stop out.

## 5. Swing Trading — `swing-trading`
Swing framework (4h-daily/weekly): HTF bias + key levels, enter at confluence with LTF trigger, wider
targets, manage on structure. Mostly consolidates the risk/management rules already in the playbook
and `entering-trades.md` (TP%, moving stops, compounding).

## 6-9. Altcoin trading + Severin's Altcoin Strategy — `Altcoins-trading-strategy`, `severin-altcoin-strategy-part-1/2/3`  ·  Vault: "Altcoin Trading Strategy"
Crypto-specific (less relevant to ES/NQ), but **Severin's top-down process (part 1) is a good workflow
template**:
- Build a **watchlist** from Bybit's Market Overview (24h % move + transacted volume); narrow to 5-10,
  then 3-5 for setups.
- **Bias order follows correlation: BTC first → ETH → smaller-cap alts.** Never counter-trade the
  BTC/ETH bias.
- Per coin: read **market structure + price location** (mid-range / retracement / significant move) →
  mark **HTF monthly/weekly/daily levels** → draw **HTF fixed ranges**, look for confluence of HTF
  levels with **VA extremes (VAH/VAL)** (POC only when it adds confluence) → nearby liquidity + naked
  POCs (ExoCharts) → **CC / 0.25 fib** → check R:R with TradingView's tool → alerts → **1% risk**
  sizing. Trade alts only after mastering BTC; keep tools simple (low volume).
- Parts 2-3 continue the same crypto workflow with examples (not transcribed in detail).

## 10. Trading All Time Highs — `ath-strategy`
At ATH there are no horizontal levels, so lean on **fib expansion/extension targets** (see
`fibonacci.md` §3), measured moves, psychological round numbers, and order flow. Don't short an ATH
without strong multi-layer confluence + tight invalidation; buying all-time *lows* is lower-risk than
shorting all-time highs.

## 11. Liquidation Trading Strategy — `liquidation-strategy` (crypto scalp)
Price is drawn to **liquidation pools**; scalp the flush + snap-back (1-5 min, 5-15m charts). Find
**consolidation below resistance / above support**, use Bybit's liquidation calculator (100x, entry =
consolidation low) to get the liquidation price, and buy **just below** it (never at it — liquidations
extend the wick). Confirm with **ExoChart liquidation bubbles + falling OI + bullish CVD divergence**.
Market order once bubbles print (safer) or a preset limit. **Stop just below the liquidation wick**;
first target = nearest SR flip. (Crypto-leverage mechanic; the futures analogue is a stop-run/SFP.)

## 12. CCV 2.0 — `ccv-2-lesson` ★  ·  Vault: "CCV 2.0 Setup"
Fixes the original CCV's poor entry (longing after the move already happened). Wait for a **normal CCV
setup** to form, then wait for a **fake-out of the current daily high (short) or low (long)** and
enter on that as an **SFP / failed auction**; target the other side of the value area. Don't short
immediately on the break of the daily high — wait for the SFP/failed auction. (CCV got too popular →
lower win rate, so 2.0 front-runs it.)

## 13. Opening Range Breakout — `opening-range-breakout-strategy` ★ ES/NQ
- **Opening Range = the high/low of the first 15 min of the RTH session (9:30-9:45 ET).**
- **Long** if price breaks the **OR high AND is above VWAP**; **short** if it breaks the **OR low AND
  is below a down-sloping VWAP**. VWAP alignment filters low-quality breakouts (institutional fair
  value). Entry confirmation = a **1-min close** beyond the range.
- **Target = 1.5x the OR size; stop = opposite side of the range.** Wide ranges: use micro contracts /
  smaller size, or target 1x with a tighter structure-based stop and watch the 50% midpoint. On NQ the
  full-range stop is often too much risk — tighten on structure, trail on 1-min swing highs/lows. Take
  partials, re-enter at the 50% retrace only while VWAP + structure stay aligned. NASDAQ example: OR
  high 23668 / low 23587, broke down with downward VWAP.
