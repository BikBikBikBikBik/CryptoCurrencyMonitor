using System.Collections.Generic;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class MonitoringSettings {
		public MonitoringSettings() {
			CurrencyDisplayType = CurrencyDisplayType.Symbol;
			Holdings = new List<HoldingsDataGridViewCellSettings>();
			MarketCurrencyTypes = new List<int>();
		}

		public CurrencyDisplayType CurrencyDisplayType { get; set; }

		public List<HoldingsDataGridViewCellSettings> Holdings { get; set; }

		public List<int> MarketCurrencyTypes { get; set; }

		public int RefreshInterval { get; set; }
	}
}
