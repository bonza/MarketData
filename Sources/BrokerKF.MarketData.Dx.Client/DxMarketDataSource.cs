using System;
using System.Collections.Generic;
using System.Linq;
using BrokerKF.MarketData.Core;
using com.dxfeed.api;
using com.dxfeed.native;

namespace BrokerKF.MarketData.Dx.Client
{
    public class DxMarketDataSource: IDxFeedListener, IMarketDataSource
    {
        private readonly Dictionary<char, string> _exchanges = new Dictionary<char, string>
        {
            {'Q',"NASDAQ OMX"},
        };

        private readonly string _address;
        private readonly NativeConnection _connection;
        private IMarketDataProcessor _processor;

        private Dictionary<string, ITicker> _mktDataTickers = new Dictionary<string, ITicker>();

        public DxMarketDataSource(string address)
        {
            _address = address;
            _connection= new NativeConnection();
        }

        public void OnQuote<TB, TE>(TB buf) where TB : IDxEventBuf<TE> where TE : IDxQuote
        {
            var symbol = buf.Symbol.ToString();
            ITicker ticker;
            if (_mktDataTickers.TryGetValue(symbol, out ticker))
            {
                foreach (var q in buf)
                {
                    string bidExchange;
                    _exchanges.TryGetValue(q.BidExchangeCode, out bidExchange);
                    string askExchange;
                    _exchanges.TryGetValue(q.AskExchangeCode, out askExchange);

                    if (bidExchange != null || askExchange != null)
                    {
                        _processor.ProcessMarketData(new QuoteData(ticker, bidExchange == null ? QuoteType.Ask : askExchange == null ? QuoteType.Bid : QuoteType.BidAsk,
                            bidExchange == null ? null : q.BidPrice as double?, bidExchange == null ? null : q.BidSize as long?, bidExchange,
                            askExchange == null ? null : q.AskPrice as double?, askExchange == null ? null : q.AskSize as long?, askExchange 
                            ));
                    }
                }
            }
        }

        public void OnTrade<TB, TE>(TB buf) where TB : IDxEventBuf<TE> where TE : IDxTrade
        {
            var symbol = buf.Symbol.ToString();
            ITicker ticker;
            if (_mktDataTickers.TryGetValue(symbol, out ticker))
            {
                foreach (var q in buf)
                {
                    string lastExchange;
                    _exchanges.TryGetValue(q.ExchangeCode, out lastExchange);

                    if (lastExchange != null)
                    {
                        _processor.ProcessMarketData(new QuoteData(ticker, QuoteType.Last,
                            lastExchangeCode:lastExchange, lastPrice:q.Price, lastSize:q.Size
                            ));
                    }
                }
            }
        }

        public void OnOrder<TB, TE>(TB buf) where TB : IDxEventBuf<TE> where TE : IDxOrder
        {
            
        }

        public void OnProfile<TB, TE>(TB buf) where TB : IDxEventBuf<TE> where TE : IDxProfile
        {
            
        }

        public void OnFundamental<TB, TE>(TB buf) where TB : IDxEventBuf<TE> where TE : IDxFundamental
        {
            
        }

        public void OnTimeAndSale<TB, TE>(TB buf) where TB : IDxEventBuf<TE> where TE : IDxTimeAndSale
        {
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public void StartSubscription(IEnumerable<ITicker> tickers)
        {
            var enumerable = tickers as ITicker[] ?? tickers.ToArray();
            _mktDataTickers = enumerable.ToDictionary(s => s.Symbol, s => s);

            _connection.Connect(_address);

            var l = this as IDxFeedListener;
            var symbols = enumerable.Select(t => t.Symbol).ToArray();
            _connection.CreateSubscription(EventType.Trade, l).AddSymbols(symbols);
            _connection.CreateSubscription(EventType.Quote, l).AddSymbols(symbols);
        }

        public void Stop()
        {
            _connection.Disconnect();
        }

        public void RegisterProcessor(IMarketDataProcessor processor)
        {
            _processor = processor;
        }
    }
}
