using System;
using CryptoCurrencyMonitor.MarketData.Client;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class DataGridViewCellSettings {
		public String Value { get; set; }
	}

	internal class HoldingsDataGridViewCellSettings : DataGridViewCellSettings {
		public CurrencyType Tag { get; set; }
	}
}
