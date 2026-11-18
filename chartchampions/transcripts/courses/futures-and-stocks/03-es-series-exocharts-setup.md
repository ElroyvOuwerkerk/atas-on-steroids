---
date: course-lesson
champ: unattributed
course: Futures and Stocks
lesson_no: 3
title: ES Series: ExoCharts Setup
slug: es-series-exocharts-setup
category: courses/futures-and-stocks
cues: 123
words: 1912
duration: 16:04
source: https://portal.chartchampions.com/lesson/es-series-exocharts-setup
captured: 2026-11-16
---

# ES Series: ExoCharts Setup

## Portal Summary & Highlights

This video provides a comprehensive guide on setting up and using ExoCharts, a powerful order flow software for trading. It covers the differences between the desktop pro and web versions, pricing plans, and step-by-step instructions for importing and exporting workspaces. The video also explains how to connect ExoCharts with DXFeed for CME data and offers tips for troubleshooting common issues.

### Timestamps:

- [00:00] Introduction and legal trade disclaimer
- [00:47] Comparison of ExoCharts desktop pro and web versions
  - Desktop pro: fully customizable, supports multiple markets, runs on Windows and Mac
  - Web version: simple, pre-configured, supports only crypto
- [01:39] ExoCharts features: volume profiles, TPO profiles, order flow, charting, and drawing tools
- [02:07] ExoCharts desktop pricing plans and included features
- [04:02] Implementation: downloading and setting up ExoCharts desktop pro
  - Obtaining and using the token for login
  - Importing and loading workspaces
- [06:28] Setting up DXFeed connection for CME data
  - Choosing between CME market depth and top of book subscriptions
  - Logging in with DXFeed credentials and downloading instruments
- [09:02] Importing and exporting templates and workspaces
- [10:12] Trading with ExoCharts: no direct trade execution, management, or closing
- [10:32] Connecting AMP futures trading account to TradingView (requires CQG data feed)
- [12:04] Tips and tricks
  - Allow time to familiarize yourself with ExoCharts settings and characteristics
  - Fixing DXFeed freezing issues on ES and NQ charts by creating a new database
  - Step-by-step guide for creating a new database

By following the instructions in this video, traders can effectively set up and customize ExoCharts to analyze order flow data and improve their trading strategies. Take the time to practice and explore the software's features to make the most of this powerful tool.

## Transcript

[0:00] Hello and welcome to this ES series where I will be covering the ExoChart setup. Before we begin, it is important that you read through, understand and accept the legal trade disclaimer as everything I'm covering here is no financial advice and for educational purposes only.

[0:28] for educational purposes only. So, in this ES series, where I will be covering the ExoCharts setup, I want to begin with showing you the utility, moving on to the implementation, and then I will be ending with some tips and tricks. Utility. ExoCharts is an order flow software that runs on desktop and web. Comparing desktop pro versus web. Desktop pro is a fully customizable software supporting crypto, CME, Eurex, Nasdaq and ETF markets. It runs on Windows and Mac OS X. In my opinion, the desktop pro version is the

[1:10] preferred choice and that is the one I was using for a long time. Web version is a simple and pre-configured website designed to be easy to use and it only supports crypto. So if you want to trade CME, Eurex, Nasdaq or ETF markets and you want to gather footprint or order flow data you need to go on the desktop pro version.

[1:39] ExoCharts includes the following features. It has volume profiles, TPO profiles, simple order flow, and it also includes charting and drawing tools such as Fibonacci, trend lines, channels, and measurement tools. Next, we're going to have a look at some different plans and pricing for EXO Desktop. You can choose between a monthly paid version, which is 49 euros for each month. And here we can see what's included. So that is 10 live charts, historic data, unlimited live data, advanced plugins, custom periods, custom sessions.

[2:30] That one, for example, is necessary for the Igor sessions, for example, or even the CME sessions. We have trade filters, advanced DOM and the tape. The XFIT connectors, which is important for the CME, NASDAQ, and Eurex. I am going to cover how to set that up in this lesson as well. And you can use Bybit and Binance, all pairs, one click replay, so that is a new feature, so you can replay the chart and open interest profiles on crypto only. Here we can already see a difference in price

[3:08] between the desktop pro and the web version, but we can at the same time see that the web version has limited access to the lower features here. So if we look at a different pricing, if it is paid for three months in advance, it is 43 euros a month, but you would pay this amount times three, which is 129. The features obviously remain the same. One more option you can go with is the 6 month paid version where it only costs 38 euros and that is paid every 6 months. In this lesson I will cover the setup of the desktop pro version.

[4:02] Let's have a look at the implementation. Once your ExoCharts account is set up you can download the desktop pro version and for that you can use this link. When using this link you will get to the following page we can see here. Down here I am personally going with the beta channel 2.0.6 version and down here you can choose between the Windows or the Mac version. After that you can open ExoDesktop after the installation process is completed. ExoDesktop will ask for a token. You can find this on the same page as the

[4:53] download link. You can simply copy this token and paste it into your ExoCharts. I will show you how this looks. So on the page you can you can use the link for. You can see your token. You can click to copy this token. There is then your token inside here and you will then get to this window in ExoCharts. Down here you have to click on to log in and paste the token. Now you are logged into ExoDesktop and the next step is to load a template or a workspace. Importing a workspace works as follows. Once your EXO desktop is opened, you can see these tasks in the task menu in the top left.

[5:52] You click on workspace and here you can for example import my workspace which is located in the vault on our website. After importing the workspace you must load the imported workspace by clicking on the imported workspace and press load. If your workspace includes data of the CME such as the ES or the NASDAQ you need to set up a data feed connection. In the example of exocharts you must use the DX feed connection. After importing the workspace it will automatically give you this error message because it cannot

[6:46] download the DXFeed data without you setting up a DXFeed data subscription. For that you will need to click on set up DXFeed connection. After that you will get redirected to the DXFeed webpage. You can either use the link inside ExoCharts or you can use this link I provided here. After doing so you click on DXFeed ExoCharts.

[7:31] Here we now have to decide for a subscription. We can either use the CME market debt which is $39 per month or we could use the CME top of book for $19 a month. The difference here is with this subscription, we are not able to get the DOM and tape information. This is why I personally recommend to go with this CME market depth. As I already said, the top of book does not support DOM data.

[8:10] After you set up your DXFeed subscription, your login credentials will be sent to you via email. Here you will find your login as well as your password. And you need this to sign in on EXOCharts Desktop again. You can copy your login credentials to ExoDesktop and download the instruments. For this, you will get the DXFeed page coming up for the futures. If you click on crypto, you don't need to set up your DXFeed account. That's only if you use the futures. For example, you click on DXFeed and here you can insert your username as well as your password.

[8:48] Afterwards, you will need to click on log in. After you've completed these steps, you will have to click on reload charts to get all the data. This is how to import a template. We are going to click on import file and here for example you can import one of my templates which is also located in the vault. Now if you made some changes to an existing workspace or even created a new one you can also export your current workspace and save that on a safe location on your computer. For that, you have to click on export current workspace.

[9:36] You can save your individual workspace, give it a new name and save it on your computer. It is exactly the same or it is similar to exporting a template. Here after seeing your template in the template layout you can go on save to disk and then you will have to give it a new name and save it on a safe location on your computer. Exocharts does not support trading via its software. So it's not possible to execute, manage or close trades via Exocharts directly. It is only useful to get the order flow data.

[10:26] When trading AMP futures or when trading futures via AMP, you can connect your account to TradingView. It is important to note that in order to be able to connect AMP to TradingView, you need the CQG data feed. So once your AMP account is set up and once your AMP account is funded, it will ask you to either choose a CQG data feed or a Rhythmic data feed. In order to be able to connect your account to TradingView, once again you need the CQG data feed. And now I'm going to show you how to connect your broker to TradingView.

[11:16] I'm going to choose the example of AMP.

[11:34] Once you click on your trading panel inside TradingView, you will find a whole list of a lot of brokers and you will need to find your broker, which in our case is AMP.

[11:45] You can click on AMP and log in with your typical and normal login credentials for AMP.

[11:52] Okay, and this is how you connect your AMP account to TradingView and you can then easily enter, exit, and manage trades via TradingView. So now, I want to move on to some tips and tricks. And for that, allow yourself time to familiarize yourself with the characteristics and settings of ExoCharts. Especially when you are new to AutoFlow software, it might be a bit confusing in the beginning, but don't worry.

[12:30] but don't worry, usually it's very easy to get familiar with AutoFlow software once you spend a bit of time to practice with it.

[12:41] Once enough time is spent working with EXO, it is very easy and intuitive to change settings and customize templates to your liking.

[12:48] When using DXFeed, it might freeze the ES and NQ chart from time to time. So on ExoCharts there might be some bugs here and there especially when using the ES and NQ data. So for this I want to present you an easy way of how to usually fix this, and that is by creating a new database. Creating a new database step by step. So first, you have to save your current workspace and templates in a separate folder on your PC. For that, you can very simply follow the instructions I gave earlier on how to save workspaces and save templates.

[13:36] The next step is to close exocharts. Make sure to have your DXFIT login credentials at hand. Another tip, if you lost or forget your login credentials, you can easily request new ones on the website and it will usually get sent via email within seconds. So, the next step is to go to the drive of your PC. In my case, Windows C drive. After that, you have to open the ExoCharts folder. Move the DAO folder, so it is by default named DAO on your ExoCharts folder and you are going to move that folder to your

[14:28] desktop or an alternative safe location. Open Exo again and don't worry, but all workspaces and templates are now gone or no longer inside your ExO layout. Import and load any ES template and log into DXFeed. So once you try to load your ES template, it will automatically ask you to again log into your DXFeed account.

[15:10] again log into your DXFeed account. Now you can check if the data is now working for the ES. With these steps you created a new database and usually the ES template is no longer frozen. If that is the case and you see ES data and you see price movement, import your old workspace and load the templates again. And that is then fixing the problem and you can again work with the ES and NQ data. Thank you for watching. A live stream covering questions and answers about this session will be hosted.

[15:47] You can submit your questions for this live stream via Discord in sessions, questions, and for additional Q&A, head over to the Trading Help Forum in Discord. Thank you all for watching and see you next time.
