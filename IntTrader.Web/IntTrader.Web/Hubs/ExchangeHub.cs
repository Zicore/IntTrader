﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Currency;
using IntTrader.WebService.Base;
using Microsoft.AspNet.SignalR;
using Nancy;
using Newtonsoft.Json;

namespace IntTrader.Web.Hubs
{
    //public class TickerSignalrModel
    //{
    //    String Exchange { get; set; }
    //    decimal LastPrice { get; set; }
    //    PairBase Pair { get; set; }
    //}

    public class ExchangeHub : Hub
    {
        public ExchangeHub()
        {

        }
        
        public void RequestTicker(String exchange, String pair)
        {
            var command = ExchangeManager.Functions[APIFunction.RequestTicker];

            exchange = exchange.ToLower();

            var pairBase = WebService.Broker.GetPair(exchange, pair);

            var result = WebService.Broker.Execute(exchange, command, pair) as TickerModel;

            if (result != null)
            {
                BroadcastTicker(new { exchange, lastPrice = result.LastPrice, Pair = pairBase });
            }
        }

        public void BroadcastTicker(dynamic model)
        {
            if (model != null) Clients.All.updateTicker(model);
        }

        public void RequestTrade(String exchange, String pair)
        {
            var command = ExchangeManager.Functions[APIFunction.RequestTrades];
            var result = WebService.Broker.Execute(exchange, command, pair) as TradesModel;

            if (result != null) Clients.Caller.updateTrade(new { exchange, trades = result.Items.Take(40) });
        }
        
        public void RequestBalance(String exchange, String pair)
        {
            var command = ExchangeManager.Functions[APIFunction.RequestBalances];

            exchange = exchange.ToLower();

            var pairBase = WebService.Broker.GetPair(exchange, pair);

            var result = WebService.Broker.Execute(exchange, command, pair) as BalanceModel;

            if (result != null)
            {
                Clients.Caller.updateBalance(new { exchange, balances = result} );
            }
        }
    }
}