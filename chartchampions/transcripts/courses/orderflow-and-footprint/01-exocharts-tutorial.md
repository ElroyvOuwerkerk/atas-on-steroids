---
date: course-lesson
champ: unattributed
course: Orderflow and Footprint
lesson_no: 1
title: ExoCharts Tutorial
slug: exocharts-tutorial
category: courses/orderflow-and-footprint
cues: 290
words: 5281
duration: 24:46
source: https://portal.chartchampions.com/lesson/exocharts-tutorial
captured: 2026-11-16
---

# ExoCharts Tutorial

## Portal Summary & Highlights

This session introduces ExoCharts, an advanced charting platform that provides order flow, volume, and delta-based tools not typically available on standard charting software. We review how different chart types (tick, volume, range, and transaction-based charts) can replace arbitrary time-based candles, and explore key features including delta clusters, volume clusters, footprint bars, imbalances, open interest tracking, and value area analysis. The session also covers how to configure these tools for different trading styles, from scalping to higher timeframe analysis.

### Highlights

- [00:02:00] Alternative chart types vs. time-based charts: We learn how tick charts, volume charts, and range charts print new candles based on transactions or volume rather than time, filtering out low-information sideways noise. For example, a 10 million volume chart only prints a new candle when 10 million in volume has traded, giving cleaner confirmation of level claims.
- [00:03:29] Understanding ticks and box sizing:
  - Each tick represents a $1 price unit; increasing tick size combines price levels into larger boxes.
  - Low tick settings (1 tick) help scalpers identify exact price levels (e.g., exact point of control).
  - Higher tick settings (10–20) are better suited for daily or monthly chart analysis where broader boundaries are more useful.
- [00:06:30] Delta cluster and volume cluster views: We examine how delta clusters color-code candles based on market buys (green) and market sells (red/orange), while volume clusters highlight areas of total traded volume regardless of direction. Darker shading signals higher activity, lighter shading indicates minimal participation.
- [00:08:40] Using value area with opacity settings: Adjusting opacity isolates the value area while fading out low-volume zones, allowing quick visual identification of high-interest price levels that may react on retest.
- [00:11:00] Volume profiles vs. delta profiles: Delta profiles provide the same structural information (POC, VAH, VAL) as volume profiles but add the directional breakdown of buys vs. sells—useful for determining whether a naked point of control was built on aggressive buying or selling.
- [00:12:00] Imbalance analysis:
  - Imbalances are read diagonally using bid/ask multiples (e.g., 300x).
  - Higher multiples filter for only significant imbalances.
  - Delta display shows net buying/selling at each price within a candle (e.g., –1.8M delta at POC inside a +2M delta candle).
- [00:15:00] Liquidation markers (RECT): Circle size indicates relative size of liquidations at each candle. Green marks shorts liquidated, red marks longs liquidated. Best used in combination with open interest to assess trend health.
- [00:16:20] Footprint bar components: Each footprint displays total volume, delta, CVD, point of control, and high–low range per candle, giving an immediate statistical snapshot of participation.
- [00:17:00] Trade size analysis for positioning discrepancies: Observing where large accounts vs. smaller accounts are positioning can reveal potential discrepancies—for example, large sells on a rally while small accounts buy. This is informational only and should never be traded in isolation.
- [00:19:10] Delta bars and healthy trend confirmation: When price breaks a prior high, we want to see increasing delta above that level, confirming a healthy continuation. Divergence between price and delta can signal weakness.
- [00:20:00] Open interest, net longs, and net shorts:
  - Rising OI + rising net longs during an uptrend = healthy trend.
  - Rising price with falling net longs = less healthy structure.
  - Combining OI with directional data helps distinguish new positioning from short covering.
- [00:22:00] CVD divergence setup: Using CVD as a line (rather than bars) helps spot divergences between cumulative delta and price action, which can precede reversals.
- [00:22:40] Multi-timeframe volume profiles: Daily, weekly, and monthly volume profiles are layered to track naked points of control and identify higher-timeframe support/resistance zones (e.g., monthly resistance near 9,740, support near 6,860).
- [00:23:40] Upcoming educational focus: Future sessions will cover delta in depth, CVD-based entries, detailed volume profile and VPOC analysis, reading footprint charts for trend health, and interpreting the DOM.

We encourage reviewing these tools carefully, experimenting with the settings on your own charts, and integrating them into a structured process with proper risk management before relying on them for execution.

## Transcript

[0:00] Yeah, I'm happy to finally bring you ExoCharts tonight. So that's what we're going to be going through today. And by the end of the day, you'll be set up and ready for that.

[0:08] ExoCharts is, I think it's like $30 a month. I don't know. It's around this sort of price. It's not expensive for what it is. So to put this like, really emphasize this, for the same information. Okay, so for the same sort of information, I'm used to paying $1,400 a month for essentially the same sort of info on the stock market but nevertheless 30 months in for 30 dollars in my opinion is an absolute an absolute steal this is like incredibly incredibly cheap there's things that i just needed okay and this in my opinion is just one of those needed things

[0:41] okay and it's not not worth thinking oh i can't afford 30 pound a month what i like about this type of software is that everybody is currently just used to this this column here where you look at uh you know you go over to trading view okay and you look at the daily chart okay or you look at the 15 minute chart you look at the five minute chart and as i've been like hinting upon previously that you know it's not always just about you know for example saying oh i want a five minute close

[1:09] above this level that you know you know i like to say it because it's really easy but you know at the end of the day when when i'm sort of saying oh we'll just wait for a five minute close you know is arbitrary these five minute closes above a level are you know somewhat arbitrary numbers. Whereas if you switch, you have access to, for example, a 20 million delta candle. You have access to a $20 million candle. The range, the tick charts, referring to transactions, you obviously have your trend reversal charts,

[1:39] your point and figure charts. These are all charts. you get point and figure on trading view but it's actually less advanced so you know this is really advanced um sort of ways to view a chart essentially okay and ways that you get confirmation on a level being claimed okay when i'm sculpt trading i generally i'm not looking okay at these sort of time charts and i'll be moving over to something like a tick chart okay so basically um what we the again, I'm going to dedicate a whole stream to this.

[2:08] This is just a general overview. But essentially, I'm only going to get a new candle printed, for example, when there's been 230 transactions. So it's it's not to do with time. OK, so obviously on the one minute chart, I'm getting a new candle printed every one minute. And you can see the noise that it creates. I'm getting a new candle printed every one minute and you can see the noise that it creates okay I'm getting a lot of sideways noise one could say like useless information like I don't need to know any

[2:31] of this information I don't need to see from here to here like you know what eight candles printed um you know because there's nothing actionable in this there's no information that I could find useful whereas if I change across to for example um like a tick chart or a volume chart well then i'm only going to get information printed when i require that information so for an example if i was on a 10 million volume uh chart i'm only going to get a new candle printed okay when the total volume as you know is 10 million and then it will

[3:01] print another one e.g i can sort of see when a level is being claimed in terms of volume and i can see when a move when price is getting rejected on low volume because prices won't be essentially moving down but it's not going to be printing a new candle because there's no volume behind it so yeah this requires a whole stream but nevertheless this is extremely extremely extremely helpful and you just don't have access to this sort of thing on trading view um moving across here is

[3:25] where you have your ticks okay so if i just move this onto something that's going to be recognizable okay so for example here we're looking at uh ticks so i've got this currently again you you can change this and this is one of the main advantages of this website okay so um again you don't have this in another software period you don't even have this in really high-grade, really expensive software. So this is really crazily good, by the way. But you can adjust the ticks on all different sections.

[3:54] So obviously, whatever you have on your control panel on the left, your control panel on the right, your main charts, obviously your CVD, the way that you mark a chart, which I'll get to in a second. But each one of these can be looking at different ticks so my main chart i could be looking at six ticks for example and then the way that i look at my developing week i could you know i can change all the ticks up within this i can go into my control panel on the left change around the ticks

[4:18] on the you know on a cost on a custom level so everything can have its own viewpoint so i guess the question first is what is a tick okay so if we come down to uh one tick you can see that uh every single price is is made up of um so each movement down here is one dollar okay so you can see that there's a lot of information on this because each of these sort of boxes are one dollar okay whereas if i increase this up to for example 10 okay so now each of these is becoming 10 so uh you can see the more you move

[4:46] this up obviously the less amount of boxes that you're going to get um again if none of this makes sense then then ask a question i'll get to the end but it should be pretty simple you can kind of see visually how i'm moving up more ticks and now suddenly here we only have uh one two three four five okay so we only have five boxes here when that's 31 ticks per box whereas when we go down to you know go all the way down to um one tick okay or you know you're going to see that the amount of you know levels within here is is uh you know there's

[5:19] there's suddenly a lot more okay so that's kind of where so the advantages and disadvantages of using low ticks versus high ticks obviously when you want really low ticks like one one tick is when you really really want to zoom in okay so when you really want to zoom in here and get and you know see the exact level okay so if i want to know the exact dollar where the point of control is well then i will know it's it's 9 to 40 okay uh and then obviously you've got 9 to 40 and so that's the advantage when you're

[5:46] really zooming in and i would personally say this is for like sculpt traders okay if you're if you're looking at like a daily chart you don't you don't really anyway need like uh it on the one tick setting okay so if you're looking at like a monthly chart you're not going to have this set to one tick because you don't need to know such really small information. Maybe maybe when you're on like a monthly chart, you have it on like a 15 tick setting for an example that you don't need to know the exact dollar.

[6:13] OK, you want to see like a bigger boundary. So one would say maybe on like daily, you could have 10 ticks. And then on the monthly, for example, like 15 to 20 ticks. Again, you can experiment with it. I'll go through some of the things I have, but that's what the ticks are here. Okay. So it's how many ticks are being formed here. And you can see like, there's a lot on the one. And then as you move it up, it's going to start combining them together, essentially. So then your next section is the way

[6:42] that you look at the chart. Okay my main one okay so some of these are really really similar they just have like kind of fancy names so your delta ladder your delta cluster your volume cluster they're all essentially the same thing here it's kind of just like different ways to view the same information essentially if that makes sense so when we're looking here at the delta cluster and i am going to have to move this up because it's just so hard to view with one. But here we can see on the two hour chart that here is a Delta cluster again.

[7:11] So it does kind of somewhat require a live stream of what, you know, a full introduction to Delta, which I'm going to get to. But nevertheless, this is essentially, you know, clustering our Delta candles of market buys, market sells. clustering our delta candles of uh market buys market sales okay so it's going to say uh the way that i've got this color coded is green is obviously signifying buys okay and uh red or this orangey sort of color is signifying like dark cells the light colors are where there's

[7:37] less delta so the delta is relatively low so if you zoom out and we'll just put this to like a for simple sake like four hour chart and uh i can i can highlight some of the trades that i've made recently and i literally just based it off of this and how perfectly amazing it was i guess we'll go over this one here so as we came up yesterday to the top of the channel we come into this high delta cluster okay so high delta high volume essentially the same here but what we could see is there was previously, you know, a lot of high delta.

[8:06] You can see because of the way it's highlighted. OK, it's really dark. So you can just get that initial viewpoint of it's dark. But then when you zoom in here, you can see at this level you had 11 million buys, 6.4 million, 8.4, sorry, sales. And then you can see the buys to the right. OK, so 8.4 million, 7.5. So there was just like a lot of, you know, this is just signifying the delta here, so 8.8 million market buys, okay, versus 11 point, just 11 million market sales at this price, okay, so that's your delta cluster, and it's very, very

[8:41] similar to the volume cluster, I'm going to turn it on on and you're going to see you see when i put on the volume cluster it's just highlighting um in a different way uh instead of showing you the delta like highlighting market buys and market sells this is just going to go really dark where there's just a general big cluster of volume and light where there's no volume so this is really helpful and what i funnily enough i found this out with mike uh was when a pretty helpful thing here is when you put on the value area okay and you change these settings to uh

[9:13] opacity wasn't it uh the opacity trying to just see it here uh volume uh oh yes i think this is what we were changing was it uh we were changing this around yeah so you see like when you put this to like a lower value because i didn't realize this until a few days ago and i think this is really helpful like when you change this to a low value you can then see how the value area is really highlighted and the outer value is is really invisible so then it's only going to low it's only going to

[9:45] leave you with like high value volume areas and then what i like to do on top of this is turn up the opacity really high and then for example see really visually and really quickly if something's really white well it's not going to be a level that i'm necessarily so interested in because i can say well this was the value area this candle but there was little volume in it because it's not it's really light isn't it whereas this this candle here i can say okay so this candle had a lot of volume because it's it's dark it's just

[10:15] visually straight away you know if you like this sort of thing then it's visually just straight away you can zoom out and say okay there was a lot of volume within this i could expect reactions off of this level the next time it's retested sort of thing because there was a lot of volume within this i could expect reactions off of this level the next time it's retested sort of thing because there was a lot of volume in that candle and what you could say is okay well this candle didn't you know in comparison contain as much volume

[10:33] because of the fact it's just light okay and to adjust the amount of um whether you see you know it's all under this shade setting so for example the more you turn it down okay the more that the what more of the clusters are going to turn really dark okay because you're using a different mechanic of different calculation to work out um the high or low volume essentially okay then you have your standard volume profiles again that requires a live stream if we're honest uh volume profiles then the delta profiles again these are really really really similar again this is just simply highlighting your volume whereas this

[11:09] is going to do you know dark for like heavy areas of cells blue for heavy heavy areas of bi on it and i say heavy but that's the difference okay between volume it's generally just going to be like one color your value areas okay value high value low point of control this is out of value this is out of value and uh the delta profile exactly the same very very similar anyway but uh you know a little bit better in my opinion that you can see okay that was a high for example you go back and

[11:37] say okay so this was a high um you know this was our naked point of control but was it a naked point of control of buys or was it of sales okay at this level where we're coming up to the value area i was this component was this comprised of a lot of market buys a lot of market sales obviously that information is only beneficial so it's only it's only going to help you to know that so i personally do prefer to look at delta profiles the market profiles because they're giving me the same information but even

[12:04] more information if that makes sense moving on you have the text okay so within here i generally like to look at imbalances so i just put it back to my default so i like to have this on imbalances again how are we using balances it's a really big topic so it's not really able to explain in 10 minutes it requires a whole hour but the way that we essentially look at imbalances okay it's not really able to explain in 10 minutes it requires a whole hour but the way that we essentially look at imbalances okay it's diagonally and it's going to use multiples i think i have

[12:30] mine on default 300 yep so i use mine at 300 which is the bid bid ask you know when you're looking at it uh zigzags um i like to have mine on 300 so it's only really giving me like big imbalances, essentially. A multiple to 300 for an imbalance.

[12:50] But on here, you can have it just simply the bid ask.

[12:55] You can have it on delta, so it's literally going to give you, of this price, the delta.

[13:00] So here we can see on this point of control, for example, there was a total of this price.

[13:05] We are on six ticks, but within the six ticks, there was a total of negative 1.8 million delta whereas in the total of this candle there was plus positive 2 million delta so essentially this is just telling us here was 2 million market buys if in the whole candle there was 2 million more market buys and at this price there was 1.8 million more market sales okay that's delta oh sorry that's your delta then obviously you just have the volume so just simply adding it together 9.9 million in here 9.9 million in here so you can see

[13:31] that this is pretty helpful to know where there's a lot of volume okay and or you can just have it on none i don't know why you'd have it on none well i guess if you want your charts really clean you could have it on none but uh i guess the most helpful for me uh imbalance is just pretty nice because you have everything that you need you have the imbalances and you have the information within it so i say imbalance is is kind of nice as long as you don't get like ocd or something where it just becomes too much then you might just want to move it down

[13:57] to volume because it keeps it very simple moving on to the candle here i i personally like to have mine on none but uh this is all 100 personal preference okay because yeah this section is personal okay you can just obviously have it on a mono candle where you can really i'm not sure how well you can see it but it adds a candle to uh the surroundings okay so i don't even use mono box not even sure what mono box is i think there's a box around this um here you can just see the color candle. So you can kind of just see where the candle is. I'm always looking at trading viewer

[14:29] simultaneously, so I don't really know. I guess if I wanted to use this, I would use the open, high, low and close. So you can see then, OK, we opened here. We closed here. There was a wick and that's where the wick is. So, yeah, I guess open, high, low, open, high, low, close is an acceptable one. Or you could just see the open and the close. Again, that's not going to show the wick, but it's obvious where the wick would be. Or the high and the low. Don't know why you'd want that, to be honest. I just don't see the point in that.

[14:55] So I guess if you want to use anything, the open, high, low and close is the one to go to. Then showing you within this sort of volume candle so there's your volume then you can see how much of it was buys how much of it was sells essentially again i don't don't really use that but um some people will find that helpful okay rect rect is showing you where liquidations are so this can be helpful if you're trying to track open interest for example it could be helpful to know where the liquidations are so the way this works

[15:26] is the bigger the circle the more liquidations there were at that level it doesn't say exactly on this that how much liquidations there were you'd have to use other software for that but um you know for just a quick viewpoint you could say okay there was a lot of liquidations in this candle green signifying uh shorts getting liquidated and red actually signifying longs getting liquidated so what i could imagine is on this candle a lot of shorts were liquidated okay so i'd want to be looking at that okay a lot of shorts got liquidated on this rise what's the

[15:56] open interest like was intro open interest going down or up really is the only thing i'd care about but i don't use the rect okay i don't don't see the need of it because i can get that information without needing to see this green circle but that could be helpful for for somebody here next you have the footprint bar so this is what i do like i do have mine customized slightly this isn't going to be default so on the default this is your default view okay so the top bar is the total volume so here is put it to the daily so today we've had 2.29 million billion

[16:25] volume okay of that we've seen 61 million delta okay so 61 million market buys here we can see the cvd which is i believe on food okay on session i don't really like to use session for this uh we can see the cvd at 395 million for today uh the point of control of today is coming in at 9257 and the h and l so the high uh the high to the low difference not 492 dollars okay so quite a big difference okay um yeah that that's so i i don't have ordered this on in my personal like custom of customized

[17:03] so i don't need to know all of that information if i'm honest but um that's what each of these rows refer to here and that's what each of those are and what trade size is is it's essentially telling you where you you can get a quick view of where bigger accounts are positioning and where small accounts are positioning so sometimes you might use this for looking for discrepancies, okay? So if you see on the top row, like 1 million plus, and let's say all the 1 million orders, and you're seeing really, you know, loads of really big loads of orders,

[17:35] or sales of 1 million plus, and you've moved up and all the volumes come from like less than 20,000 or like less than 100,000 contracts, so small size. And then what you could say is, okay, prices, let's just give a random example, prices are moving up and larger traders are positioning to the short side and we are seeing all retail or smaller accounts positioning to the long side. You might think to yourself, okay, small accounts are going long, all the big accounts are going short,

[18:06] and one would generally say, follow the big money. Again, this is definitely not tradable. You cannot just trade off of what's happening there, because I'll tell you for a fact, a lot of big traders have no idea what they're doing, and they could be pumping in money from, they definitely didn't make it trading, I'll tell you that. So you don't just want to blindly follow what some big accounts are potentially doing here because many times they're going to get it wrong so um yeah never trade off of this

[18:34] but it can be useful uh again i'll be honest i don't actually use this too much but i know some people don't do and they're like it's really good so uh yeah this is essentially what it is so you can see where big positions are um basic positioning and then where smaller retail is positioning and you can look for discrepancies look for uh maybe actionable information i personally don't use it um too much delta bars i do like the delta bars i have mine done in a customizable way

[18:59] uh so the default delta bars i don't find overly useful um but essentially yeah this is just going to show you like a little bit more visually uh you know here we could see high delta in this okay so maybe on this this six hour chart there was uh 124 positive delta okay and on this following one we saw negative 48 million delta i have mine customized it's not the same view but that's what the default is going to be there give you a nice visual representation and as you know when you break a high you want to

[19:30] see increases in delta so this is healthy okay so this was a healthy move to the upside so to speak again it wasn't the overall high but nevertheless upon taking this high okay you want to see the delta going above this and you can see we moved above that price on an increase in delta. So one could say that this is healthy, this is positive and that's what you wanna see. Again, just visually looking at that quickly is what you get. Open interest, we all know what this one is, open interest, total amount of contracts open here,

[19:58] very, very simple. Next you look at net longs. So what we can see is this is net longs. Okay, so just simply how many longs are open here and net shorts how many shorts are opening or closing then in terms of my templates these are ones i will upload for you all okay and the way that i actually use this in detail i'm going to like do a live stream dedicated to it but just so you have a really like an overview uh this is where i'll be looking at my footprint charts and my default view is on the delta cluster um i can go over some trade examples

[20:31] that i used on this uh when i get to the bitcoin trades because i i use this um and here is my my custom delta bars screen so you might want to just pause and look at this okay i'll make a print screen but essentially it's the cvd line so i like i like to have it in the line you know when i'm looking for like cvd divergences okay i want to have my um yeah that's how we can look at like divergences okay um again um yeah that's a little bit of a custom setting, but it's not.

[21:09] I'll do a live stream exactly how this is used. But yeah, this is really, really helpful.

[21:15] And then at the bottom, I just simply have volume, delta and CVD, which is a running total of CVD, not session volume. OK. You know, when I give you the template order, this will be in there already. But there's the settings for that. So moving on, I have open interest uh settings and i would never use this on such a high time frame so for example if we come down to the one hour okay what i love about this is i can see the open interest here open interest going up okay that on its own is not really

[21:36] helpful because is it new longs is it new shorts well on x-ray charts going to tell you we've got an increase in open interest with new longs opening shorts staying sideways okay so that that is is healthy if we're honest here that that's how that's what you want to see increases in open interest increases in longs as price goes up okay if price was going up and the net longs were going down and net shorts were somehow well i guess net shorts were going down as well it's not so healthy is it it's kind of like a not a healthy uptrend whereas this is a healthy uptrend we're seeing net longs increase

[22:08] and open interest increase really really good the next one that i have is marked this one's a bit crazy but if i want to just quickly see what everything was marked at then i'll just quickly switch on this and just get the statistics that i want to look at really quickly again it's going to tell me everything i don't need to do any of the work it's just so quick okay so that's um that is the marker setting that i have again i'll put on this template here i have the trade statistics but again i don't

[22:36] really refer to this so often i'm more interested in just here more more interesting you know if i really want to quickly see what what the weekly value area low is i straight away know it's six thousand eight hundred and eighty nine dollars okay moving on i have the daily so this is a volume profile on the daily where i can be seeing my naked pointer controls okay naked pointer controls there um and basically a volume profile of the daily i would like this if i could combine but can't do that at the moment but yeah that's

[23:05] the daily um volume profile then i have a weekly volume profile exactly the same like to use this for my naked pointer controls i know get uh you know get some information of what our distribution is looking like moving on i have just very simply a monthly view of the same thing uh so again we could say that the next monthly resistance is 9 740 okay so 9 740 and obviously you have support down here at 6860 sort of thing so yeah that's exo charts those are all the settings within exo charts and that's how i have it set up exo charts uh in the streams over

[23:42] the next few weeks i'm going to go heavy into delta how you use delta what delta is how i trade off the delta then i guess cvd will be the next one because cvd is as you will notice like an amazingly high just insane hit rate that you have uh trading off of it so um yeah then we'll go into like more detailed volume profiles dpos uh you know how you use the imbalances uh you know, how you actually read footprint charts correctly. Because obviously you can just simply look at it like this, but then how you can see more information within a footprint chart.

[24:12] So whether, you know, whether a trend is healthy or not, what you're, you know, how, well, DOM is not so easy. DOM is, sorry, I meant to say DOM is more easy. It's really simply just where orders are stacked. So that doesn't require a live stream. I think everyone understands that. And then obviously the VPO, the volume profile on the left is uh we've done a live stream on this you'll know what that is as well um so yeah that's uh exo and we'll be going into more detail of this in the coming weeks okay so but that's the introduction if you have any

[24:39] questions on that interior in that intro to the website uh yeah just let me know and i'll get to that
