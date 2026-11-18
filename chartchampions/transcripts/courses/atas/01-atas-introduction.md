---
date: course-lesson
champ: unattributed
course: Atas
lesson_no: 1
title: ATAS Introduction
slug: atas-introduction
category: courses/atas
cues: 495
words: 8141
duration: 52:38
source: https://portal.chartchampions.com/lesson/atas-introduction
captured: 2026-11-16
---

# ATAS Introduction

## Portal Summary & Highlights

This session provides a practical walkthrough of ATAS and ExoCharts Pro, covering platform setup, multi-account trading configuration, template management, and the application of key order flow and volume profile tools. We learn how to connect exchange APIs, build portfolio groups for simultaneous multi-account execution, configure the DOM, apply hotkeys, and use advanced features such as dynamic profile tools, TPO sessions, and the top-most window for higher timeframe awareness while trading on lower timeframes.

### Highlights

- [00:00] Overview of what can be achieved with ATAS: combining monthly, weekly, and daily charts within one workspace, displaying previous period value areas, points of control, highs, and lows as reference levels.
- [00:44] Setting up multiple API connections for different trading styles:
  - Create separate accounts or subaccounts for swing and scalp strategies
  - Enable position and order permissions when generating API keys (mandatory for order routing)
  - Fund each account independently
- [01:43] Connecting the exchange data feed in ATAS using API key and secret, enabling live data and order execution.
- [02:00] Creating a portfolio group to trade multiple accounts from a single terminal, and setting a ratio value that functions as a multiplier for position size allocation across accounts.
- [05:40] Explanation of how the portfolio group executes trades: a single click simultaneously enters positions across all linked accounts, with exits automatically set as reduce-only limit orders.
- [05:55] Using the ratio multiplier Ñ for example, setting a value of 2 doubles the position size on one account relative to another, enabling tiered risk per account.
- [06:34] Adjusting common settings such as value area percentage (changed from the default 70% to 68%) and configuring the time zone to match the exchange or trading region.
- [09:30] Loading and saving templates, hotkeys, and workspaces to maintain a consistent trading environment and reduce setup time.
- [13:00] Using the Fibonacci retracement tool (hotkey F8), customizing levels (e.g., adding the 66% level), and saving as default.
- [14:20] Demonstration of trading from the chart across multiple accounts:
  - Selecting individual or combined accounts
  - Placing a sell limit that appears simultaneously on all selected accounts
  - Scaling positions via the ratio multiplier
  - Closing all positions across accounts with a single click
- [18:00] Setting up an auto-exit strategy with stop-loss, take-profit, and break-even parameters, including a trailing stop that moves protective levels as price advances.
- [23:20] Discussion of ATAS strengths Ñ multi-account trading, auto-exit strategies, and the TPO + volume profile combined tool for selecting custom ranges and extracting points of control and value areas.
- [25:00] Customizing the DOM to adjust order sizes via design mode, enabling quick-fire execution with preset contract quantities, and observing order book stacking, pulled orders, and absorption at key levels.
- [27:30] Using the Top-Most feature to pin a higher timeframe chart (e.g., weekly TPO) above the main workspace, allowing continuous awareness of major levels while executing on lower timeframes.
- [31:00] Applying the dynamic profile tool to custom ranges, highlighting:
  - The open line as a key intraday reference
  - Alignment of volume POC and TPO POC for stronger confluence
  - Value area high and low as support/resistance zones
- [32:25] Range selection methodology for profile analysis:
  - For downside ranges, select from the first red candle to the last red candle
  - For upside ranges, select from the first to last green candle
  - Extending the range slightly forward reveals the naked point of control for forward price reference
- [34:30] Example application showing how extracted points of control act as resistance on upward moves and support on downward moves, especially when combined with other confluences.
- [36:40] Reviewing multi-VWAP visualization: monthly, weekly, and daily VWAPs displayed simultaneously to assess directional bias and dynamic support/resistance.
- [38:40] Using the open/close TPO template to identify clusters of candle opens and closes, which often align with high-volume nodes and single prints Ñ useful for pinpointing reaction zones.
- [42:00] Importance of configuring TPO session settings correctly before loading session templates; proper configuration ensures accurate session-based profile data.
- [44:30] ExoCharts Pro walkthrough:
  - Clusters, delta, volume, CVD, and open interest
  - Multi-chart workspaces saved for reuse
  - Customizable Fibonacci tools with saveable defaults
  - Dynamic profile tool similar to ATAS
- [46:00] Splitting TPO sessions by daily, weekly, or monthly to analyze accumulation and distribution across different timeframes within a single chart.
- [47:30] Building a multi-window workspace: order flow chart, weekly TPO, and session profile displayed together to track levels across timeframes and align execution with higher timeframe context.
- [49:00] Combining tools and watching for CVD divergence, open interest shifts, and order flow reactions as price approaches identified levels before entering a trade.
- [51:30] Final notes on treating trading as a business Ñ maintaining an organized workspace, well-prepared templates, and a structured environment.

We encourage reviewing these configurations and tools carefully, applying them within a structured plan, and always combining them with disciplined risk management before executing live trades.

## Transcript

[0:00] We're going to be covering ATIS and ExoCharts Pro. Here's just a quick view of what you can potentially have with ATIS. For example, you're looking at a monthly, a weekly and a daily chart. You've got the previous month value at highs, point of controls point controls lows weeklies and and dailies all within one chart which is amazing you can have this saved as a template a workspace and it's it's just really good and so that's just like an overview of what you can achieve once we finish this stream today to install at us you need an account so you can add over to

[0:47] their website and just do a login and password and then that's the that's what the login screen looks like but I think majority of you guys are already using it so I don't think it's going to be a problem on at us we need to create two APIs or three or four or 20, whatever, however many accounts you want. So, for example, for me, I like to have a swing account, a scalp account. It can be done in Bybit, for example, under the subaccounts. You don't have to create a brand new account for that.

[1:17] You can just go into Bybit, create a subaccount, and then just put some money in there. And then you've got the, you know, you can create a key, select the position and order. It's a must. You must do that, otherwise you can't put the orders through. And then you just move, you know, Bitcoin from one account to the other, and then that's it. That's creating the API on the actual Bybit site. And then on Atters, in connections, we can add the provider or the connector, which in this case is going to be Bybit side. And then on Atters, in connections we can add the provider or the

[1:46] connector, which in this case is going to be Bybit, and then on that, that's where we're going to use the API key, the secret, to then load the data feed from Bybit. And with that then you can trade and do all sorts. In accounts, we're going to add a portfolio group for trading in both accounts and we can call it like for example by bit all accounts and set a ratio which is like the same as a leverage but I'll explain a bit more in a minute and then change the values, adjust the

[2:17] time zones if needed, load the settings from the documents folder, load the dome and smart tape and then do some tweaks load and edit hotkeys they're very helpful templates load and save workspaces set a watch list and save as favorites also the API so first let's do the API's and go on to connections okay and here we can disable these two and what you have to do is add the account that you've created on Bybit by the API so on here go to drop down next accept that and then you've got the API and the

[3:00] secret which we're going to paste into here and we're going to call this one CC swing for example okay swing account so that's going to be our swing account I'm just going to move it over to the side and I'm going to paste the key that I have for the swing account already so I'm just going to copy paste the API key okay you don't you can't see that but that's what I'm doing here so you don't have my details you don't want to be doing some trading my account and then you press ok and then now you're gonna have one account

[3:35] what I like to do is then create another one and add another account one for swings and one for scops so we'll do here CC scop okay and then we're just going to move up over here and we're going to put the key and we've got the secret as well and hopefully all is well so finish and now we have two accounts so we wanted this to auto connect and should take a couple of seconds this is very exciting guys and girls. Okay, so now we've got the accounts connected and we can see I moved some change into this for educational purposes only.

[4:34] So it will pull up your two accounts. So you'll have whatever accounts you can trade with. Okay, so we've put some money on there somebody on there just so I can show you guys how to use it the next thing on the list is to okay we've got connector but for group let's go to accounts down here okay and we want to create a portfolio group which is here so once we do this we can call it by bits let's call it all accounts on by bit by bit all accounts and then what you want to do is we want

[5:15] to add the Bitcoin so this is the BTC pair so BTC we want to add that one and then we we will also want to add the scalp one so now you have two in here which means that every time you open a position on one account using the by bit all section in the screen that same click that you do to enter a position it's going to enter a position on both accounts simultaneously at the same time and when you put an order on the other side it'll be a reduced order limit to get out of the position brilliant now here you've got the ratio which is like a leverage so for example

[5:54] if you want to open on your swing account just a thousand contracts you leave it as one it will open a thousand contracts but if you want the scalp account to be twice as much, then you put two. Okay, so let's just now add this and here we go. So you now have an account to trade with all your accounts and you could have a hundred of accounts. And this place here will enable you to trade from one terminal one screen in every single account which is perfect perfecto okay now before we move on to the screen we're going to

[6:34] change the common settings which is up here and this will change the values i think the default is 70 so we want to change this down to 68. The rest stays the same.

[6:50] Time zones, you can go to the exchange where you are trading. So by bit, in my case, we're going to put this to plus one because it's GMT plus one. So we then press apply and it will move forward. And now we have to load the settings in the documents folder. So you could like say you don't want to use my templates or the settings you could just go to home and pull up the chart however because we've got nice templates we might as well just go and get them all set so I'm going to move two

[7:17] folders over to this side and one is the current folder where atters is being created so this is the folder here so what we can do like in here for example on uh well there's no templates there so we would have to go to the normal let's pull up by bit uh btc ust on by bits there let's add it as a favorite BTC USD on Bybit. Let's add it as a favorite already. Something we're going to do later.

[7:53] And it's going to give us this very bright screen, which we don't like. So what we want to do here is minimize that. And on the templates that I shared, which are in here, we can just select them all, copy, and then drop them into the template folders, which is somewhere in here, and paste. That's it. So now, when we load the templates up here, they should all be in here what I like to do is unselect all of the selected already by obviously at us and just select my ones or in this

[8:37] case the ones you want to have and then I also like to unselect the snapshots so let's go ahead and do that I'm trying to do as quick as I can because I anticipate some questions so I'm trying to go as fast as I can and then a good thing to do is to whatever template you have that you like the most, select it, and then do default. That means every time you open up Atas, it's going to bring up that one screen. Like instead of being this white one in the background,

[9:15] it will be the order flow, whatever that you use. So we can now close this,

[9:20] and when we go to templates up here, every single one is here, save as favorites.

[9:26] And then you can just bring up this

[9:34] one tip that we can do now because the next folder it's not curated yet inside the atta so there's no folder here for settings so what we have to do is just click on this indicator here right click change something anything at all and then click on save as default when we do that we're going to have a new folder there called settings there you go that what we're going to do in that folder is drop that's what we just change and it creates the folder so what we're going to do here is we can delete this now, go back to the attest folder, go to settings, pardon me, and just drag all of that in there and replace that folder.

[10:13] Okay, so now when we pull up any of the tools that I've saved, they will be nice.

[10:29] be nice. So let's just quickly pull up for example the F3 tool and there you go exactly as I use. So let's just get rid of that for now. Control F3 just here. Perfect it's all there. Good so all the settings are saved now as well. DOM, so we're going to load the DOM as well. So we can go to the main station here. Come back. And then we go to SmartDOM and then we're going to load the Bybit as well. That's what we'll have have but we don't want to look at this one so we'll do the same we will import into here the same that

[11:15] we just did so let's go to DOM this is the one I use pop it in there you can see how kind of like quickly it is to set it up. And then when we go to templates, it's going to be in there. So what we can do again is set as a default. And then every time you open the DOM, it's going to be using that one in particular. So load, OK, and close. And here we go. So this will be the Dom that we well that I use and it's all being configured so we'll just park that to the side we don't need

[11:53] that now the next one on list which is the hotkeys so I can just go back here hotkeys go back to the folder I hope you can sort of follow what I'm doing here just basically saves a lot of time you know adding all those keys and everything so we just quickly go into there and then once we select here the the keys it will just be these that I like to use but then obviously you can change this as you want or you know and so on and so forth workspaces and save favorites okay so next on the list we've got workspaces

[12:34] so in workspaces here we don't have any so I think this folder here we can probably just go back quickly and just paste those in here okay let's have a look there we go there they are okay so now we've got all of these workspaces as well which has been kind of created for you all. That's how I kind of use it. So that's going to be good. Pretty much here, Atta's is set up. So all the settings we've moved across into the folders are now applied. So, for example, if you were to press F8, you would be able to pull a fib from here down to there and you have this fib

[13:26] obviously you can change it by right clicking and then just coming in here and if you want to add the year the 66 percent instead of like the 70.2 you can do so by changing here and every time you do that you have to save as default and then next time when you when you either go to indicators or drawing tools and instead of like putting the Fibonacci retracements you can just press f8 if you don't like f8 you can go to your hotkeys and change that there i like f8 so that's one of the tools

[14:02] So that's one of the tools. Other tools that we have here.

[14:15] So on Atis, once you have all of this set up, you've got another option, which is to obviously trade on the chart.

[14:21] So, for example, now here on this right, you can choose the account and the contract sizes. So you've got the Swing account and then you've got the Scalp account and you've got the swing account and then you've got the scalp account and you've got the but the year the the combined accounts together so before we put an order let me see if I can just minimize this and pull up the actual accounts on here so you can see that's the account I created and I called it Scalp, which is this one here.

[14:47] So that you can see in real time exactly how I kind of operate with this, because I think it's nice. And then this one is the Swing account. So let's just try and keep everything sort of together so that we can all see. Probably not going to fit everything. Let me just try and minimize here, maybe. I'm sure you can all probably see, but I just want to try and put an order so that you can see it being replicated across all accounts. Let's see if we can somehow. you go that will probably do okay there you go

[15:32] so scalp account which is that one so say you want to open a short position up here you sell limit one contract you can see down here one contract look at that right at the view up and but you want to then open a position instead of like having to log on to that account and then having to log on to that account because you want to put two positions on the same place or you know whatever you want to do you can come here select both accounts and then when you put one here this account will have two contracts because we've

[16:07] multiplied it by two and then this account will have just the one contract because it's just the standard and then you can move it here which will move it there and and so on and so forth so we can cancel this and it's cancelled everywhere which is brilliant so if you want to use the same and do like let's go long by market it will enter market one contract there two contracts here and then now if you wanted then because you're long if you want to move take profits up here you sell one contract here and then this account will be selling two contracts and this position here will be a reduced only and limit as

[16:51] well to exit. Let me just show you down here. Active. Close long limit as you can see which is pretty pretty handy indeed so we can just close this down so now you guys can see why i like at it because of these nice features that we can do with just a few clicks and then here if you wanted to for example we can just close close this which will close all the positions at the same time so like you're not you know if you're scared or if you're trading and you want to close everything in one go if you're using it this method

[17:31] you can close all the positions like simply just like that which is amazing okay so now what we can show you is also on maybe old coins we can use for example SHIB on Bybit there you go let's add it to favorites and then this will be our nice coin that we're doing today and then what we can do is obviously use just one of the accounts. Let's use like the swing account. And I think there's a minimum, so let's put 20. You can set up a strategy that will allow you to buy in and it will trail and do your stop loss and everything.

[18:21] So if you go to plus sign here and you want to add one which is stop loss take profit and break even you can select that and then you can then select the the parameters i like to do well on on this one it would have to be quite a larger number because it's so volatile but let's do 100 to 300 that's a three to one and then let's change this to 10 and this could stay at 1 maybe a bit more 25 okay and that will be our our strategy so say I mean obviously we don't want to buy up here but this is just for you know educational purposes so if we wanted to buy market and you wanted to get in why

[19:06] did it fail and then our us VT oh yeah that's because we don't have any us VT let's try yes that's of course let's try on the normal bit Bitcoin pair it works the same but yes so in this case it's because I don't have any USDT on that account but never nevertheless let's go on to Bitcoin I like to use this tool mainly for altcoins Bitcoin not so much so probably have to change slightly here again so let's just go back to it trailing let's do take profit let's do 150 stop loss 20

[19:48] you can play with these numbers you can measure the move and and then change them all trailing step like be 50 train stop 10 okay add and then it's active and it will only start when there's no positions open so if we just put on the swing account if we short market it will then short the 20 contracts it will set a stop-loss literally just above it obviously you can increase the ticks for the take profits but that would be you know a four to one is it and then as it starts moving it will also move your stop loss and and take

[20:32] profit and just keep moving now but let's see now don't understand what buy stop. Let's have a look here what's happening. I mainly use this on all coins. But you can see you've got your position. Let me just minimize this so we can see better be nice to see if we could actually get it

[21:16] anyway you get the point but we can close this now and just move on to the next next slides I'll probably cancel this close as Bitcoin is just stuck there I don't understand why the conditional yeah I don't think this is working properly anyway to be honest this is an epic fail because the stop should have been above and not there because we shorted this so I don't know exactly what I chose here I think I moved the the settings so if I stop this yeah on here because I use it mainly on

[22:03] altcoins I think I moved so this if I stop and if I go into here into settings yeah they go watch yeah those are the wrong way around so on old coins it's a lot easier to use but it's an option it's a it's a strategy that you can use very well so let's just close that down I'll probably do another stream just dedicated that that setting and how to use it but the main purpose of this one was purely to do like the the multiple accounts trading from one terminal only which is much better okay so you see this one as well as open and

[22:44] for example if you wanted to just like a lot of positions, for example, let's just put none here. Otherwise, that's going to break again on like all of the accounts and trade from chart. You could go, for example, you wanted to buy. You could go, okay, like below the low. You could go 1, 2, 3, 4, 5. And this would go between obviously this account the other account as well so everything would be set there to buy below the low if you wanted to cancel it all you would cancel all it would cancel in one side it would cancel it in the other side so you can see

[23:17] that one there so you can see how nice and powerful this is on atas i like obviously we've got the clusters delta volume cvd open interest unfortunately there's no ability to to save so it will always just tell you what the open interest is on that candle but not for the rest of the candles prior to those so if you change from time frame of five minutes to a 10 minutes or volume, it will just delete the data, which is really annoying. That's where ExoCharts really stands out. So the ability to obviously trade from multiple accounts like you just saw there.

[23:58] The auto exit strategies. However, that one there was an epic fail because i i swapped the two slides uh the two positions across like um you know vice versa um but it's a good one to have with all coins it works really well um so the tpo profile combined tool it's my favorite which is the one that we like i'll show in a second again but it's the one that you can just select the ranges and that for me is like a Goldmine because you can really get in there and choose You know the point of controls the value areas

[24:32] And then another one I like is the topmost Tool I'll show you guys in a second as well the the layout obviously for switching between tabs This is a new feature. They've got

[24:52] workspaces for all the charts templates, USDT, USD, altcoins and other chart types and then on the chart dome I'm going to show you because some member asked about the changing the sizes so I'll show you that very quickly on the Dom which I think it's here so on the Dom if you wanted to change these sizes up here what you do is you right click and then you go on to design mode and then you click on the position so for example this one will be 25,000 contracts. If you wanted this to be one contract or five, ten

[25:28] contracts, you can just do that, right click, and then it will be ten contracts. So you can trade from here, however I don't do it, I've got it all locked, but you can see, you know, all the orders being stacked and pulled, like this side, the bid removed, you can see all the the pull orders, all the orders being stacked and pulled like this side the bid removed you can see all the the the pull orders the limit orders being taken away here you can see all those those with a minus you can clear it all just

[25:55] see exactly what's happening at the level I've configured this one so that the year the timing is always following price so even when it really rapidly, it will try and stay on the screen. And some of the other templates I've seen, they're like the default ones. They just do not. So I've configured this one. I spent hours getting this perfect because Bitcoin, obviously, as you guys know, moved $10, $15 like really rapidly. And with the settings as is, you can really follow the price.

[26:24] You can see the order stacking here so look there you go yeah very impressive and then obviously you can clear and clean them all and it goes clean and then you can see exactly what's happening again just to take out the noise or what just happened but yeah that's how you change the the values of these settings so you go into design mode you select what you want to change and you put 25 or whatever number you want right click again design and there you go so as soon as you click that figure it will show here or if you want to just type in manually what you want like 20

[27:07] 21 or or two contracts or whatever you can just do it manually here on this column but that's the order book there and then I was going to talk about the top most figure feature which is this one. So on here, let's take this off. There's a feature called this one here, which is called top most. And if I open a new chart for Bitcoin,

[27:41] and then this time I select a higher timeframe,

[27:49] a weekly TPO okay and I don't want to I want to trade on the back but look at the window is gone so that can be quite annoying because you'll be swapping around so let's just click this tool here which means you can now sort this tool out. It's probably showing only three days so let's change this to 90 so we like a few weeks. Let's wait for it to load. Now you've got more data so here you can now see 30 minute okay so you can now see when this is in full screen up here and you want to just keep an

[28:34] eye on the higher picture where you are for example right now you're approaching this big point of control down here at 57 400 so it is likely price obviously dumping into there but you are prepared you can see the higher time frame level just below you where if you were trading down here you wouldn't be able to see it as much for example if you wanted to see the clusters you could go into the clusters and you'd be like deep down in here but you were aware that on the left hand side you've got the big level incoming

[29:06] and by having this feature when you're working on here you can have the screen anywhere on on on overlapped and you can still trade your thing uh you know with all the accounts you know you can go short long but you can still keep an eye on here and here you can do exactly the same for example if you wanted to just look here but trade on here on the higher time frame you could just keep an eye on your clusters volume Delta and everything and on this side you could pull up the trading window okay you could put by bit

[29:41] all say you wanted to let's do 10, which means one account's going to have 20 contracts. So you just, at this level, you'd be there ready and waiting,

[29:55] or having your alerts, you would be there waiting. And then when you go down here, that's probably a bit far away. Let's just minimize.

[30:09] There you go. There's the order waiting, example if obviously you were confident you had all your ta down your risk management but obviously it's always better to watch for the reaction let the level hit let the the the volume imbalances come in let you know the everything that that you want to see the open interest and then on the retracement obviously buy back or depending on on on on the level you obviously take the trade so let's just cancel this but it's a nice feature this one here so if you don't want to if you

[30:43] click on that now it disappears but having it in front is really really handy so let's just close this and then the other one i wanted to talk about very quickly it's on a 15 minute let's just look back at the moment this is just looking back a few a few days so let's just put 60 days this is my favorite so this one here you can select let's just increase the tick size to let's go 20 here so tick size 20 and now we have a better range to work with so price up here so we can bring up the f3 tool and it's going to give us not just the

[31:36] volume TPO but it will also give us the yet the TPO and the volume point of control as well as this line here. That's why I like this template so much and you can see this is probably my, well it is my favorite because you've got the open line which is like very very good in my opinion. You get really good reactions from it all the time. So you've got the open line and then you've got like the point of control and the TPO point of control very close together on that area there. You've got the value area high and value area low. So for me, this is such a powerful tool.

[32:25] just quickly while I'm here, I might as well select the one hour and show you where I like to select it and I'll keep the candles. So for example, on this range here, I wouldn't just choose like a random pivot to pull this range on this particular range. I would choose the first red candle to the last red candle there. Okay, the reason why i extend it slightly it's because i know it's finished there but when you move forward then you get the naked point of control which is just in front of it extended and that for me it's it's it's incredible that you can actually see

[32:58] like the actual price down there um so that's how i do it and then if it's in the upper range what I would do I would choose the last green one to the last green one so for example on here it would be this I expect it to all be taken out at some point if price comes back up here I don't like that but let's just have a look at one which is slightly different just so I show you this one here so this one here again I would choose probably that range up there maybe I'd go all the way to the back on this one and then just go forward. Let's choose another one

[33:52] just for you to see. We don't have more data. Maybe the one hour is not the best let's go down to the 15 minutes just do a couple more examples okay so up here what I would choose I would choose these two there and then go forward up here I would choose these two there and then go forward and you get all those point of controls which acts as a resistance on the way up and obviously on the bottom it's support on the way down so here would have been this one here there then when you go across you always have reactions at these

[34:49] levels especially if you have you know other confluences together at the area but you can see here you always get like you know a few half a percent moves out of those so for me this is a really good tool you can clean this up by deleting everything on the on here you can just go delete delete delete delete delete delete and everything is clean again on your chart you can draw lines you can do pretty much everything you can do on trading view but I like as I said the open line here the blue one it's actually the medium line which is here yesterday's medium and then you've got the CVD same as you'd have on Exo

[35:32] you've got the volume down here and Delta and then obviously a chart trader so you can see like in one template you have so much information going through now this is the TPO the values here got a little segment here of data missing but below ours you've got 57 500 I like this level you've got the open line there and I think you can pull a feed from here all the way up to the top and let's see what else we have there we've got the 786 just below the TPO value area low hi I mean so you can see how these templates are so powerful when obviously

[36:17] you combine them with other templates for example the levels one this one also is a very good one daily so let's just push this back to 300 days to show you the whole they go here's the 39 500 level being hit earlier.

[36:54] It's just, I just love this one because, you know, you will be ping-ponging from one level to another level,

[36:57] from one level to another level until you reclaim and then go up to the next one. So it's quite good, this one.

[37:04] What else? You've got the VWAPs ups here this one will be showing you the oh look at that that is the monthly view up and then the yellow one it's the weekly so we're below that and then the blue one the daily so I like this one but the day is probably a bit too big like the one hour has to download all data so it's a little bit takes a little bit time but we're literally just coming into the 57 700 so again you can see all of this area here so again look red candle all the way forward into the naked so 54 9 10 there all that area that's because we've got

[37:48] bigger value let's just increase the tick size yeah so 54 54 900 all of this area here will be good and then above that you've got this little one down here

[38:11] which is 56 650 57 650 i mean so all in this area so we've got some support approaching and then let's have a look at another template to build a good case there and then let's have a look at another template to build a good case there this is the classic one let's go to the 30 minutes just let it just slide very quickly and we can do the next one let's just delete this okay there we go you've got a single print on here as well, and then you've got the open close. I'll show you just another template that I like to use which is this one.

[38:52] It's similar, but it shows you the candle open and close as well. No, sorry, not this one, that one, number three. Okay, there we go so this one will be showing you now the open close open close open close open close and open close so here you've got just a random like just a few strong opens and closes all together so this all area here is going to be good support you've got a single print there so again if I put the tool and select all of those ranges it's coming in at the round so you've got volume 56 300 56 250 again it's a nice

[39:44] place to have an alert see the reaction if let's see if we drop down there above us you've got all these single prints and then you've got volume at 60 tpo and volume at around 62 450 and then above you've got another one at 63 950 so these would be all the levels i'd be looking for when I'm trading so what have these on the higher time frame looking for them looking for reactions as they approach you know support and resistance and then I go down to a you know a lot lower template such as the order flow on and

[40:18] I'd be looking down on one minute two minutes or a tr template which i'll show you in a minute on the

[40:33] exo charts um this one for example let me just move this from a when you do uh for example

[40:39] you know a lower time frame always change that otherwise it's going to take quite a while to to load but you could do on a one minute there you. And then if you have the chart on the left, you've got your level, you've got everything marked. You can just be ready. You've got the delta. You've got the CVD. You can just wait for the retracement. Do you get CVD divergence? You know, get in. You've got you've got your orders. You can then just obviously, you know, ladder, ladder, ladder, ladder or, you know, have your stop.

[41:04] Have you stopped below? There you go. Can cancel that. Close. Close all. you know ladder ladder ladder ladder or you know have your stop have your stop below there you go can cancel that close close all you can see how nice and quick it is it's just brilliant this is a view that I like you've got like your normal you know order flow on the left and then you can have your sessions on the right you can have your higher time frames or you know split so it's it's good so recently what i can have

[41:26] sometimes is exo charge with this view on the right of the screen i've got my levels i know i've got the mark down and what i can do then is have at is on the left ready and waiting or the flow dom it's just having an organized organized um computer and screen like for your trading sort of environment where everything kind of like sticks together so let me just show you the next one which is yeah I need to say that so after the installation when you when you finish installing obviously you have to put your token you know sort of like

[41:59] username and password and then you come to this screen which is you can either choose the default one or you can go straight into the templates and import the files. TPO sessions must be done. So some people were having problems saying that it couldn't connect or the candles weren't showing properly, and it's because the sessions, you must add this step. Otherwise, it's not going to look at the right data. So I'll show you how to do that on screen. But you basically have to go to the session, TPO session settings,

[42:36] and then you have to manage the sessions and then add, call it whatever. And then you've got the IS1, IS2, IS3, and IS4. You have to do this, save, and rebuild the chart. Once you do that, then you can load the template, and it will be just like the one below. Exit charts tools. The usual stuff, as you guys know, the clusters, delta, volume, CVD, open interest. Open interest, obviously obviously here at least works really well and unlike uh at us um here you have the ability to have multiple screens in one

[43:13] workspace you can save the workspace as well expand the view etc i'll show you very quickly as well the ability to pull fibs now and save the templates such as CC for example if you want to have the CC fib and you want to have a 786 fib you can save the both you can have them as default so that's a really good addition now to to exit charts the dynamic profile tool is very similar to the one at Atta's however it doesn't have the time yet I don't't think. I think it's just volume.

[43:45] And I've requested Varon to add the extended point of controls and value areas to be extended to the access price on the right. So once he does that, it's going to be even better. It's, yeah, it's my favorite. And then the other one is the ability to split the sessions by daily weekly and monthly really really good so for example we can go on to a normal order flow template just going to look back the data and load shouldn't take long there we go let's expand this as well remove the markers and then very quickly for the the TPO

[44:36] settings for the sessions you just have to go to TPO settings on here it's pretty much the only setting you really have to add the rest is pretty much just on and off click and make it your own you know taste really so TPO settings you've got here you've got chart settings you've got session and then it's manage sessions and once you're in sessions you have to create one or you can add another one you can see I've got loads here delete you can make delete and create but you've got the sessions here that I've

[45:10] created and then it's UTC okay give it a name so that you've got a name on the side and then you choose the start and the finish start the finish start and finish and etc save and. Save and then close. And then here you'll have a window that says rebuild. Obviously I've already got it set on that one, but you click on rebuild and everything will be set. And that's it on that one. So once you go to templates and you load the sessions template, sessions here,

[45:48] you will then have the whole thing working properly there you go it will be just like this

[46:01] on here sometimes what you can do you can go on to the TPO settings and you can go to TPO you can go to volume and you can have this selected to you know more volume calculated okay you all know how I love the sessions I think there was a POC earlier there we go up here you know you always have nice reactions out of these. And it's very similar to the tool, the dynamic tool that we use, obviously, on Natus. Because you can select the lower range of the move. And that's how the sessions were born.

[46:37] Because I was, like, selecting those areas. And I was being aware of the time as well that those were happening. So you always get nice reactions. Love it. Love the sessions. and I was being aware of the time as well that those were happening so you always get nice reactions love it all of the sessions and the times and then now what we can do is load another chart to show you the multi view so add a tab and then you go to chart then you go to templates and you can add say a weekly

[47:08] Add a weekly one

[47:13] So we've got now we've got the sessions and then we've got the weekly So this is a weekly chart and one of my other favorite Things on this is that you can now select by Weekly which is on here and monthly so on the settings this one the TPO is actually split by weekly and you can have it split by month and you can have a split by obviously day this is like really really good really really good it just saves a lot of work I like the weekly the best one. So TPO, settings, split by week, for example. Very good. And now, and you're approaching that point of control down here at 57.250 and 57.350. So that's a region that we want to be aware of as price comes down to that okay now we

[48:07] can open this up and drag it there this one let's just open a new window here and have a template for order flow okay and now you will have three windows to capture your levels in one screen. It's very good indeed. There you go. So obviously you don't need all of the data on here on the order flow, so you can turn this off if you want to, and that one, and you can just follow this, or you can take this off and you've got the point of controls of the sessions you've got the the bigger higher time frame

[48:52] levels you've got the smaller time frame levels and the sessions and then you've got the data down here that you want to look at and it's slight bullish divergence here we can go into the footprint 544 555 yet just slightly just kind of nice to see it's your open interest yeah okay yeah interesting I'd like that though I I'd like lower. Anyway, let's go to the questions because we have now completely let's go through ExoChart and Atis. You know, like say or whatever you choose, you can choose both. I still use both. I use Atis and I use Exo. I still use mainly the web. I'm using a bit more of the desktop pro because the tools and the other additions. Actually, I forgot to mention, I did forget to show you guys very quickly. I just remember the Fib tool.

[50:06] that edge of having all of these different tools all in one place and being able to pull fibs.

[50:11] So yes, I have been using a bit more of the ExoCharge Pro. The markers is a little bit annoying, but Varon is working on that. I use Access to put my orders because otherwise you'd have so many windows open on Bybit, web browsers and so on on so let me just quickly close that and this one just keep one yeah I forgot to show you guys the that's probably not the best best template let's just go dynamic range yep just to show you the other Fib pull tool that we can now have here in ExoCharts Pro.

[50:47] So I'll just do this one here so from here down to the low so now we can pull the Fibs and then if you right click on that you've got all of these different ones and I love that Varon actually put the CC fib built into the software that's pretty pretty niche kind of him and then I've put my own fib there and you can save as a default which one you want to go and use which is pretty good and then the other tools you've got obviously the anchored view app actually I know this is

[51:26] the dynamic that dynamic tool which this one is obviously similar to the one we have on um trading view would be the profile tool and i've asked baron to increase or extend these to the axis so that we actually know what value is of the range selected rather than trying to guess

[51:47] actually know what value is of the range selected rather than trying to guess 63484 or have the actual value and then the view up is here that one okay there you go and you can also change change this one so if you want to just see the view app only, save as the default, etc., etc. So those were the last things I kind of forgot to show that I like to use now. I think that's it. So, yeah. So final words. Keep it all organized. Trading is a business, so let's keep it as a business.

[52:23] Nice environment.

[52:30] Good templates. all the settings and I think any other questions we're always here to help you guys so on that note thank you very much
