---
date: course-lesson
champ: unattributed
course: Beginners
lesson_no: 5
title: Trading With Leverage
slug: how-to-trade-with-leverage-margin
category: courses/beginners
cues: 88
words: 1397
duration: 8:03
source: https://portal.chartchampions.com/lesson/how-to-trade-with-leverage-margin
captured: 2026-11-16
---

# Trading With Leverage

## Portal Summary & Highlights

This session introduces the fundamentals of leverage in trading, covering how leverage amplifies both position sizes and potential losses, the key differences between cross and isolated margin, and how leverage can be used strategically for hedging offline assets. We also learn why high leverage increases liquidation risk and why proper stop loss placement at invalidation is essential rather than relying on liquidation.

### Highlights

- [00:00] Introduction to the leverage trading session, outlining the structure: utility, implementation, and tips and tricks.
- [00:22] Utility of leverage explained:
  - Allows traders to open larger position sizes than their account balance would normally permit.
  - Example: $10 account with 10x leverage enables a $100 position size.
  - Faster account growth is possible, but losses accelerate equally.
- [01:00] Reduced exchange exposure as a key use case:
  - Keep most of the portfolio offline for security.
  - Use leverage on a smaller on-exchange balance to match the desired position size.
  - Supports a hedging strategy for offline holdings.
- [01:40] Discussion of liquidation risk: higher leverage brings the liquidation price closer. On 100x leverage, roughly a 0.5% adverse move can trigger liquidation.
- [02:42] Why high leverage frequently leads to losses Ñ new traders often misuse it by treating liquidation as a stop loss. Recommendation: begin with no leverage, then limit to a maximum of around 5x.
- [03:21] Implementation Ñ 1x (no leverage) example: A $1,000 position with a 1% move produces a $10 gain or loss. Simple, linear risk.
- [03:50] 100x leverage example:
  - $1,000 account controls a $100,000 position.
  - A 1% favorable move doubles the account.
  - A sub-1% adverse move can liquidate the entire position.
  - Illustrates how probability-based trading becomes extremely risky at high leverage.
- [05:07] Cross vs. Isolated margin:
  - Cross margin: uses the whole account as collateral; liquidation can wipe out the entire balance.
  - Isolated margin: only the margin assigned to the trade is at risk Ñ safer for those new to leverage.
- [06:18] Hedging with leverage:
  - Example: Holding 10,000 in spot Bitcoin but only keeping 2,000 on-exchange.
  - Using 5x leverage on the 2,000 allows a 10,000 hedge position.
  - Protects offline holdings against expected downside while limiting exchange exposure.
- [07:17] Tips and tricks:
  - Cross leverage keeps the liquidation price further away but risks the full account.
  - Isolated leverage controls maximum loss per trade.
  - Always place a stop loss at the invalidation price of the trade setup.
  - Never use liquidation as a stop loss substitute Ñ adjusting margin to engineer a liquidation point is poor practice.

We encourage traders to review these leverage concepts carefully and apply them with disciplined risk management, starting with little or no leverage before scaling up.

## Transcript

[0:00] Hello and welcome to the beginner series where we're going to be going over the session covering leverage trading. As always, important to remind you of the important legal trade disclaimer. So please pause, read through this. And once you have acknowledged everything, let's start with the session covering leverage. Starting with the utility, moving on to the implementation and ending as always with the tips and tricks. So the utility of leverage.

[0:28] as always with the tips and tricks. So the utility of leverage. Trade more contracts than you have for larger position sizes and account growth quicker via bigger profits. This is the biggest, most famous way that people use leverage, right? So it's essentially a way to trade more money than you have in your account. So I could have $10 in my account, stick on times 10 leverage, and I can trade $100 position size while only having $10 in the account. So that does mean you can grow accounts quicker, or you can also lose profits quicker. Less exposure on exchange

[1:00] when used correctly. Using leverage in a way where you do not need to have all of your portfolio on the exchange is a great use case example you could keep them some of your portfolio offline some of your portfolio on an exchange and use leverage to cover if you are looking for a hedging strategy use leverage to cover your offline portfolio which you do not feel comfortable having on exchange so yeah leverage in my opinion this is the best use case of leverage you do not have to have all your funds on the exchange and you can increase your position

[1:36] sizes with leverage to trade your offline portfolio position size essentially so with all this right larger losses more leverage equals closer liquidation price so leverage is simply going to give you closer liquidation prices the more you up your leverage the closer that liquidation will get liquidation means you'll lose if you're on isolated your whole position size So if I put in $100, which was put on the 100 times leverage, I'm there with a $1 account. I can trade $100 times 100.

[2:13] And if price moves 0.5%, again, with the price of Bitcoin, it's always changing. But let's say 0.5% movement on Bitcoin, that's going to hit the liquidation price on around times 100 leverage. And then I'd lose my $1 that I put into the trade. So, yeah, bigger leverage. Generally speaking, we would say leads to more losses because people don't really know the way to use it correctly. And, you know, they'll just use stop loss as liquidation, which is not the way to use it correctly and you know they'll just use stop loss as a as liquidation

[2:46] which is not the way to go so liquidation equals potential of using all of your account in one trade so if you're there on cross leverage and you just put all your money on a on basically at times 100 then you can wipe out your whole account in one trade. And that's a story we hear too many times from newer traders coming into the group, that they lost everything on highly leveraged trading, which is why we really recommend don't, at first, don't use it, and then stick to round times five max, generally.

[3:20] Implementation, then. So, one times leverage, or no leverage. So, whenever I say one times, it times leverage or no leverage so whenever i say one times basically just means no leverage so if i have a hundred dollars on my account and i enter a trade with times one leverage i'm entering a position size of a hundred dollars right so in this example we got a thousand dollar position size which is no leverage uh price increases by one percent we make ten dollars price drops by one percent we lose ten dollars okay fairly simple stuff there on no leverage thousand dollar account so here we're going to

[3:50] go on a hundred times leverage so we got a hundred we got a thousand dollars in our account with a hundred times leverage we can enter a position of one hundred thousand dollars if price increases by one percent with this position size we are doubling our account every 1% it moves up because we're making $1,000. We have an account size of $1,000 times 100 leverage, that $100,000 position size. We move up a cent, we double our account every 1% price increases.

[4:26] price increases of course if price drops by one percent that is essentially a thousand dollar loss but your liquidation would be hit slightly before one percent again on times 100 it's around that 0.5 mark and liquidation so liquidation can happen before price fully drops that one percent so you can double your account with a one percent increase but if you are incorrect and you lose the trade on this amount of leverage you lose your $1,000 and your position size and potentially the account is is

[4:52] totally wiped out off of one bad trade so you can see there are advantages there but also some big disadvantages and as trading is a game of probabilities, you never know for sure which is the next trade is going to be the winner or the loser. You can see it's a very dangerous game to be using this high leverage. So the two types of available options for leverage trading on Bybit are cross and isolated. And here are a little bit of explanation that really simply crosses your whole account as margin and that you potentially can lose your whole.

[5:28] You would lose your whole account if you're on cross margin. You've got everything up there on the leverage. And if you're if your liquidation is hit, you'll lose your whole account. Isolated is that's a little bit more recommended between the two, especially for people that are new to leverage. Again, I would say stick on times one. But if you are wanting to go into isolated leverage, then this is the third option of the two, because you're only risking what you put on the trade.

[5:58] So you could have an account size of, let's say, $1,000 and you could enter a times 100 long isolated for a hundred dollar position size. And if you get liquidated, you'll lose your one dollar margin. So you only lose what you put into the trade, whereas cross you risk losing everything because it's using your whole account as margin. So using leverage is very dangerous. I would never use more than times five. Leverage can be a strategy for not keeping all your funds on the exchange. For example, you have 10,000 spot Bitcoin and you

[6:31] only want 2,000 on the exchange. I could trade 10,000 position size only having 2,000 on exchange by just using times five leverage. So I'll only have 2,000 Bitcoin on the exchange. I can trade a position size of 10,000 Bitcoin by basically using my 2,000 on times 5 leverage. I believe price will drop and I want to hedge all my spot Bitcoin with the 2,000. Again, I can time 5 leverage for a total of 10,000 Bitcoin to hedge my offline assets. Hedging is a strategy that we have already taught

[7:04] on the website and it's absolutely fundamental to understand when it comes to trading especially cryptocurrency so you can protect your offline assets while you know having an online portfolio on an exchange so tips and tricks using cross leverage option will keep the liquidation price far away Using cross leverage option will keep the liquidation price far away.

[7:30] Isolated leverage will control your maximum loss in the event of liquidation.

[7:33] Always place a stop loss at invalidation price.

[7:37] Never rely on liquidation as a stop loss placement. It's just a very bad practice. You don't even want to begin to do that. Place your stop loss at invalidation where you're wrong on that trade. And do not, you know, start adjusting leverage margin to use that as a liquidation price. It's just bad practice and you don't want to start getting into that. So, yeah, with this point, cross leverage will keep your liquidation price far away. But, yeah, you have your whole account at risk.
