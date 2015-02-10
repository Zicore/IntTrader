using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Currency;
using IntTrader.WebService.Base;
using IntTrader.WebService.Exceptions;
using Newtonsoft.Json;
using NLog;
using NLog.Targets;

namespace IntTrader.WebService
{
    class Program
    {
        private readonly static Logger Log = LogManager.GetCurrentClassLogger();

        public static void Main(String[] args)
        {
            Log.Error("Starting {0}", Assembly.GetExecutingAssembly().GetName().Name);
            WebBroker broker = new WebBroker();


            var first = broker.ExchangeManager.Exchanges.First();

            var pair = first.PairManager.GetPair(PairBase.BTCUSD);

            //broker.Add("orders", x => first.RequestOrderBook(x));

            //var result = broker.Actions.First(x => x.Action == "orders").Request(pair);

            String exchange = "bitfinex";

            String line;
            do
            {
                Console.WriteLine("Commands available:");
                foreach (var e in broker.Exchanges.Items)
                {
                    foreach (var function in e.Value.AvailableFunctions)
                    {
                        var command = ExchangeManager.Functions[function];
                        Console.WriteLine("{0}/{1}", e.Key, command);
                    }
                }
                line = Console.ReadLine();

                if (line != null)
                {
                    var commandLine = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    object[] arguments = new object[0];
                    if (commandLine.Length > 1)
                    {
                        arguments = new object[commandLine.Length - 1];

                        for (int i = 0; i < arguments.Length; i++)
                        {
                            arguments[i] = commandLine[i + 1];
                        }
                    }
                    try
                    {
                        var model = broker.Execute(first, commandLine[0], arguments);
                        var str = JsonConvert.SerializeObject(model);
                        Log.Info(str);
                    }
                    catch (ArgumentRequiredException ex)
                    {
                        Log.Error(ex.Message);
                    }
                    catch (CommandNotSupportedException ex)
                    {
                        Log.Error(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }

            } while (line != "exit");
        }
    }
}
