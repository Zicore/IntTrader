IntTrader
=============================

IntTrader is the implementation of the IntTrader.API and the target of IntTrader.API is to be a unified API for all Crypto Coin Markets. It features all the abstraction machanisms to implement another Exchange without alot effort.

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

IntTrader.API
================
IntTrader.API is the root of everything, it provides all the base functionality to add another market, but it also provides a way for front end applications, to access the markets through the unified API.

To get started, you should see the current implementations of the markets of Kraken and Bitfinex.
