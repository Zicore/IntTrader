using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using IntTrader.API.Base.Exchange;
using IntTrader.WebService.Base;
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
                Console.ReadLine();
            }
        }

        private static void UnhandledExceptionCallback(Exception exception)
        {

        }
    }
}
