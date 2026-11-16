# Igor's method, from the transcripts

A synthesis of Igor's trading method, built from the verbatim transcripts in `../transcripts/` and
cross-referenced with the existing order-flow notes. Every claim links to a source file and a
timestamp. Quotation marks mark his own words; everything else is paraphrase.

Sources:
- `../transcripts/contender-sessions/market-profile-and-dsf.md` (his framework lesson, 58:55)
- the four Igor daily updates `../transcripts/daily-update/daily-update-{28-10,04-11,09-11,16-11}`

Related material already in the repo: `../notes/orderflow-and-footprint.md`, `../notes/tpo-series.md`,
`../notes/quantower.md`, the glossary `../notes/glossary.md`, and the `AbsorptionSqueeze.cs` indicator
in `../../indicators/`, which is built on his DSF/absorption read. Severin runs Igor's DSF and
Quantower levels off their shared sheet, so where Severin's notes cite DSF, this is the source method.

## 1. What distinguishes Igor

Igor trades the index futures intraday on Quantower, reading market profile for context and the
depth-of-market surface (DSF) for live order flow. His opening is formal, "Good morning traders,
hello and welcome to this morning update", and the session is a structured read of the overnight
auction, the day's reference levels, and where participants are actually transacting. Where Severin
is a multi-asset, swing-and-position liquidity-auction trader, Igor is closer to a pure intraday
order-flow and auction reader: profile tells him where, order flow tells him whether anyone is there.

## 2. The two-tool framework: Market Profile plus DSF

The core of his lesson. Two complementary tools (market-profile-and-dsf [00:00], [08:48], [20:00]):
- Market Profile (TPO) organizes data for one-glance context: where value is developing, acceptance,
  rejection, balance, overlapping value areas.
- DSF (the liquidity map) confirms whether participants are actually transacting there, with a live
  view of resting liquidity and intent.

The workflow he states (market-profile-and-dsf [20:00], [54:40]):
1. Context from market profile and the areas of interest.
2. Participation from the 30-minute volume profile.
3. Confirmation from DSF (resting liquidity and intent) plus delta for likely direction.
4. Execution with defined risk.

## 3. Market Profile / TPO

- Session letters A through M: A is the cash open at 09:30 NY, M is the cash close
  (market-profile-and-dsf [02:06]). The A-D sessions "turn the lights on" so the opening range,
  counter-trades and developing value become visible by the C session (market-profile-and-dsf [17:50]).
- Core terms: point of control (POC), value area high/low, unfinished business, unfinished auctions
  (market-profile-and-dsf [01:11]).
- A poor low is an auction without excess, which signals incomplete rejection and a likelihood of
  being revisited (market-profile-and-dsf [02:33]). Poor highs reveal rejection even when price trades
  above value (market-profile-and-dsf [27:12]).
- Overlapping value across sessions raises the probability of rotation back inside range, so do not
  take a continuation trade without checking prior sessions (market-profile-and-dsf [03:14]).
- Overnight (Globex) inventory relative to the prior day's settlement anticipates counter-auctions at
  the regular open, and only yesterday's overnight profile is meaningful; older ones have low volume
  and irrelevant POCs (market-profile-and-dsf [04:34], [05:36]).
- POC as "pit stops": price travels from value area to value area and pauses at the POC; acceptance
  above or below decides the next directional leg (daily 28-10 [1:12:00]).
- TPO tick settings scale with volatility: higher tick is less sensitive, lower tick is raw precision.
  NQ/ES often use tick 4-10, BTC tick 5-10 (daily 09-11 [1:01:00]).

## 4. DSF and order flow

- The DSF is a live liquidity map. Dark zones with no resting orders suggest price travels through
  them; concentrated liquidity validates an area of interest (market-profile-and-dsf [07:08]).
- Watch order-placement patterns: traders adding resting liquidity lower while price still seeks value
  lower (market-profile-and-dsf [08:19]). If DSF shows strong resting orders at a weekly value area
  high and the auction is attracting business there, that level is less likely to hold
  (market-profile-and-dsf [14:12]).
- Icebergs: hidden, replenishing orders that absorb activity before price can break through. Watch the
  tape to recognize them (market-profile-and-dsf [24:00]).
- Low participation at auction extremes is exhaustion. Fewer than ten lots on NQ at a profile extreme
  suggests a rotation (market-profile-and-dsf [15:29]).
- The "banana auction" analogy for price discovery: price is only meaningful when participants
  transact; rejection happens when no one engages; price moves to attract participation, from no
  activity to peak engagement and back (market-profile-and-dsf [09:23], [13:40]).
- Data quality matters for serious order flow: Rithmic feeds give deeper, more detailed DOM than CQG
  (market-profile-and-dsf [22:30]); tick-by-tick accuracy over aggregated data (daily 28-10 [09:02]).

### Delta, CVD and absorption
- CVD divergence: lower CVD lows without lower price lows means sellers cannot push price down, a
  bullish absorption signal (daily 28-10 [18:50]).
- Aggressive genuine buying: rising open interest plus rising delta plus price holding support
  (daily 28-10 [31:30]).
- Absorption: CVD rises aggressively but price fails to move, so large limit sellers are absorbing the
  buying, which often precedes a reversal (daily 28-10 [1:04:45]). This is the read the
  `AbsorptionSqueeze.cs` indicator targets.
- Trapped-trader analysis: open-interest increases mark longs stuck above price; a three-bar pattern
  (open, retest, close below) is the short trigger (daily 28-10 [58:30]).

## 5. Reading the open and the levels

- Gap / overnight read at the open (market-profile-and-dsf [25:40]):
  - Overnight above yesterday's RTH value and the gap holds: bullish.
  - Gap fills completely: expect rebalancing.
  - Gap holds above settlement: potential long continuation.
- The deep-flush-and-reclaim long: a confirmed excess wick, a reclaim of a key moving average (the
  daily 200 SMA), and a close back above the prior day's close (daily 04-11 [06:30], [08:00], [19:00]).
  Switching to a line or area chart filters wicks and stop runs to focus on meaningful closes
  (daily 04-11 [22:30]).
- Reference levels: yearly high/low and the yearly midpoint (yearly EQ) as a long-term magnet
  (daily 28-10 [05:53]); monthly volume-profile POC/VAH/VAL (daily 28-10 [10:52]); pivot points, where
  S1, R1 and the central pivot act as attractors (daily 28-10 [14:11]); daily 200 SMA; 50 and 61.8
  percent retracements of the impulsive move (daily 04-11 [33:00]); single prints and excess wicks.
- The "look below and fail" pattern: price sweeps the lows but fails to accept below, setting up mean
  reversion back into the prior range (daily 28-10 [21:27]).
- He uses the "line in the sand" too: the overall swing low is the invalidation of a bullish thesis
  (daily 04-11 [10:30]). Structural integrity holds while higher highs and higher lows remain intact
  (daily 04-11 [13:00]). Reclaim levels are the triggers: QQQ 700, BTC 73.3-73.5K, BTC 75K weekly 50
  percent (daily 09-11 [00:09:30], [00:36:00]; daily 16-11 [09:38]).
- Coinbase premium as confirmation on BTC: still deeply negative, so spot demand is weak despite a
  price recovery; its absence supports a risk-off thesis (daily 28-10 [16:40]; daily 16-11 [22:46]).
- He flags BTC as thin and inconsistent for intraday trading (the 66K-77K zone has little acceptance)
  and prefers the index futures for cleaner respect of levels (daily 16-11 [13:04], [20:48]).

## 6. Worked entry setups (from the lesson)

- Poor Low Repair Short (market-profile-and-dsf [27:12], [32:19]): trading above yesterday's value
  looks bullish, but poor highs reveal rejection and delta decreases on the upward pushes. Entry when
  price fails above the POC and returns below it. Tight stop above the failed POC, scale targets at
  the prior lows, conditional orders to add on confirmation.
- Overnight Continuation Long (market-profile-and-dsf [46:30], [52:00]): a weak high, then a half-back
  retest with no participation, then a delta spike into support confirms the long with minimal risk.
  Stacked offers above act as magnets; collapse the position incrementally near the prior overnight
  highs.
- General preference: confirmation entries over anticipating tops and bottoms, trading with momentum
  rather than against it, to avoid repeated small losses (market-profile-and-dsf [38:00]).

## 7. Trade grading and the confirmation signal

Directly relevant to the indicator work. His grading (daily 04-11 [47:00], [52:00], [55:00]):
- A++ trades: multiple untested levels with a reversal confirmation.
- A trades: a single untested level with a structure-break confirmation.
- C trades: retest-based setups (POC, opening-range equilibrium), suitable for scalping.
- The confirmation only fires when price breaks the structure of the reaction candle at the level,
  which reduces the risk of being run over on a premature reversal.
- Clean-chart philosophy: a well-built signal framework can reference historical untested levels
  automatically and flag high-quality trades without pre-drawn levels. This is the same design intent
  behind the LevelTrigger / scoreboard and `AbsorptionSqueeze.cs` work.

## 8. Risk and trade management

- Tight stops above the failed POC; scale and take partials; trail by minor structure (lower
  highs/lower lows) (market-profile-and-dsf [36:36]). Do not place stops directly on visible
  liquidity, because of the first-in-first-out queue (market-profile-and-dsf [36:36]).
- Build a day-trade position with trend confirmation: add on evidence (one-time framing, positive
  delta) and trail stops below prior period lows for extended holds; example stop at the period-F low
  below POC (market-profile-and-dsf [56:30], [57:30]).
- Worked ES long: entry near 6475, stop below 6461, targeting the 80s for roughly 3:1
  (daily 28-10 [47:00]). On an event day like FOMC, trail longs through resistance rather than taking
  fixed targets, since volatility can extend the move (daily 28-10 [56:00]).
- Consistency: "traders who constantly change plans never become consistent" (daily 28-10 [1:06:30]).
  Come to the market without demanding a specific outcome, and use order-flow tools to confirm whether
  a desired level actually has participation (daily 28-10 [1:21:00]).

## 9. Psychology

- Over-trading rules: one trade per 30 minutes, or a cooldown such as the "Woodchopper" 10-minute
  pause after a loss, to clear the emotional fog (daily 09-11 [45:00]).
- Revenge trading and chasing losses: account for every trade's potential loss before entry, return to
  the model and wait for the next valid setup, and if emotional, step away, breathe and reset, or stop
  for the day (daily 09-11 [48:00]).
- Context can override a textbook setup; he trusts the intuition built from screen time, but only on
  top of the process (daily 09-11 [48:00]).

## 10. What maps to an indicator

- Absorption detector: CVD or delta rising while price fails to advance, flagged as likely reversal
  fuel. This is the `AbsorptionSqueeze.cs` target; the dailies give the exact read (daily 28-10
  [1:04:45], [18:50]).
- Trapped-trader / open-interest divergence: rising OI with price stalling above a level, plus the
  three-bar open-retest-close-below trigger (daily 28-10 [58:30]).
- Untested-level engine with reaction-candle confirmation: the grading in section 7, which references
  historical untested daily/weekly/monthly levels and fires only on a structure break of the reaction
  candle (daily 04-11 [47:00], [52:00]).
- Poor-low / poor-high detector: an auction extreme without excess, marked as a revisit magnet
  (market-profile-and-dsf [02:33]).
- Overnight-inventory-versus-settlement classifier for the open bias (market-profile-and-dsf [25:40]).

These align with `../indicator-backlog.md` and the live DSF/HMD depth-history work noted in the ATAS
facts (`atas-depth-history-hmd` in memory). Live, an ATAS indicator only receives about twenty prices
a side of the book, which limits how much of Igor's DSF read can be reproduced in real time.

## Open items

- These four dailies and the one lesson are the Igor material captured in the window. His dealer-
  positioning and options-gamma read (the DSF "dealer levels" that Severin cites off the shared sheet)
  is referenced here but not fully transcribed; the deeper dealer-gamma method lives in the
  `igor-dsf-method` memory note and the order-flow notes rather than in a single captured lesson.
- Nothing here is backtested. The method is as he states it; whether the absorption and
  untested-level reads pay out on Elroy's instruments is an open measurement.
