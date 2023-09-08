namespace DataModel
{
    public class Trades
    {
        public int TradeId { get; set; }

        public string? StockSymbol { get; set; }

        public DateTime TradeTime { get; set; }

        public double TradePrice { get; set; }

        public int Qunatity { get; set; }

        public int BuyerId { get; set; }

        public int SellerId { get; set; }

        public string? TradeType { get; set; }

        public double CommissionFee { get; set; }
    }
}