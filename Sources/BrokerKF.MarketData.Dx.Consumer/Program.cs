using System;
using BrokerKF.MarketData.Core;
using BrokerKF.MarketData.Dx.Client;

namespace BrokerKF.MarketData.Dx.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dxTickers = new[]
            {
                new Ticker("AAPL"),
//                new Ticker("YHOO"),
                new Ticker("GOOG"),
//                new Ticker("MSFT"),
            };

            try
            {

                var dxProcessor = new CsvProcessor("Dx.MktData");
                IMarketDataSource dxClient = new DxMarketDataSource("rt1.ec2.dxfeed.com:7831");
                dxClient.RegisterProcessor(dxProcessor);
                dxClient.StartSubscription(dxTickers);

                Console.ReadLine();

                dxClient.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("MarketDataSource was stopped");
            Console.ReadLine();
        }
    }
}
