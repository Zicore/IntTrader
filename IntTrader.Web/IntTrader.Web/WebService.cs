﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.Web.Model;
using IntTrader.WebService.Base;

namespace IntTrader.Web
{
    public static class WebService
    {
        static WebService()
        {
            DashboardModel = new DashboardModel();
            Broker = new WebBroker(GetBasePath());
        }

        public static void Initialize()
        {
            var hash = "2c46a54ae692abc129001b0c0704108e5f123c9c2902c767ce8f735f2ab94211";
            LoadExchangeSettings(hash);
            LoadModels();
        }

        public static void LoadExchangeSettings(String hash)
        {
            Broker.ExchangeManager.LoadSettings(hash);
        }

        public static void LoadModels()
        {
            foreach (var exchange in Broker.Exchanges.Items)
            {
                DashboardModel.Items.Add(CreateExchangeModel(exchange.Value));
            }
        }

        public static ExchangeModel CreateExchangeModel(ExchangeBase exchange)
        {
            return new ExchangeModel
            {
                Id = exchange.Name.ToLower(),
                Name = exchange.Name,
                Pair = exchange.DefaultPair
            };
        }

        public static string GetBasePath()
        {
            if (HttpContext.Current == null)
                return AppDomain.CurrentDomain.BaseDirectory;
            else
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        }

        public static DashboardModel DashboardModel { get; set; }

        public static WebBroker Broker { get; set; }
    }
}
