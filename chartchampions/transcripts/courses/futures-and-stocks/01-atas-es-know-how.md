---
date: course-lesson
champ: unattributed
course: Futures and Stocks
lesson_no: 1
title: ATAS ES Know-How
slug: atas-es-know-how
category: courses/futures-and-stocks
cues: 211
words: 3361
duration: 23:12
source: https://portal.chartchampions.com/lesson/atas-es-know-how
captured: 2026-11-16
---

# ATAS ES Know-How

## Portal Summary & Highlights

_(summary not captured)_

## Transcript

[0:00] Hello and welcome to the ATAS and ES know-how lesson. Before we begin, as always, I'd like to remind you all of the important legal trade disclaimer, so make sure you read through it, understand it, and then you may proceed with the rest of the video where we will be going over the ATAS and ES know-how starting with utility moving on to implementation and ending with some nice tips and tricks for you all utility following on from our last lesson where we covered the atas installation connecting to a broker with

[0:49] cqg and rhythmic feed and all add templates and snapshots level 2 and tape my favorites and saving with a completed workspace so for those of you who did not watch the first lesson please go back and watch it before this one because this is a follow-up from this first lesson so you will go through the installation connecting the broker adding the, templates, level 2 and tape, and then completing with the workspace saved. So really important. This is the foundation of all that you need to get going with ATOS.

[1:40] Okay, right. In this lesson, we will cover the following ES contract and position size, the tick calculation and trading fees. and stop losses right this is super important and it also is known as brackets okay really really important okay utility one single contract for the futures, the ticker is ES1, is 50 times the index price. OK, so that's 50 times the index price. For example, if the index price is trading at 4,500, then the nominal position value for one single contract is 50 times 4500 okay which is the index price at

[2:53] the time which equals 225 000 for one single es contract okay i hope that is super clean and clear so if you open 10 ES contracts that will have a nominal value of two point two hundred and fifty OK, because that's going to be 10 times 225. OK. When trading the ES futures, a trader must be aware that each year there are three, sorry, four different contract names on each quarter. On each quarter, these are the following.

[3:51] So you have E-S-H, E-S-M, E-S-U and E-S-Z. So H-M-U-Z.

[3:57] Each one of them represents the period.

[4:01] So you've got March, June, September and December.

[4:20] So, for example, right now we are trading the ESZ2023. So this is for next year to show you the whole, you know, complete contract the contract switch dates so down here you'll get a switch over contract switch and it'll give you the date the contract from and to so for example in this case we went from esu to esz so from september to de December okay here is the link for the CME where you can get all the data such as such as current month volume open interest so you can see if it's the whatever month you're trading on what's the switch are

[4:59] you in December are you in September what's the open interest what's the volume what's the settle from previous day for example you can get all of that information there so very very very crucial information so here's the link you know make sure you have this pin to your favorites or bookmarks because I check this very very often okay okay so ES is measured in ticks, unlike for example crypto and other assets that could be and are percentages. ES is in ticks and each tick represents a $12.50.

[5:38] There are four ticks in a full handle, One handle being a $1 increment. See the example of a full handle move, which is four ticks. So you have 45, 14, 25, 50, 75, and then 25, 45, 15. So this is a whole one handle, which is a $1 increment. So if price moves one increment of $1, one handle, then that will be four ticks. One tick, two ticks, three ticks, four ticks. Each tick is $12.50. Therefore, four ticks equals times $12.50, which is $50. Okay, so four ticks times $12.50 equals $50.

[6:37] The screenshot below is from an A&P statement, and it shows the fees for just a one single contract position round trip. Why round trip? Because it needs to go there to the exchange and then come back to your PC. OK, so you you basically going around in a circle. So round trip.

[7:11] $2.20 each way meaning a $4.40 total for just a one contract position size on the ES this will be different for the MES but for the main one contract of ES it will be $4.20 so this is not bad for an approximate 200 well 225 position size at the kind of time of doing this. So you can see here, so the commission breakdown and fees are for the exchange, for the NFA, for the clearing, the transfers and the commissions. So you can see 4, 1, 20, 50, 266 and then the total is 440. 4, 1, 20, 50, 266, and then the total is 440.

[7:46] Okay.

[7:55] The only other fee required is the data feed, as we covered on our last lesson.

[8:02] This fee is only once per month, and this is the CQG CME Non-Pro Level 2 bundle.

[8:06] This is what I use, okay, and it's once per month you pay $35 you get access to the level 2 depth of market and the year obviously the feed so you can see the orders coming in etc etc really important you need this in Hatters creating an exit strategy is a must so this is the brackets I was talking about so this is super important and for me it is a must. So this is the brackets I was talking about. So this is super important. And for me, it's a must. OK, there are a few options, but we will focus only on the multiple stop loss and take profit and stop loss and take profit.

[8:37] So multiple and then just stop loss and take profit. This is two different ones. OK, you can see so multiple and then stop loss and take profit. You could try these, but I don't use these because I manage them myself. And on the next video, we will do a more detailed on how to place them, etc. But for now, we will learn how to make them. OK. So let's start with the simple stop loss and take profit. You can rename them to anything you like. Let's start with the simple stop loss and take profit.

[9:04] You can rename them to anything you like.

[9:10] Set your take profit and stop loss and finish by clicking add. So here you can give it a name. Keep these as are because you want this to activate when only your position is zero. Yeah, with guarantee as well. This is good till cancel. So you keep it on or you can change this per day because you might want to place the trade and walk away from your desk. And if it gets to the end of the day, you want to be out or you want to just do it yourself, then keep it good till cancel. So you

[9:35] can do the strategy exactly as you wish. And then you want them to auto cancel. So if it gets filled, your stop loss, you want everything to be closed. OK, so in this case, we have a pair of ticks. Obviously, there has to be a pair of ticks. And then you want to have the profit and the stop loss. OK, always give yourself a little bit of room, OK, depending on what you're doing and how you're trading. But this is just a standard. Take profit, 20 ticks. Stop loss, 12 ticks.

[10:08] ticks. With the strategy activated, when you enter a trade with any position size, it will automatically set a take profit and a stop loss at the set ticks with the same size open. In other words, if you open one contract with this strategy, it will place, as soon as the position is activated and you enter a trade by limit or market, it will automatically place one contract on the taker of it and one contract at the stop loss. However, if you open 10 contracts, it will open 10 contracts and it will place 10 and 10

[10:48] if you open three it will be three and three it will always be exactly what you get it so it's a really good way to always be protected this is a must must must must use because you just never know the outcome of a trade you know from get go when you place the trade you don't know if there's going to be a big move straight away that gets you off you know off of you know position and having this in place it's all automatically an instant the moment that trade is activated it will automatically send that order in.

[11:27] So you will be filled. OK. Now let's make a multiple stop loss and take profit exit strategy. Here we can have the quantity stop loss and take profits. And it can be any size on each protection level. So, for example, give it a name, keep these as it is, and then you want to do quantity. Here you can be 1, 2, 3, meaning that each position, each three contracts, or each protection level, there's going to be one. But you can also put two or three so for example if you're trading with the mes you could do 10 10 10 so that's three es

[12:12] contracts in other words okay um which means that when you enter 30 contracts then on the stop loss being the same 12, 12, 12, that means 30 contracts will be then your stop loss at 12 ticks as soon as your position is open. And then you're going to have three different take profits straight away. One, two, three, at 10, 18, and 28. And every time a take profit is hit, let's say that in this case you got filled 30 contracts at say a given level and then you're going to see your stop loss right above if you're short and

[12:56] then you're going to say you're going to see three um three take profits 10 10, 10, 10 at these ticks. Every time take profit one is hit, your stop loss will then be reduced to 20. And then as soon as the next one gets hit at 18 ticks, your stop loss will then just show 10. So you don't actually have to manage reducing the size. It will do everything for you. Okay. It's pretty powerful, this strategy. So this is why this is a must, because it kind of makes you avoid any mistakes and everything is

[13:34] handled by the system. The system will handle your reduce and it will always reduce the position. And as soon as your stop loss is hit, for example, if let's say you got into the trade, 30 contracts, and instead of moving, like it moved down eight ticks, didn't hit profit one, but came back 13 ticks. Well, as soon as it gets to 12 ticks, for example, in this case, if it's 12, it will get you out and it will stop you out and all three take profits will just go disappear. It will cancel those orders for you.

[14:07] OK. The total quantity on this example is three, meaning that when you enter three contribution size, it will auto place, as I was saying, the three separate. So in this case, the total is three, one, two and three. So one, one, one means three total size so you can see here the count is three for the levels but it could be count three but you could have two two two meaning six could be four four four okay that's it i hope that it's clear make sure when you finish setting up your exit

[14:40] strategies you save it by clicking on the star okay so here you have a little star let's say you've created the one previously and then you you clicked add then it'll come to the screen and then here before you you have to press stop when you press stop here because it will be activated when you press stop then you'll be able to save it as a star and if you want to change or adjust you can just press the wheel the settings will and just you know change let's say oh i don't really like 12 ticks i want to do 8 or i don't really want to do 12 i want to do 16

[15:11] so you can or you know 2020 you can you want to do one to one so you can just adjust and then save it again okay here are a few that i use so you've've got 3060. This one is nice for the EQ. This one can be used for the ES. This one is multiple. So this one is like 2222. Very good for when you're trading with the trend and you want to just add and reduce, add and reduce. I'll use that one, the multiple. And then this one is like for breakouts, like when I'm trading and I want to just literally take, you know, let's say there's a momentum trade.

[15:45] You know, I'll just set this one. It literally just takes me in and then the market is moving up or down and I'll literally just get out. So, yeah, it's a good I use. Okay. Now, now that you understand the ES contract size, the tick value of, obviously, the whole mechanics and how it works, like the 50 times the index price, etc., the tick value, you know, the 1250. And also how to implement an exit strategy, which is, in my opinion, super, super, super, super important. The next thing to do is to enter and manage a trade. For this, select the account,

[16:36] set the position size, start the exit strategy by pressing that play button and wait for the trade to come to you. When it does, all you have to do is press market buy or sell or preset with limit and the rest is all automated, including the reduced stop loss size change when you take profit is hit and you have the multiple strategy selected so by doing the auto and exit strategy your trade is pretty much being managed automatically all you have to do is just be patient wait for your levels get in and watch it play out because your risk to reward will be built in to your strategy.

[17:28] And then you can just manage by dragging slightly. But I would highly recommend you keeping it consistent to your stop loss and take profits because that's the whole purpose of having a nice risk to reward. OK, otherwise your profitability will be skewed tips and tricks in attas save multiple exit strategies for different pairs and sessions for example one strategy using less ticks if you're scalping during the am session for example during the end session you have low volatility so you want to lock your

[18:06] profits quicker so instead of going 20 ticks go 10 or more ticks if longer trades when trading during new york session as there is more volatility so during the new york session you could say okay i'll go for 20 because you know price is moving a bit faster a bit quick to get there so you can do 20 ticks relatively quicker you know when it compared to the am-am session keep your position sizes consistent this is so important okay this will help you a lot as each day you trade you are repeating this over and over again before you know exactly the average win and loss per trade.

[18:49] This is so important. Just keep this. Just do this religiously for a long time because then you're going to learn so much about yourself and then you can increase over time. But don't do it straight away. Stay with one, stay with one for months. Go to two, stay with two for months. This is a long term game. Okay. This is a profession. This is, you know, this is your business. So you want to go slow and slow and grow and grow. You don't want to go from here to there within a week. You want to just

[19:26] keep consistent, small. Don't worry about the end goal. Just keep going slow, slow and steady. So position sizes, keep them consistent. Then you're going to have no surprises because you just know because you do this over and over again. So, repeat business. You want to achieve consistency. Therefore, the use of an exit strategy is a must. This way, you are always protected from the get-go. There's been times where I enter a trade and price is moving so quick that by the time I put my stop loss in place, it's moved two or three ticks.

[20:11] And you know that two or three ticks is a little bit on the yes. Therefore, depending if you're trading with four contracts or six, that can that can hurt you. So if you're using an exit strategy, it will always just place it in for you the moment that trade gets filled. And look, for a trade to get filled and you've got at least minimum eight ticks, well, the slippage, it's going to be hard to see an eight ticks slippage. It can happen, but it's very, very low probability. So using an exit strategy, you are pretty much guaranteed that you're always going to have that stop-loss in place.

[20:51] It's a protection mechanism, okay? Must be used. If you want to be consistent, if you want to be in this game for a long time, if you want this to be your bread and butter, just use an exit strategy. Like other softwares, not just Atis, other softwares have this. They call it brackets, even TradingView. Okay, so it's there for a reason. And some softwares, some companies actually make you use it. Okay, always ensure to have the correct account. Super, super important. The number of contracts as well, very important.

[21:25] And strategy selected before entering the position. Okay, so here, this is super important. Make sure on, this is on level two or on your trading terminal. So just press T and it'll come up on your address. Make sure you have the correct account because sometimes you might enter a position and then you go back and say, oh man, that was on a demo account. make sure you have the correct account because sometimes you might enter a position

[21:51] and then you go back and says oh man that was on a demo account or you enter a position on

[22:12] account a and you wanted it on account b so before you enter make sure the drop down is select on the correct account. And then if you're using, say, for example, a multiple and each multiple is 1, 1, 1, then put 3.

[22:17] Or you could start entering 1, 1, 1, make 3, but make 3 limits laddered in.

[22:30] Okay, so you could go, instead of doing number 3, you could go one, one, one. And once those three positions are fully entered, you're still going to have those three take profits. OK.

[22:34] And then finally, make sure it is enabled. OK.

[22:39] And it will tell you exactly how many positions you've got open there once you press play. OK. Thank you for watching.

[22:48] A live stream covering the questions and answers about this session will be hosted.

[22:54] You can submit your questions for the live stream via the Discord in session questions.

[23:00] For additional Q&A, head over to the Trading Help Forum in Discord. And thank you for watching. See you in the next one.
