# Severin's method, from the transcripts

A synthesis of Severin's trading and investing method, built from the verbatim transcripts in
`../transcripts/` and cross-checked against the earlier trade-log reads in `../live-reads.md`
and `../live-reads-trades.md`. Every claim links to a source file and a timestamp so it can be
re-checked. Quotation marks mark his own words as they appear in the captions; everything else is
paraphrase. Where the transcripts only imply something, it says so.

This is the deep pass. It draws on the full set of Severin material captured in the last 30 days:

- `../transcripts/contender-sessions/context-mastery-liquidity-auctions.md` (the framework lesson)
- the five Champions live streams `../transcripts/champions/champions-384..388` (19-10 to 16-11)
- the five `../transcripts/live-trading/severin-live-trading-*` sessions
- the seventeen Severin daily updates in `../transcripts/daily-update/daily-update-*` (19-10 to 13-11)
- the seven Severin BTC updates `../transcripts/daily-update/daily-update-btc-*`
- the ten Severin lessons in the Building Plans & Context course
  (`../transcripts/courses/building-plans-and-context/`): Trading Bias 1-2, Context-Volume-Tips, the
  Scalping Strategy series (overview, daily plan, execution), the 3 Candle Entry plus two refinements,
  and How/when to modify your plan. These lessons are the systematic foundation (sections 14-18 below);
  the 30-day application above shows how he uses it. The three Traders Checklist lessons in that course
  are Daniel's, not Severin's, and are in `daniel.md`.

Timestamps reference each file's `## Transcript` and the portal's own Highlights index. Igor's four
dailies are synthesized separately in `igor.md`; the stocks Contender lesson is Daniel's and is in
`daniel.md`.

## 1. The core idea: liquidity auctions

Price moves from one meaningful liquidity region to the next, and each region is the contextual
target for the move out of the last one. Supply and demand zones and significant previous
highs/lows are the regions (context-mastery, Highlights [11:30], [10:20]). A trade is judged first
by whether it runs with the active auction, and only second by its confluence. A stacked confluence
does not override what the market has been doing: context first, confluence second
(context-mastery [06:30]). The daily updates repeat this as a hard rule. Both context and confluence
must align for a high-quality trade; longs at 70K-68K were poor because context had flipped bearish
even though a level sat there (champions-388 [23:00]).

Rules for which way an auction points (context-mastery [13:20]):
- Supply flips to demand: target is the previous demand or the previous high.
- Demand flips to supply: target is the previous supply or the previous low.
- Sweep (SFP) of a low: target is the previous demand or the previous high.
- Sweep of a high: target is the previous supply or the previous low.
- Whichever region comes first.

A completed liquidity auction is itself a signal. On BTC, "demand flipped to supply" raised the
probability of a new low and told him there was no case for a long in that zone
(daily 12-11 [07:11]; daily-btc 29-10). The clean worked example: a demand retest targeted a new
low that lined up with previous demand, which started a fresh auction and the move higher
(daily 27-10 [25:50]). Auctions work fractally, so the same read filters scalps on the 5 and 1
minute as it frames the day on the 30 minute (context-mastery [31:30], [1:23:40]). He calls his
intuition "subconscious context reading" and nothing more (06-11 [24:02]; context-mastery [1:19:00]).

## 2. The daily process and what he trades

The daily update is a level-mapping session, not a signal feed. He maps the day's reference levels
across a fixed instrument set and states where a trade would and would not be valid, then waits.

- Instruments: BTC (USD and Tether pairs), the index futures ES and NQ, gold, oil, silver, and a
  rotating set of large-cap equities (NVIDIA, Google, Amazon, AMD, Tesla, Micron, AVGO, ASML,
  Palantir, SpaceX and others). The BTC-only updates are the short `daily-update-btc-*` series.
- Session: New York only. "The vast majority of significant moves occur during NY open; other
  sessions often produce traps or low-quality setups" (champions-384 [06:16]). Avoid the first ten
  minutes; if the first ten to fifteen minutes are tight and overlapping with no follow-through,
  the environment will not produce clean momentum, so step away (daily 22-10 [34:49]).
- He works from Regular Trading Hours (RTH, 09:30 to 16:00 EST) charts on CME instruments, because
  highs and lows formed in the cash session carry more weight than overnight prints
  (daily 23-10 [07:00]; daily 11-11 [14:30]). A true RTH gap exists only when there is no overlapping
  price between RTH sessions; internal overnight prints are not gaps (daily 11-11 [36:00], measured
  from the prior RTH low, [1:49:00] on 13-11).

## 3. Level selection: the toolkit

Context picks the region, then confluence refines the exact price (context-mastery [36:40]). The
tools he stacks, from the dailies and streams:

- Moving averages. The daily and weekly 50 and 200 SMA are the backbone. "For investing and swing
  context, weekly and monthly SMAs are kings" (daily 02-11 [29:00]); institutional desks reference
  the 50 and 200 on daily and weekly, while lagging oscillators like MACD arrive late versus the
  simple MA retest (daily 19-10 [51:00]). In a strong trend that will not pull back deep, the 1-minute
  50 SMA serves as the dynamic pullback entry (daily 23-10 [47:00]).
- Volume profile. Previous day / week / month / year value areas (VAH, VAL), point of control (POC),
  and naked (untested) POCs are the primary map. A-session POC and Initial Balance POC add confluence
  when they align within a few ticks (daily 19-10 [1:09:00]; daily 11-11 [48:00]). He distinguishes
  RTH profiles from 24-hour, and uses visible-range volume profile to spot thin zones where price
  travels fast (daily 02-11 [17:40]).
- Anchored VWAP, from a major volume-injection low or from an all-time high. Repeated reclaims above
  an uptrend anchored VWAP confirm bullish structure while price holds it (daily 21-10 [19:30]).
- CC Fibonacci (the ChartChampions Fib), plus yearly Fibonacci quadrants (25/50/75 percent). Price
  pausing at the yearly 25 percent level, aligned with the weekly 200 SMA, is a "region where value
  historically returns" (daily 13-11 [17:30]).
- Options walls. Major put walls act as magnets and support, for example the 7650 level on ES, and
  combine with profile and order flow for multi-layer confirmation (daily 13-11 [44:30]).
- Coinbase premium, as a sentiment confirmation. He waits for the premium to turn positive before
  adding to spot, because it signals genuine US institutional demand returning (daily 13-11 [12:20];
  daily 23-10 [21:30]).
- DeMark sequential 9 and 13 counts, used only as a confluence timing clue, never a standalone
  buy/sell (daily 06-11 [12:10], [30:20]; daily 19-10 [53:00]).
- Market-profile shapes. A P-shape is bullish, a B-shape is balance; when a P-shape fails to hold
  its POC, expect rotation into a lower B-shape and a move to value area low (daily 21-10 [40:30],
  [46:00]).
- TPO letters and the "Lemon Shop" session structure. Limit trading to the first four 30-minute
  periods, A B C D, and wait for the highest-quality setup rather than many mediocre ones. The
  B-session halfback-of-A and especially the C-session halfback-of-B are his most consistent intraday
  entries (daily 13-11 [28:00], [49:00], [51:00]).
- Single prints. They form on the 30-minute and need lower-timeframe execution; a row of single
  prints below a level is a textbook reaction zone (daily 05-11 [23:20]; champions-384 [20:00]).
- DOM / DSF order flow. Depth-of-market footprint, stacked bids, icebergs (visible size is a fraction
  of real resting size), delta and CVD confirm willingness to transact at a level before he pulls the
  trigger: "profile defines the where, order flow confirms the willingness" (daily 23-10 [86:00];
  daily 13-11 [41:00]; daily 06-11 [44:00]).

## 4. Level quality: which levels he will actually trade

Selecting the level is most of the edge, and he is strict about which ones qualify.

- A level decays each time it is retested. He calls an over-tested level "washed": the 83K zone
  tested across six sessions is "a washed level with lower reaction potential" (daily 19-10 [14:00]),
  and the third test of a support shows a weaker bounce than the first (champions-385 [11:20];
  daily-btc 20-10 [06:40]). Fresh, untested levels are the highest probability; retested ones are for
  scalpers (daily 23-10 [108:00]).
- The "line in the sand". For every instrument he names the single level that, if lost, flips the
  whole context: "it's kind of like the line in the sand where I would like to see it" hold
  (daily 22-10 [34:05]). The framing question is "which level must be lost for an extremely high
  probability of a new high or new low" (daily 03-11 [24:25]); as long as it holds, dips stay buys.
- Range quartiles. Do not short the bottom quartile of a range (the 25 percent level) because the
  risk-to-reward is unfavorable, and the mid-range is a no-trade zone (daily 02-11 [09:50];
  daily 21-10 [16:00]). The two highest-conviction zones are previous-month VAH (the reversal area)
  and previous-month VAL (the continuation target) (daily 20-10 [41:00]).
- "If one, then two" balance guideline. When one side of balance is taken and reclaimed, expect
  rotation to the other side; if the highs were taken, the preferred scenario is a sweep of the lows
  to complete the move (daily 19-10 [45:00]; daily 22-10 [19:01]).
- Finished versus unfinished business. A prior swing high with no excess (no clean wick) is
  unfinished, "a cookie for later" that the market returns to clear: "just hold that cookie until it
  takes the A session high" (daily 23-10 [13:30]). This needs accurate tick data, which is why he
  prefers paid CME tick data over TradingView for reading excess (daily 23-10 [11:00]).

## 5. Entry

- Trade with the active auction. "Trade with the trend rather than against it": in a market making
  lower lows and lower highs, prioritize shorts into pullbacks rather than fighting momentum
  (daily 23-10 [25:00]). The low-probability trades are the ones taken between the start and the
  target of an auction, and fading an active auction with retracement entries collapses the win rate
  (context-mastery [14:40], [18:00]).
- His two preferred, highest-win-rate triggers at key levels (daily-btc 22-10 [23:50]):
  - Swing Failure Pattern (SFP): a sweep of a level that reclaims. Reliable only at major highs and
    major lows, not minor swings. Do not treat every new low as an SFP long, and do not fight a
    downtrend with repeated SFP longs (daily-btc 12-11 [07:30]; daily 29-10 [06:00]).
  - Failed auction: a fake-out above the range high that returns below. Entry on the reclaim back
    inside the range, stop above the fake-out high, target the opposite side of the range. Stronger
    and more bearish than an SFP (daily-btc 22-10 [09:46]). A fake-out above a descending-triangle
    high is more bearish than the plain pattern because it sweeps short stops and traps new longs,
    turning both into fuel (daily-btc 29-10 [08:36]).
- Do not front-run a wick. A wick through a level is not acceptance; wait for a close, or an SFP that
  actually sweeps liquidity, before calling it (daily 20-10 [09:30]; daily-btc 10-11 [04:45]).
- The entry trigger itself is his three-candle method (COC, COT, momentum), the refinements being
  small edges that compound (context-mastery [00:35]). On a higher-timeframe reversal he waits for a
  confirmed 5-minute impulse before executing rather than catching the exact tick (daily 03-11 [34:18]).
- Fractal breaks in the trend direction, targeting the next opposing fractal, give smaller stops than
  catching a reversal (daily 23-10 [64:00]). A true fractal is two bars lower on each side of a high.
- Laddered entries. He ladders into a position on a retest rather than taking it all at once, for
  example "I'm going to ladder in if we get a retest of overnight low" (29-10 live [27:56]). On a
  monthly-level reversal, expecting a clean one-minute reaction is unrealistic, so he scales down to
  micros and builds a laddered position (daily 03-11 [30:00]). This is the "Severin ladder", and it is
  why a single average entry in a screenshot can hide a scaled-in build.
- Use realistic targets, the full liquidity auction, not a tiny one (22-10 live [38:36]). Before the
  open, wait for the market to show intent; do not short every pop or long every drop
  (06-11 live [1:20:00]). Sometimes the best trade is no trade.

## 6. Trade management: this is the edge

The transcripts line up with the trade-log finding in `../live-reads-trades.md`: his winning trades
are held to a context target with the stop trailed by structure, and the money comes from holding,
not from the entry or a fixed take-profit.

- Two jobs once in a trade: de-risk at the first logical liquidity target, then trail the stop by
  each new auction that forms (context-mastery [1:09:30]). De-risk means moving the stop to break-even
  once price no longer needs to retest the entry (06-11 live [1:14:00]); a stop only exists to stop a
  loss, so it can be pulled the moment conviction fades (29-10 live [1:00:08]).
- Trail by structure, but give the trade room first. He trails behind fractal highs/lows and scales
  the stop as price advances through liquidity nodes, letting the trade develop rather than
  over-managing (06-11 daily [56:00]). He calls the healthy version a "staircase": a trend trailed by
  "staircase structure, never breaking below prior pullback lows" (daily 13-11 [56:00], [1:45:00]).
  Giving the early part of the move slack is deliberate, to avoid being wicked out of a winner, the
  exact mistake he flags (22-10 live [13:26]).
- No mechanical partial take-profits on a high-conviction trade. "Remove fixed take profits on
  high-conviction trades; instead, trail stops closer to initial targets to allow winners to extend"
  (daily 23-10 [70:00]). Successive auctions are held with the whole position so profit compounds
  (context-mastery [1:12:30]).
- Trade around value, not around the POC. Value area high and low are where he trades; the point of
  control is a take-profit zone, because it is where the majority of volume and time transact
  (daily 13-11 [1:15:30]). The M-session close often prints very near the day's POC due to end-of-day
  covering, which is useful for managing late exits (daily 13-11 [1:22:00]).
- Reversal trades carry a roughly 75 percent chance of being stopped at break-even; he accepts the
  trade for a very high R multiple or a break-even outcome (daily 03-11 [35:03]). He separates two
  leverage types and manages each accordingly: setups built for a high win rate, and setups built for
  a high risk-to-reward (daily 03-11 [36:00]).
- Risk rules stated in the dailies: never lose more than 50 percent of the previous day's profit, to
  protect the equity curve (daily 23-10 [82:00]); ES respects levels to the tick, allowing tight
  10-12 tick stops of roughly 125 to 150 dollars, while NQ's spread runs 6-8 ticks (about 50 dollars
  a contract) so he uses micros or wider risk there (daily 23-10 [57:00]; daily 11-11 [50:00]).
- "Take profits while there are buyers, add while there are sellers", tied to the DeMark 12 and 13
  counts for trimming and adding (daily 06-11 [1:09:30]).

## 7. Hedging the spot book

A distinct theme from the dailies, not visible in the lesson alone. He hedges a profitable spot
position with a futures short to protect unrealized profit, not to make new profit.

- A short hedge taken at the yearly value area high captured a 5 percent drop; on a 50 percent spot
  gain, a 5-percent hedged drop preserves most of the unrealized profit (daily 27-10 [05:16], [06:28];
  daily 22-10 [07:51]). He fills the hedge with multiple limit orders.
- After the rejection he moves the hedge stop to break-even: if a reclaim invalidates the hedge, the
  spot thesis is still valid, so there is no reason to eat a hedge loss (daily 22-10 [09:03];
  daily 27-10 [09:00]). Patience on hedges matters, because repeated stopped-out hedges bleed the spot
  book (daily 27-10 [13:00]).
- When a key support is lost he will fully hedge via short selling or options, which preserves the
  spot holdings while eliminating downside, and unwinds once structure stabilizes
  (champions-388 [1:01:00]).

## 8. Position sizing and scaling

- Predefined, high-conviction trades get larger size; scalps get reduced size. Concentrate capital
  where conviction is highest (context-mastery [1:03:20]). Only A++ context-changing levels justify a
  large position; in-between levels are small scalps only (daily 20-10 [41:00]).
- A larger range to target justifies a built (laddered) position; a small range calls for small size
  (context-mastery [45:50]).
- Scaling up is a loop: set a target, disrupt the comfort zone, absorb the volatility impact on P&L,
  re-establish it as the new comfort zone, repeat (06-11 live [56:15]). Trading real size produces
  extreme mental stress and needs recovery time (06-11 live [24:40]). He runs multiple accounts for
  margin and contract limits; accounts above 100,000 dollars get double day-trading margin on NQ
  (06-11 live [41:20]).

## 9. Investing, as a separate book

He runs trading and investing as two separate books with separate invalidation. This is his own
framework from the dailies and streams; the Daniel stocks lesson is filed apart in `daniel.md`.

- Why two books: trading builds capital quickly through leverage, investing compounds that capital
  safely over time (daily 10-11 [29:40]). Trade and investment portfolios are managed separately with
  pre-defined invalidation (daily-btc 29-10 [21:00]).
- Buy-the-dip on quality equities at technical support, aiming for 20 to 30 percent per rotation, then
  recycling profits: he closed Amazon at a 24 percent weekly gain to redeploy into NVIDIA
  (daily 10-11 [29:40], [44:00]). In a large drawdown he frees up at least 30 percent of portfolio
  liquidity by trimming winners to redeploy into higher-conviction setups (daily 10-11 [29:40]).
- Thresholds. A 10 percent drop from all-time highs is his minimum before deploying investment capital
  in an index like QQQ; below that he ladders deeper (daily 20-10 [45:00]). Investment-grade upside
  wants at least 12 percent room back to new highs to make the risk-to-reward worthwhile
  (daily 03-11 [32:17]).
- The investment entry criterion (this is the correct source for the earlier mis-attributed claim): a
  daily candle into the anchored VWAP plus a value-area reclaim, not an intraday trigger
  (29-10 live [1:03:55]; daily 29-10 [08:30] on Google). Investor mindset holds through drawdowns;
  trader mindset closes on technical invalidation; the position size must match the chosen mindset
  (champions-384 [57:00]).
- Position size scales with the quality and rarity of the level (daily 29-10 [13:30]). Funding rates
  are an extra yield stream while holding a successful short: positive funding means longs pay shorts
  (champions-386 [1:11:00]). Year-end loss realization on drawdown positions can be tax-efficient
  (daily 12-11 [27:25]). Avoid doubling exposure to highly correlated assets such as gold and silver
  (champions-386 [1:14:00]).

## 10. Context and correlation reads

- BTC correlates with equities and rarely holds up in a broad drawdown; when equities sell off hard,
  BTC usually follows (champions-386 [07:40]; champions-388 [08:13]). BTC holding sideways while
  equities trend lower is a bullish longer-term tell (daily 19-10 [21:00]); BTC failing to reclaim a
  level while equities rally is relative weakness (daily 10-11 [12:45]).
- BTC is a "trader's asset" that gives gains back over months, so he trades it rather than blindly
  DCAing, and requires strength confirmation before accumulating (daily-btc 12-11 [04:26];
  champions-388 [38:30]).
- Elliott Wave is a probabilistic guide, not a prediction. He weighs a WXY correction against a
  1-2-3-4-5 impulse and notes wave one is the hardest to time (champions-385 [26:30];
  champions-388 [33:00]).
- "Trade the charts, not the news": if event-driven volatility is uncomfortable, stepping aside for 24
  to 48 hours is a professional choice (daily-btc 03-11 [17:20]). News clusters at lows; he separates
  unconfirmed FUD from a confirmed fundamental change before repositioning (daily-btc 10-11 [26:49]).

## 11. Psychology and the meta-skill

- "No trade is a valid decision. Missing a trade costs nothing; forcing a trade in poor conditions
  costs capital" (champions-388 [55:30]). A "missed trade" is always better than an "avoidable loss"
  (context-mastery [42:00]). Overtrading in range conditions is the primary destroyer of performance
  (daily 20-10 [54:50]).
- A trade is judged by its plan, context and confluence before entry, not by the outcome. A profitable
  short taken above the range high with no confluence was still a bad trade (daily-btc 22-10 [04:37];
  champions-388 [1:15:30]).
- Unrealized profit is not yours until realized, "simply numbers on a screen", which reduces emotional
  attachment during management (daily 03-11 [36:21]).
- Staying in trades is the hardest discipline; the urge to close early is beaten by zooming out to the
  bigger picture (06-11 live [50:45]). Conviction comes from a profit cushion plus pattern recognition
  from screen time (06-11 live [39:10]). The single biggest factor for an early-stage trader is time:
  study historic price action, journal wins and losses, and mark the trades you missed
  (06-11 live [44:00]). He meditates on one area per session, often visualizing holding through the
  unrealized drawdown (06-11 live [47:30]).

## 12. Cross-check with the trade log

- 2026-11-03 NQ long, +325k: a long held all day in a strong bounce, stop trailed after the retest,
  "almost stopped to the tick" from the morning low. Matches the framework exactly
  (`../live-reads-trades.md`).
- 2026-11-11 NQ long, +5.4R on a pmVAL retest: Elroy confirmed a morning entry, so Severin sat through
  the cash-open dip below both his entry and his implied initial stop. The gain came from holding
  through an unrealized drawdown, a discretionary de-risk-and-hold, not the trail rule, which would
  have stopped the morning entry early. The transcripts support this reading (hold, de-risk by
  conviction, trail late).

## 13. What maps to an indicator

The mechanical pieces worth flagging automatically, for the ATAS / TradingView work in
`../../indicators/` and the level sync. The dailies sharpen several of these.

- Liquidity-auction state: from the last supply/demand flip or sweep, which region is the current
  target, and is this level with or against it. The rules in section 1 are fully specified. The
  "in-between" guard flags when price sits between the start and target of the active auction, the
  low-probability zone to avoid.
- A confluence-level engine is exactly what he describes building himself: a level indicator that
  marks untested and retested daily / weekly / monthly values plus A-session POC and IB POC, with
  candle-structure (three-candle / fractal) confirmation as the entry trigger, and a filter for
  untested versus retested levels (daily 23-10 [101:00]). This is the closest thing in the material to
  the LevelTrigger / scoreboard Elroy is building, and it is his own design intent.
- Washed-level decay score: count how many times a level has been tested and how close it sits to
  current price, and down-weight it accordingly (champions-385 [11:20]; daily 19-10 [14:00]).
- Line-in-the-sand marker: the single level per instrument whose loss flips the context, surfaced
  explicitly (daily 03-11 [24:25]; daily 20-10 [36:40]).
- Range-quartile guard: flag the bottom-quartile short and the mid-range no-trade zone
  (daily 02-11 [09:50]).
- P-shape / B-shape classifier and the P-to-B transition alert (daily 21-10 [40:30], [46:00]).
- Finished / unfinished high detector: an unfinished high (no excess) is a magnet; needs tick data to
  read excess correctly (daily 23-10 [13:30]).
- De-risk and trail-start guards: mark the break-even move once price has left the entry far enough,
  and do not trail until price is a set distance from entry (06-11 live [1:14:00]; 22-10 live [14:38]).
- Demand/supply quality score: times re-proven, distance from current price (22-10 live [09:38]).

These line up with the backlog in `../indicator-backlog.md`. The concrete pieces the transcripts add
beyond the first pass are the confluence-level engine, the washed-level decay score, the
line-in-the-sand marker, the range-quartile guard, and the P/B-shape classifier.

## 14. The systematic foundation: trading bias (course)

From Trading Bias Part 1-2. The mechanical read underneath the discretionary "context first".

- A price bar's four components carry the read: a close far from the open is conviction, a close near
  the open is indecision, an open far from the prior close is a gap and unfinished business
  (Trading Bias Part 1 [03:07]).
- The prev-day high/low rule, stated as a hard line: never long above a previous day high, never short
  below a previous day low. Those levels are where he takes profit with limit orders and where stops
  sit (Trading Bias Part 1 [06:10]).
- Bias holds with the trend until a key fractal swing (two swings back) breaks; only then does it flip
  (Trading Bias Part 1 [02:19]). Trend is confirmed while 60-70 percent of bars close in the direction
  (Trading Bias Part 1 [44:00]).
- Williams fractals: a bullish fractal is a low with two higher-low bars each side; entry at the open
  of the third bar after the arrow prints (Trading Bias Part 1 [22:12]). He trades fractal breaks in
  the trend direction at a key level, so key level plus fractal break plus market-structure shift is
  the setup (Trading Bias Part 1 [25:00]).
- Liquidity grab versus trend shift: a quick wick to a prior-day low that closes back inside is bullish
  liquidity collection, not a bearish shift; the first real break of a prev-day low in a bullish run is
  only a first sign of weakness, and he waits for a retrace to the 0.618-0.66 CC zone before shorting
  (Trading Bias Part 1 [07:48], [16:49], [28:00]).
- Take-profit split 34/33/33, compounding with slightly less than the realized profit
  (Trading Bias Part 2 [01:08:48]). In crypto a bar's open equals the prior close (no gaps), unlike
  stocks and forex (Trading Bias Part 2 [01:20:44]).

## 15. The scalping strategy: workspace and three entry types (course)

From the Severin Scalping Strategy series (overview, daily plan, execution). This is the order-flow
execution layer, and the daily update in section 2 is this plan run live.

- Workspace: a customized ExoCharts (order-flow) grid split into context (volume profile, TPO, session
  POCs, single prints) and execution (5-minute order flow, delta ladder, footprint, a CCTR template),
  per asset (ES and NQ). It needs a DXFeed CME feed (Scalping Strategy [01:58], [03:27]). He reads
  reactions to levels rather than pre-setting them (Scalping Strategy [00:48]).
- Three entry types (Scalping Execution [02:25]):
  - Aggressive: an SFP or first touch of a key level, confirmed by order flow (a sell block holding as
    support, a CCTR signal) (Scalping Execution [06:19], [10:33]).
  - Reclaim or loss (of orders and of levels): buy/sell blocks are the reference; enter on a loss of a
    buy block, or a reclaim of a level with positive delta followed by a backtest
    (Scalping Execution [15:06], [19:04]).
  - Backtest or retest: after an impulsive move or heavy volume at a reference (single print, VWAP,
    POC), comparing delta into the move versus on the retest (Scalping Execution [25:40]).
- Risk rules (Scalping Execution [03:07]): risk per trade under 1 percent of the account; minimum 2:1
  reward-to-risk; halve size after two consecutive losses; pause after three losing trades.
- Take-profit ladders, context-dependent (Scalping Execution [04:01], [05:26]): ES a 12-tick stop with
  TP 20/25/30 ticks; NQ a 26-tick stop with TP 40/50/60. Allocation 50/25/25 with trend, 75/15/10
  against trend. Manage by scaling out, moving the stop to break-even, treating levels as zones, and
  exiting promptly if signals conflict after TP1 (Scalping Execution [45:00], [50:51]).
- The daily plan is five steps (Formulating a Daily Plan [00:35] on): read market structure from the
  daily highs/lows indicator; read the daily open versus the previous day's range; read it versus the
  previous day's value area; update higher-timeframe levels (prev month/week/day opens, SFPs, CC
  support/resistance, value areas); build the final plan in ExoCharts around untested values that
  coincide with liquidity grabs, with both long and short scenarios.

## 16. The 3 candle entry and its refinements (course)

From the 3 Candle Entry Strategy plus refinements 1-2. This is the exact trigger the 30-day sessions
call "the three-candle method", given in full.

- Base pattern: candle 1 arrival (reaches the level), candle 2 reversal (the confirmation, never a
  Fibonacci level), candle 3 entry (Refinement 1 [01:58]). Built for a clear yes/no; inside candles
  (trading entirely within the prior candle's range) are the low-conviction case to skip
  (Refinement 1 [05:01]).
- The three refined types, in priority order (Refinement 1 [01:26]; Refinement 2 [01:41]):
  - Cherry on Top (COT), highest probability: the reversal candle takes the prior candle's liquidity
    and closes in the extreme quartile (upper for a long, lower for a short). His statistic: an
    extreme-quartile close gives an 88 percent chance of follow-through on the next candle
    (Refinement 1 [06:02]).
  - Change of Context (COC): the reversal takes out the prior candle's high (long) or low (short),
    changing the 1-minute context; it combines with COT for the highest conviction (Refinement 1 [09:18]).
  - Momentum: when no COT or COC is available. A break of a valid supply/demand zone (three or more
    touches) with an extreme-quartile close, entered on the retest of the next candle, with a local
    stop just beyond it. Trapped traders fuel the move, which enables 10:1 or higher
    (Refinement 2 [05:46], [09:05], [13:42]).
- Draw supply/demand boxes to include the most wicks while staying as small as possible
  (Refinement 2 [09:49]). Compound by taking the COT or COC entry, then adding on a momentum break for
  a better average (Refinement 2 [14:06]). The mindset he repeats: "I am not convincing the market to
  take a trade. The market has to convince me to take a trade" (Refinement 1 [14:14]). Do not mix entry
  methods, and accept missed trades (Refinement 1 [15:57]).

## 17. When to modify the plan (course)

From How and when to modify your trading plan. Step away from the plan when behavior is unexpected,
when invalidations have become too extreme, when the plan and correlations disagree, or around a major
event (CPI, FOMC, NFP), waiting for structure afterward (Modify Plan [01:53], [04:17]). The decision
must be data-driven, not emotional (ego, confirmation bias, or revenge trading disguised as following
the plan). To adjust: update the levels and test their validity, re-evaluate invalidations, reduce
size and wait for confirmation. "There are three types of trades: long, short, and no trade"
(Modify Plan [05:20], [06:07]).

## 18. How the foundation maps to an indicator

The course method is more mechanical than the 30-day application, so more of it is directly codeable,
which is the point for the LevelTrigger / scoreboard work.

- The 3-candle trigger is fully specified: arrival, reversal, entry; COT (a liquidity take plus an
  extreme-quartile close, the 88 percent stat), COC (takes the prior candle's high/low), and Momentum
  (a zone break with three-plus touches plus an extreme-quartile close plus a retest). An indicator can
  classify each candle and fire COT/COC/Momentum with the quartile statistic attached
  (Refinement 1 [06:02]; Refinement 2 [09:05]). This is the single most codeable piece in all the
  Severin material.
- Prev-day-high/low guard: block longs above the prior day high and shorts below the prior day low, and
  mark them as the limit-TP and stop lines (Trading Bias Part 1 [06:10]).
- Williams-fractal entries at a key level, entering at the open of the third bar after the arrow
  (Trading Bias Part 1 [22:12]).
- A buy/sell-block reference engine for the reclaim/loss entry (Scalping Execution [15:06]).
- A risk governor: enforce under-1-percent risk, 2:1 minimum, halve size after two losses, lock out
  after three (Scalping Execution [03:07]); plus the TP ladders and the with/against-trend allocation
  (Scalping Execution [04:01]).

## Open items

- The Building Plans & Context course was captured in full (13 lessons). Attribution was checked per
  lesson from the transcripts, not assumed: ten are Severin's (Trading Bias 1-2, Context-Volume-Tips,
  the Scalping and 3-Candle series, Modify Plan), and the three Traders Checklist lessons are
  **Daniel's** (he self-identifies "this is Daniel" and introduces Igor; the audience addresses him as
  Daniel). Those three are in `daniel.md`, not here. Trading Bias Part 1 carries no self-ID and is
  attributed to Severin by pairing with Part 2 (which refers to Daniel in the third person).
- `making-the-move-to-stocks` is Daniel's, not Severin's (Elroy confirmed 2026-11-16: the "Daniel"
  greeting in the Q&A was Daniel reading aloud a question addressed to him). It is synthesized in
  `daniel.md`; section 9's investment framework here stands on Severin's own dailies and streams, not
  on that lesson.
- This deep pass mined all five Champions streams, the seventeen Severin dailies, and the seven BTC
  dailies, plus the lesson and the live-trading streams. Timestamps are from the transcripts and the
  portal Highlights index; a handful of signature phrases were confirmed verbatim against the captions
  ("line in the sand", "washed", "trade with the trend", "no trade is a valid decision", "cookie",
  "staircase", "take profits while there are buyers, add while there are sellers").
- Nothing here is backtested. The method is described as he states it; whether his specific level
  choices, the washed-level decay, or the hold-and-trail edge pay out on Elroy's instruments is an
  open measurement, and the ATAS candle-cache replays so far have not found an edge for mechanical
  entries (`atas-candle-cache-replay`).
