using System.Diagnostics;

namespace BrokerKF.MarketData.Core
{
  public interface IMarketDataProcessor
  {
    void ProcessMarketData(IQuoteData data, Stopwatch watch);
    void ProcessMarketData(IQuoteData data);
  }
}