---
date: course-lesson
champ: Igor
title: DSF and Market Profile
slug: market-profile-and-dsf
category: contender-sessions
cues: 842
words: 10900
duration: 58:55
source: https://portal.chartchampions.com/lesson/market-profile-and-dsf
captured: 2026-11-16
---

# DSF and Market Profile

## Portal Summary & Highlights

This session explores how the market profile (TPO) and the depth of market surface (DSF) can be combined to build trading context, assess participation, and refine entries and risk management. We review how organized data from market profile reveals balance, acceptance, rejection, and poor structures, while DSF provides a live view of resting liquidity and intent. The discussion also covers the auction process, overnight inventory relative to settlement, and practical examples of using delta, volume, and liquidity for defining entries, stops, and targets.

### Highlights

- [00:00] Introduction to combining DSF (liquidity map) and Market Profile (TPO) as complementary tools: one organizes data for quick context, the other confirms if participants are actually transacting at those levels.
- [00:52] Role of market profile in understanding where value is developing, including acceptance, rejection, balance, and overlapping value areas across sessions.
- [01:11] Review of core TPO terminology: point of control, value area high/low, unfinished business, and unfinished auctions.
- [02:06] Breakdown of session letters (A through M), where A session is the cash open at 9:30 NY and M is the cash close.
- [02:33] Explanation of a poor low: an auction without excess, suggesting incomplete rejection and a likelihood of being revisited.
- [03:14] Why overlapping value across sessions matters:
  - Avoid taking continuation trades without checking prior sessions.
  - Overlapping value increases probability of rotation back inside range.
- [04:34] Importance of the overnight (Globex) inventory relative to the previous day's settlement for anticipating counter-auctions at the regular session open.
- [05:36] Why only yesterday's overnight profile is meaningful; older overnight profiles typically have low volume and irrelevant point of controls.
- [07:08] Reading DSF liquidity: areas with no resting orders ("dark zones") suggest price will travel through them, while concentrated liquidity validates areas of interest.
- [08:19] Observing patterns of order placement — traders adding resting liquidity lower when price is still seeking value lower.
- [08:48] Simple framework: Market profile tells us where; DSF tells us where and when, with live confirmation of liquidity.
- [09:39] The banana auction analogy explaining price discovery:
  - Price is only meaningful when participants transact.
  - Rejection occurs when no one is willing to engage at a level.
  - Price moves lower (or higher) to attract participation.
- [13:40] How participation builds from the extreme of no activity to peak engagement, then tapers — a cycle visible every day on auction profiles.
- [14:12] Practical application: if DSF shows strong resting orders at a weekly value area high and auction shows attracting business, that level is less likely to hold.
- [15:29] Importance of low participation at auction extremes: fewer than 10 lots on NQ at a profile extreme suggests exhaustion and potential rotation.
- [17:50] Session analogy — ABCD sessions build visibility ("lights turning on") so traders can identify opening range breakouts, counter-trade actions, or developing value by C session.
- [20:00] Workflow summary:
  - Context from market profile and areas of interest.
  - Participation from 30-minute volume profile.
  - Confirmation from DSF.
  - Execution with defined risk.
- [22:30] Technical note on data quality: Rhythmic feeds generally provide deeper, more detailed DOM data compared to CQG for serious order flow analysis.
- [24:00] Interplay of limit and market orders: watching the tape and recognizing icebergs (hidden replenishing orders) that absorb activity before price breaks through.
- [25:40] Reading overnight behavior at the open:
  - If overnight trades above yesterday's RTH value and the gap holds, bias is bullish.
  - If the gap fills completely, expect rebalancing.
  - If gap holds above settlement, potential long continuation.
- [27:12] Live example — "Poor Low Repair Short":
  - Trading above yesterday's value suggests bullish bias, but poor highs reveal rejection.
  - Delta decreases on upward pushes, confirming lack of commitment.
  - Entry trigger: price fails above point of control and returns below it.
- [32:19] Risk management on this setup — tight stop above the failed point of control, scaling targets at prior lows, and using conditional orders to add on confirmation.
- [36:36] Trade management philosophy: close partials, trail stops using minor structure (lower highs/lower lows), and avoid placing stops directly on visible liquidity due to first-in, first-out queue dynamics.
- [38:00] Trade entry discussion — preference for confirmation entries over anticipating tops/bottoms to avoid repeated small losses, trading with momentum rather than against it.
- [40:50] Using market profile as a context gauge on short/long trades: identifying when lower prices stop attracting participants to signal auction completion.
- [44:30] Study tip: load market profile and review how untested value areas and naked point of controls get respected and offer tight stop opportunities for reversal trades.
- [46:30] Live example — "Overnight Continuation Long":
  - Weak high followed by half-back retest with no participation.
  - Delta spike into support confirms long entry with minimal risk.
  - Stacked offers above act as magnets for price.
- [52:00] Managing exits near prior overnight highs — collapsing position incrementally to lock in reward while allowing room for continuation.
- [54:40] Summary of the integrated approach:
  - Market profile organizes data for one-glance context.
  - Volume reflects participation.
  - Delta indicates likely direction.
  - DSF confirms intent and resting liquidity.
- [56:30] Building a day-trade position with trend confirmation: adding on evidence (one-time framing, positive delta) and trailing stops below prior period lows for extended holds.
- [57:30] Example of stop placement using period F low below point of control, with alternatives for scaling in using DSF-identified liquidity zones if the market retraces.

We encourage reviewing these concepts carefully, practicing them in context, and applying disciplined risk management when integrating market profile and depth-of-market analysis into any trading plan.

## Transcript

[0:00] Today, we are going to talk about DSF, which is the liquidity map, and market profile, which for me is a way to organize data or data. Both of these two tools, they really complement each other, because with one, we can have that one glance look and get a lot of information without having to spend hours doing unnecessary work. And then the other one, which is the SF, is what tells us, is there anyone actually there doing business at those locations? Read the disclaimer, because that is very important, because everything we cover is not financial advice.

[0:47] The market profile provides us with a broader context. It helps us understand where value is developing. I think a lot of people look at these concepts or look at these names and they just go over their heads, but really can be quite simple. just goes over the heads, but really can be quite simple.

[1:08] So I'm going to try and just really make it so that we don't spend an hour talking about exactly what everything in the market profile does. And for those of you who don't know, market profile is just TPO. So it's time, price, opportunity. So I think, are we all familiar with market profile, TPO? Yeah? So we know point of control, value very high, value very low, unfinished business, finished business, unfinished auctions, all that stuff. So we can probably skip a lot of this stuff,

[1:31] so I'm not going to read everything. But basically, market profile, let's just think about organizing data, and then obviously the DOM surface kind of like brings the two together and gives us, okay, can we actually have a trade here? So that's kind of like let's call it as a bridge that brings together okay and then we will look at another tool which is the actual participation and for that i do have a great example so here's obviously the market profile it looks like just to break it down very quickly please for those of you who don't know what it is, A session, like from what we were

[2:07] talking about earlier. So A session will be the regular trading hours. So this is New York 930, and that becomes the cash session. So this is the first A session with that little green dot. That is when the market opens. It opens at the A session, then goes B, C, D, E, all the way to M. And M will be the close, cash close. Okay. So each and every single day, we're going to have that. So you can see M, close. In this case, we actually have a poor low. So if you're wondering what's a poor low,

[2:39] does anyone knows what's a poor low? Yeah. So you can see for those who don't know what a poor low is, it's those two letters on there. There's not excess. That means that that auction on there, it's not quite finished. There's not been a real rejection on there. So typically, from our understanding, just by looking at that chart, we could say, hmm, maybe tomorrow, depending on where we open, we'll probably go down there and take that low out. And that is just that context by just simply looking at data that's been organized by price.

[3:14] So it's very, very powerful. And I look at the market profile to show me acceptance, rejection, balance, and more importantly, if we have overlapping value. For example, some days you'll see traders that are trying to make a long trade to take the whole way. But then you look back in two of those market profiles from the previous sessions, and they're going to be overlapped. And then that trader, if he's not looking at that, he's going to buy expecting price to go higher. But then I would look at it and say,

[3:51] well, based on what happened yesterday and the day before, it is actually more likely that price is going to rotate again today. In other words, instead of having a day that that long trade could have played out, we might have a move back inside the range. So that trader would take a long trade, see a bit of profit. Market comes back in, stops them out. And he's like, here we go again. That's why sometimes it doesn't work. And with a market profile, it just does the job for me.

[4:22] I load it. It tells me, OK, here's the data from yesterday. All organized. No questions. What you see is what you get. So that's market profile. One other key component that shows the overnight from yesterday, for example. So it's this information here helps us knowing where is the overnight. So the Globex open. Where are they positioned? Are they above the settle? In other words, yesterday's M session close or are they below the settle? Because as you can see here, typically at the open of that A session,

[5:16] we get that counter trade either lower or higher, depending on where the overnight market is. or higher depending on where the overnight market is.

[5:26] But yeah, this matters, so the overnight,

[5:30] because most frequently we get those imbalances during the regular sessions from the overnight to the regular trading session. So yeah, we typically get those counter trades, well, auctions back to it. yeah so the overnight is really really important because if you're looking past let's say five days and you're looking just at the regular trading sessions you will find especially those that like the untested levels like values point of controls they'll be very well respected and then if you add on all of those profiles overnight, they typically have no volume.

[6:07] The volume is really low. So those point of controls from those sessions will be quite irrelevant. And I've asked this Quantower company to add just the one from yesterday. They're the most important. And you've probably seen it. How many times the market looks below the overnight low, goes back inside, runs to the upside. And this is it. It's just last night's overnight coming into the open. That's very, very important. The others, they're irrelevant. They don't really matter.

[6:36] So if we can have a platform that shows us all of the regular trading sessions with the good levels. And then just one current session with the overnight. We can start really building enough context so that at the open, we can then say, are we higher? Are we lower? Are we below the settle? Are we above the settle? And just this here with the organized data, it's enough information for a short term trade like a day trader. OK. And then obviously there's the don surface so this is

[7:08] where we can see the liquidity live and this is what i was saying earlier you can see like these little bits here in between prices that shows no orders okay so let's say you're looking for a long trade or a short trade you have your area interest. You have your naked point of controls and tested values. You got everything and it's sitting, let's say, 25, 440. So if it's sitting there, 25, 440, you can say to yourself, it's not just me. There's quite a few other traders there. However, let's say that your level is sitting in, let's say, 46. You can

[7:48] say to yourself, ha ha, there's nobody there. So I'm not going to buy it because no one else is buying it. So why would you buy it? Just let it go through the level. It's dark for a reason. If you hide all of those colors, you can't see the depth of market. Do you understand that? You can't see the depth of market. Do you understand that?

[8:11] So you should have them there because then you can see the patterns so you can see who's adding. You can clearly see here, if I zoom in, you can clearly see there that there was no traders on that time just before that low. But then they were like, OK, this is likely to go lower. Price is still seeking value lower. It's likely to go lower. I will add more orders below. And price typically comes to those levels. OK, especially when you look left from that chart and you can see lower lows, lower highs.

[8:38] OK, so pretty simply or kind of trying to make it a little bit more easy to read. It's market profile tells you where. So you look at the chart or the TPO profile. And for those of you that don't use Quantower or AdSense or whatever it may be, TradingView does actually a good job of it. You just have it on the side and it's OK. Organize it for me. So instead of like plotting everything manually, have market profile do it. Where am I doing business? OK, where? Where is my area of interest?

[9:11] It tells you. And then Dom Surface tells you where and when. So as we've just gone through. So if the liquidity is there, great. That's what we want to do. And obviously the time as well coming down to it. OK, so and it's all live liquidity. So it's not like second guessing because you've often see people saying, oh, liquidity here, liquidity. Oh, a swing failure or liquidity sweep. What is the liquidity sweep? So here we have a simple auction. So let's begin with a banana offered at a starting price of 150.

[9:46] At this moment, we only need to ask one question. Is anyone willing to participate at this price? This is the same question the market asks continuously. Price is always searching for an area where participants, you lot, me included, are willing to transact. The starting price, 150, is only an offer. It only becomes meaningful when someone is willing to transact, like a house. If you want to buy a house and the broker comes to you or the agent says, oh, this is worth 2 million. Well, on paper, maybe. But

[10:25] if you're willing to pay two million, OK, that's worth two million. Only when you're willing to actually transact. Hope that makes sense. So let's see what happens when nobody is willing to buy the banana for 150. That's right. The order is rejected. None of you said, I'll buy it, right? So the answer is no, let's not buy it. And the starting price of 150 is rejected. So this tells us that that starting price has not been accepted and the auction cannot conduct business there. Okay, so we can't do

[11:02] business here. 150, too expensive. No, thank you. Next. So financial markets operate in the same fashion way. So when price reaches an area, but it fails to attract businesses, like those customers from ABC, for example, earlier that we talked about, the auction often moves away and search for areas where willing participants are willing to transact. OK, so this is our banana example. And what I'm just saying now, it's actually the market. OK, so, yeah, and this is the important thing about the both of them.

[11:41] OK, so the auction now needs to do something. What does he have to do? Lower the price. Right. Okay. So the auction now needs to do something. What does he have to do? Lower the price, right? Let's lower the price. Let's go to 145. So as expected, he lowers the price. And this is when people start to think, hmm, I could do with the banana for 145. So it starts. So at 145, we then have 10 buyers willing to buy. You can say two buyers at 145, 10 buyers at 146, and then 22 buyers at 149. So participation,

[12:21] as more people start to know bid on this banana, you can see other people start to then get a little bit more, okay, this is looking interesting. I'll buy it as well for 149 now. Okay, so let's see if then more people join the party. So the sequence began at 145,

[12:54] 146 149 and now here we are sitting at 153 and going on to 162 with 121 people now in the room wanting to buy this banana so remember earlier nobody was willing to bite this banana for 150, right? But now, after moving lower and attracting that participation, they're willing to pay 162. It's funny, isn't it? So what this means is that price is always seeking from that area where it goes from no participation to the extreme of participation, and then it tapers away again. And we see this thing every single day in the market profile in the auctions,

[13:33] every single day. So as you can see there, this is just an example, but I'll show you live here in the charts in a second, that price participation brings those, well, brings that auction to life. And then typically, when you see those moves fading into, let's say, an area of interest that you have, and then now you can see where I'm going with this. So you have this analogy, let's put it that way, where you understand that auction, let's say, on a 30-minute profile for, let's say, a 30 minute profile for let's say a session or

[14:06] b session or c session or d session or any session you can see how well you can use this to your advantage for example a little bit more practical price is coming down to give me a level any level that you guys like to trade area of interest a weekly value area high. So price is coming down to your weekly value area high. And it gets there, and you have the SF full of orders, and then you have, let's go with ES or NQ, it doesn't matter. But you have at the low of that move, let's say it's around that 145,

[14:42] extend that point of control there, and it's really like showing a lot of participation would you just go in and buy it now knowing this probably not because it's showing that it's attracting business there's participation willing to go and transact there so you think to yourself that auction is not quite being rejected so So price is likely to go through that. Not unless there's a massive iceberg there and someone is really willing to buy it. And you will see a price will come down, hit to go back up, hit it again.

[15:18] But eventually, later down the line, it will probably get tapped again and take the stops and then go again. All right. But it's just this is a concept that most traders should really understand, because the market does this every single day. It's always seeking for that two sided trade. And if there's no people there willing to transact, that means that auction is coming to an end. So no participation means no one is there to make business and often on nq for example if you have less than 10 lots and you can really start to look

[15:54] at it like you've got the point of control like 500 lots and then below 300 200 and then increases a little bit more but typically you'll see that. And this is not on the daily chart. This is on the 30-minute auction. Okay, that's what it merits for us, traders, short-term traders. Because you want to remember, you want to start A, B, C, D, get your hours done, close your shop, go home. Okay, so that really matters to us. So market profile with the overnight inventory, that's why that's going to be a key thing

[16:24] when we have that implemented. But you can see that anyway nowadays with the overnight inventory. That's why that's going to be a key thing when we have that implemented. But you can see that anyway nowadays with even trading view. So you've got your context, your area of interest. Then you've got that 30-minute volume profile. So you're probably asking the question, but where do I see that participation? That's just a 30-minute volume profile. We all know volume, okay? It's those two transactions.

[16:46] Confirmation, we use it on the surface. And then the rest, that's the key part is, OK, where's your risk? Where am I going to be out of this trade if it doesn't work? OK. And I think I was talking to Alberto earlier. There you go.

[17:10] So earlier, there you go. I was talking to you just now and I thought I'd use this because earlier I was talking about I personally use it the whole session. I can use this information the whole session. OK. And I earlier said that maybe for some of you, that business hours A to D is great business. D is great business.

[17:26] But you could also look at the market profile or any trading. You don't even have to have the market profile. Just have the 30-minute bar.

[17:35] But you can also say that that A session, B session,

[17:38] could be just like this room, completely dark.

[17:42] So you come into the market and it's dark. And then you have A session. You've got a bit of more light so you can start to see where you're going sort of tripping over chairs and stuff and then you've got b session it's getting a bit more brighter c session we're already either running above the overnight high or low we've got some developing value we can start to see how i can start to see where the market is going that's why i always say C session typically gives us a nice trade.

[18:07] Could be the opening range breakout to the upside. Could be to the downside. Maybe we've looked below the overnight low, and now we're doing a counter trade action relative to yesterday's overnight inventory. Okay? So all of this information can be used in context. So ABC could be, okay, put the lights up. Where are you? Where's value in relation to yesterday? So now you can look at C, D and E. All right. I've got context. I know more or less what the market is trying to do here. here i can go the rest of the session and build a nice position for the end where i will try and

[18:46] collapse that trade at m session which is the close okay so you can either build it during those four sessions or you can just sit on your hands a little bit and wait for the confirmation which is great so think about that analogy where it's the lights lighting up your room so that you can actually see what we're trying to do with those sessions. But yeah, context, participation, 30-minute volume profile. Is there anyone actually there trying to do business? And then we can then look to DOM surface.

[19:16] The business is there. I can see it. They're right there. I'm not guessing. There's no second guessing. You can see it. It's right there. And I'll show you in a second. And then you've got your execution. Yeah, of course. When I'm using the DOM and I'm looking for liquidity and you've got the bright red, I'll say there's some contracts sitting there. When I'm using the DOM surface and you've got the red sort of line, there's a lot of contracts sitting there. Yeah, you can see.

[19:40] You mean like when you have like a red line? Yes. Yeah, that means in relation to what you can see, let's say that's why sometimes you see me, well, you probably see me, I sometimes will zoom in and out because I want to see, because it's a visible range. So let's say you're only looking at five minutes. If you zoom right in, it will show you the thickness of that color, the brightness of that color will be just for those five minutes. But if before five minutes there was like 100 lots, that would change this color.

[20:10] That's why sometimes you see me zooming in and zooming a little bit out to have a perspective. Okay, what about early? What happened earlier? So that line can change depending on. But typically, if you keep it consistent, like to the five minutes, for example, 10 minutes, 15 minutes, you'll have a good picture of what's going on. like to the five minute, for example, 10 minutes, 15 minutes, you'll have a good picture of what's going on. Yeah, I was just wondering, should I then also be looking at the 30 minute sort of TPO to see whether that's a POC or do?

[20:32] Yeah, you could. Because then if it is, do I then know, OK, well, that's not where necessarily I want to enter because that's not one of the extremes of the range. Exactly. And look, we talk about a TPO, point of control, and it could be anything else. For example, options. We know the walls of options. It's the biggest thing ever. And typically, you could see where those big levels are yeah because that's where the orders will be concentrated so you have so let's think about this so let's say you got me

[21:11] where's severin severin are you here where is it i was where anyway so you've got severin you got me you got him her you you got all of these people looking and doing ta and i'm not going to say that we're all right most of the time it's the opposite way but you have big pockets all looking at these nice places these nice locations and we can see a fraction of it because we can't see all of it because they hide it obviously but we can see a fraction of it in DSF. And what happens more often than not is I open Quantower,

[21:48] I open DSF, and I can say, okay, that is a good level. I then go back through my TA. It says, oh, yeah, it's a naked point of control. It's an options gamma wall. It's one of these big levels that we all know too well. So, yeah, absolutely. But instead of spending two hours getting there, it's quicker. I don't want to say the word quicker. I want to say or use the word or the phrase, it's not a shortcut, but it's a more efficient way to get to where you need to go. Great, thanks.

[22:21] All right, you're welcome. So probably a couple of parts to this, but the first one's just on the actual DSF and the depth of market. CQG, I think, has a limited amount versus Rhythmic. What are you using typically on? Rhythmic. Which is what depth relative to CQG? Yeah, that's it. So CQG will always limit the data. So the packages being sent will be limited by CQG. And, for example, if you load ATTRS, you'll find that your volume profiles will load very quick, and that's because they load the data, they store the data locally.

[23:00] Quantower and TradingView, they will go and download that data from the server. If you're using CQG, you're going to have a little narrow path to get your data down. You can have the data, but little chunks at a time, whereby Redmix just says, it's here. Have it. Yeah, because your DSF there looks way more detailed than Atas' heatmap, for example, which is quite narrow. You can't use it. Yeah. I would say if you're serious and committed, rhythmic is the way to go. Yeah. And then the second part is…

[23:33] You will see the depth. It will be very deep. Yeah. And that will give you the context that you need. Because if you're only looking at… Because the difference is, this is great for getting onto a train and riding that train. And if you can't see what's happened or where you're going, it's like driving without any lights. That can be quite a dangerous situation. Honestly, well, actually, that's a great analogy. I might use that on my next stream. And that just came up. But yeah.

[24:02] Then the interplay between like limit orders and market orders because dsf showing limits at levels limits yeah limits yeah obviously like once it gets filled nq you'll get a huge influx of market orders which then moves the price quite quickly right because it just takes out all of all of the offers yeah that's it you will always see limit orders there when they get filled that's why on the next, you'll see I always have the tape, time and sales. OK, because I want to see because let's not forget, like a lot of institutions, a lot of traders, they can hide orders.

[24:33] You will see, for example, 100 lots and you think it's just 100 lots. But what you don't know is that as soon as that 100 lots get depleted, like it replenishes. It will be 99, another 100 lots, and you'll have a massive order behind, like an iceberg of 1,000 lots, that every time it goes down to 99, it just goes back to 100 and just keeps adding. That's why sometimes we see price coming up, hitting a level, come back down, goes back up again, hits the level, back down, and then goes until that 1,000 lots order

[25:03] that you can't see the rest of it gets depleted, and then it breaks through. And often, if you're there and you can see it, you can actually take a long trade with a relative small stop. And you can say, okay, we're going to break through that. And you probably see me sometimes, okay, it's gaining the energy to break through the level. And once it breaks, you, if you have, let's say you're there and you can see it and you've got the knowledge, you can say, OK, there's a little trade here.

[25:29] I can put myself literally a few points just below and let it ride. If it works, great. If it doesn't, well, that was an attempt. It doesn't have to be 100% win rate. When you mentioned overnight profile and whether it's going to rebalance or not, I'm just curious, is there anything that you see ahead of the open that tells you whether it's likely to rebalance today or it's going to be accepted like, I don't know, where overnight's trading relative to previous day value or something along those lines that tells you like,

[25:55] okay, we're strong bullish in the overnight, but actually I'm expecting a correction on the open or it's going to actually continue higher, like how you would decide that. Good point. If at the open you it's going to actually continue higher like how you would decide that good point if at the open you are very long so and this is yeah it's a good question i like it so if at the open so you've got the market profile let's see this is yesterday's regular trading hours the profile

[26:17] and the overnight is already trading up here think about it like in a chart yeah overnight sorry yesterday's rTH session. And now you've got this section here. That is the overnight. If we're trading up here, well, there's a gap. So the question here is you just have to look at that gap. Does that gap hold? If it holds, it's bullish. Something has changed and the market is going higher. If the market comes down and fills the gap totally, that could often signify that the market is either rebalancing, correcting the overnight inventory, and if it really holds above

[26:51] the settlement from the previous day, the end session, then that could be a really good long trade. I prefer it when it completely doesn't fill it because it's like traders miss the train, and then we can see the delta coming in, and then you buy to go higher but yeah that's a great example and this is why we just need to see one overnight not 20 or 50 just one and then the rest stays at the regular trading hours it's very important great question so it's a poor low repair short so you're looking at the market profile and on that left, that is yesterday's session.

[27:26] OK, so you can clearly see that today's session. We're already trading H period. So not A or B, but we're trading where above yesterday's value. So what does that tell us just by looking at that picture? Higher, it's bullish, right? Not always. We can always come back inside. Obviously, that's why we right? Not always. We can always come back inside. Obviously, that's why we use other contexts and confluence. But just by simply looking at that, we can all agree that the market is going higher. But what else can we see? We can see that the E and H, okay,

[27:57] the low, they're very poor, okay? So in this day, we're attempting to go higher, but the market is leaving these poor lows on here. And you can see that every time it goes higher to the H period high there up here, there's literally what do you see there? That little white bit. There's nobody home like the market goes there and for Gazi, there's just no but one at the high. I think it was in this case. Yeah, one. so only one order up there as in like one transaction so the market is clearly

[28:28] saying that don't want to go higher okay so we have a significant participation at 225 I'm here what can I zoom in oh come on I think yeah 22, we've got 400. So to us, traders, looking at that and looking at the poor low, so here's the weak low, F and H, okay, you can look at the two and you can say, okay, the market comes higher and it rejects those higher prices. So it's not going higher. Look at the SF on the right. So, and when we go higher later, you'll see, I'll play it all

[29:04] in a second, but you can see down here, we've got the Delta. Okay. So this is the right. So and when we go higher later, you'll see I'll play it all in a second. But you can see down here we've got the delta. OK, so this is the delta. OK, and typically price will follow delta. OK, so if delta is positive, price will typically be running higher. Delta is negative, price will be going lower. OK, so in this case, we have just by looking at this little profile here, we can see that higher prices, just like the banana, are saying, look, unaccepted.

[29:29] But lower prices, the market is basically saying, OK, we we typically want to go lower because that auction to the downside, especially when the PRC moves lower. So we can play a little bit longer and you can see here that we're trying to go to those 260s. I wish we could see a little bit better. So this is here that we're trying to go to those 260s i wish we could see a little bit better so this is me already looking at the h low i can see those bars with the red delta so price is going higher but the buy delta is decreasing down here but we don't

[29:57] have those buyers fully committing we still have a weak low down there. We're going higher, but the delta right here, if you just pause it very quickly, and then let's look down here. You're going high. And this is things that, yes, you may find a bit overwhelming, but if you're doing your business hours, A, B, or C, and you're just taking one trade per session, then this is your cue. It's going to take some processing power, but then you can have the rest of the day to do as you please. You can

[30:29] go to the beach and recharge your battery. Okay. So yeah, it's going to take some concentration, but then what kind of job doesn't give us concentration? This is it. Hard work pays off. So you can see what's happening here. Price is going up to the 60s. Look at that liquidity around the 60s. OK, and that liquidity only came there. You can see it. If you have all this black, you're not going to see it. If you have it like that, boom, there you go. So price is swiftly going up there. But what's happening on here? There's no delta to support price going

[31:03] higher. OK, so to me, that's a cue. OK, my poor low right now with that massive auction and 400 lots. Oh, yeah. Price is likely to reject this, especially if below this, you know, the time frame beyond what you can see here is lower. OK, which you can see from the left just one one chart one chart isn't it amazing that you don't need anything else it's quite astonishing right right so no delta price going up into the 60s no delta you've got the point of control right there you can see the point of control can't break it too heavy this is me just saying yep

[31:46] look at that there's nobody home above that level we come lower and then get in the habit when you're trading to go 40s so we're at 40s now you can see i've made a line here the higher the higher that just means high and low there high and low and low. And then that's me. Okay, that's my cue.

[32:09] No delta positive higher than the previous. Right there.

[32:17] Price goes up, fails, below the point of control, cannot get acceptance above. The lights are on. Nobody's home above 260. Price comes back below it. That's your trigger.

[32:27] Alberto, is that good risk management for a stop loss? Thank you. So your stop. So you can see here, there's my stop. What I love about this, right, is what I call painless trading, because that is quick, right? painless trading because that is quick, right?

[32:48] But what you have here is if you're wrong on your thesis that this is going to take the low, you are wrong very quick. And what you're going to pay for being wrong, it's not a lot. But your reward, you can see there, it's actually quite nice. So there's a lot of context, I know. There's a lot of context. And then watch the delta down here. That's me measuring the risk there, just always keeping an eye. And you can see I actually have a conditional order below 37 there. Should that go through that low again, that is even more confirmation than this.

[33:20] And there you go. That's me saying, okay, go below that 41 now, and I want to have another one there to go because my stop is literally just there it's only a few points away keeping it under delta and look there's nobody home there now they've got their orders filled above that spike there with the delta they're all happy and the rest is just waiting for the load to be repaired still if this was to go a lot higher, you would see those massive spikes in delta there, but you had one and then the rest, and there you go.

[33:51] There's your little telltale sign. Okay, well, there's a couple of sellers here turning up. And then at this point, you have a nice trade, painless trade with a bit of context from the market profile. You're aiming for that low that you want to repair. So that's your context. DSF is showing you live exactly what's happening. There's the sales coming in into the box. And at this point, you've got two options. One, break even. Stop. Second, take one by yourself. Say, well done. Good reward. And then just watch the rest come to fruition.

[34:27] And what I like to do, as you all know, is let it cook. I either will close one, let the rest drop into target or get stopped out. You can see me here. And I'm yeah, there you go. You can see me here what I'm doing. So actually, in your view, when you're moving your stop, you're looking at two lows back, three lows forward. You can see here, I'm using slight structure. So lower height, lower low, lower high. Maybe I can point it. No, it doesn't matter. You can see lower high, lower low, just there on this section. But I'm using a placement for my stop. What I know,

[35:03] But I'm using a placement for my stop.

[35:08] What I know, there's going to be a few sellers below it so that the market doesn't just go there, hit those orders, which is a great compound. Let's say you want to add. That's where you put it. But you don't put it on it. You put it slightly below because it might not even get there. Because let's say it's first in, first out. If there's 100 lots there and your order is on a queue of, let's say, 48, it may fill 15 orders, but yours is left behind. So put one tick below. You're more, not guaranteed, but you're likely to get your fill.

[35:35] You can add another one. Your entry's at 41. You've got plenty of room still to add. I probably wouldn't add it this late, but most traders do, because that's, let's say you missed that first spot. You could actually go with one trade here. And this is the beauty of it. If you're just looking at a little section and you've got that context, you can say, okay, that's my trade. You don't have to over trade, but you can say, okay, one per session. And when you kind of have all the information presented like simply like this, and you can see all of

[36:02] that red on the tape there oh yeah that market is poised to go lower does that make sense to everybody it's because it may sound really overwhelming but that trade is netting is i can't quite see and typically i don't really i think it's in points but yeah that's that's a good pay for a few minutes really pay. You just have to be patient. Okay? And this is classic. You know, once you start looking at this, and there you go, there's that little last push. Look at the delta down.

[36:32] It's just beautiful. I mean, it can be fun. And then there's the yes, I'll flip. And that was my target. So we can just say, okay, that poor low, look at that on the left now. There's that business finish. Look, low auction now. See, lower price is now bringing less participants. low repaired and business is finished and that's not cherry picking that happens every single day yeah is it good yeah we've got a couple of questions there i'll bet so good hello okay does it work okay sorry i i don't, I still don't use Quantower, so I didn't see where you put the order.

[37:09] You didn't sell at the top, right? You just first waited for the break of structure to downside and then you sold. So the question is, of course, after the price action developed, it's easy to say, oh, I should have sell there. But if you saw from the DOM that there were a lot of order where actually there was the top, why didn't you short aggressively there with a really tight invalidation? You would have a huge risk reward. You could do and you could face missing the trade because it never got there. And we know that because

[37:41] it's hindsight trading now. What I like to do, and everybody will nod their heads and say, yes, he does that all the time. I prefer to be a little bit later than too early. And I know that from first-hand experience because we get burned. Okay. Because in that case, it was like 2 to 1, 2.5 to 1 risk reward instead of like ten to one if you aggressively shorted yes yes but i prefer to go with the ones that i know okay there's a higher order here because i'm going with momentum okay so like riding the wave instead of anticipating

[38:17] a wave that may not come you see the wave and you write it when you see it. Yes, correct. Like a surfer, okay.

[38:28] Let's say I want to have predefined levels as in like that's my max pain. You will fail where you see that bubble, but you could get burned many times that you could count. So why not let the sheep, no offense to anybody, why not let them get absolutely wrecked? Okay. And then as soon as it comes back below, you then say, here's my queue. And now you've got two things. Now you've got your entry. Okay. And you also have an invalidation. Instead of having a wide stop, instead of putting your stop much higher,

[39:01] now you can put your stop right there. Okay. By just waiting a little bit longer. Okay. I'm still not used to see the market profile. It's something I need to learn and practice. You're not used. Try. Can we interpret it as something like a lot of participants, no trade zone, low participants? Well, no. No, actually, look at it as context building for your level. You're going to have your area of interest, and then you can look at the market profile there as a gauge of, let's say you're on a short trade, an amazing short trade from whatever level you have.

[39:36] And then price comes down, and we have that, you know, just no one home. This profile, we're looking at this here. It's time to close the short. Yeah. At that point. Well, you can do two things.

[39:52] One, you can say, okay, this lower prices are not bringing more people into the table, just like the banana. That's why I explain it in those simple terms.

[39:59] So if no one is willing to come lower, you can look at the profile.

[40:04] Look, I wish I could have my system plugged into there because I'd probably go into more examples.

[40:13] Look at the profile. A, B, down, C, all the way back up. So if you're on a day trade and the profile, we know this, we see it, it's there every single day. 70 to 80% of the time, we're doing exactly that. We're going higher, we're coming lower. We're going higher. We're coming lower. We're going higher. We're coming lower. On the right, that's always what you see. And doesn't all those few participants at the low, doesn't it automatically mean long entry? You could. Okay. You could, yeah, if it's your area of interest. And that's when you, with great context, you could say, I'll close my short, I'll go long, I'll do the opposite,

[40:51] and then people are buying the breakup, getting wrecked, and you're taking profits and going short. And then I buy a Ferrari. And then you buy a Ferrari. Make sense? the risk management, the stop placement, sometimes I have a very hard time moving and deciding where to put the stop. Do you think this can help by seeing that? Do you see when we move our stop above the orders which are being tapped right now? You could use this to identify where is the highest level of liquidity in relation

[41:24] to your entry. And you can just use a but. Let's say you're on a short from, like we had here, you're on a short from the point of control, and you want to take this trade lower. You can either, if you want to let it cook, as I often say, you can do so. Your stop can be above the point of control. That's the safest play because you know there's a lot of activity there, and be sure if price goes back up there, there's going to be more traders willing to transact because that's the fairest value. That point of control will carry on all

[41:53] the way to M session and will probably be the point of control for the day. So you can already say, okay, because there's a lot of transactions there. And this is things that you can see, like the analogy with the lights, you can see it's right there and it's not even finished it's only h we still have a few more periods so yeah if the price is coming down you're on a short trade and he says you know what this could potentially go back down to the initial balance low which is this section here so that blue that purple line that's the initial balance

[42:21] so you're still trading inside it typically you will find most days will go down to the initial balance. So you're still trading inside it. Typically, you will find most days will go down to the initial balance low, which is B, C and D. And honestly, it may sound it may look overwhelming, but that is just a 30 minute bar. It's it's just organized by ABC. It's nothing too complicated. I urge you to really study it. So yeah, you can put your order above this section here. So let's say that's probably not the best example. Or let's say

[42:50] you're long. Okay, let's say you're long. For example, you're into your long trade, and you can see at 29 to 10, there's a bunch of orders there. Not enough, but there's a bunch of orders. Your stop, if you put your stop above it, it's very likely you'll get stopped if price just comes back down a little bit. Just put a little bit away from it. Yes, but once you're inside and it starts going your way, I have a hard time knowing when to put my stop to break even, for example. Break even can...

[43:23] Sorry, Jim. Sometimes it just goes and it goes back on my way. That's going to come with repetition and being confident, a little bit more confident. And you will find that if you're looking at these things at those certain areas, times, you will become like, okay, I can see it. Like Stelius, you could comment on this. We were looking at the markets not long ago and he saw something on the charts that i always talk about and he spoke to me it's all in the chat and i said to him based on what i can see yeah he's right price is going to do the bish bash bosh at the

[43:56] time wasn't it and five minutes later what happened bish bash bosh and it's just that kind of like oh i've seen this before. It's repetition. It really is repetition. Yeah. So you will find that you will get used to seeing these little patterns, especially if you have it all like that. I know it's a lot. I understand. Maybe someone is looking at this thinking, wow, that's too much. You know, I want something simpler. But this is the live auction. You're looking at a live auction.

[44:23] This is participation. So traders, are we actually here trading and making transactions? but this is the live auction you're looking at a live auction this is participation so traders are we actually here trading and making transactions and this is who's actually trading it is there actually traders there and we can see everything here this is all the live data market profile is just telling us context poor low okay let's go tack it um what untested value you can see here's the untested value very low for today right there that

[44:45] blue line and this is volume the time is a little bit higher that one there is volume as well so these levels day upon day get respected to the t and don't take my word for it that's your homework go home load this market profile and just look back you'll find most days depending on where you're trading you come down retest them and continuation. And often it's quite tight, like stop losses. So instead of buying the untested level, you now can go home and say, let's try this. Here's your, let's say, if we extend

[45:18] that line and that line comes across to here, okay, and that is your untested value area low. The overnight is long. OK, in relation to the close of the previous market profile, the market comes down at the open, your business hours taps the untested value area low. A bunch of orders get hit at the bid there. And then you've got a zero market profile or near or below 50. Below 50 is great. 10, greater. 1, buy it. Okay? Because there's nobody there. And as soon as it goes back inside or above the level, that's your trigger.

[45:54] So you could say, Severance Strategy, it's great to go with this. Because then you can say, okay, well, that's it. That's my confirmation. You probably don't have to wait one minute or two minutes. Because you can see it. That's my confirmation. You probably don't have to wait one minute or two minutes because you can see it.

[46:10] It's like this is tick per tick. So that's not a one minute chart.

[46:14] This is tick per tick, order per order. And that's the timestamp.

[46:17] So you've got New York. This is 129. So this is the PM session.

[46:23] I hope that makes sense. So this is a week high. So week high is when you have over there.

[46:25] You've got the E pretty much at the same tick. You've got U, V, pretty much the same tick. And I love this one. Look at this. Look at that. So the market is blasting to the upside. Okay. Blasting to the upside. We're outside value. We're going long. We have a massive excess. And then V, look at V. V opens up there. All right. That's the open, the green letter. Everything that I've created there is to make you really look at this as if it was a candle. Look at the 50% of the previous candle.

[46:53] That's the yellow. So if you have a candle chart, that right there, oh, go back. Yeah, there you go. So that V is the half back. So that's just the 50% of the previous candle. If the market is bullish, it's not even going to attempt to go close to that. It didn't come to there. What did we get? A really low auction. There you go. So around the 20s. Comes down to there, there's nobody home. So you are bullish. You have poor highs, weak highs. Market is really trying to squeeze here.

[47:20] We're building value above. You can see the value there. We come down, there's nobody home. And then you have an open very, very close to to the high and then the initial move is down i love that i explained that this morning if you're bullish you want to buy where below the open like we get a flush below the level you can see a massive spike you get some delta i'm already getting in because look at this this is only a few points risk you've got your clear defined level i love when the flash happens like that coming into your level what

[47:50] else do we have here we have a big psychological level just above the 600s comes to the tens you've got context above at this point you can move your stop to break even if you wish that's what i like to do. Just put it slightly above it, at least get paid and rewarded. The market typically likes to come down, retest, keep an eye on the auction, keep an eye on the volume, keep an eye on the delta, and look above price. What do we see? When you see those stacked up there on the DSF, that is the market saying, magnet, come to me. And it feels rewarding when you're

[48:28] in the trade and it's going your way, obviously. But if you have this data, you're not guessing, you see, it's right there. Look, I don't want to say the word easy. And look, more adding. Can you see more adding up there? And what the price is? 69. Look, there you go. And price comes up. This is me thinking, do I get out here with one lot? Look, it came so close to it and did not hit my order yet. Because let's face it, there could be 150 orders that we can see, but look how stacked that is.

[48:58] And look, now you have a poor high. Can you see? So it's likely that the market is going to try and blast that. Maybe a lot of traders would say, I'm out. But I'm saying, no, SR flip. There you go. Let's see if we can come up there and take that. You don't know at this point. You're just going with the gamma probabilities. Typically, the market likes to go there, especially after a beautiful SR flip. And look at the arrows. What do we have? Participation coming in. They're still buyers. Okay. Nothing to be scared of. Let it cook. This sometimes can be a tricky part

[49:32] because it's like, I'm not going to get stopped out. Just let the market do what it wants to do. You already, on this trade, if price came all the way back down, you would lose nothing. And on an as you guys know, it's cheap. To open a quarter million position. And there you go. That's me out. I've got one left. And I'm thinking, hmm, do I keep it on? No. I'll take it. Thank you very much. Yeah. Again, context. So this one, overnight continuation long. Sometimes what do we see? The market goes higher. You can play it. And this one, overnight continuation long. Sometimes what do we see?

[50:06] The market goes higher. You can play it. And this is from get-go. Look at the delta. Massive delta. We're building value a lot higher. The same thing again. The market, look at the excess. And this is, I'm not even going to read the notes, but you can see down here, look at the open. So A session A, they're green. The E session, look at the w wick look how long that wick is you open the market profile wow look at the buy where's the point of control down there okay we reclaim the point of control well what do we have the overnight high lovely let's trade for

[50:38] that comes down lights home a lot of activity loads of orders loads of orders what do we have here 29 600 and the market says i'm not getting below it so i saw that with that delta oh yeah i'm buying that and it's right there i'm how can i not how can i miss that can you see it how can i miss it it's right there look at the delta i'm like wow this is a good you could add two lots three lots four lots and have your stop literally where? At entry. So for those who like to drive 100 miles an hour, that's probably, you know, it's nice.

[51:12] Because it's not zero risk. There's always a risk, but it's very limited, right? You can all agree that. And then we just let that go through the high, because now you have a poor high. 30s. You can see 30s there, that ASA flip. Once was resistance. Can you see here? Once that was resistance then it supports and then it continues higher what do we have at the 50s look at that lovely price typically likes to go there especially if that delta is still positive and then what do we have building here look at that baby there

[51:41] look at that there what's happening here higher there. What's happening here? Higher prices. You know, they're still buying those bananas. Look, they're still coming in. Look at that. They're still coming in. And then you have an order hub. OK. And there's nothing wrong with class. I can't quite see it. Maybe a thousand. I don't know. But you can see it can remove a bit of emotion when you're in a trade that you know that whatever happens, you're not going to say to your wife or your husband, oh, I've lost 1500 today because your stop is very nearby.

[52:17] Because when you zoom in on that section, there's still participation. This is obviously it's now using the previous big orders from down here.

[52:30] So that big break now has made that order relative to the small ones up there really big.

[52:34] So that's why I always zoom in and out to get that context. And there you go.

[52:42] So you could say, even though in this case, the overnight high is my target. I'm not naive. Because I can ask you now, what's the risk to reward now from where price is to the high in relation to my entry? It's best to collapse as much as you can. And that's what I'm doing here. Because I know that once the business here at 650, which is always a big level anyway is finished the market might just come inside but I'm very close to it I'm very I can see my target is right there so I want to go as close

[53:15] as I can to it but allowing it to still breed look at the Delta boom there through the level we near the all the overnight high. And I'm going into it. And I can bet you a lot that you have traders up there waiting to take a short. And we're like cooking along when the market is saying, let's go along. And then what happened? There it is. So we basically took a trade and look, and then the market comes back in. You can play the next video. Trade is finished. But you can see the context. context and we still after we go back above it but that

[53:49] doesn't mean the market can't come back and take all of those profits away so you see profits take profits just manage that risk it takes practice but once you get good at it you can literally come in any day on any session as long as it's between the abcd all the way up to m and there's traders to be at i would avoid the power hour m because typically you have a lot of traders closing positions and that can be a little bit inconsistent but sometimes you can find a scope or two go in if you have small risk i think we went through a lot here but maybe now you can understand why i always preach the market profile

[54:27] or tpo why because it shows the data organized in one place um like the um ichimoku cloud who remembers that indicator yeah so i think i'll correct me if i'm wrong i think that means one glance ichimoku cloud means one glance and i think whoever'll correct me if i'm wrong i think that means one glance ichimoku cloud means one glance and i think whoever created it wanted it to be like look at it and that's it you've got your information if price is above the cloud whatever it was uh it's the cross the cross

[54:55] is bullish just you're long you're bullish and that was the one glance you look okay thank you and you go ahead to your trading plan and market profile in the sense does a similar thing. It just organizes that data so that you can then go into your trading session a little bit more confident. OK, based on my statistics, my history of trading, typically this is what happens. OK, so the volume always is going to show participation again. Back to the banana. Delta is where price likes to follow.

[55:29] So if delta is positive, price will most likely follow suit. Okay? So if you're trading and you see delta is still increasing, don't close it. Don't cap yourself. How many traders, like Daniel was explaining earlier, take an SFE and then they just get really excited because they've lost a lot of trades leading up to that one. And then they just close the trade as soon as it starts going. It's like, man, just let it breed. Let it go. And then it goes really well and they start to scratch their head thinking, oh, my God, should have left it open.

[55:59] Yes, you should have. So that's it. Look at the delta. OK, so that's why we always have volume obviously delta and then obviously all that liquidity that we saw earlier that's amazing because what we can see is like what's the intent here is it going to stay there because if it stays there there's actually traders trading that level if it's pulled you can see it it will just go from being really bright red to like darker okay so what's the intent are they actually trading that level are they actually

[56:32] present and then just add let's say you can see you're trading this really low time frame abc session and you can see a really every session coming down for example b opens comes down half back of one of a and then goes higher c does the same thing well as long as that mark is one time framing higher delta is coming in you can add you can add you can build a nice day trade position if it's going to start trending. And then you can take that position all the way up to M session or keep your stock below a period M low. So you can do a lot of stuff,

[57:10] but add when you see that evidence. What's the evidence? Well, participation, delta. And then my favorite one, just boxing the runner, trail behind. That's why I say let it cook. I think that's very important. I think this is a good lesson. I think for some of you who didn't quite understood why I like to look at the market profile, it is just simply a visual. Organize the data to show me what it is. And yes, we can move on to questions. I usually want in a day trade, for example, we're talking like, let's say a scalp are you talking like in a scalp trade or a day trade or here for example let's say you saw this massive long wick okay this is this is a rotational day

[57:53] but let's say you saw this massive long wick and you're trading higher so a good placement will be below e sorry f low so that would be because you've got then the support from the point of control and the F. That would be the one time frame breaking down. Okay. So at that point, if it goes below that level, you can say it is likely to go where? If we go back below the point of control and accept below the point of control, it is likely to go to value area low because that's what typically the market does. yeah and then but if you're using

[58:26] dsf and you then zoom out you can then say okay but where is the thickest like the highest area of concentrated orders and that if it's really bullish could be a place where you want to get back into another lot long if you have a good, then you can add in as the market comes down instead of stopping out. So you could build that long trade into it. Okay, so you can use this context and what's really live there to help you manage your risk.
