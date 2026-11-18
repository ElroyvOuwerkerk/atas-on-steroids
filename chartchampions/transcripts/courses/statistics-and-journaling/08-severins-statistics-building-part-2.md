---
date: course-lesson
champ: Severin
attribution_note: "portal slug severins-statistics-building-part-2"
course: Statistics and Journaling
lesson_no: 8
title: Statistics Building: Part 2
slug: severins-statistics-building-part-2
category: courses/statistics-and-journaling
cues: 207
words: 3083
duration: 19:19
source: https://portal.chartchampions.com/lesson/severins-statistics-building-part-2
captured: 2026-11-16
---

# Statistics Building: Part 2

## Portal Summary & Highlights

This lesson focuses on the preparation phase required before building trading statistics, emphasizing how structured planning ensures reliable data collection and accurate probabilities. We learn how to create a detailed backtesting plan, exclude distorted market conditions such as holidays and daylight-saving time changes, and organize an Excel-based framework for consistent observations. The session also introduces practical examples using the previous day value area low and daily open quartile statistics to illustrate how preparation directly impacts the quality of statistical analysis.

### Highlights

- [00:36] Why preparation matters: Clean data creates reliable probabilities, while poor preparation leads to misleading data and unnecessary losses.
- [01:05] Core reasons to prepare:
  - Ensures consistency in data collection
  - Removes distorted observations
  - Improves statistical accuracy
  - Reduces emotional interpretation by tracking each observation mechanically
- [01:40] What must be prepared: A backtesting plan, a structured document (Excel or notebook), a charting workspace, and marked holidays and session-time changes.
- [02:35] Backtesting plan example Ñ Previous Day Value Area Low (PDVAL): The plan must be documented in detail so that its purpose and rules remain clear over time.
- [03:29] Rules for inclusion in the PDVAL study:
  - Only include days where the daily open occurs inside the previous day's value area
  - Track only the first touch of the PDVAL during the following session
  - Exclude days where the 30-minute opening candle already touches the level, since no valid entry is available
  - Subsequent touches belong to separate statistical studies
- [05:00] Handling holidays: Mark all holidays on the chart to avoid including abnormal sessions. Holidays often produce distorted price action due to reduced liquidity.
- [06:09] Session time changes: Daylight-saving shifts move the New York open between 3:30 and 2:30 CET. Failing to adjust for these changes can severely distort session-based statistics such as the New York open level.
- [07:26] Dedicated workspace setup: Create a separate charting workspace per statistic Ñ for the PDVAL study, use a volume profile chart and clean price action on both 5-minute and 1-minute timeframes.
- [08:33] Tracking fields in the Excel template:
  - Observation count and asset
  - Level/playbook name and timeframe
  - Market context (uptrend, downtrend, ranging)
  - Session (London, New York, etc.)
  - Win/loss classification
  - Deviation above/below the level (in ticks)
  - Move away from the level (in ticks)
- [09:49] Defining a win: Measure deviation in ticks and the follow-through move; a move producing approximately a 2:1 ratio away from the level is classified as a win.
- [10:24] Optional entry tracking: Include 3-candle entries on the 1-minute and 5-minute timeframes (COT, COC, momentum, or no entry) with stop loss and take profit in ticks, allowing automated calculation of winners and losers per session and entry type.
- [12:12] Adjusting the template for different statistics: Context-based studies such as the daily open quartile statistic do not track trades but rather probabilities that support the daily trading plan.
- [13:05] Daily open quartile study rules:
  - Use a Fibonacci retracement with the 0.25 tool to define quartiles of the previous day's range
  - Include only days where the daily open occurs in the upper or lower quartile
  - Record whether price trades above the previous day high or below the previous day low during ETH
  - Do not track session, entry, or risk-reward Ñ purely a contextual probability
- [15:55] Statistics overview: A summary tab aggregates all tracked probabilities and observation counts, providing a consolidated view for market analysis.
- [16:05] Why holidays must be excluded: Reduced liquidity leads to irregular volatility, distorted session behavior, and misleading probabilities.
- [17:08] Why NY open time changes matter: Session opens shift with daylight saving, affecting volatility and liquidity timing; consistent timing produces consistent data.
- [17:39] Building a statistical framework:
  - Define structure before collecting data
  - Apply the same framework to every observation
  - Prioritize consistency over complexity
  - Start each study neutral Ñ let the market prove or disprove the level
  - Keep tracked variables minimal; complexity can be layered in later

We are encouraged to review these preparation principles carefully and apply the same structured framework to every statistic we build, always maintaining disciplined data collection and proper risk management.

## Transcript

[0:00] Hello everyone and welcome back to this contender series covering everything about statistics. In this lesson I will be looking at the preparation before we build a statistic. Before we begin it's important to note the legal trade disclaimer as everything I'm doing in this video is not financial advice and all for educational purposes only. So please read through this video except and only then continue with this video. As always, I will be starting by giving an overview of the utility,

[0:30] moving on to the implementation and as always ending with some tips and tricks. The quality of your statistics depends on the quality of your preparation. Bad preparation leads to misleading data. Misleading data leads to poor decision making. And poor decision making leads to unnecessary losses. Clean data creates reliable probabilities. So let's get into the preparation. Why preparation actually matters. It creates consistency in data collection. We always want to track every single statistic with a consistent approach. It removes distorted

[1:13] observations. Some observations which you will see in the series have to be excluded from the statistics collection. It improves statistical accuracy and it also reduces emotional interpretation. We should build a system when it comes to statistics that entirely removes emotions and we track a statistic like a robot following the same rules for each and every single observation. What we must prepare, and this is where I, for the first time, will also take you over to my Excel file, where we're going to have a look at everything more in depth.

[1:50] We have to create a plan. What am I going to backtest and how? We also have to prepare a document. This could be an Excel sheet, a notebook, or whatever you prefer to use. In this series, we will be looking at an Excel file, which is also going to be available in the vault for statistics building in the most effective way. Also, we have to prepare a trading view or ATAS to exclude holidays, mark time changes, and create a folder for each and every statistic. We are going to look at a real example

[2:24] of the backtesting of a previous day value area low. And with that example, I'm going to show you how to create a backtesting plan. So before we begin, as I already said, we need to know what am I actually going to backtest. So here I'm using the example for a previous day value area law. And for the explanation or your backtesting plan, you want to be as detailed as possible. So even a couple years down the line, you know exactly what, why, and how you are backtesting that specific level.

[3:00] So the explanation for a previous day value area law, I'm just going to go through this exact explanation here because you have to see how detailed that is. In this statistic, we are testing the probability of a reaction at the previous day value area low, PDVAL. This statistic measures the probability of a reaction at the previous day value area low under a specific set of conditions. Here are the rules for inclusion. Only trading days where the daily open occurs inside the previous day's value area are

[3:38] included. If the daily open is outside the previous day value area, the day is excluded from this data set as it belongs to a separate statistical study. Only the first touch of the previous day value area low during the following trading session is recorded. Very important.

[4:10] Any subsequent touch, including touches that occur in later trading sessions, is considered a separate event and should be tracked in its own statistic.

[4:23] This study is designed to validate the effectiveness of the previous day value area low, not a naked value area low or any other value area low.

[4:27] Therefore, it only measures the reaction on the immediately following trading day. Days with a 30-minute opening candle touches or tests the previous day value area low are excluded. Why? These scenarios are not tradable according to the strategy as the market may open only a takeaway from the level, leaving no valid opportunity for entry. This is very important. That is exactly what I'm going to track. Only the previous day value area low, nothing more and nothing less. What I also mentioned is that we have to prepare holidays. What does that mean? So in this Excel file, you

[5:07] will find all of the holidays from 2025 to 2026. For some of the statistics, you have to add 2024. And here I have to be honest and say this is simply done by AI. So here we can see the exact day of a holiday and then the market status could be closed or an early market finish. You could also see the exact event that happened on that day. And this I would then mark out on trading view, which I'm going to show you here. So if I track a statistic on trading view, I always mark out the holidays just in a folder with these vertical

[5:46] lines. And whenever I come to that day tracking whatever statistic I will see, oh no, actually this level has to be excluded as this is a holiday. This leads to misleading data and I do not want to include that in my statistics. So also, especially when we track statistics on ATAS, we have to be very careful with New York open times. New York open times can change during the year because of daylight saving. That means on some of the weeks or months we have a daily open. That means on some of the weeks or months, we have a daily open.

[6:24] I'm using my time now, which is Central European time. Normally, the New York Open is at 3.30. Some time of the year, it is at 2.30. And here, I need to know the exact days where I have to change my settings for New York Open to go from 3.30 to 2.30.

[6:49] And here in this tab, you can see the exact date changes.

[6:53] So we can see on the 10th of March, 2025,

[6:59] New York Open went from 3.30 my time to 2.30 my time. I have to change it back to 3.30 on the 31st of March. This is something I always have to be aware of. And if I don't respect the time changes, for example, if I was tracking the New York open level, this could lead to massively misleading data. This could screw up the entire statistic. So this is extremely important. Now I want to have another look at this example. And whenever I create a backtesting plan, I create its own workspace in ATAS, which I'm going to show

[7:37] you here. This right here is the exact workspace that I created to track the previous day value area low reaction. On the top chart, you can see my volume profile for Nasdaq. This is, as you are used to, showing all of the volume profiles for each and every day. And with the lower line that we can see, I am always able to see the previous day value area low. Now, on the bottom of the chart, what you can see is the clean price action on a five-minute time frame, as well as clean price

[8:14] action on the one-minute time frame. Why I'm doing that, you will see now again. So, if we look into here, where I'm then actually checking or tracking my statistics, we have many things to add in. Observation, this is where you simply add the count on your asset. You can choose between whatever you want to trade, whatever you want to backtest. Here you have a drop down menu of NQ, ES and Bitcoin. Then you have to name your playbook in this instance or the static level. In this instance, this would be the previous day value area low. Then you choose a time frame. For

[8:54] example, you can go from a monthly down to a one minute time frame here. And this is why I created this template the way I did, because I'm tracking the reaction on a five-minute timeframe first. We always check the market context. Are we in an uptrend, downtrend, or ranging? We just say we are in a downtrend, and then we track the session. For some of the statistics, which I will show next, we have to be very careful and adjust the Excel file, which I mentioned in the presentation before. Because on some of the statistics that we use, we don't need to track

[9:31] as many things as for a previous day value area low observation. Here, for example, I could say this level was hit in London session. Then I have to say, is this a win or a loss? How do you define if a level ended in a win or a loss? I always track the deviation away from that level. I measure this in ticks. And if we then see a move away from that level, which would end in a two to one ratio, I can class it as a win. Okay, but you will see this in the next lesson where I will do this live and build a statistic in front of you.

[10:09] Okay, so here we track the deviation above and below in ticks as well as the move away from the level in ticks. The next thing, which is unique, you don't have to do it.

[10:24] which is unique. You don't have to do it. I included it here because I'm going to tease it a little bit, but there's going to be a analysis section in this Excel file, which is going to give you for each and every statistic that you track. If you also track the three candle entries on the one minute and the five minute timeframe, it is going to give you the best or actually all of the statistics and probabilities for every single entry in every single session. For example, we track a three candle entry on the one minute time frame.

[10:55] We can choose between COT, COC, momentum or sometimes no entry. If we have a COT, we measure the stop loss and the take profit.

[11:09] And it's going to calculate for you if it's going to end in a winner or a loser.

[11:17] So let's just say we have a 20 tick stop loss, 100 tick profit.

[11:22] It is going to show you in the actual statistic file as a winner. Then we're going to do the same for the three candle entry on the five minute. I know that this adds a lot of time and complexity, but the result that you will get from this is extremely beneficial. Okay, so here we do the exact same for the five minute, track all of this, we can include a screenshot and for every single backtesting plan, I always include one example here where you can see exactly how this template looks,

[11:52] what exactly you are tracking. So that being said, it might be a little bit confusing, but in the next lesson, we will go through each and every single step, look at special moments where we have to be careful, where do we have to adjust, from where am I measuring this move, and how exactly this works. What I already mentioned is that sometimes we have to adjust this Excel file. That means before we go into statistics building, we need a lot of preparation. This is what this lesson is about.

[12:25] building, we need a lot of preparation. This is what this lesson is about. And for example, you all heard about my daily open statistics. So here, the statistics building is a little bit different. If we go to this, we are no longer tracking an exact trade, as this is not something we would trade. This is something we use as context. Also, we are not looking at each and every specific session. We don't look at the daily open in the upper quartile statistic only in London session or in New York session. We look at the

[12:51] ETH, so electronic trading hours, the entire day. So an explanation for this looks different, and also the Excel file looks different. So let's go through it quickly so you can see the difference here. In this statistic, we are testing the probability of trading above or below the previous day's high or low based on the daily open quartile. This statistic measures the probability of price trading above the previous day high or below the previous day low when the daily open occurs in either the upper or lower quartile of the previous day high or below the previous day low, when the daily open occurs in either the

[13:25] upper or lower quartile of the previous day's range. The purpose of this study is to provide contextual information that supports the daily trading plan. A Fibonacci retracement is applied using the.25 tool to divide the previous day's range into quartiles. Whenever the daily open occurs within one of the extreme quartiles, upper or lower, the study records the probability of price trading above the previous day high or below the previous day low during the extended trading hours ETH session.

[13:58] Let's also have a look at the rules here for inclusion. A Fibonacci retracement is drawn using the.25 tool to define the quartiles of the previous day's range. Only days where the daily open is located within the upper or lower quartile are included. The statistic records whether price trades above the previous day high or below the previous day low at any point during the ETH session. below the previous day low at any point during the ETH session. The study does not track the specific session or time of day in which the move occurs. This is not an entry or setup statistic.

[14:34] It does not evaluate trade execution, risk reward, or specific trading signals. Its sole purpose is to provide contextual probabilities that can be incorporated in the daily market plan. So if we go down to here, we can see observation. Of course, we still count it. Asset, yes. Daily open is the playbook. Daily open extreme quartile time frame is daily. Uptrend, we still track. I just want to see, does that make a difference with the probabilities if it's in an uptrend or a downtrend? ETH session, win-loss. And here you can see deviation above or below is not applicable. We are no longer looking at this.

[15:25] document here. At the end, you will find a statistics overview where you can list all of your probabilities, all of your statistics that you track and see a nice chart here for all of these probabilities. You can see the count of observations. This is what we're going to build in the next lesson a little bit further. And the daily open quartile is just as an example. Of course, I have way more statistics, but that are not included here. So that is how all of the preparation in terms of your Excel file looks at in real time.

[15:55] On a document that combines everything and then which I will show in the end when we go into the actual analysis of each and every level. show in the end when we go into the actual analysis of each and every level. This is where I will show you the analysis tab, how each and every entry is going to be paired with the exact statistic. So one minute COT in New York session, previous day value area low has X probability. Five minutes COC in London session has X probability. Next, why do we actually exclude holidays?

[16:28] And this is holidays often create abnormal market conditions. Holidays have reduced liquidity, and that again can affect price action a lot. The lack of liquidity can lead to irregular volatility as well, because once the book is no longer as or is not as filled as we are used to, a bigger market order can have a bigger move than we are normally used to. And that's why I like to exclude it. Overall, we can notice a distorted session behavior and including abnormal trading days can distort your statistics and create misleading probabilities. and that is what we want to avoid at all cost. Why do we mark NY open time changes?

[17:08] The session open change with daylight saving shifts between US and Europe. With different session opens, volatility timing changes. As I already mentioned, if we track a New York open level, it has a difference of night and day if the New York open is at 3.30 or at 2.30. Liquidity timing also changes. And consistency and timing creates consistency in data. What is building a statistical framework and why is it important? Before collecting data, we need a structured framework for observations.

[17:46] This is what we went through in the backtesting plan. Every observation should follow the same framework. We cannot change the framework within the backtesting plan. Consistency and data collection is more important than complexity. You start each statistic neutral. You are not trying to convince yourself that a level or playbook has probabilities in its favor. I know from experience if you think you found a good level you are starting each statistic wanting to prove that it works. However you have to

[18:22] eliminate that completely and start super neutral. You are not trying to prove that your statistic works. The market has to prove it to you. Keep statistics simple. Many traders fail because they try to track too many variables at once. I try to build this Excel file in a way that it only tracks what's needed, but at the same time, we can get massive amount of information from this backtesting practice. Complexity can always be added later. What that means is what you will also see in this lesson, most of the statistics for me start as a static

[18:59] level. But in another statistic, we can add complexity. Here we have seen the backtesting plan for a previous day value area low. I could create a whole playbook around the previous day value area low and add as much context information as I want. See you all in the next lesson.
