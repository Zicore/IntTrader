using IntTrader.WebService.Base;
using Microsoft.Owin.Hosting;

namespace IntTrader.Web
{
    using System;
    //using Nancy.Hosting.Self;

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
