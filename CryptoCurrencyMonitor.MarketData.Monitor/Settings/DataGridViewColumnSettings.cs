namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class DataGridViewColumnSettings {
		public float FillWeight { get; set; }

		public int Width { get; set; }
	}

	internal class HoldingsDataGridViewColumnSettings : DataGridViewColumnSettings {
		public HoldingsDataColumnType Tag { get; set; }
	}

	internal class MarketDataGridViewColumnSettings : DataGridViewColumnSettings {
		public MarketDataColumnType Tag { get; set; }
	}
}
