using System.Collections.Generic;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class CompleteSettings {
		public CompleteSettings() {
			Holdings = new List<HoldingsDataGridViewCellSettings>();
			Layout = new LayoutSettings();
			Monitoring = new MonitoringSettings();
		}

		public List<HoldingsDataGridViewCellSettings> Holdings { get; set; }

		public LayoutSettings Layout { get; set; }

		public MonitoringSettings Monitoring { get; set; }
	}
}
