using System.Collections.Generic;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class MonitoringSettings {
		public MonitoringSettings() {
			CurrencyDisplayType = CurrencyDisplayType.Symbol;
			HoldingsCurrencyTypes = new List<int>();
			MarketCurrencyTypes = new List<int>();
		}

		public CurrencyDisplayType CurrencyDisplayType { get; set; }

		public List<int> HoldingsCurrencyTypes { get; set; }

		public List<int> MarketCurrencyTypes { get; set; }

		public int RefreshInterval { get; set; }
	}
}
