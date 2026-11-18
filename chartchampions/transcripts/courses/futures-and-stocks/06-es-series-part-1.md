---
date: course-lesson
champ: unattributed
course: Futures and Stocks
lesson_no: 6
title: ES Series: Part 1
slug: es-series-part-1
category: courses/futures-and-stocks
cues: 421
words: 6299
duration: 41:20
source: https://portal.chartchampions.com/lesson/es-series-part-1
captured: 2026-11-16
---

# ES Series: Part 1

## Portal Summary & Highlights

In this comprehensive guide, we explore the world of proprietary trading firms (prop firms) and how they can provide traders with access to additional capital without risking their own money. We cover the essential aspects of prop firm trading, including the evaluation and funded phases, fee structures, trading regulations, and the critical differences between A-book and B-book firms. This session is particularly valuable for traders who may have limited capital but possess the skills to trade profitably.

### Timestamps

- [00:00] Introduction to Prop Firm Trading This session focuses on the logistical and management side of trading with prop firms rather than technical analysis. The emphasis is on understanding prop firms as a way to access extra trading capital. Important: This content is for educational purposes only. Prop firms are third-party entities not associated with any trading education platform.
- [01:54] What is a Prop Firm and How Does it Work? Proprietary firms allow traders with the right skill set to access more capital than they have or want to risk. Key components include:
  - Evaluation Phase: Varies between firms (one-step, two-step, or accelerated)
  - Funded Phase: Access to larger capital after passing evaluation
  - Trading regulations and profit sharing arrangements
  - Restricted freedoms (no overnight positions, consistency rules)
- [04:21] Understanding Trading Regulations and Contractor Status When trading with prop firms, you are considered a contractor, not an employee. Most trading is done on simulated accounts, which is the legal workaround for trading regulations. Your capital is placed in a SIM account while the firm's actual liquidity sits in the real market.
- [05:58] Evaluation Phase Requirements
  - Strict rule sets must be followed to pass
  - Done on simulated accounts
  - Critical Warning: Always read the fine print and do thorough research before committing to any firm
  - Some firms have gone under due to bad management - stick with reputable, established firms
- [08:04] A-Book vs B-Book Firms Explained
  - A-Book: Trades placed directly into the liquidity pool; the firm copy-trades your successful positions
  - B-Book: Trades placed in-house on simulated accounts; you won't affect the real market
  - Important: With B-book, your $500K position won't show up on the footprint - you cannot move the market
  - Forex traders often prefer A-book to avoid manipulation, but futures traders face less slippage issues due to higher liquidity
- [13:33] Fee Structure and Platform Differences Key differences between Forex prop firms (FTMO, MyForexFunds) and Futures prop firms (Apex, Topstep, TradeDay):
  - Forex pairs have varying spreads depending on the broker
  - Crypto trading on prop firms is not recommended due to CME limitations
  - Swap-free options available for holding longer positions without funding fees
- [18:10] Trading Rules and Consistency Requirements Critical rules to understand:
  - Static vs End-of-Day Drawdown
  - Trailing Drawdown
  - News and high volatility trading restrictions
  - Apex's 30% Rule: No single day's profit can account for more than 30% of your total withdrawal - this prevents gambling behavior
- [23:17] Daily Drawdown Calculation Examples On a $50,000 account:
  - Daily drawdown: $2,500 (5%)
  - Max drawdown: $46,000 (must not drop below)
  - Includes commissions and open positions
  - Drawdown resets at end of trading day based on new balance
- [27:17] Trailing Drawdown Explained Apex-specific feature that teaches consistency:
  - Drawdown follows your highest balance (including unrealized profits)
  - Example: $50K account ? make $500 profit ? new threshold is $48,000 ($50,500 - $2,750)
  - Key benefit: Once funded, trailing drawdown stops $100 in front of starting balance
  - No daily drawdown limits once funded - only max drawdown applies
- [32:00] Apex Fee Structure and Contract Limits For a $50,000 account:
  - 10 ES mini contracts (over $200K notional value)
  - 100 micro contracts
  - Profit goal: $3,000
  - Trailing threshold: $2,500
  - Withdrawal limits apply for first three months
- [34:06] Rithmic vs CQG Data Connections
  - Rithmic: Connects to Atas, NinjaTrader, QuantTower (but NOT TradingView)
  - CQG: Connects to TradingView via Tradovate
  - Apex provides Rithmic connection but it's SIM data, not live data
  - For live data: Use AMP broker with CQG connection
- [35:40] Atas Connection Setup How to configure market data connections:
  - Click Connections tab ? login with your broker credentials
  - Check "is market data" for live data from AMP
  - Uncheck for Apex SIM data to avoid conflicting data
- [38:05] Risk Management and Final Tips Critical warnings:
  - Prop firms are a very big risk - if the business goes under, you lose your profits
  - Always do your own research regardless of recommendations
  - Your actual risk is the drawdown, not the account size
  - Remember: $400/day = $100K/year - consistency over size

Ready to explore prop firm trading? Take time to thoroughly research any firm before committing, always read the complete rules and requirements, and start with proper risk management in place. The opportunity to trade with additional capital is valuable, but only if approached with discipline and understanding of all the regulations involved.

## Transcript

[0:00] Hello, and welcome to another contender session. This is part one of Rivalry's ES series. This is actually very different from the profile strategies that we just covered. This one is very special. It won't be covering any technical analysis, no trading strategies or anything special like that. But actually, it's going to be covering the logistical side of things as far as management skills and how I manage personally my prop firm accounts. I believe that this will provide a lot of you value because of the opportunities as far as extra capital for the

[0:38] ones that maybe are in a different situation, a different season in their life where capital is scarce. So please pay attention. Please take the time to read the trade disclaimer. This one's actually very important to read this time, guys, because most of the time when we put this trade disclaimer, it's about our ideas. And you guys know that our ideas as far as the trade of the week, our strategies, anything like that should not be taken as financial advice because it's only for entertainment and educational purposes only. But this time,

[1:10] it's actually super important because of the nature of prop firms. Understand that they are third-party entity. They are not associated with Chart Champions. That everything I'm doing here is actually my journey and how I have discovered just another opportunity of you know trading capital and just you know the longevity of being in this market and not risking my own capital here okay so please take the time to do your own research even after we discuss this this strategy, we will be covering the utility, the implementation, and we'll be ending it off with tips and tricks.

[1:54] Utility. What is a prop firm? How does it work? And what are the requirements? We'll be covering also the fee structure. And we'll also be touching on Addis because that's what Chart Champions usually uses. Understand that there are other platforms such as NinjaTrader, QuantTower, etc. But mainly we do connect here with Addis. We'll be also covering the brokers and also the different kinds of feeds because Addis is not a broker and is also not a feed. Understand that these are all three separate things.

[2:35] You guys may be used to, say, exchanges like Bybit where they are the trading platform, a.k.a. the Addis. They are the broker because they provide their own liquidity, and they also have their own fee. Understand when it comes to the futures market or even the stock market, the forex market, they are all separate entities.

[3:06] known as a prop firm, are businesses that allow traders with the right skill set to access more capital than they have or want to risk. Usually it's with the evaluation phase and also the funded phase. The evaluation phase varies between prop firms. You'll sometimes see things like a one step, a two step, or even an accelerated step where it goes straight to the funded phase, but then you'll be risking more of your own capital. I don't recommend that. I believe that the evaluation phase for all the various prop firms

[3:36] are fairly fair, okay, to some extent. And it'll also give you like kind of like that, that buying power.

[3:50] I know most of you have said in the past, as far as I've read on general chat, man, if I had more money, I bet I could do better.

[3:57] This is that opportunity to show that and prove it. There are trading regulations. There is profit sharing. But there are trading regulations. There is profit sharing. But there are restricted freedoms.

[4:21] As far as the trading regulations are concerned, contrary to popular belief, you are not a trader when you are trading with a profiety firm. You are considered a contractor because most, if not all, are trading on a simulated account. So case in point, an example here, let's say I decide to open a propriety firm and I have a lot of capital that I want to risk and I want to do profit sharing, let's say an 80-20 split where the trader keeps 80% and I keep 20, most of my liquidity,

[4:49] aka my capital, is placed on the liquidity pool, which is the real market. But then the trading platform that I use for you guys, the customer, is on a simulated account. So you're not actually trading real money. You're trading what you call SIM account. So you are indeed a contractor, which is why this kind of like the loop around as far as trading regulations. It's vastly covered in the Forex market. And it's also very, very firmly established on the futures market which is why i love trading

[5:26] futures more than the forex market because there's a lot more regulatory clarity as far as prop firms are concerned because there are differences between the two as far as restricted freedoms most if not all don't allow you to hold overnight positions you You know, like let's say you have a swing trade. You're not going to be able to do that. There are lots of other restricted freedoms such as consistency rules, you know, but we'll be covering that in a future slide.

[5:58] The evaluation phase. Interested parties who wish to be funded by the firm of their choice are obligated to follow a strict rule set to pass their evaluation phase. This is usually done on a simulated account, just as we discussed. The set rules vary between firms, so do your own research and always protect yourself. Please read that very last bullet point because i cannot stress this enough okay because just like any business okay let's say like the example i gave earlier where i decided to

[6:33] open a propriety firm i can rug you you know like just like mini coins on the crypto like let's say dogecoin you know all those other ones where they go parabolic to the upside and it completely gets rugged. It's the same thing with prop firms. You know, there are stories, you know, that are valid where prop firms go under. It's due to bad management. It's not so much a scam, so to speak. You know, it's just bad management on the logistical side of things as far as the company

[7:05] itself. I have not heard or know of any firm that is reputable that has just taken money and ran. Okay. However, I have had heard and experienced myself where a propriety firm has either to refuse to pay out a customer and or has gone under kind of like a coin as far as crypto is concerned so you have to be careful it's usually safe to get with the propriety firms that are more established, like FTMO is one of the largest ones, if not the oldest propriety firm, but they're really more around the Forex market.

[7:52] There are differences. The one that I personally use as far as futures is Apex, which we'll be covering here later.

[8:04] Funded phase. If a trader successfully passes an evaluation phase, they are granted access to a funded account. Some firms are considered A book, some are considered B book, and others are hybrid. So this is super important, guys, because this is kind of like where the discrepancy lies. A lot of the marketing where you'll see on prop firms, they'll state things like trade real money. When in reality, if they gave you real money, that's technically illegal because us as individuals that don't have a license to trade here in America. They call it a Series 7 license.

[8:46] I'm not sure with all the other countries, financial advisors and things like that, we are not allowed to trade other people's money. It's not allowed. It is actually very illegal here in America. But if you are trading a SIM account, it's technically a game. It's almost like a video game that's the best way to explain it which is kind of like the loop around which is why most if not all traders are placed in the b book that means it's an in-house trading firm okay where it's not on the

[9:19] liquidity pool so think exo charts or addis where you see the footprint. So if you go in, let's say you have a $500,000 propriety firm account that's already funded and you enter in with $300,000 of capital, you will not show up on the footprint. You will not because you are on a SIM account. So for you to think that you have the ability to push the market around because you have multi-million dollars in prop firm money, that is not going to happen. As far as A-book is concerned, A-book means you are placed in the liquidity pool.

[9:57] But what that usually means is that the company itself is copy trading your trade.

[10:07] that the company itself is copy trading your trade. So, you know, case in point, I do very well on Apex and I am considered an A book trader. So the firm itself is copy trading my trades. That is considered an A book because there are some discussions as far as the pros and cons between A book and B book, you um but i believe okay that is only surrounding the forex market because some like i'll give an example because i'll state this out because most people don't know this a lot of forex traders don't like b book firms because

[10:39] you can manipulate the results you can get you can get scammed in many ways. A firm, if they manage it incorrectly, like let's say you do really well, they may not have the ability to pay you because you're trading on a SIM account and they're not copying you.

[11:12] On the flip side of things, as far as A-book concerned, the pros and cons with that is obviously you will likely get paid because the firm's copy trading you because you're doing well. But the downside to A-book is sometimes you get that massive slip. You'll get massive slippage in terms of entering. You see an entry and you get slipped several points. entering, like you see an entry and you get slipped several points. And that's due to the low liquidity that a lot of Forex pairs have to deal with. And that is something where you don't

[11:32] have a problem with futures, especially if you're trading the ES. That's why I love the ES. So as an A book trader, I will never get slipped because the liquidity is there. So it's not ever a problem for me as far as being A book or B book, right? So it's not an issue at all. This bullet point, so to speak, is more considered on the Forex side of things because like, for instance, let's say you're trading the US 30 or let's say the Japanese Y, you know, something low liquidity and you're a

[12:06] B book trader, you know, you will get filled because it's a SIM account. What you enter, you'll get filled. But if you're an A book trader and you know, there's low liquidity at the time you try to enter, but it slips you like massively away from your entry, you may be completely under or, you know, get wrecked, unfortunately. And it has happened on many occasions. So just be very aware of that.

[12:34] A-book firms place your trades directly into the liquidity pool broker and B-book firms place them in-house. So just like I said earlier, that's the differences between the two. So like that last example I gave where I am the propriety firm and let's say you do business with me and I consider you a B-book trader and you enter in a trade, you will see the trade placed on your trading station, but it's nowhere besides that. Nobody's going to see it. And obviously, the market's moving the way it's supposed to, but you're not going to be able to dictate whether or

[13:17] not you are even affecting the market or anything like that. But the only issues with that is there's a huge risk for B-book firms that are in the space, especially if they're a newer company, unfortunately.

[13:33] Implementation. Fee structure and profit sharing are dependent on the firm. There is a clear difference between a Forex prop firm versus a futures prop firm. Crypto can be traded on a prop firm, but I do not recommend it as crypto trades off the Forex pairs and futures are connected with the CME futures pair. So like I said earlier, there is a huge difference between Forex and futures as far as the prop firm space is concerned. and futures as far as the prop firm space is concerned.

[14:08] Some of the big names on the Forex space are MyForexMunds,

[14:13] FTMO, which is the oldest and largest and most reputable prop firm there.

[14:19] As far as futures are concerned, there's TradeDay, which is a newer company. I'm not sure about them at all. I trade with Apex.

[14:26] There's Topstep, which is actually a very reputable company as well. But I don't recommend trading crypto at all as on either of them, to be honest with you, because if you're trading crypto on the futures pair, you will only be trading CME. So if you guys recall the CME gap, when we look at Bitcoin CME, yeah, that's the only one that you can trade. So it's completely different if you're charting by bit. As far as the Forex space is concerned, it's going to be a little different as far as the spreads. That's

[14:58] one of the big things about Forex are the spreads. Because each prop firm in the forex space like let's say my ft uh my forex funds versus the funded trader versus ftmo versus um funded next i'm naming a lot right most of these are the forex uh prop firm space they are have different brokers okay like a broker would be eight cap okay another broker would be think markets another broker would be uh purple trading so depending on the broker that they use there might be differences as far as price let's say you're trading Bitcoin and like right now, Bitcoin's

[15:45] price is 26116 on Bybit. It may be 26300 on Purple Trading and 26250 on ACAP. So you have to be careful when you trade something like that as far as the Forex. If you're a high time frame trader,

[16:06] that as far as the forex if you're a high time frame trader i don't see it as an issue i personally have a forex prop firm just for bitcoin because there are no overnight fees as far as it's just a actually that is a feature that they have that you have to pay extra but i like to hold swing positions on the forex prop firms for my Bitcoin trading. Because, for instance, if you're placing a long on, let's say, a crypto exchange, most of the time the funding is going to only benefit shorts, right?

[16:48] But in terms of trading off a Forex pair, let's say I actually I did have a long from sixteen hundred. I'm sorry. Sixteen thousand. Excuse me. On a Forex prop firm. I've been holding that since I don't know, for a while now. So I have not paid any fees or any funding fees, anything like that. OK, don't get me wrong, though. Forex prop firms do have funding fees, anything like that. Don't get me wrong though, Forex prop firms do have funding fees. But there is a feature where you pay a little extra for the evaluation to where it's

[17:15] called a swap-free option. That's part of the fee structure because there are firms that don't provide swap-free options. They're just standard to where you pay a fee every day, regardless if you're in a long or a short. There's no funding like crypto. Crypto has funding. Let's say negative funding on a long will pay you in crypto, on a crypto broker. It's not like that in the forex space. It's not even like that in the forex space it's not even like that in the future space okay and in most cases futures don't allow you to hold overnight

[17:49] okay so just be very careful with that static draw a static and end of day drawdown that's what eod means Trailing drawdown. News, high volatility trading. Trading consistency rules. We're going to be covering this in a second. There's a huge difference between static and end of day drawdown versus trailing drawdown. Also, as far as news and high volatility trading, you have to be aware of each firm's rules. Like for instance, Apex allows you to trade news. Okay. Like let's say the FOMC, they allow you. Okay.

[18:36] They don't encourage it, but they allow you. I believe if I correct me, if I'm wrong, I don't remember, but FTMO, I don't believe they allow you to trade the CPI, the PPI, the FOMC, all those really big ones. They don't allow it. If you do, you fail even if you're funded. They will close your account. Trading consistency rules are super important because, for instance, with Apex, people don't know this, they have what they call a 30 rule okay a 30 rule and this actually i need to address the controversy here because there are a lot of

[19:12] people you know outside of chart champions that say oh apex is a scam etc etc you know you might read it on twitter things like that but i can tell you from personal experience that apex has consistently paid me okay I'm telling you that personally so I've never had an issue where I believe where they come and have issues because they're blasting them is because of the 30% rule what the 30% rule states is that when you are requesting a withdrawal your withdrawal cannot be from one day's trading where 30% accounts for everything.

[19:51] So let's say you, I believe the minimum trading days is 10 days with Apex. And then you gamble because you're not thinking on a high volatility trading day like the FOMC and you make like let's say three thousand dollars okay and the next nine days you are micro closing because you want to get paid right you want to open and close just to get an active day in and you make a hundred bucks for the next nine days. Okay. Like a hundred,

[20:30] a hundred, a hundred, a hundred dollars. So another extra 900, right? You cannot request a withdrawal. And if they actually, it stays under disclaimer, they can close your account. So you have to be very consistent in terms of your trading. That's specific with Apex. There are other companies that do consistency rules, but they vary. But's specific with Apex. There are other companies that do consistency rules, but they vary. But I know with Apex, because I use them personally myself, the 30% rule is there.

[20:52] And that is where a lot of traders, because they're degenning in positions, where they've run into issues to where Apex closes their funded account and they have a lot of money in there, but that's because they were gambling and unfortunately they don't want to partner with anyone who will gamble which is why i discourage you guys when i see on some of your screenshots where you're trading way too large now if you're trading large and you're doing well like like let's say you open a 10 contract position and you do well, and then the next couple, and that day you end up with 3,000, and then the next couple of days you have a $500 day,

[21:28] $1,200 day, $2,000 day, $100 day, that's completely fine, okay? I've had those days myself. What they're looking for is a person that is just straight up gambling, okay? And they're very, very helpful as far as their chat's concerned. I had run into an issue to where personally myself where I think I made about $7,000 on a single day and the other days I was making about anywhere between $700 to $1,200. All I needed to do was trade more days to bring that 30% down. Because $7,000 in a single day accounts for 30% of my

[22:07] profit, more than 30% of my profits. So I can't request, you know, it cannot come from a single day. So I didn't trade, I didn't request a payout for, I think another like 20 days or more to even it out because you know, that's, let's say $7,000 out of 30 days of trading, you know, that's a lot. But if I manage, let's say a 500 to a thousand, the next couple of, for the next 20 days, it's going to be perfectly fine. It'll even itself out. Okay. So that's something that you have to

[22:36] really focus on. And as far as any prop firm that you want to look into really read the rules because you don't want to just look at the offer where it has the big numbers. Read the fine print. That is something that I am stressing a lot right now because a lot of people run into huge issues when they don't know what they're getting themselves into. In the course of one working day, the maximum daily drawdown on the account, which consists of accounts current result of open positions and the result of positions closed on a given day, must not exceed 5% of the initial balance on the account.

[23:17] Please note, this includes commissions. So this is what you call daily drawdown or end of day drawdown. OK, so here's our example. Like, let's say here, a $50,000 account. Your daily drawdown is $2,500. So the balance must not drop below 46% because that's the overall max drawdown. OK, so there's a difference between the two. So daily drawdown is what you can lose on a single day. And that includes open positions. This is don't confuse this with trailing drawdown because we will cover that in a future slide.

[23:51] This is what they call the static drawdown to where let's say we start the day with 50,000. Right. And I get crazy and I mean 15,000. OK. Fifteen thousand dollars.

[24:15] I cannot lose two2,500. I hope you guys understand that. But let's say I lose $2,000,

[24:21] which is fine. I would stop trading that day because I'm now in profit. And the overall max drawdown for that single day is still $46 still 46, which doesn't make sense, obviously, because we made so much profit. But once that day ends, everything gets recalculated over. In most cases, daily drawdowns are 5%. And max drawdowns are anywhere between 8% to 10%. So like I said, that's static for the day. So in that example where we made $20,000, our account size is now $70,000. Our daily drawdown is going to be 5% of $70,000.

[24:57] So obviously it's going to be a little more than $2,500. And then our overall max loss drawdown, in this case, would be 10% of $70,000. So just be very careful of what that is.

[25:15] Here's an example.

[25:19] On a $100,000 challenge, if at 5 p.m. Eastern Standard Time, you have opened a trade with a floating profit of $2,000 and your account equity will be $102,000. With a 102 minus 5 equals 97. On the next trading day, drawdown is calculated based on the initial account balance. Example number two. On a 100k challenge, if at 5 p.m. Eastern Standard Time, you have opened a trade with a floating profit of $2,000, your account equity will be 102. With a 6% daily drawdown, the equity cannot drop below 96,000.

[26:19] 102 minus 6 equals 96 on the next trading day. Drawdown is calculated based on the initial account balance. Example 3 Alternatively, if a $100K challenge at 5pm EST, you have opened a trade with a floating loss of negative $2000, your account equity will be $98,000. Because your $100K initial balance is higher than your $98,000 equity, your daily drawdown limit drop below 95,000 100,000 minus 5,000 equals 95,000 now here's the kicker with trailing drawdown trailing drawdown is big on apex I'm not familiar with any other firm that does trailing

[27:22] drawdown okay this is one of the things that most people don't like about Apex, but I think during the evaluation phase, trailing drawdown actually teaches you consistency and discipline. So contrary to popular belief, I actually like it. Starting an account with a max threshold of $47,500, because remember, it's a $50,000 account. You make a profit in $500. So $50,500 minus the $2,750 equals $48,000. So that's your trailing drawdown now. You lose profit about $100. So now your account balance is $50,400.

[28:09] But your stop loss, because it's a trailing drawdown, is still $48,000. Remember, the highest balance was $50,500. So trailing means it follows.

[28:25] You enter a live trade with unrealized profits of $600 and it stopped an entry. The nice thing about futures is that there's no market fees when you get stopped out.

[28:51] Your drawdown threshold is $51,000 minus $2,750 equals $48,500. So this is where people get confused a lot.

[29:03] Remember, our highest initial balance was $50,500 at the time before making $600 in profit.

[29:11] Remember, the first trade, our balance was at $50,500. So minus the trailing drawdown makes it at $48,000. And obviously, we lost profit about $100, but it stays at $48,000. But then we entered a live trade, unrealized $600.. So 600 unrealized plus our current balance at 50,400 equals 51,000. But, okay, unfortunately, we were stopped at entry, so we weren't able to close anything. So our current balance right now is 50,400 with a stop loss at 48,500. Okay.

[29:48] So I hope you guys understand this. Please pause and read this over and over if you are trading an Apex account. Now, the nice thing about Apex is once you're funded, okay, the drawdown stops following you. So you commonly heard me say things like I have like a $10,000 pad, things like that, which is nice. And the one thing I love about Apex, okay, where they separate themselves, in my opinion, versus all the other firms, is they don't have a daily drawdown. So case in point, an example here.

[30:16] Let's say you're funded with another firm, $100,000, and then you make your balance to $120,000. Like you've had it for like a couple months now. You're at $120,000. Your've had it for a couple months now. You're at $120,000. Your max drawdown for a day is still 5%, whether if your balance is $120,000 or $150,000. But with Apex, if you're funded with $100,000, your trailing drawdown stops $100 in front of the balance. So your initial balance will be $100,100. So $100,100. So if your balance with $100,. Because your balance is 120,000.

[31:09] Your account is blown at a hundred thousand, 100. Okay. All of that is profit. So there's no daily drawdown, like 5% or anything like that. It's just max drawdown immediately of any single day. Okay. This is why why I love it and it gives me a lot of flexibility as far as once I pass the trailing drawdown hurdle. Remember, trailing drawdown is forever on an evaluation phase with Apex specifically. But once you're funded, it stops trailing $100 in front of the balance as soon as you hit the profit threshold.

[31:47] So, for instance, the profit threshold for Ð actually, we'll go back to the next slide.

[31:56] So this is actually a nice example here that the current balance is $50,400, right?

[32:07] is $50,400, right? And the drawdown left is $1,900, not $2,750, because obviously our highest balance was at $51,000 because of the unrealized means to stop that entry.

[32:15] Funded PA accounts work differently as the drawdown stops trailing $100 in front of the starting balance, i.e. $56,000 balance max threshold 50,100 so that was the example I was trying to give earlier actually actually show you more with the fee structures with apex in the future slide so this is the fee structure with apex okay you'll see here let's start with that $50,000 one right in the top center here where it shows the amount of minis that you can trade on the ES, which is 10. That's plenty. Remember, that one is a nominal

[32:52] value, notional value of over 200,000. That's a lot. As far as micros is concerned, you can trade up to 100, and your profit goal is 3,000. Your trailing threshold is $2,500. So you can kind of like understand the differences. Like for myself, I mainly have $150,000 and $100,000 accounts. And what I was trying to explain here earlier, so like I'll give an example here. My $150,000 account, my highest balance I believe is $190,000.

[33:30] balance, I believe is 190,000. Okay. Um, with that 190,000, okay. Um, my stop loss for that account is 150, 100. Now, as far as the withdrawal is concerned, you may be asking, how come I don't withdraw it? Well, that's because there are withdrawal limits for the first three months. Unfortunately, it's another issue where most people have.

[33:49] But I'm not going to get into the withdrawals. You could probably just read that up yourself.

[33:55] But there is kind of like a combine to where you have to consistently keep the account for three months in order for you to do a max withdrawal. So like for instance, there have been some sessions where I have had an account for two and a half months and I got a little too wild on some of them and then obviously lose them because, you know, you're still withdrawing, you know, a few thousand twice a month, unfortunately. So it does take out of the balance, right?

[34:22] Rhythmic versus CQg data connection i didn't want to go too long-winded on this because you can we can go forever so what i suggest is that you just go on to this i actually found this um but there are different resources you can read up on it i i believe it was a good read um it explains the differences between the two but honestly with the es you're not going to see a whole lot of a difference okay so you this is what you call the feed okay this is what we were discussing earlier where it has feeds the feed is what gives you the live data

[34:56] and these are some of the trading platforms that we use that you can use when you have a rhythmic connection and cq or a cqg connection and you can connect it through attis ninja trader quant tower and there's also different ones um what i wanted to mention that if you are um going with apex they do provide a rhythmic connection but the difference is it's a sim account okay it's a SIM data. It's not the real data. So I wanted to be clear with that. As far as CQG is concerned, you can connect CQG through TradingView.

[35:32] And I believe a way to do that is through Tradovate. That's another trading platform. It's kind of like Loops. So you get the feed CQG, you connect it to Tradovate, and then Tradovate can connect to TradingView if you're an individual that likes to trade off TradingView. But Rhythmic can connect to all the other trading platforms that are listed on there, but not on TradingView. But both can connect to Addison and TraderQuant Tower. And anything with a Rhythmic connection, CQG can connect.

[36:02] But Rhythmic cannot connect to TradingView. Just be very very clear on that so this is attis right here obviously so you want to do is connect click the connections tab right there and then they'll you'll have a login whether if you're using amp or even apex or top step which is another futures platform, or Trade Day, Earn2Trade. These are all different futures platforms that you can connect through Riffmic or CQG. Right here is what you want to look at is that you see my CQG data right here to the left.

[36:35] I have here is market data. That means that's live data because I'm buying it through AMP. Where Apex, I have it unclicked. This is super important. This is how you get that because I don't want SIM data. I want real data. So you can have both, but it's kind of like going to change your numbers a little bit and you don't want that unless you do. It doesn't matter. But let's say you don't have AMP and you don't want AMP or you don't want to afford AMP. You just want to get Apex. Apex data is decent. I've checked it out. It looks good. There are some

[37:06] cases where the footprint might be misleading, but unfortunately, I mean, actually, fortunately footprint shouldn't be your whole entire reason to trade anyway. Right? So you can trade without the footprint. I'm sorry, with the footprint data of Apex, I've done it before. Um, and cause I don't like keeping money on my broker. So sometimes I only keep the $200 there when I withdraw. Um, right now we're in hedge mode. So obviously I'm going to have some money in my amp account, but in most cases when it is a scalpers, uh, area where

[37:36] it's in the middle of the all time high and the all time low, I will not have any money in the CQG. I will be just using Apex data. Sometimes I do add the $100 that AMP needs to have the live data, but it's up to you. But let's say you don't want to spend any more extra money. You just want to have the data from Apex. That's perfectly fine. You just have to click is market data. Tips and tricks. Prop firms are a very big risk. Read that again. Very big risk. Remember, they are businesses at the end of the day to provide a service.

[38:09] If that business goes under, so does your hard-earned work. So that example of me having $190,000, so that's $40,000 in profit. If Apex blew up tomorrow, I will lose all that. I mean, I highly doubt that because they are a large firm. They are, I believe, the largest futures platform. They're not really good on the social media side of things because I personally know the owner. And he's a boomer, real old guy, real great trader. And their Facebook group is like 20,000 strong.

[38:41] There are a lot of members on Apex. They do very well. But like I said, I don't speak for them. are a lot of members on Apex. They do very well. But like I said, I don't speak for them. I'm not their employee of Apex. And anything you do here is always going to be a risk. Remember that, just like trading. So we always have to have risk management. Please, just because I'm saying it on this thing, still read about it. Do your own research. This is what I recommend you do. Because at the end of the day,

[39:06] you are the one that is going to put your money in here, okay? You're the one that's going to put in the hours. You're the one that's going to put in the work. Firm capital provides you with purchasing power, but your actual risk of losing the account will always be the drawdown regardless of capital size. So in that example, okay, I do have $150, 000 of buying power or 190 but in reality it's more like 40 000 right so if i gave somebody 40 or i traded on amp with 40 000 it's no different than 40 000 with um apex okay it's just giving you a little bit of purchasing

[39:42] power as far as this is more uh relevant on Forex side of things, because with futures, you have contract minimums. I'm sorry, maximums, like, for instance, on one hundred and fifty thousand dollar apex account for the for futures, you can only trade up to 17 contracts. OK, so you can't use 20. And I believe, you know, you do the math. Twenty times four hundred is far less than 40,000 or even 150,000. Just be aware of that. This is more relatable to like the Forex market. Like, you know, it's still the drawdown at the end of the day that 5%, but then you have, let's say you have a $200,000 account.

[40:21] 5% is your maximum draw, your daily drawdown. 10% is your maximum drawdown, but you have the purchasing power of 200,000. That's really the differences between futures and Forex is the purchasing power because the purchasing power is really not, I guess, a variable when it comes to futures due to the limitation of contracts you can purchase but still 17 contracts is plenty guys some of you who are trading with three to five you already know right because remember 400 a day 100k a year all right thank you for watching a live stream covering questions and answers

[41:02] about this session will be hosted on August 23rd, 2023. You can submit your questions for this live stream via Discord in the sessions questions. For additional Q&A, head over to the Trading Help Forum in Discord.
