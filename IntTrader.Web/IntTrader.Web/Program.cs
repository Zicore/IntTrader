using IntTrader.WebService.Base;

namespace IntTrader.Web
{
    using System;
    using Nancy.Hosting.Self;

    class Program
    {
        static void Main(string[] args)
        {
            var uri =
                new Uri("http://localhost:80");

            HostConfiguration configuration = new HostConfiguration();
            configuration.AllowChunkedEncoding = false; // TEMP FIX
            configuration.RewriteLocalhost = true;
            configuration.UnhandledExceptionCallback += UnhandledExceptionCallback;

            var bootstrapper = new Bootstrapper();
            using (var host = new NancyHost(bootstrapper, configuration, uri))
            {
                host.Start();
                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }

        private static void UnhandledExceptionCallback(Exception exception)
        {

        }
    }
}
