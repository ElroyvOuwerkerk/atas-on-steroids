# Master Instruction Block (AI for Stock Analysis)

Verbatim contents of the Vault cheat sheet **"Master Instruction Block"** (Cheat Sheets, Last Updated
18/03/2026). Downloaded 2026-11-17 with Elroy's OK from the ChartChampions public S3 bucket
(`cc-lms-public-bucket`, no signed URL). The original ZIP held four files:

- `Structured Equity Model Master Instructions.pdf` (the prompt, transcribed verbatim below)
- `Equity Model 2026 Research TradingView Import List.txt` (the watchlist, verbatim below)
- `CC Cheatsheets - AI For Stock Analysis.pdf` (10.9 MB visual cheat sheet — not transcribed, binary)
- `READ ME - AI For Stock Analysis.pdf` (859 KB illustrated guide — not transcribed, binary)

The PDFs live only in this session's scratchpad (ephemeral); ask if you want them copied somewhere durable.

## Attribution — this is the key fact

The watchlist file's own header reads **`###TOP PICS 2026 (BY IGOR)`**. So the 2026 research picks are
**explicitly Igor's**, in the artifact itself — stronger evidence than a slug. This **corrects** the
earlier read in `../../learnings/investing.md` and `../../learnings/igor.md`, which (working only from the
roundtable transcripts, where the model's presenter quotes Igor in the third person) said the method was
"not verifiably Igor's". The **watchlist is Igor's**. The **prompt PDF itself carries no author line**, so
the scoring model is best described as the engine bundled with Igor's picks rather than signed by him.

---

## Structured Equity Analysis Model — Master Instruction Block (prompt, verbatim)

**Input Rule.** The input must contain only the company name. No additional instructions, commentary, or
questions are permitted. Upon receiving a company name, execute the following protocol.

**1. Complete all nine steps in strict sequence**
1. Snapshot & Business Overview
2. Financial Quality, Balance Sheet & Valuation Metrics
3. Competitive Positioning & Moat
4. Bull Thesis & Growth Drivers
5. Bear Thesis & Structural Risks
6. Analyst Sentiment & Market Flow
7. Scenario-Based Valuation & Return Framework
8. Scorecard, Composite Rating & Final Assessment
9. Investment Thesis & Invalidation Triggers

**Execution constraints.** Do not skip / merge / reorder sections; do not request clarification; do not
include commentary outside the framework; do not add sections beyond the nine; each scored category must
include both structured analysis and a numerical score.

**2. Scoring rubric.** 5.0 structurally dominant, durable advantages · 4.0 strong positioning, manageable
risks · 3.0 balanced strengths and weaknesses · 2.0 structural vulnerabilities present · 1.0 structural
fragility or capital-impairment risk. Decimals allowed (e.g. 3.6, 4.2); scores assigned strictly to these
definitions.

**3. Category calibration.** Business Quality · Financial Quality · Competitive Positioning · Growth
Potential · Risk Profile · Sentiment & Positioning · Valuation Outlook. Scores based solely on analysis
produced within the framework.

**4. Composite rating.** Average of all category scores. No weighting adjustments. No narrative overrides.

**5. Required conclusion.** Composite Rating · Conviction Level · Investment Stance · Explicit Invalidation
Triggers.

**Conviction bands.** 4.5–5.0 Very High · 4.0–4.49 High · 3.5–3.99 Moderate · 3.0–3.49 Selective/Cautious ·
below 3.0 Avoid/Monitor.

**Framework principle.** Procedural. The structure stays constant; only the subject changes. On receiving a
company name, immediately begin Step 1.

---

## Equity Model 2026 Research — TradingView Import List (verbatim)

Header in the file: **`###TOP PICS 2026 (BY IGOR)`**. A CSV-style TradingView watchlist import, grouped by
sector (the `###` lines are TradingView section headers). "A research deployment list, not a recommendation
list" — run the model on each, rank by composite.

- **Mega Cap Tech:** GOOGL, AVGO, LLY, V, ANET, KLAC, PANW, SNPS
- **Energy:** XOM, SLB, DVN
- **Industrials:** CAT, GEV, BA, CRH, VRT, DAN, CMC, MHK, VMI
- **Financials:** C, SCHW, ALL, VLY, TRU, TRTX
- **Communication Services:** DIS, ROKU
- **Consumer Discretionary:** CVNA, DKNG, WMB, RL, CELH
- **Consumer Staples:** CVS, SBUX, MKC
- **Healthcare:** TMO, RVMD, GL, FOLD, XENE
- **Real Estate:** DLR, CBRE
- **Materials:** ETR
- **Transport:** CP, UAL, VIK
- **Automotive:** AZO
- **Tech Software:** CRM, GWRE

(~50 names. LLY sits under "Mega Cap Tech" and WMB under "Consumer Discretionary" in the source file;
transcribed as-is, those look like the author's own groupings, not errors on my side.)
