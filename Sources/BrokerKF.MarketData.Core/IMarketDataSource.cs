using System;
using System.Collections.Generic;

namespace BrokerKF.MarketData.Core
{
  public interface IMarketDataSource: IDisposable
  {
    void StartSubscription(IEnumerable<ITicker> tickers);
    void Stop();
    void RegisterProcessor(IMarketDataProcessor processor);
  }
}