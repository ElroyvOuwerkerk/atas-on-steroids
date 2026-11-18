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

## 11. Investing / asset screening — Igor's 2026 picks, confirmed from the Vault

Elroy's hunch was that **Igor** built an AI method to find interesting assets and ETFs (stronger than
Elroy's own Sproutly). It holds up. The transcripts alone were ambiguous (in the Investing and
Diversifying roundtables the presenter who says "my model" quotes Igor in the third person, and the
`ai-for-stock-analysis` lesson is unsigned). But the **Vault "Master Instruction Block" package**
(downloaded 2026-11-17 with Elroy's OK) settles it: its TradingView watchlist file is headed
**`###TOP PICS 2026 (BY IGOR)`** — ~50 tickers across sectors, Igor's own 2026 research list. So **Igor's
asset picks are real and named in the artifact.** The companion tool is the **Structured Equity Analysis
Model** (a fixed 9-step LLM prompt with a 1-5 rubric and conviction bands); that prompt PDF carries no
author line, so it is best described as the scoring engine bundled with Igor's picks. Full verbatim prompt
and watchlist: `../transcripts/investing/master-instruction-block.md`; synthesis in `investing.md`. Note
this corrects an earlier transcript-only read that had called the method "not Igor's".

## 12. Live trading: how he actually trades the open (10 sessions, Oct-Nov 2026)

Source: the ten `../transcripts/live-trading/igor-live-trading-*` streams (06-10 through 17-11-2026),
his own live futures sessions on NQ/ES (plus BTC and single names like NVDA/AMD). These are where the
non-mechanical edge shows, which is why Elroy wanted them. Attributed to Igor by the `igor-live-trading`
slug. 2-3 hours each; this section is synthesised from the portal Highlights of all ten.

### The method he runs live
- **Minimal, functional chart:** VWAP and anchored VWAP (re-anchored from session lows or from a "box
  flip"), opening range (often a 1-minute ORB drawn on a 30-min chart), market profile (value area
  high/low, POC, untested/naked POC, single prints, P/B shapes, A/B/C/D sessions, Initial Balance),
  50/100/200 SMAs (and the weekly 200 SMA as a macro line), Fibonacci quartiles, delta/CVD,
  footprint/DOM/bookmap, options open-interest walls, and DeMark Sequential 9/13 exhaustion counts. He
  stresses simple tools *in confluence*, never in isolation (03-11, 06-10).
- **Confluence stacking:** a level earns a trade when several cluster - prior-day POC + prior-day value
  + ORB + VWAP + round number + untested POC (09-10, 10-11, 13-11). An untested/naked POC is a magnet he
  targets to the tick (20-10, 13-11).
- **His signature entry - absorption-and-reclaim** (17-11, 20-10, 23-10): find an area of interest;
  watch large sell orders getting filled while price refuses to drop (absorption); draw a trendline
  across the absorption highs; wait for a break and hold above that neckline; confirm with a spiking
  inverted delta (buyers stepping in); stop below the swept/absorption low; target >=2:1 to the next
  resistance. On 23-10 he ran it live against a 2,338-lot resting order.
- **Other setups he names:** ORB breakout/retest with measured T1/T2/T3 extensions; the **15-minute
  retest** (after the opening bar, the retest of the prior 15-min bar's open/close is his
  highest-probability continuation in a trend, 20-10); halfback rotations; momentum off bull/bear flags
  (a 45-degree trendline on the consolidation, 09-10); one-to-one extensions scaled at 75% (27-10).
  Reclaims are validated by **one-minute closes** above/below the level, not intra-bar spikes (03-11,
  27-10). The "75% rule": expect a pullback near 75% of the prior 15-min bar, with 25% as continuation
  support (20-10).
- **Targets:** untested POC, gap fills, value-area rotation, left-behind imbalances, and overnight
  highs/lows as draws on liquidity.

### The real edge: discipline, patience, psychology (the "watch him live" part)
- **The Lemon Shop analogy:** limit your operating hours; fatigue and overtrading wreck decision
  quality, so he defines a clear session end to stop giving profits back (06-10). "Markets will be here
  tomorrow - no need to force trades" (20-10).
- **Let the market provide trades, don't hunt them** (03-11). One quality trade per session beats many
  low-conviction entries; if nothing sets up, walk away (06-10, 10-11).
- **"It either works or it does not."** Define risk (~$150/lot) and reward (300+) up front and strip the
  emotional attachment from the outcome (20-10).
- **Don't fight the market.** "A reversal is a pullback within a trend" (23-10). His own psychological
  pivot - from reversal-trading against the trend to disciplined momentum-with-bias - is what
  "dramatically improved consistency" (06-10). Counter-trend only on a confirmed structure shift; he'll
  reject a counter-trend long purely on the ~$400/contract risk against the dominant trend (13-11).
- **Emotional discipline after a missed trade:** stay calm, process it objectively; hindsight clarity
  does not mean the trade was executable in real time (20-10).
- **Account size as a psychology tool:** ~$1,000 minimum on MNQ, $2,500 reasonable, $5,000 preferred -
  so watching P&L doesn't drive execution (27-10).
- **Consistency over size:** modest, repeatable profits ($100-300) are the foundation; small consistent
  gains "pay for the tuition" and compound the skill (09-10, 17-11).
- **Late vs early:** "there is always a next trade" - entering after confirmation at a slightly worse
  price with clearer structure beats being early into an unclear setup; never chase, the first
  resistance is usually the prior swing high (10-11, 17-11).
- **Stand-aside signals (no-trade zones):** a flat or down-sloping VWAP, opening at equilibrium (the EQ
  midpoint removes directional confluence), and the midpoint of a range are all low-probability - he
  walks away until a level flips (03-11, 13-11, 20-10).
- **Tempo read:** slow tempo *into* a level signals exhaustion/reversion; high tempo pierces through;
  stops triggering confirms an expansion phase (27-10).
- **Risk discipline at the open:** after an early profit, size down and get cautious to avoid handing it
  back (10-11).

### Live trade management
- Scale partials at defined targets; move to breakeven once momentum confirms; then trail on structure.
- He trails by **placing a resting market stop below the swing low** (for longs) rather than manually
  nudging it, so a structure break auto-exits (20-10). Two-bar / one-time-framing: hold while price
  keeps one-time-framing in your direction; a break of the prior bar reverses the sequence (20-10).
- Instrument split: on ES he lets trades cook to target and trails two bars back; on NQ he takes quicker
  1-minute-structure entries when ES is hitting a key level (17-11). Reads delta-vs-price divergence
  (rising price, weak/again negative CVD) as a cue to take profit rather than hold (06-10, 10-11, 17-11).

### In his own words (the live texture, from full reads of 06-10, 20-10, 27-10)

This is the part you only get by watching him trade, and it is mostly psychology and process.

- **The Lemon Shop, in full (06-10).** A trading day is a lemon stall with fixed opening hours. Each
  partial booked is "selling a lemon"; the first customer leaving is the first scale-out. When the
  session target is done: "you've sold all the lemons, there's no more lemons to be sold, now just get
  up, and tomorrow you come back and do exactly the same thing." Between sessions: "skip London, go
  drive your car, check the wheels, then come back on New York and wait for that nice momentum trade and
  boom, hit it." The whole point is the hard stop - "shop is closed, you walk away, otherwise a lot of
  traders will be in a good day and then give it all back." He notes closing the shop on a trending day
  "is a challenge in itself," which is exactly why he drills it.
- **Absorption-and-reclaim, narrated live (20-10).** "Two massive sell orders, price absorbed, draw a
  trendline... that's a playbook." His mental image: "whoever is selling there, there's a bigger play on
  the other side saying, yeah, you can sell everything you have, give me all your lemons, I'm gonna eat
  them all - then price pushes through and you want to see that delta invert instantly, boom, massive
  delta spike, and you put your stop literally below that low." Stops: one or two ticks below the
  absorption low (he showed lows at 62/63 -> stop below 62). Risk ~$150/lot to make at least $300.
- **The one risk mantra:** "In your head, this is all you have to say to yourself: it either works or it
  does not." He takes scalp longs "to let it cook, and if it doesn't, I'm out - I don't need to be
  overly long exposed" (20-10). Trailing is non-negotiable and mechanical: "always trail," by moving a
  resting market stop two candles back as price one-time-frames (20-10).
- **Why momentum beats reversals, shown by a real member (06-10).** A member (MES) who was "down big
  yesterday" finished "up today"; pressed on what changed, the answer was trading *with* the trend plus
  the lemon discipline instead of fading it. Igor: "reversal trading can be psychologically really
  tiring because you're literally going against the trend all the time." His low-pain-threshold logic:
  "we don't like to be in drawdown, so get in when the market is already moving, your stop only a few
  points away - psychologically it feels good. If you don't see it on Monday, walk away. If it comes
  Wednesday, risk 10-15 points and get 30."
- **Patience, in a member's own words (27-10).** A member: "watching the live price action, I have
  suppressed the urge to jump into all kinds of trades - that's why I value these live trading sessions,
  it teaches me to be patient." Igor "practices what he preaches," skipping his own would-be entries
  when structure is poor ("this was not really in the books to be a buy here; I'd have preferred to see
  it lower"). The common error he names: "everybody is a bit too early or too late, and then their stop
  is tapped."
- **Tap vs reach / tempo (27-10).** "I consider that a tap, because it's at slow tempo - if it was high
  tempo it would have gone through; slow tempo means not many people are actively going for it." Stops
  triggering is how he confirms an expansion phase.
- **Account size as a psychology tool (27-10), verbatim:** "$1,000 minimum, $2,500 reasonable, $5,000
  perfect, and build from there - so you're not relying on it, you just focus on the high-probability
  setups" without P&L-watching driving the trade.
- **"Only what makes sense" + visual over numbers (06-10).** "I only trade what makes sense to me; if it
  doesn't make sense, I don't touch it." On his Delta bars vs raw footprint numbers: "your brain can't
  compute all of that, you're not a cyborg - with the visual bars it becomes easier, because price
  follows delta."
- **The mindset talk (06-10).** An unprompted stretch on the psychology of success and jealousy -
  surround yourself with good energy, drop people who bring you down - and, quoting Severin, "compare
  yourself with yourself." On the timeline: "go at your own pace; if it takes five years, so be it...
  like a car journey, the people who speed have accidents, the one who goes slow and steady gets from A
  to B with no problems." He also encourages member accountability (one member journaling publicly:
  "talk about accountability and transparency, that's really nice").

### How this connects to the indicator work
This is the live companion to `AbsorptionSqueeze.cs` and the `igor-dsf-method` note: the
absorption-and-reclaim model (resting sell orders absorbed -> trendline break -> inverted-delta
confirmation -> stop below the low -> 2:1) is exactly codeable as a signal, and the untested-POC magnet,
the one-minute-close reclaim trigger, and the 15-minute-retest continuation are all discrete, testable
rules. Nothing here is backtested - same caveat as the rest of igor.md.

## Open items

- Igor material captured: 4 morning-update dailies, the DSF/market-profile lesson, and (added
  2026-11-18) 10 live-trading streams (Oct-Nov 2026, section 12). The 36 un-captioned Igor trade-recaps
  could not be transcribed (no subtitle tracks). His dealer-
  positioning and options-gamma read (the DSF "dealer levels" that Severin cites off the shared sheet)
  is referenced here but not fully transcribed; the deeper dealer-gamma method lives in the
  `igor-dsf-method` memory note and the order-flow notes rather than in a single captured lesson.
- Nothing here is backtested. The method is as he states it; whether the absorption and
  untested-level reads pay out on Elroy's instruments is an open measurement.
