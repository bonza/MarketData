using System;

namespace BrokerKF.MarketData.Core
{
    public class QuoteData : IQuoteData
    {

        public QuoteData(ITicker ticker, QuoteType quoteType, double? bidPrice = null, long? bidSize = null, string bidExchangeCode = null, 
            double? askPrice = null, long? askSize = null, string askExchangeCode = null,
            double? lastPrice = null, long? lastSize = null, string lastExchangeCode = null
            )
        {
            CreateDate = DateTime.Now;
            Ticker = ticker;

            BidPrice = bidPrice;
            BidSize = bidSize;
            BidExchangeCode = bidExchangeCode;
            AskPrice = askPrice;
            AskSize = askSize;
            AskExchangeCode = askExchangeCode;
            LastPrice = lastPrice;
            LastSize = lastSize;
            LastExchangeCode = lastExchangeCode;
            QuoteType = quoteType;
        }

        public ITicker Ticker { get; private set; }
        public DateTime CreateDate { get; private set; }
        public QuoteType QuoteType { get; private set; }
        public double? BidPrice { get; private set; }
        public long? BidSize { get; private set; }
        public string BidExchangeCode { get; private set; }
        public double? AskPrice { get; private set; }
        public long? AskSize { get; private set; }
        public string AskExchangeCode { get; private set; }
        public double? LastPrice { get; private set; }
        public long? LastSize { get; private set; }
        public string LastExchangeCode { get; private set; }
    }
}