IntTrader
=============================

IntTrader is the front end and implementation of the IntTrader.API and the target of IntTrader.API is to be a unified API for all Crypto Coin Markets. It features all the abstraction mechanisms to implement another Exchange without alot effort.

IntTrader is written in C# and makes use of WPF for the front end and Newtonsoft.Json for parsing of the APIs.
That means the actual front end of IntTrader will currently only run on Windows, but I assume the IntTrader.API should work on Mono too.

The following Exchanges are partially implemented right now. I kept focus on basic functionality.

* Kraken
* Bitfinex

**IntTrader** stands for Integrated Trader and it basically means, everything important is in one view ;-)

Release - Dev
================
Here you find the current releases:

* ![Download R1](https://github.com/Zicore/IntTrader/raw/master/built/dev/R1/IntTrader.zip)
![(Hashes)](https://github.com/Zicore/IntTrader/commit/5cf75ca42a402a9a920bf8e055d64874b53be553#diff-1e1a0b86da1816227825625ad9f78851)

I will maybe setup a build server soon.

Exchange Features
================
* Dashboard
* Order Book
* Recent Trades
* Create Orders
* Cancel Orders
* Order Overview

General Features
================
* API Keys Management
* Encrypted Settings for the API Keys and Secrets
* Bitcoin Address Lookup via Blockchain (Wallet Balances)

Unfinished Features
================
* OrderBook price grouping. It currently has a harcoded 2($) step for each group, which obviously wont work for prices lower than Bitcoin, also the grouping itself isn't finished yet.
* Kraken orders which need two prices are not working yet e.g. stop-loss-limit.

IntTrader - Dashboard
================
The Dashboard is an overview about different pair prices. The Dashboard is planned to be customizable.

![Dashboard](http://upppor.it/8aQ1.png)

IntTrader Exchange - Bitfinex
================
Bitfinex is the first market, I have implemented. While I tried to keep focus basic functionality and abstractions, this application may tend a little towards the Bitfinex API.

![Exchange - Bitfinex](http://upppor.it/4kPX.png)

![Exchange - Bitfinex](http://upppor.it/mGsE.png)
Trades are on the left side now, with a new basic trading sentiment indicator. 

IntTrader Exchange - Kraken
================
Kraken is the second market implemented.
Be aware that orders with two prices are currently not implemented. E.g. stop-loss-limit.

IntTrader - Addresses
================
This view allows to make lookups for bitcoin addresses. It could help to keep track of all wallets and see if the final balance is valid.

![Dashboard](http://upppor.it/iG9V.png)

IntTrader - Login
================
You can login by clicking the Key symbol. It tries to unlock the application for private market usage and will prompt for a password.

The first time you do this or by logging in and clicking the Change Password button, you can define the password.
This password is not saved anywhere and it's just used to decrypt the settings file for the API keys.
Please choose a strong password.

If you lose the password, you have to manually remove or rename the **ExchangeSettings.aes.json** file, from AppData and recreate one, by logging in again, you also have to add the api keys and secrets again.

IntTrader - API Keys
================
In this section of the Application you can manage the API Keys of the exchanges.
* Please use only minimal possible Keys e.g. Trading only.
* Do not share one key with multiple Applications.

IntTrader - AppData
================
The application creates two different settings files. Both located at %appdata%/IntTrader

* **ExchangeSettings.aes.json** stores the API Keys and Secrets and is encrypted with a password you choose on first login.
* **AppSettings.json** stores public addresses for the Blockchain balance lookup.

IntTrader.API
================
IntTrader.API is the root of everything, it provides all the base functionality to add another market, but it also provides a way for front end applications, to access the markets through the unified API.

To get started, you should see the current implementations of the markets of Kraken and Bitfinex.
