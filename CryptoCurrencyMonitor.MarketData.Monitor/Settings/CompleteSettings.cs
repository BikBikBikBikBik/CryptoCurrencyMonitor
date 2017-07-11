namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class CompleteSettings {
		public CompleteSettings() {
			Layout = new LayoutSettings();
			Monitoring = new MonitoringSettings();
		}

		public LayoutSettings Layout { get; set; }

		public MonitoringSettings Monitoring { get; set; }
	}
}
