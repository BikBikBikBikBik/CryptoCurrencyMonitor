using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	[TypeConverter(typeof(CurrencyTickerConverter))]
	internal class CurrencyTickerDto {
		public String Id { get; set; }

		public String Last_updated { get; set; }

		public String Market_cap_usd { get; set; }

		public String Name { get; set; }

		public String Rank { get; set; }

		public String Percent_change_1h { get; set; }

		public String Percent_change_24h { get; set; }

		public String Percent_change_7d { get; set; }

		public String Price_btc { get; set; }

		public String Price_usd { get; set; }

		public String Symbol { get; set; }

		[JsonProperty("24h_volume_usd")]
		public String VolumeUsd24h { get; set; }
	}
}
