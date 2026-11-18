---
date: course-lesson
champ: unattributed
course: Atas
lesson_no: 2
title: ATAS Stocks
slug: atas-stocks
category: courses/atas
cues: 476
words: 7892
duration: 44:54
source: https://portal.chartchampions.com/lesson/atas-stocks
captured: 2026-11-16
---

# ATAS Stocks

## Portal Summary & Highlights

This session provides a comprehensive walkthrough of the Atas trading platform, covering its setup, configuration, and key features for trading futures, stocks, and crypto markets. We learn how to configure data feeds, brokers, templates, and workspaces, along with practical applications of order flow analysis, depth of market (DOM) functionality, exit strategies, and conditional orders. The content emphasizes building an efficient trading environment that integrates multiple markets and advanced order flow tools into one platform.

### Highlights

- [00:00] Introduction to Atas as a one-stop-shop platform supporting crypto, stock indices (ES, Nasdaq), and CFDs, with the ability to use the data feed independently of broker connections.
- [01:11] Overview of key Atas features:
  - Trade from chart capability
  - TPO Quick Tool for market profile analysis
  - Exit strategy automation Ñ auto-populates take profits and stop losses, reducing size as targets are hit
- [02:40] Agenda overview covering connections, multiple accounts, cluster settings, tick sizes, hotkeys, OCO orders, templates, workspaces, volume profile, smart tape, and DOM configuration.
- [03:38] Setting up the Rhythmic data feed for CME real-time Chicago data Ñ approximately $20-25/month, essential for accessing futures market data.
- [04:47] Adding broker and exchange connections (Bybit, FTX, Bitmex, etc.). Important practice: label each account with a descriptive portfolio name (e.g., Swing Short, Swing Long, Scalps) to avoid confusion when trading.
- [05:49] Broker options discussed including Optimus Futures and AMP, with guidance on selecting fee structures based on expected volume.
- [07:05] Critical initial settings adjustments:
  - Change buffer setting from 70 to 68
  - Set DOM levels count to 500 to show more order book depth
  - Configure time zones per market for accurate session alignment
- [08:10] Hotkey configuration Ñ setting B for buy market and S for sell market, with integration options for Stream Deck using symbolic keys (bull/bear/cancel) for faster execution.
- [09:18] Sound alert management Ñ disabling defaults and configuring a subtle fill notification for better execution feedback.
- [10:05] Template installation workflow Ñ copying chart templates, smart tape templates, DOM templates, and workspaces into the appropriate Atas folders.
- [13:30] Loading and setting a default chart template, with instructions on importing templates individually when bulk file transfer is not preferred.
- [15:08] Configuring the Fixed TPO Profile (Ctrl+F3) to display: Each profile is labeled accordingly (D for day, W for week) and positioned on the chart.
  - Previous day profile
  - Previous week profile
  - Previous month profile (optional)
- [18:30] Setting lookback periods Ñ using at least 15 days to properly display previous week data.
- [20:00] Tick size guidance by market:
  - Bitcoin/crypto: 10-20 ticks (up to 50 for broader ranges)
  - Stocks/futures: tick 1 for precision, or tick 10 for a cleaner visual similar to crypto
- [20:45] Example of a single print on the chart being filled, demonstrating how previous market profile levels can act as price magnets.
- [22:40] Setting up a multi-chart workspace with higher timeframes (1-hour) for daily, weekly, and monthly pivots, highs, and lows.
- [23:20] Using fractals as liquidity lines Ñ multiple overlapping high/low lines indicate liquidity pools price may seek to take.
- [25:19] Chart navigation tips Ñ using Shift/Ctrl keys with the mouse wheel to zoom into cluster details without losing overall context.
- [26:19] Loading and positioning the Smart DOM template for order book visualization.
- [27:08] Smart Tape setup Ñ displaying price, volume, and whether trades occurred at the bid or ask to confirm order book activity.
- [28:00] Using the Freeze feature on the tape to pause and analyze order flow at specific price levels.
- [29:02] Filtering tape by minimum volume (e.g., 10 contracts) to focus on significant orders and reduce noise.
- [29:33] Creating a multi-level exit strategy:
  - Three protection levels with equal quantity distribution (e.g., 2 contracts each for a 6-contract position)
  - Take profits at 15, 30, and 45 ticks
  - Single stop loss across all levels
  - Saving as favorites per instrument (ES, BTC) for quick deployment
- [32:19] Importance of the padlock feature on live accounts to prevent accidental order submission from clicks on the trading panel.
- [34:23] Managing orders directly from the DOM Ñ dragging limit orders to new price levels and canceling with one click.
- [37:00] Depth changes column analysis:
  - Monitoring whether contracts are being added or pulled from the order book
  - Pulled orders at a support level suggest reduced likelihood of a hold
  - Stacking sell-side orders can indicate levels where price may be pushed through
- [39:00] Cluster mode visualization Ñ using bid/ask centered display with heat map by volume proportion for cleaner identification of imbalances and volume concentrations.
- [40:20] Correlating cluster print numbers with live order book activity Ñ observing executed trades populate in real time within the volume cluster.
- [41:46] OCO (One Cancels the Other) orders explained:
  - Entering a position, then setting both a stop loss and a limit take profit
  - Once one order executes, the other automatically cancels
  - Functions as a conditional risk/reward structure for manual trade management
- [43:30] Platform includes a full suite of charting tools (channels, Fibonacci, time-based tools) comparable to major platforms, consolidated into one environment for efficient workflow.

We encourage reviewing the setup carefully, practicing on a demo account first, and applying each feature with proper risk management before deploying in live markets.

## Transcript

[0:00] today is attis yeah so i'm excited for this one why attis and you guys know guys and girls obviously you all know i love attis and the reason why i like attis it's because for many months years i've been looking for a one-stop shop okay because you know when you're trading you want to be efficient like a like a business you want to be super efficient down to business this is where the tools are and let's use them basically okay so for me it's because it's easy to connect to crypto but also the stock market okay es you know nasdaq

[0:37] like really really good trading those markets and it's all in one platform. The ability to see order flow on these big markets and the use of this information to aid us, OK, to aid trade crypto, the CFDs, like, for example, on SPX, you know, on Oanda. Like even if you don't want to have, like, for example, a broker connected to Atas, you can use the feed, pay for the feed and have that real-time information okay and then use that information to trade the cfds if you don't want to go through the whole hustle

[1:11] of setting up so many many options here and crypto also perhaps one of my favorites trade from chart amazing tools like the tpo quick tool it just beats you know exo pro you know you can't even compare it like oh cool that tool is um and quick shortcuts and then as exit strategy like it basically you set like say you want to do five contracts you can set that exit strategy which means that it will auto populate your take profits your stop loss as soon as one like let's say the stop loss actually hits first you'll just

[1:54] remove all three or four depending on how many levels you have or if one take profit hits it will reduce the size it's just just really really incredible so during this lesson i'll be going over some of the key features available atters that i find useful and look i said i find useful there's a lot of stuff i don't use i don't look at it and it's basically just what i actually find useful and use on a daily basis okay and i think you should know before you start using atters there are a couple of other streams related to this one in fact I think there's more than

[2:28] two already so do go back watch them but this one will be good because it's a cleaning store I'll talk you through so this could be the one-stop shop for you to come and watch okay the aim of these streams or these series or you know maybe we'll do two or three depends is to prepare you all with an easy and complete setup that just works and I love that because it just works so we're gonna go over connections multiple multiple accounts groups it's more advanced the available brokers that, the cluster settings, tick sizes, hotkeys, exit strategy.

[3:09] Really, really, really, really, really good. Important. OCO orders. One cancels the other. Templates, workspace, sounds. I like the sounds. It's a bit geeky, but I like them. Tools, indicators, global line. My favorite. Volume profile, daily, weekly, monthly. Smart tapes, orders, made. Threes, freeze. Line, my favourite. Volume by file, daily, weekly, monthly. Smart tapes, orders made. Threes, freeze, really, really cool. DOM, depth of market, or level two order book. But for current orders, I'll remove the lock position.

[3:37] Very important. Can you see, there's so much in one software. It's absolutely incredible. Trade, edit in the dom as well i'll show you that go to feeds and then you can add this rhythmic one which is up here okay make an account with them go to the rhythmic website which is i'll show you very quickly here so you can download this software Rhythmiq make a quick account download this trader put on your desktop okay which is like something like this okay and then once you have all that set up you will have access okay you're gonna have a

[4:19] username and password for that and then you can go back into Atas okay add the connection rhythmic okay so go next and then you'll put your username password and log on to the paper account after 30 days if you wish to stay with them and pay you then have the option to actually connect to it instead of paper paper trading you actually trade okay so um and then you hit finish and then that's it you've got the rhythmic okay there are other brokers obviously and then obviously you add your by bit let's say you want um you know like

[4:53] these are um for the year there you go crypto so ftx they've added just recently so you can add your ftx account your buy bit betrays bitmex you know whatever or many however many accounts you want from the same so just be sure when you add the buy bit account, your Bybit, Bitrex, Bitmex, you know, whatever, or many, however many accounts you want from the same. So just be sure when you add the Bybit account, for example, you add, for example, a description. So the portfolio name, so that when you're trading, you know exactly what

[5:15] account. So for example, I have one for Swing Short, Swing Scalps, Swing Longs,, swing, you know, like you can plug in four, five or six. OK, Rhythmic will be for you to get the real data. So the Chicago data feed. OK, so you actually get the feed, but then you're going to need a broker. OK, and the broker is where something like Bybit. OK, for example.

[5:49] So there are a couple of, well, a few options.

[5:55] For example, if you go to brokers, you've got the Optimus Futures, Extant, but there's others.

[6:02] And currently, OK, we are trying to get a deal with AMP, A for example you can select the minimum deposit or if you just want to go with a flat rate the the fees change but say if your example you want to do volume okay then you select the data feed readmik which we already connected for example and it's only twenty to twenty five dollars per month okay so that's not bad at all for what you get out of it okay and then obviously that platform we're going to be using is atta you know obviously there's others but you know because we're using attas there's attas

[6:35] and that's the fees everything obviously attas you're going to get a really good deal to be honest i think it's really cheap even the even the the unlimited or whatever like the the lifetime one it's i mean fifteen hundred dollars or whatever i mean i think it's really like cheap like you know yeah cheap cheap cheap and then those are all the markets and the fees depending on what or much volume in my opinion all very accessible all very good right so settings very very very important this

[7:05] should be on 70 there you go so 70 obviously you want to use 68 so always change this to start off with and also i change the levels count to 500 this is the depth of market and it's going to show you not just one row but many rows because sometimes when you open this from default it's going to be just showing like one or two okay not good so go 500 back and then you'll have more data so those are the first two things we should change and then there's the time zones and that will show you for

[7:38] each market so if you're going on by bit at the moment we've got plus one so just go plus one and the time will auto adjust okay so if we go for the futures so here cme you go times one okay that one as well so we can actually put these time one and then once the daytime saving lights go back then you can change it back but that that should be enough so now when you open the chart it should be for example in my case 1946 rather than 1646 so that's how you do it okay hotkeys these i like to go just down to the charting okay and at the end you have

[8:20] here there you go i like to go with the buy market you can just click on this and go b and then the sell market obviously s okay because what i have and yes that's the next on the agenda is a stream deck okay and this is how i've got it so i think i've showed this before but it's really cool because on the on the far right i've got and i've been using this all the time i have like a bear for the cell and then i've got it keyed in so that it hits cell and then i have a bull for the buy and then a c for the cancel okay so i've got the three keys c being in

[8:56] the middle so cancel all here which means it will close sorry yeah cancel all it means it just closes everything okay like cancelling everything from the chart, so just go close, okay, and that's it, I think from here, we're pretty much ready, okay, hotkeys, sounds, yeah, sounds, these sounds, I always disable all of them, and there's one, which is like, because these can be quite annoying, but there's a couple, which is just like a little sound. And I just go into the actual system and enable and change the file.

[9:30] Okay, so that it goes every time the order is filled, it goes instead of like order filled because it's so annoying. So come here, disable all of them and just change them so you can go into the actual folder and then select whatever. There's one which is just like a little like a little sort of like alert like did it and it that's what i like because it means like for example if you're on trading view and you've put the order you can actually hear it going to do it and you know okay yeah it's filled let's go to home and then let's go to chat but

[10:03] before we do that when you start the program you're going to have this so let's uh let's go to home and then let's go to chart but before we do that when you start the program you're going to have this so let's let's open BTC okay and you're going to have this chart and you're going to have these settings and it's just too much yeah not no bueno so what I like to do is I like to just click anywhere in here on this template okay go to settings sorry click on there go to settings okay and then just change anything but on this

[10:36] one there you go this one so right click go to this profile settings and just go extend lines okay and click Save that's it that's all you have to do and then ignore it because what we're going to do now we're going to bring up the folder where it is so in my case it is in this computer documents and it goes into this folder here so at us so what i'm going to do is i'm going to go to chart templates and then on that folder that I sent you guys, just select all of those, okay, right click, copy, and then paste them in there.

[11:15] Okay, very simply, there you go, all the templates in one go. Close that, go back, and then go back again, and then smart tape down here oh it's not here I forgot to do this one doesn't matter we can change them and it's probably good that you can see that but I think I do have it on the old they go let me see smart tape templates okay it's there take thatpe by Igor. So I'll send, I'll put this tape in a minute. Copy, go back, back and let's go back to the latest one. So Atters and Smart Tape and we'll paste it in there. Okay, so you can see exactly

[12:03] what I'm doing and then we're going to go smart DOM. Okay. And then there they are. So go back to the folder, smart DOM, templates, and then just drag it into that folder. So now we've done DOM, templates, okay, and the workspace we don't have to do just as of yet. We can go back on the main folder. OK. Workspaces. And workspaces. I've got CME. You can just drop that in there. OK. And then we can go back again. And then because we clicked on this file here, right clicked and extended and saved saved this settings folder is now appeared

[12:45] with that so what we're going to do is use the setting folders I've sent to you guys and girls and traders and then just copy this and paste in there replace that one okay and that's it so now we have all the files set up, all the templates set up and settings. Artkeys have already done that. OK, so we don't need any of that. So, well, that's not too bad. 25 minutes to go through the whole setup. OK, and that's pretty much no one can rewind. I'm sure you can watch it back later, the stream, and I'm sure just maybe,

[13:26] I don't know, maybe the team can help you. But anyway, so here we go. So now we can close this chart and go to the chart here, okay, and go to templates. Okay, so let's open this up and let's open BTC, okay, again. And once the the chart comes we're going to go to right click on this we're going to go to settings and we're going to go templates and we're going to open like the stocks one let's go stocks 13 and then do this one as a default so whatever whatever template you like from these that are the

[14:07] favorite or when you are really good at it and you can make your own templates say you don't need open interest or whatever you can save your own okay and then click on on the one that is your favorite and then you click the world here default that means that every time you open a new template it's going to pop that one up okay so that's how you do it okay if you can't for some reason drop the files or you know you don't know what folder you get confused just have the folder on your desktop and come here and do import and do one by one one by one okay

[14:41] and then it's the same you know it's the same principle so now we can load this template so would you like to load and there you go so now you're going to have the same template as i do and what we're going to do now we're going to set it up as pair my favorite and then again you change it how you like it okay so this is BTC let's delete this one here let me just make sure I've got time for everything so exit okay we'll do that in a minute templates workspaces and sounds okay yeah we could we going

[15:18] through quite quick so okay let's add one profile here for the previous day. Okay, so to do that, we'll go fixed TPO and profile, or you can press control F3. Okay, and there you go. So I've got it extended. So if you right click on this, so click once and then right click, you'll have the settings. Okay, so this is really cool because what you can do in here is the same thing as you do with exo albeit you know exo is a little bit more simple but here it's exactly the same it does the same job so what you do is you have the period so in

[15:59] this case i've got last day but you can then go last week current week last month okay and the only thing you have to change in here is the name okay so i'll leave the last day and i'm going to add another one to show you how you can then add previous week and previous month so let's go add another one so click on it again and now it's going to be there. So if you click once, drag it, click once until this little dot appears, can you see? And then drag it. Oh, there you go. Come on. I'll just right click and go left, location left. There you go. And it should

[16:40] go to the left, and the other one should be here it should do this there you go it should be doing that on your system so click when you see all blue then drag it so let's change this one on this side to left and then we want to go left to right look at the profile left to right okay and then in the text here we choose last week okay and then we can just go down slightly and here just go instead of d put w previous week and then just for the vowels do the same thing so week and previous week and then just close don't save as default because you always want to have just

[17:23] previous day there okay so just close it now you're gonna have previous day and the previous week be sure to always go if you want to see more than 14 days so two weeks yeah two weeks to see the previous week auto 15 and load okay there you go it's back so now you have previous day point of control previous week but hi all the values should be showing okay it's really good

[17:59] right so now you've got previous week previous day and if you want go back into the same or just press ctrl f3 okay and go and do fixed tpn profile okay it's going to have another one here just move it across uh come on let me just try let me try there you go there you go i managed to grab it and then you can go instead of last day or you can go last month okay and then you've got last month and then just go in here and you can change the colors as well to reflect last month but we don't need that let's delete it

[18:35] okay so so now we have like a chart for previous day with order flow and we'll just keep it here. OK, let's just keep it there. Let's just minimize this a little bit. And then we're going to go back into the chart. We're going to open a new one. And I think we're going to do this on ES. OK, because then we OK okay let's just let's just load ES so this is the actual ES and this is the micro so let's load the micros okay there we go that's all the nice stuff that we're going to work with and then we're going to open a new one okay so here micros

[19:27] which is going to be the same chart because we've told the system to always go back and use that same one okay and now what we're going to do we're going to load a new template but before we do that go to settings and the templates that we've just put in, unselect all of these, the system ones, the ones that come by default from Atas. So take all the stars away, do that on templates and also do that on the snapshots like this.

[19:59] Okay, so that's all done. And now you can go and just add these. So that one, that one, that one that one that one that one that one the view apps classic my favorite this one pivots swing eyes and swing lows ccdr obviously clusters white chart stocks there and that's it okay so now what we're going to do we're going to do the classic so classic daily TPO chart okay that's six let's earn this one okay and then let's go to 30 minutes so this is the classic classic always on a 30 minute and let's delete

[20:35] that one remember and is there one no so no in the side and let's go back at least let's go um let's go 20 days yeah just 20 days is enough and then let's go tick yeah i want to talk about the ticks right for btc for bitcoin crypto depending on the on the pair um bitcoin is higher ticks always use like currently 10 to 20 okay you can even go 50 but 50 is going to be like quite a big range but for stocks it has to be tick one okay you can put tick one on this template you can actually go like tick 10 and it would look really good as well so

[21:20] i'll show you tick 10 what it looks like okay it looks just like the good old bitcoin chart okay or you can go take one which is the actual true actually you can leave it on auto and it'll go to tick one okay and down here this is what i wanted to show you look absolute perfect this is a single print that it came down to and I was watching this on the left. Oh, it was just incredible to see this actually play out. So don't think for a second that this process is a waste of time because it is not.

[21:56] Because this is a gold mine for paper trades, obviously. Let's hide the trading panel. Let's's just look can you see what i'm doing you drag the chart and then you're going to drop it until you see this wheel here in the middle can you all see the wheel yes okay so where would we like to put this one i like it on the left okay and then we're gonna go back to our chart chart and we're gonna open a new one so this is the micros that's the micros let's open a new one for the micros and have at least three okay for this last one I

[22:38] like to have the year it's like a liquidity chart that I like they go previous they high this is incredible like from a single print so it filled a single print and came all the way to previous day high incredible and i had a one-on-one earlier and i said that exactly so it's like exactly as predicted anyway um so let's go to on this one let's go to what one hour okay i want to do one hour on this chart and i'm going to load the chart, which is this one here. So it's going to give us daily pivots, weekly pivots

[23:12] and monthly pivots. Okay. And it's going to have the highs and the lows. Now, here's the good thing about these highs and lows. You all know about the fractals right and because we're looking at previous month let's go 60 days okay 60 days let's go 60 days see it's even very quick I mean that was really quick and I can see already a first batch of questions coming through so there you go so that's the micros we're above the high we can actually get rid of this one okay and what we're going to do well actually what i was going to explain is that

[23:53] this template okay you have each orange line and each green line is actual fractals. Remember the fractals? So these are fractals, but I made them in a line. So instead of having a little arrow, it's a line. And when you see loads of them together, there's a lot of highs together, meaning price will go and take them.

[24:28] the highs together meaning price will go and take them okay so this is just like having a visual aid i like to call aid because it is a visual aid to see where that liquidity is the more lines the more liquidity because there's more highs and even if you disappear they still they're on a chart okay when you expand and show clusters you can can then see, like for example this morning, when price comes below that low and fills the single print. This is not a TPO chart, but single print is right here.

[24:55] Fills it and goes. It's really mind-blowing. I like it a lot. Anyway, so now that we have this last of the the three chart sort of like workspace we can then grab it okay and then we're going to pop it down here so let me just move this one let's just minimize it keep it there on the left so or the right so that you can all see so chart and pop it on the bottom there okay so now let's expand this window very quickly just to have a better better view at it and look because it looks a little bit like compress when you press your

[25:36] shift or control you can press look so you can come down here you can see the single print you can go with your mouse wheel you can go up down really quick look really quick but if you want to go in to this section okay and then go left or right okay you can press you can press the ctrl key i mean this is so cool because you can literally go in there very quickly and see what's going on on those clusters this is not a cluster one it's literally just a C2 single print.

[26:08] But yeah, you can then just go back to normal like this and like so. Smart DOM. Okay, so DOM. And it's the micros. And let's open it up. And there you go. I mean, that is just not nice. I don't like that. So what we're going to do is load the one I've already done. So settings. And let's go to templates and let's go to the one I've shared with you all and just load and okay we're going to go back in here okay we're going to click on it and we're going to put default you don't have to

[26:37] use mine but why not it's already done okay once you have it just push it it's probably going to come up very big but just drag it have it just push it it's probably going to come up very big but just drag it make it like that make it smaller because then we'll go in it we're going to adjust it hold it drag it into here and pop it on the right so now it's there but we need one more thing. We need the tape. OK, so smart tape. We need the micros as well. We need to open it and then we need to go into settings and then we need to go to templates.

[27:16] And then we need to load the one that we dropped in the folder and then we need to open it. And let's put this by default and okay it and all we need to know here is the price and what was the volume traded and if it was on the ask or if it was on the bid okay and that's all the information we need to know because this will tell us what's happening on the book and it should match okay so again we hold it and we drop it here okay you don't have to use this order or this sequence but i kind of like it i think it works

[27:54] for me and there you go so now we pretty much have at us the way i like it and the way i find easy to trade so let's expand this window okay And let's just move this in a little bit. And depending on what screen you have, you can have this like super, super long, but this, I'm just going to show you this feature because it's the freeze. So if you click on freeze, it will stop so that you can actually have a good look. Okay. How many orders were at that price level? Cause like you want to see here, like 35, 6, 48, you want to see them coming through, okay,

[28:30] so you just freeze to make sure if it matches, you know, what's coming in, and you can actually add more rows here, speed or take, but I think it's a bit distracting, like when you add, for example, the speed, like you can add the speed here speed here there but i think it's just too much like on the sell side buy side but you know i prefer to have it off just have it minimal like just down to business you know you want to see that so let's just make this really small there you go you can see the volume and if you don't want to see just one contract being traded

[29:02] or two and the minimum you want to see 10.

[29:08] OK, it's important you know this. Just right click, go settings.

[29:12] OK, and change it. The filters on the minimum just go 10.

[29:18] OK, and now it should go slower because it's going to show you the big orders aggregated.

[29:21] OK, so filter by 10 and should be good to go.

[29:26] So just close that. And there you you go so the tape is done so like for example you can see 10 12 23 you can see all of those orders coming in as price is going higher okay so let's just quickly go um create a new strategy okay we want to do a multiple stop loss and take profit okay and then we just want to set the protection levels okay in this case we're going to add three so one two and three and what we want to do we want to do it per tick so quantity is going to be two because and then you can come in and change it

[29:57] and if it's um if if you like want to go with six you you put two on each. OK, so quantity there, two. On the next one is two and on the next one is two as well. OK, so that means the total in here is six. But if you go in with less size, then it will just obviously close and it's not going to open a position. OK, that makes sense. So stop loss should be the same for all of them.

[30:27] that makes sense so stop loss should be the same for all of them okay so that when you then put an entry or market buy it will be just one stop loss and three take profits so what i like to do let's say just for example let's do on the first one let me just expand this a little bit okay we can go on the level one we can go take profit we can go 15 and then the second one can go 30 and then here can go 45 ticks okay and then we'll just add this one and it's going to come to here it will only be activated if there's no

[31:00] positions okay so now it's activated you can stop now it's activated you can add it to your favorites here by clicking on this little arrow and then you can for example say you want one for six contracts just do es six contracts say you want one for BTC you want to to do 1500 contracts or a million contracts or 500 contracts you can then just specify so when you're trading that pair because this system will allow you to trade all of them you can just select whatever strategy you want to use with that pair

[31:37] so each pair is good and it's unique for each strategy, exit strategy. OK, so use it according or the USDT pair. OK, depending on how you want to use it, what pair you want to trade, just come and do it that way. So now when you press buy market or sell market, it's going to make a stop loss above price if it's sell and three take profits. OK, whatever hits first will either reduce or close the entire position okay um okay so we'll do that in a second uh this is just if you if you um turn this on then you will

[32:18] see for example here the order so you can just use the mouse buttons to sell limit or buy stop okay that's what will happen as soon as you hit so it's really really quick I don't really use that much I prefer to use the stream deck key you can use all of these settings for the crypto with the exception of a couple which I'll change and show you how to go and change it so there's no much that changes but a few changes for example the tick size and um a couple of others but i'll change it i'll show you that in a minute uh but anyway let's look at

[32:57] the dom and i'll for that i'm going to bring the settings up but in here okay you have your current orders for example say but yeah i wanted to just before i just saw in the corner my eye lock yeah if you don't want to burn your account if you if you've got the demo fine go for it test it try because that's what it is for for you to get used to the program and everything but when you connect your real live account okay make sure a you have okay if you're trading stocks then i've made these numbers so one two three five i've also changed this button

[33:34] down here so that you actually see what's selected so can you see 10 there so you know if you send an order through it's going to be 10 you know if you click down here okay to buy it's going to be 10 okay you know if you go market it's going to be 10 so it's there just to say hey mate if you click it's going to go five contracts three contracts okay so that's how you do it if you want to use this for crypto you can change this or you can ask me i can make just a quick one because you

[34:06] can go to design mode and just change the quantity really easy you can just change the quantity on them and instead of being three contracts it you can add 30 000 okay you can do that so as well but for stocks those numbers are really adequate let's put it that way so i've added that there just to show you that if you have for example six, six, you can drop down, go six. And now you've got the same as here. So quantity six, quantity six, they match. Can you see? So it's a match.

[34:34] But if you have your live account, I recommend you go padlock it because then you can click anywhere in here and nothing is going to happen. OK, so make sure you do that just in case because this can, yeah, burn you. So I'm just going to pull up the settings and I'll show you the, there you go. So I've added my orders, which is this column here. So this will show you your orders and you can change them. So for example, say I put in a buy order down here let's put one at 37 what's this down here 3708 so let's do it here so buy limit so six contracts you can see it

[35:18] actually let's just go closer to price let's just go and move it up to up to here there you go can you see your order is to buy six at limit if you click on it like this oh yeah it's locked so if you click on it you can change it so you can drag this to ah there you go there's a lot of people wanting to buy at 3 8 10 let's put it there as well well in fact we might not get filled there so why don't we change it to 8 10 25 so click on it again obviously it moves very quick and then move it higher or click on it again and move it

[35:50] lower so it's a very very very good and quick to do it if you want to cancel that order you can just click cross and the order will go so as you can see it's really advanced and you can do that on the sell side as well so whatever you do here it will show here so those are your orders which you can change so again really cool stuff so in other words you don't even need to trade from there you can trade straight on here and if it's moving too fast you can slow it down by going to settings and on here you can just go speed. You can lower the speed. Okay.

[36:25] Just in case.

[36:29] But I feel this speed is good because it keeps the price in the middle. Okay. So that's much better. So let's just bring the settings back up. And I'll show you the rest of the components. So columns. And I hope I'm not going too fast. But at least you can always pause, watch again um and go for it the stock templates is definitely there uh been shared i just saw that question coming uh okay so that's my orders depth changes so i've got them as these here so this one okay will be the bid changes. OK, which is in the side green.

[37:05] OK, so this will be showing you if it's been contracts added or contracts pulled from the books. Really important, because when you see that number disappear, OK, and you see this number being reduced, then you can gauge the sentiment of the market at that level for example say price is coming down to the previous day or to daily open now and then as it's coming into that daily open all of these orders suddenly start to go from a hundred and six right here and then you see a lot of miners here and all of

[37:41] those orders are being removed well you know it's less likely that it's going to hold because all the participants are just removing the orders it's pulling they're pulling the orders out and inversely if you look at this side the the sell side and you see all of those orders up here stacking like adding so those are the other side okay where they're going to stack those orders up okay by the numbers being positive then you know they're going to push price straight through it so

[38:12] it's basically going to give you the level two where you're going to be looking at your level because you know that the level is is there you can see it like the for example in this case a volume point of control whatever whatever the level is and you can then look at the order book okay what's going on here what's the actual real stuff and then you can do the following which is like really really good you can go with your shift key and then your control key. OK, so both both keys.

[38:47] So like in or you can just right click and go mode clusters. OK, which already that. OK, and just zoom in. So zoom in here and look how crystal clear this is. This is just the if you click on this side here, you've got the bid and ask centered. You can go by volume straights, but bid and ask is my favorite. And I like that they've made this new feature, which is centered, because if you just go bid and ask, it's got a cross in the middle. And this is just really, really cleaned up, like honestly.

[39:19] And you can do the following as well. If you want to have a color scheme i recommend you do heat mark by volume sorry not volume but by a volume proportion there you go i like that one like that okay and the rest stays as is because then it shows you like you know a little bit more whiter where there's more volume and now you've got like um imbalances um okay and you can see everything in there so now what you can do is keep it here in the middle you can see it coming down to previous day high

[39:51] now and you can see these orders so look when you let's put a new candle let's go down to a one minute and just wait and I think those questions we're gonna next week because it's already one hour in.

[40:08] Oh, my God. But let me show you this, because this is like.

[40:16] It's it's what's going to make you really enjoy the order book, because, look, these here. So I've put the sell orders and the buy orders in this order because it's the best order to have them for a trader. It's the best order to have them because for a trader it's the best order to have them because you you want to buy into the into the ask and sell into the bid okay because for a buyer needs a seller for a seller needs a buyer so this way you know that this one will be the sales okay

[40:36] here on the red and this will be the current buys okay so it's really really nice the way it's been set up so i wouldn't change it. I would keep it and I would use it, abuse it and, yeah, have fun with it. Because look at this. Watch this number here populate and watch the numbers here. 27, 30. There, 30, 37, 39, 45. There you go. Can you see? 52, 61. Look, 6, 25. Adding. So like 2 at the top, 5, 6 five six can you see it matches the buy side buy side let's look at the sell side so here 69 4 can you see four at the top four these are the

[41:16] current traders that have gone through the book on that profile on that volume on that candle and you will see them right there so in actual fact you don't actually need to look at the candle because you're looking at it being printed here okay can you see so all of these buys coming through you can see them straight here see look 24 43 these are the current trades going through that book so one cancels the other okay okay? So say, for example, let's top this strategy, okay? And I think I'm going to do it for the sake of it as well.

[41:49] So one cancels the other. If you do one contract, okay, and you go sell, okay? Let's just go, well, let's go buy, okay? So we just buy one contract here. Oh yeah, there you go, two. It doesn't matter, let's go two if you now go to contracts to close that and we go one cancel the other so you've entered your position now you want to set your risk and your reward so go one cancel the other and you can do okay if it goes below this level here well this to sell. Okay. However, if it comes up to daily open, I want to sell limit.

[42:29] So that's it. Take profit. And that's a stop loss. That order now has gone from orange to a normal order. And it means that as soon as one hits, the other one will close. Simple. So that means you can, it's like a conditional order. Okay. So you can go in there, do your trades as normal if you don't want to set, like, for example, a stop loss and take profit. But if you do it this way, OK, if you work out your size, your contracts, et cetera, et cetera, if you do it this way, then that means you don't have to manually put the stop and the take profit.

[43:02] It will just does it. I'll just do it automatically for you. let's just you know I'm gonna squeeze this stop-loss to that point of control to see if it hits and then you'll see it in real time happening let's just squeeze it up a little bit and I'll use the order book so there let's go really close I go should hit now dig it everything's gone so that's really good like that's a really really good feature you don't have to say it anymore you can all nod your heads and say yeah it's really really cool

[43:33] because you can even do channels fibonacci fibonacci time you've got all the tools every single tool even on the access sorry um trading view all of them in here it just needs a little bit of practice i'm not going to say it's easy, okay, but it just needs a bit of practice. Makes trading a lot easier, quicker, efficient. And with that, we as traders have time to do more tasks, i.e. spend time on CC, spend time on making these presentations. It takes time to share the stuff because for me it works.

[44:03] It's easy because I've spent many many hours with this so i'm sure you're going to appreciate the amount of work that's gone into it the templates i mean changing fiddling with all the things here and everything and like getting it all perfect i mean even this dom here just adding all the features it takes time so by having it all set up properly it just means that you have more time to do other things research log whatever you have the time now so give it a go not a goal give it a go

[44:36] load the templates the workspaces because i am sure you like it. You will like it. Okay. And user, abuser. And with that, I'll thank you. And I will see you in a chat.
