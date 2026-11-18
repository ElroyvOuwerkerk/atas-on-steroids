---
date: course-lesson
champ: unattributed
course: Beginner Strategies
lesson_no: 3
title: CCSS Strategy
slug: CCSS-strategy
category: courses/beginner-strategies
cues: 330
words: 6093
duration: 27:13
source: https://portal.chartchampions.com/lesson/CCSS-strategy
captured: 2026-11-16
---

# CCSS Strategy

## Portal Summary & Highlights

This session introduces a counter-trend scalping approach called the Chart Champion Sculpting (CCS) strategy, built around Bollinger Bands, three exponential moving averages (8, 12, and 26 EMA), Elliott Waves, horizontal levels, and Fibonacci retracements on the 3-minute or 5-minute chart. We learn how to identify entries when price closes outside the Bollinger Bands, how to structure take-profit targets using the EMAs, how to manage stop losses, and how to filter trades with higher-timeframe confluence such as monthly support, divergences, and volatility conditions.

### Highlights

- [00:00] Introduction to the CCS (Chart Champion Sculpting) strategy, built from Bollinger Bands, EMAs, Elliott Waves, horizontal levels, and Fibonacci retracements. It is a high-frequency scalping method, not suited for swing traders.
- [00:36] Timeframe selection:
  - Use the 3-minute chart for average volatility.
  - Switch to the 5-minute chart during extreme volatility to reduce false signals.
- [00:47] Core setup logic Ñ this is a counter-trend strategy:
  - When price closes outside the lower Bollinger Band, look for a long.
  - When price closes outside the upper Bollinger Band, look for a short.
  - Targets are the 8, 12, and 26 EMAs.
- [01:26] Expectations on win rate: no strategy wins 100% of the time. Emphasis on maintaining a favorable risk-to-reward ratio so that winners outweigh losers even at modest win rates.
- [02:12] Trade management rule: if a sculpt position has not moved in the intended direction after one hour, close it manually as it no longer fits criteria.
- [02:25] Top-down analysis workflow before scalping:
  - Mark monthly, weekly, and daily horizontal levels.
  - Add a higher-timeframe Elliott Wave count (or simply note the trend via higher highs/lows).
  - Set price movement percentage alerts and RSI alerts.
  - Add Fibonacci levels from relevant swings.
- [03:02] Volatility filter: this strategy performs best in high-volatility conditions (e.g., 5Ð10% daily moves). Avoid trading when daily range is very narrow.
- [04:08] Risk warning: this is a high-risk counter-trend scalping strategy. Paper trading is strongly recommended before using real capital. Discretion is required Ñ not every setup should be taken.
- [05:40] Entry mechanics step-by-step:
  - Wait for a candle to close outside the Bollinger Band.
  - Place a limit order at the candle body close (not the wick).
  - Use Japanese candlesticks for accurate readings.
- [06:52] Exit mechanics:
  - Scale out 33% at the 8 EMA, 33% at the 12 EMA, 33% at the 26 EMA.
  - After the 8 EMA is hit, move stop loss to break-even.
- [07:14] Stop loss sizing based on volatility:
  - Low volatility: ~0.2% stop targeting ~0.5% gains.
  - Normal/high volatility: ~1% stop targeting approximately 2% to the 26 EMA.
  - Maintain at least a 2:1 risk-to-reward.
  - Use nearby Fib levels or support/resistance to refine stops when available.
- [09:35] Filtering invalid setups: a candle closing with only a wick above/below the Bollinger Band (not a true body close) should not be traded.
- [12:25] Discretion on long lower wicks: if a signal candle has a very long wick, the required stop loss becomes too wide for the limited upside, making the risk-to-reward unfavorable.
- [13:37] Precision tip for limit entries: place the limit a few cents above the candle close to improve the probability of being filled on the following candle.
- [14:49] Trade frequency: the strategy can involve many simultaneous charts (e.g., monitoring multiple assets), which is how a high number of daily trades is reached.
- [17:28] Reinforcement that consecutive losses are normal. The focus should remain on consistent execution and letting overall expectancy play out.
- [18:33] Adding confluence for stronger setups:
  - Look for bullish or bearish divergences on RSI and MACD between the current and prior swing points.
  - Confluence increases the probability of a successful counter-trend entry.
- [19:56] Highest-conviction setup: a Bollinger Band close that aligns with a monthly support/resistance level (and ideally a Fib level). In such cases, position size can be increased due to stronger confluence.
- [22:00] Discretionary filtering Ñ not every Bollinger Band close is a valid trade. Context, structure, and market behavior matter. This skill develops with time and screen experience.
- [24:33] Avoiding trades against strong momentum:
  - Do not short when BTC is breaking through major resistance with strength.
  - Do not long when price is breaking major support with strength.
  - If attempting to fade, wait for extreme RSI readings (e.g., single digits or 90s) combined with divergences.
- [25:26] Advanced execution note on soft stop losses: the practice of using mental stops with alerts requires full attention, strict discipline, and immediate exit when the invalidation level is reached. Beginners are advised to use hard stop losses until experience is built.

We encourage reviewing these concepts carefully, backtesting and paper trading the setup thoroughly, and applying strict risk management before considering live execution.

## Transcript

[0:00] my sculpting strategy okay i felt like i had to put a name to it so i've called it the ccs it stands for chart champion sculpting ccs this was a strategy that i made myself so yeah you probably never seen this talk before so it's based around bollinger bands exponential moving average elliott waves horizontals and fibs basically it's a sculpting strategy so if you're a swing trader it's probably not for you as i have wrote here in an average day i can easily be taking between 50 and 100 trades so this is really

[0:31] time consuming but yeah it's on the three minute or the five minute chart depending on volatility if there is extreme volatility i will use the five minute chart just because the three minute chart you might get a bit too many false signals but yeah basically you're looking for price to close outside of the bollinger band obviously on that three or five minute time frame okay and when it closes so it's a counter trades trending strategy okay so you are trading against the trend if price is going down you're looking for longs and if

[0:57] price is going up you're looking for shorts okay so you are playing against the trend so that that's a little bit advanced i would say not many people do this um but yeah it's a counter trend strategy okay and likewise when price closes outside of the bollinger bands to the top i will look for a short and target the 8 12 and 26 exponential moving averages this is a script that you've probably all seen before and this is i've released this script ages ago on on trading fee for free so uh as with

[1:26] every strategy there are losses to be had you know obviously this is not going to win 100% of the time uh that would be crazy to think that it would okay so as right here sometimes it will take several losses in a row okay uh but then you get several wins in a row and obviously the winners as with any strategy the winners should always be bigger than the losses so let's say that you end a 50% win rate, but every win you've been winning $10 and every loss you've been losing $5.

[1:50] Well, 50% you obviously end with money. OK, so, yeah, just so you're aware, you're not going to win every trade. There's no sort of a magic recipe. But, yeah, to trade this, you need time. That's the main thing for me. Like in an average day, as you know, 10, you know, a of lot of trades can be taken some of those last a few minutes others up to an hour uh what i've put in here is then when i would close manually if i enter a sculpt trade and one hour later the price has not moved i will manually close out of that trade because it's uh

[2:20] not fit my criteria i don't want to be looking at it any longer so i would just close out of that trade so this is the way that i do it so it's always starting with the top down yes this is a sculpting strategy but you do need to do your technical analysis starting with the monthly you have to do your work look at the bigger picture before i start sculpting okay every single time okay so adding horizontal support and resistances you know that's fairly simple going through those monthly weekly daily levels okay add an overall high time frame elliott wave count if you do not know how to do

[2:49] elliott waves at this point which is fine then just look at the trend okay elliott waves is basically just following the trend just say is it making higher highs and higher lows adding the moving up and down percentages this for me is really really important so the moving up and moving down percentage that's a really important alert for me to have okay because i prefer to do this with volatility so if you're seeing bitcoin move 0.0.5 percent on the day i wouldn't say it's a good sculpting environment

[3:14] okay you want to see bitcoin with like a five percent move for example uh where there's big volatility five ten percent you know great numbers like that real big volatility that is when this is just amazing just like so i loved it in 2018 because the average day was like a 10 move it was just so good uh obviously you don't get that every single day but when it comes yeah i would i would spend 12 hours just trading so it was great but uh yeah so the moving up and moving down percentage

[3:40] is a must for me uh the crossing which just means price crossing a certain level put that on your levels okay and rsi alerts which is obviously moving up or down adding fib and actually levels okay so fib and actually levels i also like to have on the chart okay so you know that's just adding in fibs from a whole range of environments that would be the overall low to overall high the move that you're actually on low to high or high to low depending if you're going up and down i would always have a fib level in there as well this is a counter trend trading strategy scoping strategy high risk okay

[4:08] after i've explained how to do it today i would say you're best to paper trade this before you just jump in and start using real money i would yeah really really really say paper trade this this is not a mechanical strategy where i will take 100 of the setups that close outside of the Bollinger Bands. There is discretion to be had. So I guess it would make sense to start, you know, off here, you know, for example, drawing your daily levels, okay? Drawing your daily, drawing your monthly, drawing your weekly levels.

[4:36] So add in your horizontal levels, okay? This is a really, really, really big important level, okay, to be aware of on the chart. So make sure you always start with your top-down view of the chart, okay? And then that's when you can then add in your Fib levels, okay? That's when you can then start adding in like an overall Elliott Wave Count, just so you have a bit more of a aware of what, you know, is the price more likely to be moving down or up in that day, okay? If it's more likely to be moving down here, let's say,

[5:04] then where is a strong area of support okay we could we could argue 70 for example this is basically what we're looking for and it's again this is on the three minute time frame okay so i like to do on the three or five minute time frame you can alternate see what works best for you but uh yeah for me if there's an extreme volatility i'll use five and if there's like average volatility then i'll use three minute uh just because uh if there's any, you know, if you've got little, really, really, really small volatility,

[5:30] you know, the moves are, like, so small, you'd be scalping for, like, 0.5% gains, and you can do it, but it's just, yeah, for me, I just don't, okay, I just don't, I just would prefer to just not trade on that day, if the volatility is so low, so this is, like if the volatility is so low so this is like the core okay so this is uh forgetting everything else this is like the core basics okay here you can see where price was coming down okay and then it closes here outside of the bollinger band okay

[5:55] so when it closes outside of the bollinger band on the three minute chart hopefully you'll have some volatility then you will enter a long okay so you see it close you enter a long okay a long limit position at the close of that candle so let's say that it closed at $12.40 you will enter a limit at $12.40 okay and then on the next candle you obviously would hope that it drops to $12 let's say 39 cents and that obviously fills your long position. Okay. So you would look to long at the close of this candles and then your target. Okay. And instead of the wick,

[6:31] I will do it on the candle close. Okay. So the candle close, let's say this, the close of this candle was $112.40 and the wick went down to $112.35, okay, so I'd enter the limit at the candle body close, not the wick, okay, so you do have to use Japanese candlesticks for this strategy, and then the target is simply hitting the 8 exponential moving average, hitting the 12 exponential moving average, and hitting the 26 exponential moving average, you will take 33% of profit on the first, 33% of profit on the second,

[7:03] 33% of profit on the third, okay, of profit on the second, 33% of profit on the third, okay, equally enclosing out the position when it hits 26. When it hits the eight exponential moving average, you will move your stop loss, which is just going to be a standard stop loss, depending on the volatility, okay? Because if you're aiming for like a 0.5% gain, then your stop loss is going to be a 0.2% stop loss, this is what i do okay so if there's real little volatility then that's something you can do you use a 0.2 percent

[7:28] stop loss standard 0.5 percent gains okay if you're trading with a bit more volatility now i will increase it to a one percent stop loss and then obviously your you know whatever this is to the 26 exponential moving average at the time let's just say for for sake of arguments two percent okay you're always keeping that at least two to one risk to reward uh but it's obviously because the way that when it comes and closes here you just have to give yourself like a standard stop loss okay um unless you've

[7:54] got a level nearby which then obviously aids you a lot okay so for example if we saw here oh here's the the 618 fib then we can start to use that as as a stop loss as well but just standard trading when you're in the middle of a range for example yeah you would just literally place a standard one percent stop loss so as you can see here price would have come down we'd have entered our limit order at the candle okay at a candle wick let's say we get filled on the next candle price comes

[8:18] up to the eight as price comes up to the eight exponential moving average and hits your first take profit that one percent stop loss that you had you move to break even okay so you move it up straight away that way if you hit the eight and you fall back down you end with a little bit of profit because you obviously hit take profit one and then got stopped out break even so you ended with that little bit of profit taken okay so as you can see here we would have hit eight exponential moving average

[8:42] moved our stop loss up on the following following calendar, we hit the 12. Great, we've hit 66% of profit taken, and then we get stopped out. But we get stopped out at break-even, having taken 66% of profit. Brilliant, no? And then we come back down for a second trade here. Price closes outside of the Bollinger Band. We get filled on the next wick. Great, we're in another long position. Price comes up here, hits the... Look, it takes a little bit of time so you have to remember this is uh taking what 15 minutes and then price hits the eight exponential

[9:09] moving average great we take a bit of profit here 33 move stop loss up to break even then it gaps up but hits uh the 26 exponential moving average in the end you come all the way up to the top of the bollinger band uh but we're just aiming for the 26 here um so then you close out 26 and that would have been a whole profitable trade so for example here you get to take profits then stopped out okay still a winning trade here you take the whole of the trade of a win then you come up to the top of the bollinger band this for me i wouldn't have entered a short because that

[9:39] for me is not closing outside of the bollinger band fully i want to see like this candle close this candle close this is more of a wick above so i wouldn't have shorted here but you can see if you had shorted here it would have obviously been a profitable trade because you could have shorted here and actually took profits but that wouldn't have been one that i've taken because the close is not actually for me outside of the bollinger band here but another long here for example where you close outside of

[10:01] the bollinger band clearly closing outside and, give yourself the stop loss, and then you can see hitting a 26 exponential moving average. So that's the first slide. Okay, then you can just see here. Oh, that is obviously moving down. That would help. Okay, so just more examples. I'm just going to go through these next few. I just had a few more examples. So you can see here, price closing outside. Enter a limit order at the candle close okay target the 26 that would be where you could close out with your long trade

[10:31] okay then you obviously you're looking for it to come back down and enter another long or for example here coming up to the top of the bollinger band and uh you know here you could enter another into a short position target the 26 here you come back down, close outside again, enter a long, target the 26, here you come outside, close outside again for a short, target the 26, okay, so the base strategy is literally just with EMAs and Bollinger Bands, where, you know, this is nice,

[10:58] okay, but it does work better when you start to add in, as mentioned earlier, those other factors, but yeah, we'll get to those slides in a minute but this is just the base strategy okay and moving down okay so as you can see here long outside long outside of the bondage band stop-loss here so this is one for example where you wouldn't have actually got your long field because as you can see here okay you would have entered a long position here okay but you can see that the next candle is just a straight upwards move. There's you wouldn't have got a long position filled. Okay, because let's say that candle closed here 11,000 11,350 and you enter your long position 11,350 and price just moves up straight away. Obviously, that doesn't fill your long and that happens sometimes. Okay, it's just yeah, that yeah that just happens so in that example you obviously just do not get a position filled

[11:48] no worries here though you know this is one way obviously you would have got a position filled because here you obviously can see your price closes outside you enter your short here and then obviously on the following calendar you get your filled on your short and as you can see price very nicely actually came down to the 26 and in the end yeah took out the the low here and as you can see price very nicely actually came down to the 26 and in the end yeah took out the the low here and as you'll know from today we're down at in the 10ks now so um or higher high 10k

[12:11] is better so yeah that's obviously um what you want to see to obviously get your positions filled uh a little follow-up rejection on the next candle uh that's that that would be perfect okay and then straight up rejection uh here this would have been a problem for me so this is one where you know a little bit of discretion is needed if you see such a long lower wick is that a good trade i would say no because your stop loss is below that wick remember so you'd have to in this example place your stop loss below the wick with such a small gain

[12:39] it's obviously not worth the trade the rest of the award would be really really bad although after a long wick taken out that low you have a high probability of hitting that it's still not worth it really in the grand scheme of things it's not worth it because although the high probability of is if hitting there the gain is so small for the potential loss it's just not a trade you would take close on here with long lower wick uh is not a trick yeah i wrote it i forgot i wrote this but

[13:03] yeah i did so uh here long lower wick is not a trade I would take as the stop loss would be too big. Okay, because you have to remember your stop losses would be below that wick for the small gain. It just wouldn't be worth your time because the loss is too big for the win. Okay. So this is now just enforcing what I've already explained here. So you can see here, price closes outside of the Bollinger Bands on the three minute chart. Okay. So step number one is enter a limit order at the price of where the Bollinger Band closes, sorry, of where the candle

[13:37] closes or a few cents above. What I mean by this is let's say the candle, let's say you get that wick down to $8. Okay, but the candle closes at $10, okay, if that candle closes at $10, I might enter my limit order, let's say it's started to move back up on the next candle, and I might enter that limit order at $10.02, okay, so slightly above that candle close, just to try and get filled on the next order, just buy like a cent or two, depending on what you're trading obviously i would uh not always

[14:05] go just exactly on the candle close but just a few cents above you know if you're on bitmex like 0.5 of a cent okay uh standard stop loss of one percent 0.5 percent or 0.2 percent depending on the the dollar range of the coin you are scoping okay and you target the 8 12 26 exponential moving averages to close out of the trade once one target has been hit so the a exponential moving average moves stop loss up to entry that way if you hit the eight and get rejected uh you you have a winning trade and you don't lose any

[14:35] money and this was a funny one so yeah this this was going through uh litecoin and as you can see for example so here this is just showing you how many literally trades you can take in a day so here for example you would be and this is just on one coin remember like i'm doing this obviously when i've got my eight on the screen so i'll be like trading eight coins generally at a time uh and you know if you're taking like 10 trades here and then you've got eight other coins going on that you're trading at the same time that that's how you take so many trades

[15:01] because you're doing it across more than just one coin at a time obviously that that takes practice and experience. But, you know, for example, here, enter along when you close outside the Bollinger Bands. Here, you hit the 26 exponential moving average winning trade, okay? Where's the next trade? Okay, here, so this is where, okay, so this is where there's a bit of discrepancy, and we'll have to go through the charts in a minute, okay? Because you can see here, you actually'll have to go through the charts in a minute, okay?

[15:29] Because you can see here, you actually close outside of the Bollinger Band here, and then you move down.

[15:33] I don't know how much this is in terms of percentage.

[15:36] If this was obviously more than 1%, then you would have got stopped out of the trades. But I would take two trades in a row, and sometimes even three trades. So let's say, for example, here, okay? So you take one losing trade where you well actually this isn't a losing trade because you hit uh i think it's better i just go for a live chart at the end of this um and we'll ignore these losing trades in the middle of it because obviously this would could potentially be a losing trade and then you get your winning trades okay then the next

[16:00] close outside is here so this is obviously a winning trade winning shorts that you trade down to here let's say then you hit two take profits you hit potentially a losing trade here coming down to your next trade uh next trade i think you'll get the idea okay of how it you know how you can take so many trades where there's a lot of closes outside each time okay um yeah i'd like to go through a live chart basically at the end and just go through this on a on an actual live chart uh where the strategy loses so yeah this is like

[16:31] where i just want to highlight some places where you can get losses okay so obviously here opening along i think this was a zoomed in view of this chart while i was just trying to explain so for example there where you close outside of the boollinger Band, okay, you would have had to have fast reactions, but you do hit, obviously, in the coming candle, the 8 and 12 exponential moving average. So, if you were quick, you could have got out. But if not, then, obviously, you hit the 8 here.

[16:56] But let's just say, pretend we don't hit anything, and it's just a losing trade. So, you know, sometimes you're going to close outside of the Bollinger Band, and you just continue moving down, okay? It happens, okay? So, obviously, opening along here, let's's say would have just stopped you out okay taking the next trade though three minutes later would have present would have presented a win so then you got a loss followed by a win okay here uh if you had opened a long you obviously never even hit take

[17:18] profit one and you get stopped out on the next candle so uh yeah just be aware um yeah you you are going to get losses okay a hundred percent you are 100 going to get losses um and that's just part of trading okay you're never ever going to win every single trade um so yeah this is another example of some losses where here so you see here you close outside of the bollinger band you got filled on your short on this candle like this is a perfect one like you just filled get you know you get filled hit target okay then you start to get some wicks okay and then let's say

[17:50] here you could have potentially taken three losses in a row what happens you take a long here you get stopped out on this candle let's say you take a second trade where you enter your long here you actually do hit take profit two but nevertheless uh we'll just say it's a losing trade here let's say you take another long you actually hit no take profits you get stopped out and uh this thing could be say you're successful long so let's just say you know sometimes you're going to go win three losses in a row then a win um you know you're not going to consistently just

[18:18] get win win win win win win win i just really want to enforce and just make you all aware that it's not going to be a perfect perfect strategy okay uh but overall you should that you should be ending with uh more money than you lose okay um so yeah this is where i like to add in some complexity okay so for example along here if you want to add complexity to this strategy this is where you can look at the divergences on the rsr and the macd okay so this is a bit of a you know this is what you could be looking for so let's say you close outside of the bollinger band here

[18:49] okay so this is would have been a winning trade but what you could be looking for at the same time is from this low to this low does the rsi form bullish divergences okay so this is obviously three minute chart so you want to see this moving down heavily not really sideways so this is obviously three minute chart. So you want to see this moving down heavily, not really sideways. So this is where volatility comes into play. But let's say that you get a strong movement down. You close outside of the Bollinger Band from the last low. Have you made a low with bullish divergences?

[19:15] If so, well, that's adding more reason to take the long. OK, trading against the trend. If you've got you know, if you can start adding in more confluence. OK, so if you can look at the last low to this low, have you closed down here with some bullish divergences on the RSI or potentially the MACD? Obviously, this isn't a great example photo, but this was just a random live one from yesterday. But yeah, have you seen bullish divergences on the RSI and the MACD? Obviously, it goes without saying if you're trading this on Coinbase or a binance alt coin you obviously cannot short so you'd only be able to look for longs um yeah that obviously is

[19:51] kind of obvious i guess but yeah if you're trading this on an alt coin on binance or just trading this on coinbase um yeah you um you just wouldn't be able to short okay so then you're only looking for longs okay so long taken so this is like this is where like my perfect sort of trade would be when you close outside of the bollinger band so let's say you close outside of the bollinger band here on top of a monthly level that would be like the most perfect confluence that i've like could

[20:19] imagine okay like a monthly with closing outside the bollinger band that's one where i would enter that as a sculpt but let that run with a big size because that for me is like that's where i want to enter my biggest trades because that for me is the biggest confluence like that's a really big confluence if you get let's say you get a monthly with a with a with a fib well that's obviously the best if you got a fib in there as well but a monthly with the close outside so this was a this

[20:42] was a walton trade uh walton trade, and it was just really, really nice. This monthly level, obviously, 2675. What did you get? You got a sharp move down, close outside of the Bollinger Bands. OK, and look how well it holds the monthly level. So this is where you could say long taken with confluence of monthly support equals increasing the position size. OK, we're increasing the position size because there's more likelihood of it to actually hold here because we're a really strong support and this is why mapping out these supports obviously is

[21:12] going to help you do this strategy because instead of just randomly taking it and thinking don't know what's going on here you have that overall view and you know your important levels okay so yeah that's just something I would note and then obviously i would add into the position size that i would normally trade with because of the confluence that we have with the level i would actually increase the position size than my just standard when i'm aware that we are bouncing off of that level okay um and so um that that moves me on to um

[21:50] going back to my original post of there is an element of um discretion that every single time okay every single time when it closes outside of the bollinger band does that mean a long and the answer is no so every single time it's closing outside so when we go back to this okay this is obviously one that i've done for an example um you know every single time it's closing outside here would i be taking a long every single time like the answer is no there is an element of discretion okay um and this i would say really does come with time

[22:26] you know actually using it um and i'm really getting a feel it's really really hard to describe but like this was just a pure example that i had yesterday so uh this was yesterday literally in the in the champions group chat where it closed outside of the bollinger band where obviously price was sat here i can hardly read ten thousands eleven thousands i don't know i can hardly read this to be honest but yesterday whatever the price was like yesterday about 11k okay and we closed

[22:53] outside of the bollinger band twice in a row obviously yeah you can see here you hit the exponential moving average but as i wrote here uh no bullish divergence yet or support retained personally would not long just yet so you know here is a long opportunity presented from this strategy but i do not take it as i believe it is going to go down and drop a gain in price which it did we're now sat down in the 10k so it has dropped again in price but this is where being selected with the trades it comes with time and

[23:20] experience you know if you just had watched the slides understood the strategy and you start taking every single one then you might find yourself taking trades that i would then say you know if you let's say we were in a room together and we were going through and doing the trading together you might say say in that that that 10 minute session take three trades whereas i might have taken zero trades um and it's it's just like saying it comes with really like getting a connection with a chart i would say it might sound weird but you

[23:49] can like get just like get this connection with a chart and just like really feel whether you believe it's going to go down or up and that might sound really weird i don't know but you do kind of like get it like it's really strange but when you trade something a lot you just really get a feel of whether this is going to actually continue or whether it's going to drop. And this was just a nice example that I just found yesterday while making this slideshow because I may just put this together yesterday.

[24:10] But yeah, this was like an example where, you know, most people are going to be looking to long this. And I just wanted to give an example of, you know, just because it closed here, I just felt it was going to drop again. And obviously it did drop again. So this is as predicted from the post. And our price, in fact, did drop again um and obviously it did drop again so this is um as predicted from the post and our price in fact did drop again okay and that's you know when it comes to being

[24:30] selective with the trades does come with just time and experience when btc is breaking through a big resistance level and moving strongly i would not trade a short strategy like this okay likewise if it's breaking through a massive support level, it is not wise to try and long. OK, when it breaks like this, I prefer to trade with the momentum. If you do want to try and fade a resistance or a support break, wait for RSI to reach extreme levels with divergences. So that's extreme levels, e.g. going into the teens, going into single digits with some bullish or bearish divergences.

[25:03] I would definitely not try and fade a move until i saw at least that so this strategy does require quick thinking and moving to enter a limit order on the candle close to set a stop loss once take profit one is here and then moving it up means full concentration is required and that's like you literally would have to block out everything just focus only on this with this i will trade okay so this is one that i do and this is not one that i recommend for everybody okay so i trade

[25:32] this with no hard stop loss set so eg when i'm entering like a trade and i put in that one percent stop loss in i don't actually place the stop loss i'm really good with just having an alert just above and if it hits it i will just mark it sell the position or mark it by the position depending if it's long or short i don't actually put in that stop loss i will just close out early or when it hits that level okay so when i see price moving to my validation area i will mark it close out of the trade this is because i will be sat closely

[26:00] watching the price um you know and i'm not going to miss anything okay one of my monitor is just sat looking at that trade um one of eight trades potentially but i will be looking at it and it's not one that i'm gonna go to the bathroom a second and and suddenly find myself 10 down you know so although i use a soft stop loss uh you know i'm managing it okay uh but yeah and i was also closed out without a second thought you know if it starts breaking it, okay, but yeah, and I was also closed out without a second's thought,

[26:29] you know, if it starts breaking through where I said, I won't think, I'll hold this a little bit longer, no, I'll just take the loss, and just be done with it, but I do not actually enter that hard stop loss, one, it saves me time, and two, that's just the way that I've progressed with this, like, when I first started, I always entered the manual stop loss, like, 100% of the time, this like when i first started i always entered the manual stop loss like 100% of the time i would always enter the manual stop loss and move it up okay but i'd say i've advanced now where i don't

[26:52] enter that manual stop loss each time i just have the mental stop loss there i know where i'm going to trade i know where i'm going to close the trade in as it hits i close out that adds on a lot of risk um but that's just something i've come to do now i just never enter hard stop losses thank you everybody and um yeah cheers and i will catch you all next week thank you thank you so much
