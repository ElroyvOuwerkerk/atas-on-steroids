# The investing pillar, from the transcripts

Cross-champ synthesis of the ChartChampions **investing** side (reinvesting trading capital into stocks
and ETFs for the long term), as opposed to day-trading. Built from:

- the **Investing and Diversifying** roundtable series, 5 parts
  (`../transcripts/investing/01..05-investing-and-diversifying*.md`);
- the **AI for Stock Analysis** course lesson
  (`../transcripts/courses/futures-and-stocks/17-ai-for-stock-analysis.md`);
- Daniel's **Making the Move to Stocks** (`../transcripts/contender-sessions/making-the-move-to-stocks.md`);
- Severin's own investment book, already written up in `severin.md` §9.

Everything here was read from the transcripts, then checked against the Vault artifact. **Correction
(2026-11-17):** the Vault "Master Instruction Block" package was downloaded with Elroy's OK, and its
watchlist file is headed **`###TOP PICS 2026 (BY IGOR)`** — so the 2026 research picks ARE Igor's, in
the artifact itself. That overturns the transcript-only read below (which, going on the roundtables
where the presenter quotes Igor in the third person, had called it "not verifiably Igor's"). The full
verbatim prompt and watchlist are in `../transcripts/investing/master-instruction-block.md`.

## Direct answer to the brief: is there an "Igor AI script" to screen assets/ETFs?

Elroy asked me to find the stream where **Igor** describes his method / AI script for finding
interesting assets and ETFs, on the hunch that Igor built something stronger than his own Sproutly
tool. After sweeping the Investing and Diversifying series and the AI for Stock Analysis lesson:

- **The AI method exists and is well documented** (details below). It is a ChatGPT/LLM "value
  investing model" plus a TradingView stock screener for the universe.
- **The 2026 stock picks ARE Igor's.** The Vault watchlist file is headed `###TOP PICS 2026 (BY IGOR)`
  (~50 tickers across sectors). So Elroy's hunch was right: Igor's asset picks are here, named in the
  artifact. This is the find he was after.
- **The AI prompt itself carries no author line.** The `Structured Equity Model Master Instructions.pdf`
  is unsigned, and in the roundtables the presenter who says "my model" quotes Igor in the third person
  ("as Igor said...", part 4), so the person *demonstrating* the model on stream may be a different coach.
  Honest statement: **the picks are Igor's; the scoring prompt is the engine bundled with Igor's picks,
  not separately signed.**

So the premise ("Igor's screener") was essentially correct — his 2026 research list is the screener
output, and the Master Instruction Block is the companion AI model. Both are captured verbatim in
`../transcripts/investing/master-instruction-block.md`.

## The AI value-investing model (the actual "script")

A fixed LLM prompt that scores one company at a time. The `ai-for-stock-analysis` lesson reads it out in
full, and the exact packaged prompt + watchlist have now been pulled from the Vault cheat sheet
**"Master Instruction Block"** (verbatim in `../transcripts/investing/master-instruction-block.md`).

- **Input rule:** the *only* input is the **company name**. Nothing else. No extra questions in the
  same chat (that is where people break it).
- **Nine fixed steps, in strict order, never skipped/merged/reordered:**
  1. Snapshot & business overview
  2. Financial quality, balance sheet & valuation metrics (5-year view)
  3. Competitive positioning & moat
  4. Bull thesis, catalysts & growth drivers
  5. Bear thesis & structural risks ("the step most investors skip; unpriced risk destroys capital")
  6. Analyst sentiment & market flow
  7. Scenario-based valuation (bear/base/bull over 3-5 yr) — "we price probability, not certainty"
  8. Scorecard & composite rating
  9. Investment thesis (5 bullets) + explicit invalidation triggers
- **Scoring rubric, 1-5 (decimals allowed):** 5 structurally dominant / 4 strong, manageable risk /
  3 balanced / 2 structural vulnerabilities / 1 fragility or capital-impairment risk. Seven categories:
  business quality, financial quality, competitive positioning, growth potential, risk profile,
  sentiment & positioning, valuation outlook. Composite = average.
- **Conviction bands:** 4.5-5.0 very high · 4.0-4.49 high · 3.5-3.99 moderate · 3.0-3.49 selective/cautious ·
  below 3 avoid or monitor. **Position size follows the composite + conviction band, with no narrative
  override.** "Never upgrade conviction without upgrading the evidence."
- **It deliberately ignores technicals.** "My model doesn't look at technicals at all." Technicals are
  done separately on the chart (200 daily SMA, 50 weekly, anchored VWAP, range POC) — the trader's edge
  is combining the model's value score with chart reading ("NVDA still scores 8 at $200, but the chart
  says wait for the pullback").
- **Operationalised as a persistent ChatGPT "project"** that "sticks to the script" and is run the same
  way every day; stocks are re-scored after earnings ("earnings are a data update, not a new narrative").
- **How to build your own (his tip):** study how famous hedge funds / value investors screen, take what
  resonates, let AI help you recreate it, and "always verify sources — AI summarises, the filings
  confirm." He would "definitely not share" his exact model.

### The universe / screener
The candidate list is built with a **TradingView stock screener**: start from ~8,400 listed companies,
filter down to ~500, then hand-curate to a **~50-60 name "pie"** of top picks across sectors (finance,
defence, tech, etc.), sitting on top of broad ETFs. The lesson also ships a **2026 TradingView import
watchlist** of the author's research candidates ("a research deployment list, not a recommendation
list") — import it, run the model on each name, rank by composite, monitor invalidation triggers
quarterly.

## ETF & diversification approach (the roundtables)

- Core long-term book is **ETFs bought by DCA** (dollar-cost averaging), with a satellite book of
  individual names for the "nice midterm moves".
- **Diversify across markets and sectors**, not just the Magnificent-7 software names ("if everything is
  in the Mag-7 you're currently being hurt"): add healthcare, bonds, precious metals, defence.
- Concrete ETFs named as examples (not advice): **ARKQ** (ARK Autonomous Tech & Robotics, part 1),
  **CQQQ** (Invesco China Technology, part 2), an **iShares Brazil ETF** (part 4), ARK funds.
- **Reinvest dividends (DRIP)** to compound faster; **ladder in** on pullbacks rather than buying tops;
  think in a **long time horizon** ("100% of the time the market has recovered").
- Running theme: *patience and discipline*, and using the daily/weekly 200 and 50 SMAs plus anchored
  VWAP to time entries into names the model already rates well.

## How it connects to the champs we do have on record

- **Severin (`severin.md` §9):** his own investing book is the daily candle into anchored VWAP + a VAH
  reclaim, with spot-book hedging. He co-hosts the Investing and Diversifying streams but is **not** the
  AI-model author. His angle is technical-structure-led; the AI model is fundamentals-led and ignores
  technicals, so the two are complementary, not the same system.
- **Daniel (`daniel.md`):** *Making the Move to Stocks* is the crypto-to-stocks bridge (tokenised vs
  broker, gap handling, long-only mega-cap investing, delta-neutral funding farming). He is the coach
  most plausibly behind the AI value model, but unconfirmed.
- **Igor (`igor.md`):** a commenter in the investing roundtables; no screener of his own is described in
  these transcripts. See the note added to `igor.md`.

## Not retrievable
Three investing-relevant course lessons have **no captions** (their subtitle VTT returns CDN 403 for the
native player too), so they are not transcribed: `long-term-stock-market-strategy-part-1/2/3` (Futures
and Stocks module). If their content matters, they would have to be captured another way (audio).

## Open items
- **Done 2026-11-17:** the Master Instruction Block was pulled from the Vault (verbatim prompt + the
  `TOP PICS 2026 (BY IGOR)` watchlist in `../transcripts/investing/master-instruction-block.md`). The two
  source PDFs (visual cheat sheet, READ ME) are in this session's scratchpad only — ask to keep them.
- The prompt PDF has no author line; the stock picks are Igor's by the watchlist header. If Elroy wants
  the model's on-stream demonstrator pinned down, that needs a video where the host is named on screen.
- Compare this model against Elroy's **Sproutly** tool once he says what Sproutly screens on.
