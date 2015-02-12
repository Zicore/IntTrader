using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using IntTrader.API.Base.Exchange;
using IntTrader.WebService.Base;
using Microsoft.Owin.Hosting;
using Zicore.Security.Cryptography;

namespace IntTrader.Web
{
    using System;
    //using Nancy.Hosting.Self;

    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://+:80";

            //Console.WriteLine("Enter your password");
            var hash = "7bc650cf25f355aed4e9704d638c9c1b10cf8c4effe9232c896b159eb5d71755";

            WebService.LoadExchangeSettings(hash);

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

        private static void UnhandledExceptionCallback(Exception exception)
        {

        }
    }
}
