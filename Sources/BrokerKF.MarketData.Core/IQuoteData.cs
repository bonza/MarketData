using System;

namespace BrokerKF.MarketData.Core
{
    public enum QuoteType { Bid, Ask, BidAsk, Last }

    public interface IQuoteData
    {
        DateTime CreateDate { get; }

        ITicker Ticker { get; }
        QuoteType QuoteType { get; }

        double? BidPrice { get; }
        long? BidSize { get; }
        string BidExchangeCode { get; }

        double? AskPrice { get; }
        long? AskSize { get; }
        string AskExchangeCode { get; }

        double? LastPrice { get; }
        long? LastSize { get; }
        string LastExchangeCode { get; }
    }
}