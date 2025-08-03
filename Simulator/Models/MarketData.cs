using System;

namespace Simulator.Models
{
    // Represents a single market data entry (symbol, price, timestamp, previous price)
    public class MarketData
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal PreviousPrice { get; set; } = 0m;
    }
}
