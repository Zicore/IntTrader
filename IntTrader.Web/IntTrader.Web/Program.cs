using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using IntTrader.API.Base.Exchange;
using IntTrader.Web.Hubs;
using IntTrader.WebService.Base;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Zicore.Security.Cryptography;

namespace IntTrader.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://+:80";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");

                Task.Factory.StartNew(UpdateTicker);

                Console.ReadLine();
            }
        }

        public static void UpdateTicker()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<TickerHub>();


            //while (true)
            //{
            //    foreach (var exchangeBase in WebService.DashboardModel.Items)
            //    {
                 
            //        var pairBase = WebService.Broker.GetPair(exchangeBase.Id, exchangeBase.Pair.Key);

            //        var data = TickerHub.GetData(exchangeBase.Id, exchangeBase.Pair.Key);

            //        hub.Clients.All.update(new { id = exchangeBase.Id, lastPrice = data.LastPrice, pair = pairBase });

            //    }
            //    Thread.Sleep(4000);
            //}
        }

        private static void UnhandledExceptionCallback(Exception exception)
        {

        }
    }
}
