using System;
using System.Diagnostics;
using System.IO;

namespace BrokerKF.MarketData.Core
{
    public class CsvProcessor : IMarketDataProcessor, IDisposable
    {
        private readonly string _fileName;
        private readonly StreamWriter _writer;

        public CsvProcessor(string fileName)
        {
            _fileName = fileName;
            var file = File.Open(string.Format("f:\\{0}.{1:yyyy-MM-dd.HHmmss}.csv", fileName, DateTime.Now), FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            _writer = new StreamWriter(file);
            _writer.AutoFlush = true;
        }

        public void ProcessMarketData(IQuoteData data, Stopwatch watch)
        {
            var msg = string.Format("{0:yyyy-MM-dd HH:mm:ss.ffffff};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                data.CreateDate, 
                data.Ticker.Symbol, 
                data.QuoteType,
                data.BidExchangeCode,
                data.BidPrice,
                data.BidSize,
                data.AskExchangeCode,
                data.AskPrice,
                data.AskSize,
                data.LastExchangeCode,
                data.LastPrice,
                data.LastSize
                );
            _writer.WriteLine(msg);
            Console.WriteLine(_fileName + ": " + msg);
        }

        public void ProcessMarketData(IQuoteData data)
        {
            ProcessMarketData(data, null);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}