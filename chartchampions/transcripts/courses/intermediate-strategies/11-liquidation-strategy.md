---
date: course-lesson
champ: unattributed
course: Intermediate Strategies
lesson_no: 11
title: Liquidation Strategy
slug: liquidation-strategy
category: courses/intermediate-strategies
cues: 167
words: 2971
duration: 13:52
source: https://portal.chartchampions.com/lesson/liquidation-strategy
captured: 2026-11-16
---

# Liquidation Strategy

## Portal Summary & Highlights

This session introduces a scalping-focused liquidation trading strategy designed to capitalize on over-leveraged traders being flushed out of the market before a reversal. We learn how to identify consolidation zones at pivotal support or resistance levels, calculate liquidation prices for high-leverage positions, and combine order flow tools such as ExoCharts, open interest, and CVD divergences to time precise entries on very short timeframes.

### Highlights

- [00:00] Introduction to the liquidation trading strategy: looking for consolidation below resistance or above support at pivotal levels as the primary setup condition.
- [00:25] Clarification that this is strictly a scalping strategy Ñ trades typically last one to five minutes Ñ and is best applied on the 5-minute to 15-minute charts.
- [00:50] Core concept explained: price is drawn toward liquidation pools, so the strategy focuses on buying where over-leveraged longs get flushed, then capturing the reversal.
- [01:19] Walkthrough of the Bybit liquidation calculator:
  - Set leverage to 100x
  - Enter quantity (e.g., 1)
  - Input the consolidation low as the entry price
  - Read the resulting liquidation price (e.g., $45,000 long ? liquidation at $44,776)
- [02:09] Practical example showing how placing a buy order just below the liquidation price allowed participation in the snap-back reversal after a liquidation wick.
- [02:52] Explanation of using ExoCharts to visually identify where 100x longs entered and anticipate where they will be liquidated.
- [03:39] Important execution tip: never place the buy order exactly at the liquidation price Ñ liquidations cause additional sell pressure and extended wicks, so entries must sit slightly below.
- [04:02] Checklist for a valid setup:
  - Consolidation under resistance (or above support)
  - High volume around the consolidation low
  - Quick leverage flush followed by a sharp reversal
- [05:00] Using ExoChart bubbles to confirm actual liquidation events in real time, removing guesswork from the entry decision.
- [05:21] Two valid entry methods: The market order is considered safer and more suitable for beginners, while limit orders require experience and anticipation.
  - Limit order preset slightly below the calculated liquidation price
  - Market order once liquidation bubbles print and the first upticks appear
- [06:44] Invalidation rule: stop loss is always placed just below the low of the liquidation wick once the trade is entered.
- [07:35] Confluence example combining the liquidation strategy with a swing failure pattern (SFP), where the liquidation price sits just below the consolidation lows.
- [08:09] Discussion on target selection:
  - Targets are subjective and context-dependent
  - Typical first target is the first SR flip or nearest resistance
  - Conservative management: take partial profit quickly, move stop to entry, let remainder run
- [09:14] Identifying bullish divergences on CVD at the consolidation lows, where strong limit buying absorbs sell pressure Ñ adding confluence to the liquidation entry.
- [10:30] Example of a textbook setup combining:
  - A wicked candle at the liquidation price
  - A decrease in open interest (confirming leverage flush)
  - Bullish CVD divergence
- [11:05] Strategy summary Ñ this is a pure scalping method with:
  - A clear entry trigger (liquidation flush)
  - A defined invalidation (low of the wick)
  - A reasonable first target (nearest SR flip)
- [12:50] Insight on strategy development: the approach was refined through live observation and forward-tested statistics rather than back-testing, emphasizing the value of recording ongoing performance.

We are encouraged to study this scalping framework carefully, practice identifying the setup in live markets, and apply strict risk management when experimenting with liquidation-based entries.

## Transcript

[0:00] then obviously i want to go through tonight the liquidation trading strategy how do we trade this strategy really simply what i'm looking for is looking for an area of consolidation at a pivotal level so what we're generally going to be looking for is consolidation below resistance or consolidation above support okay so it's really simply you have to see an extended consolidation this strategy is is only for sculpt traders okay so if you're a day trader if you're swing trader you're a position trader or you're hodler sort of thing you know this strategy is

[0:29] not of no use to you to be totally honest with you this is literally only for sculpt traders you you get in and you get out within maybe one minute maybe five minutes okay it's a very very very very quick trade so we're looking for this setup to appear anywhere from the five more likely 15 minute chart is where you're going to get this setup okay it's a mixture of looking at exo charts and just really simply looking at the price chart could i do this strategy with no exo charts the

[0:54] answer is yes i could but obviously exo charts is something that's helpful quick reversal so essentially we're looking at where traders are going to be liquidated where they're over leveraging their trades we're going to look to see the liquidation and then we're going to basically buy people getting liquidated because as we know price likes to draw in liquidations and then basically reverse we've got some examples here uh to begin with to really show you how we come about this there's

[1:19] this tool built into to the program of buy bit The way that we actually first get the tool is within Bybit you just click on this little calculator. With this calculator you can get a liquidation price and so what you want to do is I'm looking for the times 100 leverages so I'll type 100 in the leverage box the quantity you can put anything so we can just put one and then what you're going to be doing is for the price you're going to look where there's a lot of you know basically the low of the

[1:48] consolidation so let's say the low of the consolidation was 45 000 we would know that anybody longing 45 000 with 100 times liquidation is going to get liquidated at 44 776 dollars so what we're going to be doing is basically looking to buy just below this price let's just see for an example what happened here so for an example 45 000 so let's just say yeah this is actually a really good example so you could just say people are buying here and this is just a visual way we can see there's this clear layer area of support

[2:18] here if somebody's buying up the support so what we'd like to do is we would actually try to buy forty four thousand seven hundred and seventy six okay so which is forty four seven hundred seven so it's basically this wick well here's a live example that literally worked so you would have basically been looking to buy as soon as you had the liquidation run here and then you would have looked to actually take profit on you know the trade is this is this trade literally was two minutes long you're the board of first

[2:43] liquidation just below this price and you know you could look to take profit really simply on support resistance in the end this comes down again for a secondary test that's what we're looking for though this example was taken while price was at 17 000 so you know I've been sitting on this strategy a while I've been testing I've been using it and it's really good so uh yeah you can see here uh this would have been a price for, again, from the 17th of November.

[3:10] So, you know, this is where it's easy to see where times 100 longs were entered via exit charts.

[3:13] And then we use the calculator to see where they will get liquidated. We place the buy order below this and wait.

[3:22] So, again, you can see here, I'm seeing that people are going to get liquidated after buying basically 17,700. Okay.

[3:27] After buying 17,700, I've entered the liquidation price, okay,

[3:31] and I can see they get liquidated at $17,704.

[3:35] I'm going to be waiting with my buy order below that because you don't want to wait at the price because what happens is if price hits the liquidation price, they get liquidated and you get a big wick to the downside, don't you? So you don't want to literally buy the exact liquidation price because when they get liquidated they you know they're going to add to the sell orders so you get you get a bit more downside so you don't want to buy exactly on the level but wait for a little bit below this is the one minute chart you can see then we'll go over

[3:57] exercise so you can view this better see there's a really simply consolidation below resistance you look to take everybody out before it reverses really to the upside because the market doesn't like loads of people in the game you want to try and get as much people out of the game as possible consolidation under resistance so here we have consolidation under resistance high volume around the consolidation low high volume around the consolidation low we're going to look for a quick leverage flush before a snapback reversal you want

[4:23] to see this quickly come down and then really look for a reversal relatively quickly to the upside again this trade i'm going to be holding it for very long at all okay so here we can look into exit charts this is the same example here and then this is where we're looking into exit shots i didn't take the low of this but nevertheless you can see the zoomed in view um And this is from the 17th. It's obviously live updates when I was posting it.

[4:50] And people were impressed because we looked for that liquidation price.

[4:53] People, as you can see here on ExoCharts, you can actually see people getting liquidated. So again, there's no guessing about it. We can see the hard cold facts, the evidence. These little bubbles represent liquidations. So we're able to see that there's a lot of buy orders here. We're able to see on the charts the big bubble which indicates people getting liquidated that's obviously an entry for a long position you buy the liquidations okay consolidation under resistance high volume around

[5:16] the consolidation low we can see the liquidations in exo chart so these bubbles are people getting liquidated they generally are going to give you reversal points in the market the entry can be preset with a limit order e.g you could preset your entry just a little bit below the liquidation price or alternatively market order as soon as it starts to turn around so there's one two ways you could trade it literally the the market order entry is when you get the bubbles form that shows you there's liquidations and then as soon as you get

[5:43] your first uptick that would be for me anyway the market entry so that i'd enter as soon as i see those upticks chip away is better of these two one would say that the market order is quote unquote safer you're going to um buy only when you definitely 100 see the liquidations okay so sometimes with the limit order what i've noticed is that sometimes i'll anticipate there being a liquidation there are no liquidations and you know the the trade is just going to instantly be a bad trade so you know in that regards the market order is better

[6:15] but i've got pretty good at recognizing when i do most of mine with the limit order entries to be honest with you but um you know the market order is like a safer way of getting into the market and it's absolutely fine to use the market order to enter on this strategy um and i would probably say that this is the best way to start off and then when you have more experience with the strategy then you can start to anticipate with limit orders um but the easier way is 100 with the market orders and you're going to take a lot less losses so the invalidation is

[6:44] always the wick so what i mean by the invalidation is always the wick is that once you've got those liquidations and the low of there is formed, the invalidation of the stop loss is going to go below that wick. So here we can see we had a flush, we had the liquidations, you have a low, your stop loss is really simply the low of that candle. So here is another example of being able to see an error of consolidation below resistance. And again, what we're going to look for is the quick snap.

[7:11] So we've entered here the price $19,000, $13,940, which is this consolidation low. We're then seeing people are going to get liquidated at $13,870. So we would want to place our, if we're placing below this, obviously you don't want to place at it because if people are getting liquidated at this price, you know, they're going to cause slippage through the market. So we want to place below this slightly. So consolidation under resistance, liquidation price is also a swing failure pattern of the lows. Okay. So these are our lows. The liquidation price here is also below the lows, which also gives you a swing failure pattern of the lows okay so these are our lows the liquidation price here is also below the lows which also gives you a swing failure pattern so

[7:48] you can see we're combining theories here as well combining a swing failure pattern entry with a liquidation strategy entry okay entry a little bit under the liquidation price stop loss can only be set once the trade has been entered not before okay so you're not going to have your you know you can't have a stop loss before you've entered the trade uh target is subjective the reason why i say target subjective is because generally speaking it's going to be the first sr flip but in like this

[8:14] example it's going to be not it's not going to be that so the target is obviously the subjective bit of this strategy because there's no way you can always say this is they're going to be the target of the trade unfortunately this is context dependent. Generally speaking, you're going to look for the first SR flip, maybe the CC resistance. Or let the trade run, you know. There's endless ways that you can use for the target. The main strategy here is the entry. The entry trigger is the thing that you're learning about tonight.

[8:39] So the entry should always be below the liquidation price if pre-set. I think I've made that pretty clear.

[8:49] Targets are subjective. I personally like to be conservative so that means i'll take my first take profit really quickly i'll move my stop to entry for example and i'll let the rest run but my first take profit on this strategy is very very gonna be is gonna be quick i don't want to hold this trade for ages i'm kind of just looking for a quick trade and should be viewed as a quick quick sculpt trade okay so it should be viewed as you're not looking for a quick trade and should be viewed as a squik quick sculpt trade okay so it

[9:06] should be viewed as you're not looking for this forever you know it should be maybe a five minute trade so you have bullish divergences forming all along these lows and this is pretty nice so you can see here what does it mean by bullish divergences well it means you've got the limit orders obviously trying to hold up the price here and you can see the selling at the lows 1.4 million

[9:28] the price here and you can see the selling at the lows 1.4 million 800,000 800,000 900,000 so there's a few million here that we can see obviously if this is a market order you know limit orders are holding it up you know you got bullish divergences here so if you're simply saying that this is consolidation okay at a resistance what we could say for this strategy is really simply we could say okay we have bullish divergences here we have a few million you know longing here the strategy is simply going

[9:52] to be saying to us uh okay we've worked out the liquidation price that's you know let's try and long essentially the the liquidations and you could see on here this first one you obviously had to move down to your level if you had preset where was the liquidation did we say um so many windows oh my god 44 776 so if we come back here 44 776 so yeah you would have obviously got your move down just below that level about a hundred dollars below the level which is obviously you're never ever ever going to get hit on a stop loss

[10:30] on this it's tiny in terms of percentage of these levels you can see a lot of orders coming down on this you're generally going to want to see that open interest decrease you got the open interest decrease here and um what you might want to see is these wrecks and there you go actually you actually had an absolute perfect setup look at this you have the wrecked candle you have the decrease in open interest and it was coming off of bullish divergences on the cvd this was actually a

[10:54] really perfect setup that we were given here a liquidation strategy the whole strategy is based off of liquidating traders before reversing the trades must be taken quickly you're not going to want to hold on to this trade for ages it's very much sculpting trades and the invalidation is simple and so is the entry the invalidation is always the low of the wick that's where you put the stop loss and the um entry is also very easy you know you look for that open interest uh you use like patrick's template you're going to look for that high delta and you look for the

[11:27] red bubbles and you know when that's equaling liquidations like the entry is so so so easy to get and the invalidation is even easier because it's just the low of the liquidation wick and to be honest the first target is generally your sr flip uh but again the target is going to be a little bit in the air but it's such a nice strategy you literally have everything there uh a rule set to get in a rule set for the stop loss and a pretty much a rule set for the air before they take profit I mean you have literally

[11:56] everything there and I'm telling you this has such a high win rate it's absolutely crazy that this is such a good strategy why i mean this is probably the thing why is this such a good strategy it's because it is fairly advanced i mean it's also unique i've never ever ever seen somebody trading this well literally i made this strategy so this is totally unique uh that's also probably why it's really good um but yeah i mean i'm happy with the strategy and now you can try it as well

[12:29] but i suppose there's quite a high barrier for it the reason why it works so well is it's sculpting strategy it's on the one minute charts not many people are interested in one minute charts it has you know it's based off a liquid i don't know it just works so i'm not i'm not going to uh overthink it works and again how did i find this strategy it's based off a liquid i don't know it just works so i'm not gonna i'm not gonna overthink it works and again how did i find this strategy it's just by simply spending hours upon

[12:50] hours upon hours upon hours on the charts doing this every single day and then seeing to myself wow i keep on seeing this same thing and then it's starting to record the statistics backtesting you know you can go back and check and see if it's when it's worked when it's not i don't really like to back test i just like to actually see record going forward so i'm not i'm not a person that spends hours back testing either way i do my statistics is going forwards so i want to know whether it's relevant now and whether it's

[13:16] relevant for the next 20 trades i don't don't care so much about the last 20 i want to know going forwards how is this working so uh when i see something that i like that's when i'll start my that's when i will personally start my statistics i don't care about backtesting so much but um yeah it's as simple as experience recognizing testing putting money on the trade thinking yeah this really works and then i i can share it thank you ever so much everybody have a good evening and um thank you once again
