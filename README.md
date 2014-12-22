IntTrader
=============================

IntTrader is the front end and implementation of the IntTrader.API and the target of IntTrader.API is to be a unified API for all Crypto Coin Markets. It features all the abstraction machanisms to implement another Exchange without alot effort.

IntTrader is written in C# and makes use of WPF for the front end and Newtonsoft.Json for parsing of the APIs.
That means the actual front end of IntTrader will currently only run on Windows, but I assume the IntTrader.API should work on Mono too.

The following Exchanges are partially implemented right now. I kept focus on basic functionality.

* Kraken
* Bitfinex

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
* Bitcoin Address Lookup via Blockchain

Unfinished Features
================
* OrderBook price grouping. It currently has a harcoded 2($) step for each group, which obviously wont work for prices lower than Bitcoin, also the grouping itself isn't finished yet.
* Kraken orders which need two prices are not working yet e.g. stop-loss-limit.

IntTrader Dashboard
================
The Dashboard is an overview about different pair prices. The Dashboard is planned to be customizable.

![Dashboard](http://upppor.it/8aQ1.png)

IntTrader Exchange - Bitfinex
================
Bitfinex is the first market, I have implemented. While I tried to keep focus basic functionality and abstractions, this application may tend a little towards the Bitfinex API.

![Dashboard](http://upppor.it/4kPX.png)

IntTrader Exchange - Kraken
================
Kraken is the second market implemented.
Be aware that orders with two prices are currently not implemented. E.g. stop-loss-limit.

IntTrader.API
================
IntTrader.API is the root of everything, it provides all the base functionality to add another market, but it also provides a way for front end applications, to access the markets through the unified API.

To get started, you should see the current implementations of the markets of Kraken and Bitfinex.
