namespace IntTraderConsole
{
    /// <summary>
    /// Just some tests
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //var result = WebClientExtended.Get(new GetRequest()
            //    {
            //        Uri = "https://api.bitfinex.com/v1/symbols"
            //    });

            //var symbols = new Symbols();
            //symbols.Parse(result); 

            //var order = new Order
            //    {
            //        Symbol = "ltcbtc",
            //        Amount = "1.0",
            //        Price = "5.0",
            //        Exchange = "bitfinex",
            //        Side = "sell",
            //        Type = "exchange limit",
            //    };
            //var json = order.ToJsonString();
            //var result = order.Send();
            //OrderStatus orderStatus = new OrderStatus
            //    {
            //        OrderId = 16165530
            //    };
            //var result = orderStatus.Send();
            //var resp = orderStatus.Deserialize(result);

            //Balance balance = new Balance();
            //var result = balance.Send();

            //Stopwatch sw = Stopwatch.StartNew();

            //ExchangeManager exchangeManager = new ExchangeManager();
            //KrakenExchange kraken = new KrakenExchange(exchangeManager);
            //TradesRequest request = new TradesRequest(kraken.CurrencyManager.AvailablePairs[PairBase.BTCEUR]);

            //var result = request.Request<TradesResponse>().Transform();


            //var request = new MultiAddressRequest(new List<string>{
            //    "1LX7jabk3sFz6DswXS2FkzTihfagFhKYUo" ,
            //    "17zwsCHrESL725wqu2Ge3RBXcfHfxaSwfN",
            //    "1JVzyW5TFP5sXiEiQbXJZt4bY58pVSER7Y"
            //});
            //var model = request.Request<MultiAddressResponse>().Transform();

            //Console.WriteLine(model);

            //sw.Stop();
            //Console.ReadKey();
            //var s = new PairBase() { Name = "xbteur" };

            //var exchange = new KrakenExchange(new ExchangeManager());
            //exchange.ExchangeAPI.APIKey = "";
            //exchange.ExchangeAPI.APISecret = "";
            //exchange.ExchangeAPI.Name = "Kraken API";
            //var order = new CreateOrderRequest(exchange);
            //order.Type = "buy";
            //order.Price = 150;
            //order.OrderType = "limit";
            //order.Volume = new decimal(0.1);
            //order.Pair = s.Name;
            //order.Validate = true;
            //var response = order.Request();

            //Console.WriteLine(response.Value);

            //Console.WriteLine("Enter OTP");
            //b.TwoFactorPassword = Console.ReadLine();
            //var response = b.Request<BalanceResponse>().Transform();
            //Console.WriteLine(response.Value);


            //Balance b = new Balance();
            //var rs = b.RequestSolid().Transform();

            //ExchangeLoader.Kraken.RequestOrderBook(SymbolBase.BitcoinEur);

            //var newOrder = new OrderNew
            //    {
            //        Price = "22.0",
            //        Amount = "1.0",
            //        Exchange = "bitfinex",
            //        IsHidden = false,
            //        SideSafe = Side.Sell,
            //        Symbol = s.Name,
            //        TypeSafe = OrderType.ExchangeLimit
            //    };
            //var result = newOrder.Request<OrderNewResponse>();
            //var result = new Orders().RequestSolid().Transform();

            //var book = new OrderBook(s);
            //var bookTransform = book.Request<OrderBookResponse>().Transform();

            //var ticker = new Ticker(s);
            //var tickerResult = ticker.Request<TickerResponse>();

            //var today = new Today(s);
            //var todayResult = today.Request<TodayResponse>();


            //bookTransform.Asks.Reverse();
            //bookTransform.Asks.ForEach(x => Console.WriteLine(x.ToString()));
            //Console.WriteLine();
            //Console.WriteLine(tickerResult.LastPrice);
            //Console.WriteLine();
            //bookTransform.Bids.ForEach(x => Console.WriteLine(x.ToString()));

            //Console.WriteLine("High:{0} Low:{1} Volume:{2}", todayResult.High, todayResult.Low, todayResult.Volume);

            //var settings = new Settings();
            //settings.SetKey("bbb");
            //try
            //{
            //    settings.Load("BittyTrade", "Settings.aes.json");
            //    Console.WriteLine("Encrypted");
            //}
            //catch (FileNotFoundException ex)
            //{
            //    settings.Save();
            //}
            //catch
            //{
            //    Console.WriteLine("Wrong key!");
            //}

            //settings.SetKey("bbb");
            //settings.Save();

        }
    }
}
