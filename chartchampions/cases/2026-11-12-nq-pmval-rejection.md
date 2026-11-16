# Case: NQ pmVAL rejection → flush (2026-11-12)

Reconstructed from a footprint screenshot Elroy shared on 2026-11-12 (~15:02 CET) and read with him. Instrument **NQ** (confirmed by Elroy; 28232/28163 are his NQ pmVAL/pdVAL). Platform: Rithmic / OMNE footprint, ~1-minute candles, times as displayed **13:58–14:08** (the chart's own timezone, not independently confirmed).

⚠️ **OHLC per candle was not in the screenshot** — only the per-candle delta/volume footer, the cumulative (session) delta, and the drawn levels. This is a reconstruction for the build session to diagnose a missed signal, not a tick export.

## Levels drawn on the chart
- **pmVAL ~28232.5** (previous-month VAL; yellow dotted + purple line) — the rejection level.
- **pdVAL / VAL ~28163** (blue) — previous-day VAL; broken on the flush.
- 28210 (yellow), 28156 (pink), 28135 (cyan).
- **ONL 28106** (overnight low, red dashed) — downside target.
- Two descending purple dashed lines through ~28200→28150 = anchored-VWAP-style resistance (price was one-time-framing down under them).

## Per-candle footer (left → right, ~13:58–14:08)
| # | ~time | delta | cum. session delta | volume |
|---|-------|-------|--------------------|--------|
| 1 | 13:58 | −94   | 196  | 278  |
| 2 |       | −25   | 171  | 187  |
| 3 | 14:00 | −41   | 130  | 405  |
| 4 |       | −25   | 105  | 159  |
| 5 |       | +14   | 119  | 78   |
| 6 | 14:02 | −32   | 87   | 86   |
| 7 |       | +45   | 132  | 287  |
| 8 | 14:04 | −35   | 97   | 329  |
| 9 |       | −145  | −48  | 427  |
| 10| 14:06 | **+277** | **+229** | **1817** |
| 11|       | **−505** | **−276** | **1721** |
| 12| 14:08 | −21   | −297 | 31   |

(Cumulative session delta is a running total; each candle's delta is its increment — it reconciles across all 12.)

## The read (the test case)
- **Candle 10 (+277 / 1817):** aggressive buying probe UP into **pmVAL ~28232**. Footprint was **thin at the extreme** (28230-40 = single / low-double-digit prints) vs heavy volume lower → volume drying at the auction extreme. Price rejected hard (long upper wick, close back ~28170). Big positive delta with no price hold = **absorption / trapped longs**; a **poor high / failed auction** at pmVAL.
- **Cumulative session delta topped +229 exactly at candle 10's high and refused to extend** → **bearish delta divergence** at the high.
- **Candle 11 (−505 / 1721):** the **flush** — trapped longs out + fresh aggressive sellers; breaks **pdVAL 28163**.
- **Outcome:** ~25 more NQ points down after these two candles (Elroy-reported), toward / through **ONL 28106**.

## For the Level Trigger build session
- The indicator logged **no entry at 28232.50** in this window (per CC | Indicators) → this is a **missed case to diagnose**: did the rejection / absorption path not arm, and why.
- Validate these candidate order-flow words against this case: **absorption** (large delta into the level, no price progress), **volume dried up at the extreme**, **delta divergence** (cumulative delta makes a new extreme while price fails to).
- ⚠️ Unverified for a precise fixture: exact per-level footprint volumes (only qualitative "thin at the top"), the chart's timezone, and OHLC. A tick/footprint export of NQ 13:58–14:08 would turn this into a hard test fixture.
