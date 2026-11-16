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
his 5 Champions live streams, and his 7 BTC daily updates, plus Igor's 4 daily updates (his
market-profile/DSF lesson is one of the 3 Contender lessons). 41 transcripts in all. The synthesis
per champ is in `../learnings/` (`severin.md`, `igor.md`, `daniel.md`).

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

## Live trading (`live-trading/`) — all Severin (named in slug)

| File | Words | Duration | Notes |
|---|---|---|---|
| `severin-live-trading-06-11-2026.md` | 10,651 | 2:44:17 | Richest. Full live session. |
| `severin-live-trading-22-10-2026.md` | 7,639 | 1:32:39 | |
| `severin-live-trading-29-10-2026.md` | 6,644 | 1:51:25 | Combined with the altcoin weekly update. |
| `severin-live-trading-05-11-2026.md` | 3,227 | 2:20:52 | Sparse, quiet session (long silent stretches). |
| `severin-live-trading-12-11-2026.md` | 2,590 | 1:28:32 | Sparse, quiet session. Day of the NQ pmVAL rejection then flush case. |

The two sparse ones genuinely had little speech, not a partial capture (a live session has long
stretches where Severin watches rather than talks).

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

## How this feeds the learnings

`../learnings/severin.md`, `../learnings/igor.md`, and `../learnings/daniel.md` synthesize each
champ's method from these transcripts, cross-linked to `../live-reads.md` and
`../live-reads-trades.md`, and flagging where a transcript confirms or contradicts the earlier
trade-log reads. `severin.md` is the deep pass (all Champions streams, all 17 dailies, the 7 BTC
dailies, the lesson, and the live-trading streams).
