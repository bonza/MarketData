namespace BrokerKF.MarketData.Core
{
  public interface ITicker
  {
    string Symbol { get; }
    string SecType { get; }
    string Currency { get; }
    string Exchange { get; }
    string PrimaryExch { get; }
  }
}