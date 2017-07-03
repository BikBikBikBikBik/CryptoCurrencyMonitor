using System;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	public class CurrencyTicker {
		public int Id { get; set; }

		public DateTime LastUpdatedOn { get; set; }

		public decimal MarketCapInUsd { get; set; }

		public String Name { get; set; }

		public int Rank { get; set; }

		public decimal PercentChange1H { get; set; }

		public decimal PercentChange24H { get; set; }

		public decimal PercentChange7D { get; set; }

		public decimal PriceInBtc { get; set; }

		public decimal PriceInUsd { get; set; }

		public String Symbol { get; set; }

		public decimal VolumeInUsd24H { get; set; }
	}
}
