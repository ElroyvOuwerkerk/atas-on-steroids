# ChartChampions transcripts

Full verbatim transcripts captured from the ChartChampions member portal
(`portal.chartchampions.com`) while the subscription is active. Each file carries the portal's
own Summary + timestamped Highlights, then a `## Transcript` of the video's English subtitle
track, merged into `[m:ss]` paragraphs.

Private study material for one member. Not a copy of the videos, not for redistribution.
Capture method and the attribution reasoning are in the side-session hand-over note
(`cc-transcript-pipeline.md`); the short version: cues read from the page's subtitle TextTrack,
assembled in-page, copied to the clipboard, written to disk, nothing uploaded, byte count
verified against the page.

Scope captured: the 3 Contender lessons, Severin's 5 live-trading streams, his 17 daily updates,
his 5 Champions live streams, his 7 BTC daily updates, Igor's 4 daily updates, the full
**18-folder `/courses` library** (187 lesson files, the Building Plans & Context course among them),
and the 5-part **Investing and Diversifying** series. **233 transcripts in all.** The synthesis per
champ is in `../learnings/` (`severin.md`, `igor.md`, `daniel.md`); the investing pillar is in
`../learnings/investing.md`.

## Contender sessions (`contender-sessions/`)

| File | Champ | Words | Duration | Notes |
|---|---|---|---|---|
| `context-mastery-liquidity-auctions.md` | **Severin** | 13,552 | 1:24:18 | His liquidity-auctions framework. The core Severin method lesson. |
| `market-profile-and-dsf.md` | **Igor** | 10,900 | 58:55 | Market profile (TPO) + DSF (depth-of-market surface) order flow. The core Igor method lesson. |
| `making-the-move-to-stocks.md` | **Daniel** | 9,466 | 46:49 | Crypto to stocks: tokenized vs broker, gap handling, long-only mega-cap investing, delta-neutral funding farming. Elroy confirmed (2026-11-16) this is Daniel; the "Daniel" in the Q&A is him reading aloud a question addressed to him. |

## Champions live streams (`champions/`) — all Severin

The weekly Champions live stream, hosted by Severin (confirmed by his opening ritual: soundcheck,
"as always", "welcome to Champions Live Stream N"). One per week (Sundays) across the window.

| File | Date | Words | Duration |
|---|---|---|---|
| `champions-388-16-11-2026.md` | 16-11 | 14,581 | 1:34:15 |
| `champions-387-09-11-2026.md` | 09-11 | 12,121 | 1:14:32 |
| `champions-386-02-11-2026.md` | 02-11 | 14,505 | 1:27:35 |
| `champions-385-26-10-2026.md` | 26-10 | 13,038 | 1:33:29 |
| `champions-384-19-10-2026.md` | 19-10 | 12,097 | 1:16:36 |

Champions 383 (≈12-10) falls outside the 30-day window. All five have a captions track that loads in
full and spans the whole video (coverage checked: last cue ≈ video end).

## Live trading (`live-trading/`) — Severin (5) + Igor (10), named in slug

### Severin (5)
| File | Words | Duration | Notes |
|---|---|---|---|
| `severin-live-trading-06-11-2026.md` | 10,651 | 2:44:17 | Richest. Full live session. |
| `severin-live-trading-22-10-2026.md` | 7,639 | 1:32:39 | |
| `severin-live-trading-29-10-2026.md` | 6,644 | 1:51:25 | Combined with the altcoin weekly update. |
| `severin-live-trading-05-11-2026.md` | 3,227 | 2:20:52 | Sparse, quiet session (long silent stretches). |
| `severin-live-trading-12-11-2026.md` | 2,590 | 1:28:32 | Sparse, quiet session. Day of the NQ pmVAL rejection then flush case. |

The two sparse ones genuinely had little speech, not a partial capture (a live session has long
stretches where Severin watches rather than talks). Severin's latest is 12-11 (up to date).

### Igor (10) — added 2026-11-18
His own live futures sessions on NQ/ES (plus BTC and single names), 2-3 hours each, captured for the
recent weeks: `igor-live-trading-` **06-10, 09-10, 13-10, 20-10, 23-10, 27-10, 03-11, 10-11, 13-11,
17-11-2026**. Igor by slug. These are the richest Igor source — order flow, absorption-and-reclaim,
market profile, and the live psychology/discipline reads. Synthesised in `../learnings/igor.md` §12.
(The video-library `live-trading` category holds ~340 streams back to 2025; only the recent Igor set
was pulled. The 36 Igor *trade-recaps* have no captions — see `igor-recaps/`.)

## Daily updates (`daily-update/`) — 28 files: 17 Severin, 4 Igor, 7 Severin BTC

The daily-update slugs do not name the presenter. The reliable signal is the host's own opening, not
the content (Severin routinely reads Igor's DSF/Quantower levels off their shared sheet, so DSF
mentions are not an Igor tell):

- **Severin** opens casually: a meditation chant or song, a soundcheck, "sound confirmation as always",
  "in the house", and he reads the chat roll. His content is liquidity auctions and supply/demand on
  BTC / ES / NASDAQ.
- **Igor** opens formally: "Good morning traders, hello and welcome to this morning update", and he is
  on Quantower. Confirmed on 16-11 by the host screen-sharing Quantower and reading "the actual snail
  [Severin] is in the house", i.e. Severin was a viewer, not the host.
- The **BTC** series (`daily-update-btc-*`) is Severin's "Daily Bitcoin Trading Update"; the longer
  ones carry his soundcheck ritual (29-10 "soundcheck 1212... always good to have a confirmation"),
  the short ones skip it but share his voice and vocabulary (value area, POC, scalp, "ever so").

Over the last 30 days (2026-10-19 to 2026-11-16) there are 21 regular daily updates: **17 Severin,
4 Igor**, plus **7 Severin BTC** updates.

- **Severin regular dailies (17):** 13-11, 12-11, 11-11, 10-11, 06-11, 05-11, 03-11, 02-11 (Nov);
  30-10, 29-10, 27-10, 26-10, 23-10, 22-10, 21-10, 20-10, 19-10 (Oct).
- **Igor dailies (4):** 28-10, 04-11, 09-11, 16-11 (synthesized in `../learnings/igor.md`).
- **Severin BTC dailies (7):** 20-10, 22-10, 27-10, 29-10, 03-11, 10-11, 12-11.

Lengths run 18 min to 2h05; word counts 3k to 19k.

## Courses (`courses/`)

Evergreen course lessons (no 30-day window), same transcript format as the rest.

### Building Plans & Context (`courses/building-plans-and-context/`)

Severin's systematic-method course. 15 lessons in all: the 13 here, plus lesson 14 (DSF and Market
Profile, Igor) and lesson 15 (Context Mastery, Severin) already filed under `contender-sessions/`.
Attribution was checked per lesson from the transcripts, not assumed:

| # | File | Champ | Words | Duration |
|---|---|---|---|---|
| 1 | `01-trading-bias-part-1` | Severin | 7,947 | 47:03 |
| 2 | `02-trading-bias-part-2` | Severin | 14,883 | 1:29:23 |
| 3 | `03-traders-checklist-part1` | **Daniel** | 15,412 | 1:21:17 |
| 4 | `04-traders-checklist-part2` | **Daniel** | 11,521 | 1:05:10 |
| 5 | `05-traders-checklist-live` | **Daniel** | 11,470 | 1:02:49 |
| 6 | `06-context-volume-tips` | Severin | 10,296 | 59:51 |
| 7 | `07-severin-scalping-strategy` | Severin | 1,195 | 9:44 |
| 8 | `08-...-formulating-daily-plan` | Severin | 2,714 | 25:25 |
| 9 | `09-...-execution` | Severin | 7,017 | 53:20 |
| 10 | `10-severin-3-candle-entry-strategy` | Severin | 3,225 | 20:48 |
| 11 | `11-...-refinement-part1` | Severin | 2,713 | 17:41 |
| 12 | `12-...-refinement-part2` | Severin | 2,828 | 17:22 |
| 13 | `13-when-to-adjust-the-trading` | Severin | 1,400 | 8:45 |

The course split is **10 Severin + 3 Daniel** (the Traders Checklist series). The parent session
assumed all 13 were Severin; the transcripts show the checklist series is Daniel's (he self-identifies
"this is Daniel" in Part 1 and introduces Igor; the audience addresses him as Daniel in Part 2 and
Live). Trading Bias Part 1 has no self-ID and is attributed to Severin by pairing with Part 2.

All captured in English. These lessons carry multiple subtitle tracks (including Spanish); the English
track is selected explicitly and coverage is checked against the video duration.

### All `/courses` modules (full sweep, 2026-11-17)

The remaining 17 modules were swept per lesson. The lesson pages do not label the presenter, so
attribution follows the hard rule: a champ name only on an explicit portal slug or a self-ID in the
transcript, else `unattributed` (the file then carries an `attribution_note`). Almost every lesson
here is `unattributed` — these are general multi-coach modules.

| Module | Files | Attributed |
|---|---|---|
| `beginners` | 6 | — |
| `basic` | 9 | 06, 07 market-structure = **Severin** (slug) |
| `entering-trades` | 9 | — |
| `trading-psychology` | 5 | — |
| `indicators-and-tools` | 14 | — |
| `fibonacci` | 11 | — |
| `channels-and-patterns` | 13 | — |
| `beginner-strategies` | 9 | — |
| `intermediate-strategies` | 13 | 07–09 severin-altcoin-strategy = **Severin** (slug) |
| `orderflow-and-footprint` | 9 | — |
| `atas` | 10 | — |
| `quantower` | 2 | — |
| `tpo-series` | 15 | — |
| `futures-and-stocks` | 11 | — (making-the-move-to-stocks = **Daniel**, filed under `contender-sessions/`) |
| `statistics-and-journaling` | 7 | 08 severins-statistics-building = **Severin** (slug) |
| `advanced-strategies` | 19 | — |
| `elliott-waves` | 12 | — |

Captured in English (lessons carry a Spanish track too; the English track is selected explicitly and
coverage is checked against video duration). The parent session's earlier guesses that TPO parts
11–13, Advanced `sequence`, and `statistics-building-part-1` were Igor/Severin did **not** hold up
under the transcript (no self-ID, zero or trivial mention counts), so they are left `unattributed`
with a note.

**Eight lessons have no retrievable transcript** — their subtitle `.vtt` returns CDN 403
(AccessDenied) for the native player too, so no captions exist to capture: `candle-structure-formation`
(TPO); `templates-and-live-strategies`, `value-importance-part-1`, `value-importance-part-2`,
`long-term-stock-market-strategy-part-1/2/3` (Futures); `correlations-in-trading` (Statistics). The
three long-term-stock-market-strategy parts are investing content with no captions — flagged for the
investing pillar.

## Investing (`investing/`)

The **Investing and Diversifying** roundtable series, 5 parts (`01..05-investing-and-diversifying*.md`),
captured in English. Co-hosted coach roundtables (Severin co-hosts; Igor and Scott join part 4) on the
long-term stocks/ETF book: an AI "value investing model", a TradingView stock screener for the universe,
ETF/DCA diversification, dividend reinvestment, laddering pullbacks. All left `unattributed` (no self-ID;
the AI-model presenter quotes both Severin and Igor in the third person, so he is neither). The synthesis
and the synthesis are in `../learnings/investing.md`. Related: the `ai-for-stock-analysis` lesson under
`courses/futures-and-stocks/` teaches the same model, and the Vault cheat sheet **"Master Instruction
Block"** (its packaged prompt) was pulled to `master-instruction-block.md` here — whose watchlist file is
headed `TOP PICS 2026 (BY IGOR)`, confirming the 2026 stock picks are **Igor's**.

## Igor trade-recaps (`igor-recaps/`)

Checked all 37 Igor recaps in the video-library `trade-recaps` category (2026-11-17). **Only 1 of 37 has
a subtitle track** (`nq-trade-recap-by-igor-2024-05-03`, captured); the other 36 are un-captioned short
scalp recaps and cannot be transcribed via subtitles. See `igor-recaps/README.md`. Igor's method is
covered instead by `contender-sessions/market-profile-and-dsf.md`, the 4 Igor dailies, and
`../learnings/igor.md`.

## How this feeds the learnings

`../learnings/severin.md`, `../learnings/igor.md`, and `../learnings/daniel.md` synthesize each
champ's method from these transcripts, cross-linked to `../live-reads.md` and
`../live-reads-trades.md`, and flagging where a transcript confirms or contradicts the earlier
trade-log reads. `severin.md` is the deep pass (all Champions streams, all 17 dailies, the 7 BTC
dailies, the Building Plans & Context lessons, the live-trading streams, and — folded in 2026-11-17 —
his three-part **altcoin strategy** (§19) and the **statistics building** course (§20)). The 5-part
Investing and Diversifying series feeds `../learnings/investing.md` (the investing pillar).
