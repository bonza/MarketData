namespace BrokerKF.MarketData.Core
{
  public class Ticker : ITicker
  {
      public Ticker(string symbol, string secType = null, string currency = null, string exchange = null, string primaryExch = null)
    {
      Symbol = symbol;
      SecType = secType;
      Currency = currency;
      Exchange = exchange;
      PrimaryExch = primaryExch;
    }

    public string Symbol { get; private set; }
    public string SecType { get; private set; }
    public string Currency { get; private set; }
    public string Exchange { get; private set; }
    public string PrimaryExch { get; private set; }
  }
}