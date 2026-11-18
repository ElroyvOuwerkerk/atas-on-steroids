---
date: course-lesson
champ: unattributed
course: Orderflow and Footprint
lesson_no: 7
title: Chart Types Part 1
slug: chart-types-part1
category: courses/orderflow-and-footprint
cues: 151
words: 2229
duration: 13:51
source: https://portal.chartchampions.com/lesson/chart-types-part1
captured: 2026-11-16
---

# Chart Types Part 1

## Portal Summary & Highlights

This session introduces alternative chart types available on ExoCharts, focusing on range, range Y, and tick charts. We learn how these chart types help remove market noise by printing new candles based on price movement or transaction count rather than fixed time intervals, which can assist in reading momentum, volatility, and order flow more effectively than traditional timeframe charts.

### Highlights

- [00:00] Introduction to non-time-based chart types on ExoCharts and the concept of moving away from timeframe-based analysis.
- [00:50] Overview of the seven available chart types: point and figure, trend reversal, timeframe, delta, volume, range, and tick charts. Understanding these allows traders to assess momentum without waiting for a candle close on higher timeframes.
- [02:11] Core purpose of alternative chart types: reducing noise. New candles form only after a specified amount of price movement, dollar value, or volume is reached.
- [02:46] Range chart explained:
  - A 6-range chart prints a new candle every 6 ticks (every $3 of movement).
  - One tick equals $0.50; two ticks equal $1.
  - Useful for visualizing price action during consolidation phases.
- [04:04] Visual comparison between a standard chart and a range 60 chart, demonstrating how range charts produce cleaner structure during ranging markets.
- [04:57] Using range charts to detect breakouts from a range, aided by the time-per-candle feature showing how long each candle takes to form.
- [05:25] Range charts for measuring volatility: candles printed in rapid succession indicate increasing volatility.
- [05:39] Combining range bars with volume and delta to identify buying/selling pressure, support/resistance zones, and potential reversals, especially within well-defined channels (upward, downward, or parallel).
- [06:36] Example on a range 34 chart: candles noticeably larger than the standard $17 range size can signal that large orders are being processed.
- [07:18] Range Y chart explained:
  - Measures price movement from the candle open rather than high to low.
  - A new candle only forms when price moves the defined distance from the open point.
  - Results in a cleaner visual by effectively merging smaller fluctuations into single candles.
- [09:18] Tick chart explained: One tick equals one transaction. An 89-tick chart prints a new candle every 89 trades. Important distinction from tick size, which relates to the y-axis price increments.
- [10:34] Tick charts as a visualization closest to order flow, giving a sense of order sizes and momentum simultaneously.
- [11:11] Example of reading tick charts with volume:
  - If 25M of volume prints within only 20 trades on a 230-tick chart (versus an average of 10M), this suggests large players entering.
  - Spreading the same volume across many candles implies smaller participants, making directional moves less reliable.
- [12:28] A compact candle with high delta can signal absorption in the market.
- [12:41] Summary of differences:
  - Range chart: measures high to low.
  - Range Y chart: measures from open to high/low.
  - Tick chart: prints new candle after a set number of transactions.
- [13:14] Closing guidance: take time to study and observe each chart type, test which fits your style, and draw conclusions from personal observation to build intuition.

We encourage reviewing each chart type carefully, practicing their interpretation in live market conditions, and applying them within a tested framework alongside sound risk management.

## Transcript

[0:00] I'm going to cover a part of the different chart types that you can find on ExoCharts and how can you read them. What we're going to work towards is moving away from timeframes. Different chart types can help us traders to make better trades, in my opinion. And the chart types that I will be talking about may very well include chart types you've never seen before. So that's why from this stream, I will provide you with the explanations of what they mean and how you can use them, of course.

[0:34] But do keep in mind that it will probably take some time before you fully understand each and every chart type and also to digest all the new information that's contained within this charts and stream as well. So on ExoCharts we have a selection of helpful chart types that many would have not have seen before. In total there's a seven types of different chart types. They're not necessarily overly difficult to understand but if you are able to use these chart types then it allows you to slowly move away from the timeframe charts where you normally it has been basically common as we've been teaching you guys to wait for, let's say, an hourly close or 50 minutes close.

[1:17] Or if you're a swing trader on a daily or four hour close by using these these chart types, you you can now read and judge momentum, for example, in a variety of different ways with different chart types. And this can actually help you get into a trade at a very early stage as you don't really need to wait for a bigger term timeframe close or for confirmation. And which one you ultimately use is really entirely up to you and ultimately it really boils down to personal preference and and also how well do they

[1:51] fit into your own personal strategy so of all these chart types that that you can see here in this image you can see we have to point figure charts the the the trend reversal charts the the time frame charts, delta charts, volume charts, range charts and tick charts. We're going to cover the range, the range Y and the tick charts. Because if you do understand those, then it will also make it a little bit easier for you to understand the trend reversal chart type.

[2:28] Basically, the reason why we use these types of charts is primarily to remove noise as a new candle will only be created when a certain amount of trades, a certain amount of price movement in dollar value or volume has occurred, rather than let's say every 15 minutes. So this ultimately leaves a chart with less noise and leaves only useful data for you to see. Let's start off with the range chart. As you might have seen in the previous picture, there's a couple of different range charts that you can choose from.

[2:56] You can choose a six range chart. You can choose a 30 range chart.

[3:11] But essentially what does it mean is, let's, for example, a six range chart would create a new candle every six ticks.

[3:16] That is essentially every $3 of price movement, a new candle is going to be printed.

[3:20] And the ticks represent 50 cents. So one tick is 50 cents. Therefore, two ticks is $1. So if you look at a six ticks range chart, then you're really looking in dollar fluctuations of three. So basically divided to two. But let's show some visual examples. This is basically an example that we had today. And you can see that price made a directional move to the upside. In this case, it was above the weekly.

[3:50] And then essentially consolidated the majority of the day within a $30 range.

[3:55] Now on the left, so in this picture, you see a standard view of trading view.

[3:59] And here on the right side, you see a range 60 chart.

[4:04] It shows basically that every $30, a new candle is being created.

[4:10] And this is, for me, visually, first of all, very pleasing because you have much less noise on your chart.

[4:16] And even when price is volatile, within the range, you have a nice, clean chart to look at.

[4:20] And, I mean, you can see that here there's so many candles being printed. It goes up and down, up and down, obviously.

[4:33] Whereas on the right side here, you have only one, two, three, four candles. And this is just, for me, this is very nice to just remove the noise. There's not so much happening, you know, whereas normally maybe you would look at, let's say, at a one-minute chart, or some people even look at one-second charts. Well, with this chart type, it's just totally not necessary because you have to range in view, and it basically cleans up the chart.

[4:57] In addition to that, it also becomes very, very clear when price is actually starting to move outside of the range, as you can also keep track to how much time every candle takes to form because of the new feature of extra charts that it has. So you now also have like this time function. So you can see exactly how long did it take for price to make these kinds of movements. So in that sense, you can use this chart type to, for example, measure breakout of the range. Another way to use the range chart type is what you can use it for is to measure actual volatility.

[5:33] So if you see candles being created in rapid succession of each other, it shows that volatility is picking up. But range bars are also really good for identifying support and resistance areas, so to speak.

[5:54] And just like as in most other chart types, and it also has to do a lot with, let's say, volume and delta coming in,

[6:00] you can determine the buying and selling pressure and foresee a potential reversal coming. And that is where you can use it for in a certain trending market. So this gives you like another way to use this type of chart. There's multiple ways to use it but in my experience at least the way that I use it you get the best results when using this type of chart in a well-defined channel and it can be an upward sloping channel, it can be a downward sloping channel or parallel channel.

[6:27] But this range type, it can really help you determine whether something's stopping out or whether something is about to break up or down. So to clarify that a little bit, as you can see on this picture, this is a range 34 chart.

[6:47] And we see here that on the drop that the candles are much larger than $17 because you know 34 divided by 2 is 17 so every there's always a new candle basically being printed after $17 of price movement and it indicates because you can see that these candles are way, way, way, way, bigger. And just only from this zoomed out visually, you can already see that basically it's an indication that big orders are being processed. Another chart type, which is called the range Y. In a normal chart, range chart, a new candle opens beyond the boundaries of the upper and lower point of a previous bar,

[7:27] and a bar closes in its top or bottom point. I'm going to show you a visual image in a second, so don't worry. A range Y chart is a little bit different than a range chart in the sense that, let's take an example that price is zero. And we have a range Y6 chart open. So price moves up $3 and then it starts a new candle, right? And then price then moves up from that open, moves up $1 and then moves down $4. And it now moves for a total of $5 and then only then a new candle gets printed.

[8:08] So basically the range Y charts calculates from the open, and then measures the movement. So if you have a range 6 chart, if it opens and then, for example, moves up, but if it doesn't move three points up, then it doesn't print a new candle. But if it then instead does come back to the open and then moves three points down, that is when it creates a new candle. And that basically visually results in something similar to this. It's not overly, it's not super differently, it's just a little bit cleaner in my opinion, because for example, here's not super differently. It's just a little bit cleaner, in my opinion.

[8:45] Because, for example, here is a good example. I hope you guys can see my mouse. Here's a good example. And here you can see price, actually.

[8:57] Look at my nose. Yeah, this was a range six chart. And then, basically, after that happened, after after this movement happened it prints a new candle but here you can see for example this was the this was the close and then here you have a new candle starting to print right then it first moved down and then it first moved up so essentially what it did is it moved it basically merged these two candles, so to speak, together. The tick chart. Basically, what it means is one tick.

[9:31] If you're looking at a tick chart, one tick is one transaction.

[9:35] So bear that in mind before we move on to the next slide. It's really important to not confuse this with the tick size here. So what does it mean? If we look at, let's say, an 89 tick chart, it basically means that a new bar is being created after 89 trades. And that's where I arrive at tick charts are different than tick size because the tick, let's say tick times four, is based on the y-axis, right? Which essentially means $2 because one tick is 50 cents. And it essentially means that for tick size four is that everything is being combined in increments of two.

[10:21] A tick chart, on the other hand, so like tick 89, as in chart type, literally just means a new candle after 89 trades. And tick charts, therefore, are a good way to keep the chart clean. And in addition to that, the way that I view tick charts is essentially, personally, I think this is something that the closest to a visualization of the order book you can see the flow of orders and the trades coming in in a way that gives you a sense of the order sizes and momentum all at the same time and for example if i see once again candles being

[10:58] printed in rapid succession it indicates that volatility is picking up. And in combination, for example, with looking at the volume and delta, you can get a sense of bigger players that are entering or moving into the market or exiting out of the market. An example would be if you're looking on, let's say, a tick 230 chart, which basically creates a new candle after 230 trades. Um, if you look at that type of chart with an average volume of let's say 10 million, and then on the next candle, you see volume passing the 25 million mark on its 20th trade

[11:37] already. And this is something that you have to view in the moment. Um, so on this candle, you see volume passing the 25 million mark on its 20th trade. It essentially shows that 25 million of volume was transacted in less than 20 trades. So that indicates that there are big players coming into the market. And on the other hand, if you see that same amount of volume then being printed in let's say three or four or five or maybe even 10 candles, then you can safely assume that there's generally smaller traders at work, more retail traders or not necessarily smaller trades, but smaller position sizes going into play, which then also makes a directional move a little bit less reliable, which I was under the impression

[12:26] of today, for example. Another way that you can also look at it is if you, for example, see a very, very compact candle with a high amount of delta, then that signifies that there's absorption going on. To finalize and to summarize, range chart essentially measures from high to low. Range Y measures from open and then from high to low. So that's the difference. Tick prints a new candle after an X amount of transactions based on what setting you have it set to so an 89 tick chart

[13:07] gives a new candle after 89 trades final words I would say with this kind of with this kind of new knowledge I would say do not rush do take your time um take your time learning and just spend some time observing the charts and um see which chart also you like the best and also what conclusions uh you can pull from your own observations because i think that that is really in my opinion one of the best ways to learn this. And ultimately that will help you get a feel for it.
