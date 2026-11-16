# ChartChampions live reads — Severin & Igor

Running log of **trade- and strategy-relevant** things Severin and Igor say in the CC Discord
(#futures-stocks, #severin-trading, #igor-trading) while Elroy is a member (ends ~2026-11-19).
Read-only, banter filtered out. **I never write anything in Discord** ([[discord-never-write]]).

---

## 2026-11-13 — deep read of #severin-trading (Nov 3–13) + #futures-stocks (today)

Read #severin-trading back to **2026-11-03** (every daily plan + trade post). #futures-stocks is mostly
member banter; Igor/Severin posts there are sparse (captured below). Severin's trades are logged
separately and machine-readably in **`live-reads-trades.md`** (times UTC, CET = UTC+1).

### ⭐ Severin's EXIT rule (current method — supersedes the scalping lesson's 50/25/25)
Elroy (2026-11-13): *"Severin zijn strategie is veranderd sinds dat hij die instructies had geschreven.
Zijn doel is nu om niks te verkopen maar de SL te trailen op een gegeven moment. En dus zoveel mogelijk
in een trade te verdienen."* So **Lesson 9's scale-out (50/25/25 at TP 40/50/60 ticks NQ, stop to BE) is
out of date.** He now takes **no partials** and **trails** instead. Verbatim from #severin-trading:

- Nov 3, 11:01 UTC: *"Almost got stopped to the tick on this trade from the morning but luckily i didnt.
  Still holding and monitoring the market."* (initial stop sat just beyond structure, held.)
- Nov 3, 12:23 UTC: *"With this retest now there is technically no point in taking the lows again so i am
  trailing my SL in a way that i am securing 100k on this trade."*
- Nov 3, 13:27 / 13:30 UTC: *"Trailing to lock 150"* … *"250 locked"*.
- Nov 3, 13:32 UTC: *"Another generational long trade … i knew it was time to just keep it and risk the
  unrealized. I am securing 325k now."*
- Nov 11, 16:46 UTC: *"Context pointed towards a new high so i held it all the way. 1 trade for a 5:1
  R:R. Risk was 21k for over 100k profit now … I am locking 100k now."*

The build session's three questions, answered only as far as his own words go:
1. **When does he start trailing?** Not at a fixed R or fixed profit — **once structure confirms** (a
   retest that means the prior extreme won't be revisited): *"with this retest now there is no point
   taking the lows again so i am trailing my SL."* Before that he holds on the initial stop.
2. **What does he trail behind?** The **structural swing** the retest protected (he secures so the
   *"lows"* can't take him out). The **exact anchor** (previous candle vs swing vs zone edge vs MA) is
   **not stated verbatim** in these posts.
3. **Which timeframe?** **Not stated** in the posts.

Also: **no partials** now (holds the full size, "keep it and risk the unrealized"); the stop moves to
**lock progressive profit**, well beyond break-even, not just BE. Stall-vs-reversal behaviour is **not
stated verbatim** in the Discord posts. Q2/Q3 (the exact anchor + timeframe) are now answered from the
portal text — see the next section.

### Portal lesson + video text (now accessible — a big new source)
The portal is readable again and **every lesson and daily-update page carries a text summary +
timestamped "Highlights"** (not a word-for-word transcript, but a detailed, timestamped breakdown). This
fills the two exit gaps above and pins the notes-vs-current difference.

**Exit anchor + timeframe (fills Q2/Q3), from the Igor daily update 13-11-2026:**
- **[56:00]** *"Positions are trailed by staircase structure — never breaking below prior pullback lows —
  and covered at the prior day high with options wall confluence."*
- **[1:45:00]** Severin's **"staircase" vs "big step"**: healthy trends build small steps
  (consolidations) between legs; moves without steps lack foundation and retrace fully.
- So: **anchor = the prior pullback lows** (the staircase steps) of the intraday swing structure;
  **timeframe = the execution legs** (1m and 5m — [07:20] in the Execution lesson contrasts a 5m reclaim
  vs a premature 1m entry), stepping up as the trend builds its staircase; **exit/cover = prior-day high
  + options-wall confluence**, not a fixed-tick target.

**The notes' exit is the OLD rule — confirmed outdated.** Lesson **"Severin Scalping Strategy -
Execution"** (lesson 9 of Building Plans & Context):
- **[04:01]** fixed-tick TPs: ES 12-tick SL, TP1 20 / TP2 25 / TP3 30; **NQ 26-tick SL, TP1 40 / TP2 50
  / TP3 60** ("context-dependent, may be adjusted").
- **[05:26]** scale-out by structure: **with trend 50/25/25, against trend 75/15/10**.
- **[45:00]** trade management: *"scaling out, moving stop to break even, treating levels as zones."*
So any measurement scored on the lesson's fixed TPs / 50-25-25 / move-to-BE is scoring the **wrong
exit** — he now holds and trails the staircase instead (per Elroy + the Discord posts above).

**Other Execution-lesson details worth having:**
- **Risk [03:07]:** < 1% per trade, **min 2:1**, after 2 losses cut size 50%, pause after 3 losses.
- **Three entry types [02:25]:** (1) aggressive (swing-failure, first touch of key S/R, confirmed by
  order flow), (2) reclaim / loss of orders and of levels (buy/sell blocks), (3) backtest / retest.
  Entry confirmed by a **CCTR** order-flow signal + delta ladder / footprint; levels treated as
  **zones**, not exact prices.
- From the daily update: **[1:15:30]** trade around **value (VAH/VAL), not the POC** (the POC is a
  take-profit zone); **[49:00]/[51:00]** the B-session halfback-of-A and C-session halfback-of-B are the
  bread-and-butter longs (stop below the session low, target the session high); **[1:49:00]** RTH true
  gaps measured from the prior RTH **low** (not the close), fill to the tick.

Lesson index (confirms structure): 7 Scalping Strategy, 8 Formulating a Daily Plan, **9 Execution**,
10–12 **3 Candle Entry (+ refinements)**, 14 **DSF and Market Profile**. The daily-update + lesson text
is the richer source from here on. The 3-Candle (10–12) and DSF (14) lessons are harvested next.

### 3-Candle Entry (lessons 10–12) + DSF/Market Profile (14) — from the portal lesson text
This is the source behind the Level Trigger's COT / COC / Momentum, and it is more precise than the
written notes.

**Lesson 10 — 3 Candle Entry (base).** 1-minute AutoFlow + candle structure; aims **+9% win rate**.
Three candles: **1 arrival** (approaches the level) → **2 reversal** (confirms the reaction) → **3
entry**. 5-min order flow for execution; sell blocks at support, buy blocks at resistance. Gives a
yes/no entry.

**Lesson 11 — Refinement Part 1: COT + COC (+8% win rate).**
- Candle roles: 1 arrival, 2 reversal (**the 2nd candle is ALWAYS the confirmation, not a Fibonacci
  level**), 3 entry.
- **COT (Cherry on Top):** the reversal candle must **take the previous candle's liquidity** (low for a
  long, high for a short) **and close in the extreme quartile** (upper for a long, lower for a short),
  with a large wick on the sweep → **88% follow-through** on extreme-quartile closes. COT can fuse
  arrival+reversal into one candle (a 2-candle entry).
- **COC (Change of Context):** the reversal must **take out the previous candle's high** (long) / **low**
  (short); standard 3-candle form; changes the 1-minute context. COT+COC together = highest conviction.
- **Avoid inside candles** (entirely within the prior candle's range = indecision). Confluence raises
  odds: ES prev-day VAH flip **with supply confluence = +17%**. *"The market has to convince me to take
  a trade."* Don't mix entry methods; accept missed trades.

**Lesson 12 — Refinement Part 2: Momentum entry (the 10:1).**
- **Priority: COT → COC → Momentum.** Momentum is for when no COT/COC is available (choppy price around a
  level); fuelled by **trapped traders**.
- **Checklist:** long = **break of a supply zone + close in the upper quartile**; short = **break of a
  demand zone + close in the lower quartile**; **entry on the RETEST candle**; **stop just below/above
  the retest candle (local stop only)**. Zone needs **3+ tests** (examples use 4–5 touches). Same 88%
  extreme-quartile stat.
- **Stop/exit:** tight **local** stop → **10:1 R:R or higher**, bigger size; *"trail the stop to box in
  price; don't limit to small R:R, aim for big moves."* **This is the 10:1 that overturned the 2R
  measurements.**
- **Compounding:** take the COT/COC entry, then **add a compound position on the momentum break** for a
  better average entry. Draw supply/demand boxes to **include the most wicks while staying as small as
  possible**.

**Lesson 14 — DSF + Market Profile.** *"Market profile tells us WHERE; DSF tells us WHERE and WHEN."*
- **Exhaustion threshold:** **fewer than 10 lots on NQ at a profile extreme = exhaustion / likely
  rotation** ([15:29]) — a concrete number for "volume dried up at the extreme."
- **DSF dark zones** (no resting orders) → price travels through; concentrated resting liquidity
  validates a level; **icebergs** (hidden replenishing orders) absorb before a break.
- **Trailing (confirms the exit rule):** *"trail stops using minor structure (lower highs / lower lows)"*
  and *"trailing stops below prior period lows for extended holds"* ([36:36], [56:30]) — behind the
  **prior TPO-period lows**, the staircase steps. **Do NOT place stops on visible liquidity** (FIFO
  queue). Example: the period-F low below the POC.
- **Data note for the depth-history work:** *"Rithmic feeds generally provide deeper, more detailed DOM
  data than CQG."* Poor low = auction without excess, likely revisited. Workflow: context (profile) →
  participation (30-min volume) → confirmation (DSF) → execution with defined risk.

Exit caveat across lessons: 9 and 14 still mention **partials** (50/25/25, "close partials"), but Elroy
says the current method takes no partials and trails the staircase — treat "scale out" in the lessons as
superseded by the trail rule for scoring.

### Rules that change how a trade is scored (priority for the build session)
Severin's own words; several are NOT in the (2R-based) notes. **R1 alone overturns both measurement
rounds**, which scored every trade at a 2R target.

- **R1 — target = context, not a fixed R.** Nov 11 NQ long: *"1 trade for a 5:1 R:R … context pointed
  towards a new high so i held it all the way."* Nov 3 long: *"technically no point in taking the lows
  again so i am trailing my SL … keep it and risk the unrealized"* → +325k. He holds to the next level
  / ATH, not to 2R. Scoring his method at 2R understates it badly.
- **R2 — stop trailed by structure.** After a confirming retest he trails the SL to lock profit ("no
  point taking the lows again"), not a fixed stop or fixed trail. Initial stop sits beyond structure
  (Nov 3 he was *"almost stopped to the tick"* and it held).
- **R3 — only pre-defined levels, nothing in between.** *"Stay patient and wait for my pre-defined
  levels and not trade anything in between."* Repeated almost daily.
- **R4 — extreme selectivity.** *"November already banked over 7 figures by playing 2 setups."*
- **R5 — context gates direction.** *"These type of grinds i will NEVER fade with shorts, too low
  prob."* *"Once pwPOC is claimed i am not interested in any short trades."* Strong days = longs-only or
  stand aside.
- **R6 — on strong/trending context he often does NOT day-trade.** *"I don't see good trading
  opportunities today … mostly just riding investments now."* His trending-day behaviour is to trade
  less, not more.
- **R7 — a level too easy to identify is more prone to a fakeout** (QQQ $637: *"not difficult to
  identify this level and thus a f[akeout]"*).

### How he reads the day / "trending" (priority #3)
Every morning he frames context by **dOpen vs pdVA** (inside/outside, which quartile), **wOpen vs pwVA**
(weekly quartile), the **profile rotation pwVAL → pwPOC → pmVAL**, whether **pdHigh/pdLow** are taken,
and "signs of weakness". **Strong / "lows are in"** = claiming levels, recovering, no weakness, *"unless
major bearish news"*. On those days he goes **longs-only or stands aside** and **holds to the next level
/ ATH**. He nudges a level to **match the avwap** when they are close (that makes it a level of
interest; otherwise it is "only a scalp").

### Order book / DOM (for the depth-history work)
- Severin's edge here is **profile levels + context + footprint** ("lovely squeeze, very technical"),
  not an explicit DOM-ladder read in this window.
- The resting-book read is Igor's **DSf = DOM Surface Liquidity Heatmap** (green bids defending the put
  wall), logged in the 2026-11-12 entry below. That is the closest thing to an order-book read from the
  coaches.

### Igor — #futures-stocks
- **ES RTH values** posted today (value areas → RTH vwap).
- **Risk "chips" rule:** one chip per trade, *"no chips no trade"*; a daily loss limit written as N
  chips (e.g. 5 × $100), *"after 5 losses your pocket is empty for the day"* → hard stop.
- **"True gap"** concept (members asking): a true gap = **no price action in between**; he fills gaps
  (called an NVDA RTH gap-fill "to the tick"). How long "no price action" stays valid was asked but not
  clearly answered in text.

### Not yet folded into notes/
R1–R7 belong in the notes, but the bilingual (EN/NL) build is **mid-run on `notes/` in its own branch**.
Folding them into the English notes now would drift from the Dutch. Fold R1–R7 into the playbook /
trade-management note once `lroy/library-bilingual-nl-…` merges.

---

## 2026-11-12

**NQ — pmVAL rejection → flush (order flow).** Full breakdown: `cases/2026-11-12-nq-pmval-rejection.md`.
Short: aggressive buying probed **pmVAL ~28232** on drying volume at the extreme, got absorbed
(cumulative delta topped **+229** and diverged), then a **−505 flush** through **pdVAL 28163** → ~25 pts
down toward **ONL 28106**. This is the archetype for the Level Trigger rejection/absorption gate.

**Severin — NVDA positioning (stocks).** News: NVDA CEO (Jensen) resigned; NVDA selling off, Severin:
*"$150 Nvidia here we come."* His stated approach:
- *"First if we drop below 150 i will realize some losses on my latest Nvidia buys as its year end."*
- *"If my position goes into profit i will TP more aggressively and sooner. Not expecting ATH for now."*
- *"for now going to buy more but the outlook that its the easiest investment changed a little for me."*
→ He's shifted from buy-and-hold conviction to **tighter, faster profit-taking** and **year-end loss-
harvesting below 150**. Not a futures setup, but his risk posture.

**Igor vs Severin — index lean.** Igor leaning bearish on indices (*"massive discount – panic sell"*,
*"might as well clean the low all the way down there"*); Severin skeptical of the recurring panic
narrative (*"How many times have we seen these posts… Tomorrow: 'Iran deal almost done'"*). Net:
divergence, no shared high-conviction setup.

**Macro / noise:** jobless claims ~200k (in line); **PPI tomorrow (2026-11-13)**. Otherwise mostly
tooling banter (QT vs ATAS; Black Friday deals on QT & ATAS ~Nov 27).

---

**Igor — live DSf + dealer-positioning read (ES).** His own channel, ~16:10–16:58 CET. Igor posted an
**"ES pressure ladder"** of options / dealer-positioning levels and traded off it with DOM Surface (DSf)
confirmation. This is the clearest live example yet of "the method as I teach it."

- **Ladder (top→bottom):** 7,700 secondary call wall · 7,675 primary call wall · 7,650 local call
  concentration · **7,627 gamma-balance transition** (zero-gamma flip) · 7,625 opening-range / A-period
  resistance · **7,600 dominant put wall** · 7,550 secondary put concentration.
- **PM update:** above 7,630 → 7,640–50, acceptance → 7,675; below 7,600 (put wall + developing RTH
  value low) → risk 7,593 → 7,581–72.
- **Trade:** price held the **7,600 put wall**; the **DSf (DOM Surface) printed green bids defending it**
  ("it's honestly text book… seeing green those bids at the put wall"). He bought the low ("could not buy
  any lower lol", open 7,583.25 → 7,616), Gross P/L **12,400 / 3,050 USD**. Also called an NVDA **RTH
  gap-fill "to the tick."**
- **Teaching line:** *"If what you are doing is not working out… pay more attention to market profile and
  DSf as I teach."* DSf = **DOM Surface Liquidity Heatmap** (Quantower) — see `notes/glossary.md`. His
  method stacks three layers: **profile** value/structure (TPO) + **options-gamma levels** (call/put
  walls, gamma flip) + **DSf** order-flow confirmation *at* the level. This put-wall-held-+-green-bids
  pattern is the archetype for the Level Trigger's absorption / defence-at-level gate, and for
  `atas-data-feasibility.md` (can ATAS reproduce the DSf read).
