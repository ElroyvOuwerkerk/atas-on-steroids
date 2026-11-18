---
date: course-lesson
champ: unattributed
course: Futures and Stocks
lesson_no: 7
title: ES Series: Part 2 (Contenders)
slug: contenders-session-es-series-part-2
category: courses/futures-and-stocks
cues: 342
words: 4860
duration: 29:12
source: https://portal.chartchampions.com/lesson/contenders-session-es-series-part-2
captured: 2026-11-16
---

# ES Series: Part 2 (Contenders)

## Portal Summary & Highlights

### ?? Detailed Timestamps

- [00:00] Introduction to Part 2 of the ES Series - Important disclaimer that this content is for educational and entertainment purposes only, not financial advice
- [01:02] Session Overview - Three main topics covered: Utility, Implementation, and Tips & Tricks. Discussion will focus on position sizes, contract sizes, contract rollovers, and managing trades on ATIS/Apex
- [01:44] Platform Focus: ATIS - Why ATIS is preferred over NinjaTrader, Quantower, SierraCharts, or ExoCharts: ¥ Superior tick refresh rate ¥ Used by majority of futures coaches ¥ Better execution capabilities
- [03:05] CME Exchange Fundamentals - Critical difference from crypto: ¥ Only ONE data source - the CME (unlike crypto with multiple exchanges) ¥ What you see is what everyone sees ¥ No exchange discrepancies like Binance vs Bybit
- [05:06] Contract Size Explained - ES contracts are $50 ? S&P 500 index price ¥ At $4,000 price level = $200,000 notional value per contract ¥ Called "mini" because it's one-fifth of the full S&P 500 contract ¥ S&P = Standard and Poor Index (top 500 market cap companies)
- [07:07] Position Size Warning - Important perspective: ¥ 5 contracts = $1 million in purchasing power ¥ The ES market trades in billions - individual traders cannot move the market ¥ Unlike altcoins where walls can be created and price manipulated
- [08:17] Tick Specifications for ES: ¥ 1 tick = $12.50 ¥ 4 ticks = 1 point = $50 ¥ Tight bid-ask spreads due to high liquidity ¥ Note: This differs from NASDAQ and Russell futures
- [10:35] Trade Example Calculation: ¥ Entry at $4,000, exit at $4,004 (4-point move) ¥ 4 points ? 4 ticks = 16 ticks ¥ 16 ? $12.50 = $200 profit per contract ¥ Fees typically $4 per contract with Apex (range: $2-$6 across brokers)
- [11:41] Scalping Philosophy - Why small moves matter: ¥ 1 point = $50 profit, minus $4 fee = $46 net ¥ "Who can't get one point in one hour?" ¥ 8-12 tick moves are substantial; 20 ticks can pay for your entire day
- [13:03] Contract Rollovers Explained: ¥ Futures are cash-settled, not held in perpetuity like stocks ¥ ES uses quarterly contracts: - H = March (Q1) - M = June (Q2) - U = September (Q3) - Z = December (Q4) ¥ Rollover happens at beginning of each quarter
- [15:16] ATIS Dashboard Overview: ¥ Chart trader panel location and usage ¥ Account tab (highlighted in yellow) - Critical for portfolio management ¥ Quantity settings for position sizing ¥ Buy/Sell market buttons and hotkey shortcuts (Shift key for chart trading)
- [16:34] Level 2 DOM Explanation: ¥ Shows all pending limit orders ¥ The tape fills these limit orders ¥ Market orders fill through limit orders, creating dollar cost average entries
- [17:39] Portfolio Setup in ATIS: ¥ Click the plus sign in main ATIS window ¥ Name your portfolio (e.g., "PA Main") ¥ Use dropdown to add multiple accounts ¥ Select portfolio in account tab before trading
- [19:30] Portfolio P&L Display Quirks: ¥ Multiple accounts show extended decimals ¥ ES vs MES accounts display different values ¥ Slight slippage differences between copied accounts is normal
- [20:47] ?? Critical Bug Warning - Shadow Entries: ¥ Portfolio may show incorrect position sizes ¥ Always check the Positions Tab - this is NEVER incorrect ¥ Chart may show phantom profits that aren't real ¥ Closing "ghost positions" will open NEW trades, not close existing ones
- [23:06] Three-Step Verification Process: 1. Check portfolio shows "Flat" 2. Check Positions Tab shows all copied accounts are flat 3. Manually scan each individual account for shadow limit orders ¥ Use higher timeframe (TPO/Volume Profile) to scan entire chart ¥ Limits can remain even when positions show flat
- [25:46] Detailed Copy Trading Workflow Example: ¥ Portfolio of 5 contracts across 3 copied accounts ¥ Taking partial profits may desync portfolio from actual accounts ¥ Must manually adjust portfolio quantity to match actual positions ¥ Shadow limits can reapply themselves after being hit
- [27:34] Handling Desync Issues: ¥ If one account has different position size, close it manually ¥ Remove that account from portfolio temporarily ¥ Reapply later once synchronized ¥ "I copy trade as many as 10 accounts with no issues" - because of thorough checking
- [28:30] Final Tips: ¥ ES is one of the most liquid assets available ¥ E-mini = one-fifth of standard S&P 500 contract ¥ Allows smaller capital traders to participate alongside institutions

?? Key Takeaway: Success in ES futures trading requires understanding the contract mechanics, respecting position sizes (each contract = $200,000 exposure), and meticulously managing trades through proper verification processes. The extra steps of checking positions and scanning for shadow orders will

## Transcript

[0:00] What is going on everybody? This is part two of Rivalry's ES Series Contender Session. Take a moment to read the trade disclaimer. Understand that anything that we discuss here today, whether it be regarding any type of financial advice, understand that I am not a financial advisor. This is only for educational and entertainment purposes only as i've said many times this is very important because a lot of the things that we discuss here really is different from what you most of you are used to in terms of

[0:38] the if you're transitioning from the crypto space understand that the futures market is totally separate it works completely different and if you are one of the rare people that are coming over from the forex market this is actually different for you as well we will be covering the utility the implementation and then ending it off with tips and tricks The implementation and then ending it off with tips and tricks. Utility. We will be talking about position sizes. Contract sizes.

[1:15] Contract rollovers.

[1:27] And managing trades on ATIS or what if you want to call it ATOS, and Apex. Know that although this is more of a futures series,

[1:30] we are talking specifically about ES. I can mention other futures markets such as the Russell or NAS, but it's primarily focused on ES, and I'll get to that as to why. We will be talking heavily on ATIS. I know that there are other platforms out there like NinjaTrader, Quantower, SierraCharts, ExoCharts, but this is specifically for ATIS, ATOS, whatever you want to call it, only because that is the majority of what the coaches are using when you are trading futures and in my opinion the more superior uh platform are out there because

[2:07] even if you're using ninja trader or even exo exo can't trade off the platform although ninja trader can i just believe the tick refresh rate on addis is much more superior uh even more superior than something like cr charts i don't really have a whole lot of experience with Quant Tower, but I have checked it out. I'm not even sure if it has a trading platform embedded in it. Apex is a futures propriety firm. So if you have any confusion, please refer to part one where I talk about futures prop firms.

[2:42] Apex is the one I personally use. There are other futures prop firms. Apex is the one I personally use. There are other futures prop firms out there. Just don't confuse them with Forex prop firms because Forex prop firms are not traded off the CME, which we will cover. They are traded off an individual Forex broker because they are paired with the Forex pairings. So if you see anyone say trading the SPX, although it is, I guess it's possible, I mean, I do it and I've seen others do it, to chart and pull data from

[3:12] the ES, say like if you're charting on TradingView, reading footprint on Addis for the ES and trade SPX, is that possible? Absolutely. But it's not accurate if you're micro scalping so just be very aware utility es futures contracts are traded off the cme so what we were talking about earlier cme just understand that um the i guess the differences of what you want to talk about on there is let's say you're talking if you're coming from the crypto space, you know, you have Binance, you have Bybit, you have all these other platforms, right, that are, say, crypto exchanges.

[3:54] Although us, we recognize Bybit as the more accurate source for a quote unquote data feed for crypto, especially with Bitcoin.

[4:08] The futures market only has one, that's the cme you do not pull any other from any other quote-unquote exchange so let's say all the crypto places just integrated into one which is what bybit actually pulls from i know personally for myself uh when ftx was up and running, Bybit was an average of the four major exchanges. I know FTX and Binance being one of them, which is why Bybit is so accurate. Now, think in part where the CME plays into that. The CME only has one. So whether you're using your own personal funds, Apex Propriety Firm, or even another

[4:47] futures propriety firm, you're still going to be pulling data from the CME. So what I see is what you're going to see versus let's say if you are trading off Binance, you may not get the same tick to the point accuracy as if you were going to trade on buy bid and vice versa. Contract sizes are $50 times the S&P index. So understand why they call it mini. Like, you know, I've always said before, I will never explain something without you fully understanding. They call it mini because the S&P 500 index is the big brother.

[5:23] Mini because the S&P 500 index is the big brother.

[5:30] And most of those cases, only larger firms and banks were able to trade that.

[5:37] S&P stands for Standard and Poor Index, the 500 top market cap companies in the world.

[5:43] Where they wanted more of a retail, I guess, exposure is why they created the mini. And you can kind of say this is leverage but in reality it's not because with leverage you're timesing the value versus here you are basing it off the current price so if the S&P 500 is at 3,000 it'll be $50 times 3,000 okay that's the notional value but right now the ES is priced at around $4,500. But with this example, I use $4,000. Okay. So let's say the price of the S&P 500 is at $4,000. It'll be $50 times $4,000, which derives your notional value.

[6:22] Where in this example, you get about $200,000 in purchasing power. I'm just going to cough. One second. Excuse me. Apologize for that. But just understand this is why I kind of have Ð I give you guys a hard time in terms of opening so many contracts. Understand that one ES contract is this amount of purchasing power so if you're trading let's say i've seen people on general chat um opening up like 10 contracts although that's fine if you are indeed a veteran and you know what you're doing because i've spoken on both ways like if

[6:57] you're trading one contract effectively then there's no difference if you're trading 10 contracts effectively because one-to-one on a momentum style of trading, it doesn't matter your exposure because you're not going to move the market. The market's going to go where it goes no matter what. It's not like an altcoin. It's not like Bitcoin where you can create a wall and hold a wall yourself, push the price down. You can't ask, hey, rivalry, can you nuke the price right now? No, it doesn't work like that. I don't care how much I have,

[7:30] no matter if I'm the best trader in the world, I cannot move this price because it trades in the billions. And even if I was a billionaire or when I am a billionaire, I would never throw in billions in a trade. It's just not smart. I can hedge that amount, but I would never scalp that amount. It's just not smart. It's not wise. So just understand that when you are opening so many contracts, this is the amount of purchasing power you're trading with. So think about it in terms of responsibility and wisdom.

[8:06] So if you're opening five contracts, you're trading with a million dollars. And if you're not able to win with five contracts, you're not going to be able to win with a million dollars. It's as simple as that.

[8:17] Futures contracts have a minimum price fluctuation, also known as a tick. Tick sizes are among the contract specifications set by futures exchanges such as the CME and are calibrated to encourage efficient liquid markets through tight bid-ask spreads. So I say this all the time, why the ES, why I've always said the ES is where a beginner should start. So like, for instance, let's say you trade off Bybit and you're trading Bitcoin or Binance and you're trading Ethereum or whatever.

[8:54] In the crypto language, in the community, we always say things like, hey, the low wasn't taken on this exchange or the low was taken or wasn't taken or it was front run. There's only one and it's CME. And if it was, quote-unquote, front run, you can trust the data that if it starts running, let's say it front runs an SFP to the downside and it starts running, changes market structure, the data that you see on there, because it's so liquid, okay, a.k.a. lots of money running through it, when you see that positive delta come in, you can be confident that it's not going back there unless it changes much later.

[9:35] Whereas in a lower liquidity market, even if you see $30 million from a bounce, that thing can drop easily with $60 million. Very difficult to do that with billions of dollars. Someone is more likely to jump on multi-billion or even multi-million dollar pushes to the upside versus trying to stop it and trapping those individuals. It's not very effective. Understand that each point, there are four ticks, and each tick is $12.50. Now this is not universal for let's say the NASDAQ

[10:14] or the Russell, this is specifically for the ES. So for one point, it is $50 since there's four ticks in each point. So from 4,000 to4,001 is four ticks because it's one point equals $50 in profit. Okay? Implementation. A few examples include a trader A enters a long buy position at $4,000 for one contract. He, she exits at $4,004. Four point move, four ticks in each point, 16 ticks. 16 times $12.50 equals $200. So understand that the moves like this, you don't need a whole lot.

[11:01] Why I say things like eight to 12 tick moves, 16 is a lot 20 is like man you get 20 tick moves single day it'll pay for your entire day no like think about it like that okay think about it like that this is 200 that's a four point move you'll get that multiple times in a single day so some of you guys who are ting like 10 points away because you want the VWAP to get hit or you want it to go to a certain price level, you open multiple position sizes and your take profit 1, 2, and 3 are like 5 points away from each other each.

[11:38] This is what you have to think about, okay? Really think about it. Understand also that the fees vary from broker to broker.

[11:47] I know with Apex, they're about four dollars per contract.

[11:55] So imagine it like this. I enter with one futures contract and I exit at one point, which is a fifty dollar take profit. And my fee is four dollars, that's forty six dollars.

[12:03] Who can't get one point in one hour?

[12:07] So understand that you just don't have to take a whole lot out of the market. It's not your job. I got the sniper position.

[12:14] But in reality, it's not me entering at a sniper position. It's a level that I recognize through TA.

[12:20] Now, when you are scalping on the ES, something like this, you have to think about it that way as well. If you're a scalper, you've got to go with the momentum so that example with bitcoin if it rejected a 31.8 if you understand that that was a very strong high time frame level then take a piece of it from the drop which is why distribution highs and lows work so well know that fees exchange uh from broker to broker vary. I've seen as high as $6 and as low as $2, which is why I use about the average about $2 to $6, but usually they're about $4.

[12:56] Futures contracts have finite expiration dates. Futures contracts are cash settled as opposed to stocks that trade in perpetuity, hence the reason for a rollover. So perpetuity means that you own the actual asset. OK, so let's say you own shares, quote unquote shares, you know, futures contracts are cash, usually cash settled. So like I said in part one of I believe it's profile trading series Series 1, a contract is exactly what it means. You are agreeing between you and another party a future price and you are signing that contract that you

[13:34] will sell or buy at a certain amount. So because they are not in perpetuity, meaning you don't own the actual asset, you're just signing an agreement they have to have a rollover these are your letters for each month but in reality specifically for the es you want to think about it in quarters which is why on march you see there's eight and then in june is esm and then in uh quarter three is esu which we just traded and then in quarter three is ESU, which we just traded. And then in the end on quarter four, it is ESZ, which is what we're trading right now around October.

[14:13] They typically roll over for the entire quarter at the beginning of the next quarter. So, for instance, we're trading Z now in the beginning of October so that would be the entire one and you may ask why these other letters for well they are actually for smaller cap futures markets like let's say soybean oil soybean futures wheat oil other things like that I mean if you want to get real specific but it with the ES they typically just go all the way to the end of the quarter, which is why it has the most volume.

[14:48] If you wanted a more accurate representation of, let's say, if you're going to go into another market in futures, you can check the CME based website just to see how many how much open interest is in each letter. How much open interest is in each letter? Okay.

[15:11] Just know that it's for us with the ES market is H, M, U, and Z is what you really want to pay attention to. Okay.

[15:16] Here is a photo of a typical dashboard on Addis. You guys are probably familiar with it.

[15:24] Just understand that on the chart trader on the bottom right there, once you press that, it'll pop up this little panel. This is what I trade with. Some of you guys may trade off the DOM or directly on the bottom right there you that once you press that it'll pop up this little panel this is what i trade with some of you guys may trade off the dom or directly on the screen it's up to you what's most important is the account tab that's marked in yellow just remember that okay because i'm going to go back to that later on the right right here that's on blue that's

[15:40] your quantity or if that's purple i don't know it depends how you want to see it and then on the top there's your buy market and sell market if you want to enter it you can trade off the screen I'm sorry on the screen itself by hitting a hot key and you can I think it's shift thank you I believe it's xbox or is rendy who shout out to you one of you and if I apologize if it wasn't one of you whoever it was where you can hold down the shift and really trade off the chart itself.

[16:09] It's actually very helpful. Me, I'm a little old school, a little slow, so I like to right-click. When you watch me live trade, that's how I do things. If I do need some sort of volatility preparation, I'll use an exit strategy. But in most cases, I will be trading manually off there. OK, just remember the account tab that's listed in yellow because I'm going to mention that in a second. Right here is your level two DOM. If you are confused on this, I discussed this on profile series volume one.

[16:41] And also there's a great module with Igor. Will he go? He covers this. Just understand that these are all the limit orders that you will see on the chart. These are pending orders. Understand that the tape, which is not photo here, will be filling these in. So let's just say, for instance, there is 100 in each of these. Let's just say, for instance, there's 100 in each of these.

[17:18] If someone market buys, say like 5,000 contracts, which is highly unlikely, but let's just say someone does, it'll completely fill this and they will have a dollar cost average of somewhere around here because they have filled all these limits.

[17:21] That's how it means for a market to fill a limit.

[17:26] Same way and to the downside, well, actually to the buy side or to the sell side. These are buy limits. If you got to sell into these. So they are agreeing that future price, okay?

[17:36] Right here, you have to just make sure that on the top that you have your account that you have on there that you are trading. This is my portfolio for Apex. I will be covering portfolio trading in a second. Why my P&L window is so large This is my portfolio for Apex. I will be covering portfolio trading in a second.

[17:54] Why my P&L window is so large is because when I do trade profile, let's say my take profit is around $500.

[18:02] Because I have so many accounts listed into one portfolio, it shows the decimals and it has like 20 digits behind it.

[18:10] I have no idea why, but I like to see how much I'm going to make, which is why I like to Ð I'll say Ð let's say right here is my take profit one on this bar.

[18:17] And it will be like $300 Ð $350, and it will have dot, 4, 7, 6, 7, 8, 9, 10, 11, 12. Like there's just a lot of decimals on it. I don't know why it just doesn't round it up.

[18:26] But that's why it does that even if all 10 or even all five of the accounts that are listed on there have equal entries because sometimes it does slip you with lag um it it just doesn't change the fact that it shows decimals on there i don't know that i mean to me that's super accurate but it would be nice if addis did just round it up okay you won't have that issue if you're trading a single account okay so just bear in mind this is how you create a portfolio okay so you will go on your main

[18:56] Addis window and you will click the plus sign this will pop up check this out it says beta so I know a lot of you have complained about a lot of issues. So hopefully I can help you out here because that, I mean, I don't want to say that you don't know what you're doing, but I just take it from my experience. I really don't have any issues. So I just believe that a lot of it has to do with user error or internet. Okay. Um, or one or the other. Okay. And I'll explain why in the future slides

[19:25] because I do run into those issues, but they are human error and you can fix them. So on the yellow here, you can name your portfolio. So you saw on the level two DOM on the previous slide, it said PA main. I typed that in there. And then you will hit the dropdown bar right here and you will find those accounts and add them. And then let's say you want to add three you want to add 10 or whatever and then you will complete that and it'll show up on that account tab that was on the on yellow that

[19:54] i said hey remember this is super important you have to select your portfolio okay in order to trade that. Right here is an example of my portfolios. So I traded both the ES and also the MES. These are all the accounts that are connected on it. You can see on here on the closed P&L, you could see that the MES is $251.25. And then my ES is ranging between 1675 to 1700 because it does get slipped every once in a while. You just can't do anything about that. Right? So understand that you have to see this

[20:36] balance right here to see because the balance is going to have the contract sizes. Okay. Make sure that you have nothing open here. You will get shadow entries on the chart. Sometimes that happens, but here we'll never have it. This will always be accurate. OK, that's what's most important. Here's a great example of that. You can see here a portfolio that has two in it. You can see here that the short position is 12 on the portfolio showing $3,900 in profit and that's not unreal 3,900

[21:11] because it's basing it off an entry of the portfolio the actual entry is actually five where it's about sixteen hundred dollars in profit not thirty nine hundred because you see here this on my chart trader earlier did not Decrease the balance on my portfolio because it's bugged that happens however on your portfolio positions tab It will decrease on the accounts themselves. I'm going to say that again. Sometimes you will not get any of these position sizes decreased because due to a bug.

[21:55] Because originally, the short that says right here, five and five were 12 and 12. Well, actually, I think it was a 15 position short. This will say 15, 15, 15. And these should all add up together. As I was taking profit, this should decrease. Sometimes it doesn't because it's bugged. You can confirm that on the positions tab. Okay? So if I were to close another five positions, it will decrease these by zero and my real accounts will be done but i will show a position open on my portfolio of seven contracts with profit those aren't real

[22:37] profits so if i try to close that let's say take another seven profits, like seven singles, those buy limits will dollar cost average a seven contract long, not a take profit. This is where you guys make these mistakes. And honestly, lag does play a factor, but the page on the position tab is never incorrect ever. Now, remember what I said, look at the accounts tab where I said, keep an eye on this. Remember this. If you are like OCD, like me, once I am done and everything on my balance says empty, it

[23:22] says flat and there's no positions open I will go ahead and click down the drag down button wait till that account tab is blue where hi it's highlighted blue and I will hit the down arrow or up arrow and scan through each of my paid accounts or even regular accounts whatever you want just to see that there are no shadow limits anywhere on there. Because even if it's flat, it just means that there's no active open positions. That doesn't mean that there aren't any limits on your chart. So go on a higher

[23:55] timeframe portfolio, I mean, I'm sorry, a screen, let's say like a volume profile where it shows the entire day and just scan through each of your accounts just to see that there's no open limits. Because even if everything is closed and everything was accurate and you're all flat and everything is good to go, Addis might have bugged out and left a single limit order that you closed on the portfolio that didn't close on the main account. Or one of the copied accounts, I'm sorry.

[24:26] Because the copied accounts are the real ones. Those are the ones that matter, not the portfolio. The portfolio is just a way to manage them. So you have to double check. So every single time I'm done, I always double check. I scan through each one. It's very easy. I go on a TPO or a volume profile on a larger high time frame, I select my actual accounts and just scan through each one just to see there's no limits. And then when there are none, I go ahead and enter again. Okay. So like I said, a lot of it has to do with user error. Even though that your positions

[25:00] say flat, you might've missed the limit that you did not see. I guarantee it because I don't make these mistakes like that. You guys are repeating, uh, and also reporting. I don't, it doesn't happen to me. It does not because I always check. So, okay, I'm going to go over it again. Let's say you open a portfolio position of five with three copied accounts okay you enter a short and your profit you take profit one the portfolio decreases by four and all your copies accounts decrease by four fantastic you take another two profits your portfolio

[25:38] decreases by one and your and your main accounts that are copied decrease by two because you took two profits. So now you're off, so you just adjust it back to three and now you're even. Okay, great. You have two take profits left and you decide to just close it all. And you close it and you take all your profits. You check your portfolio. You see that it's flat. You check the positions tab. You see all your copied accounts are flat. Now's flat. You check the positions tab. You see all your copied accounts are flat.

[26:08] Now, you're not out of the woods yet because you placed limits. You have to check each copied account to make sure some of those limits didn't get shadow copied. Because let's say price is going down such as this and then you take a profit here right and it hits it keeps going sometimes that take profit even though it hit it just reapplies itself i've seen it happen so there's a there's a buy limit or or a sell limit whatever a limit position that price is completely over right that changes into a buy stop buy limit

[26:47] sell limit whatever you want to call it and if price goes back there it's going to reverse that trade it does happen because i've seen it with my own eyes because some of you guys may have don't even completely understand anything i'm talking about. And some of you have gotten to the next stage where you understand the positions tab right here, but you don't check the third one where I say you check each individual account. I guarantee you, if you do all three, you will not run into any issues because I have not yet run into one. And I effectively

[27:22] copy trade just fine. Okay.

[27:34] And if that's too much work for you, then I don't know what to tell you because it's worth it for me because I only have to enter one trade.

[27:39] I copy trade as many as 10 as the most I've ever done. And I don't have any issues. I do see shadow orders, but I catch them in time. Sometimes it does reverse on me, but I catch it. Meaning like, let's say it's a volatile move. I'll take multiple take profits. And then on here, let's say I have seven copied accounts. Same thing here. The short is 12. My portfolio is already incorrect. And I change it to five to even it. But somewhere along the line, one of them has three. I don't know how it happened, but it has three.

[28:14] Can't do anything about it. You're going to have to manually close right here. And at that point, I go in my portfolio and delete that one. I'll just reapply it later. Hope that makes sense, guys. Really hope that makes sense. Ending it off with tips and tricks. ES is one of the most liquid assets available. The standard cornerstone of the stock market has always been the S&P 500 index. E-mini futures, which represents one-fifth of the standard amount allows traders with less capital

[28:45] to trade alongside larger firms so like i said it's fifty dollars times the price which is about one-fifth thank you watching a live stream will be covering the questions and answers about this session will be hosted at a future date you can submit your questions for this live stream via discord at session questions and for additional q a head over to trading help forum in discord thank you for watching
