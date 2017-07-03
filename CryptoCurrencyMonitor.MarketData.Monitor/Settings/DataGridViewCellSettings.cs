using System;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class DataGridViewCellSettings {
		public String Value { get; set; }
	}

	internal class HoldingsDataGridViewCellSettings : DataGridViewCellSettings {
		public int RowTag { get; set; }
	}
}
