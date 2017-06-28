using System.Collections.Generic;
using System.Windows.Forms;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class MainFormSettings {
		public int GridContainerSplitterPosition { get; set; }

		public List<HoldingsDataGridViewColumnSettings> GridHoldingsColumns { get; set; }

		public List<HoldingsDataGridViewCellSettings> GridHoldingsQuantities { get; set; }

		public List<MarketDataGridViewColumnSettings> GridMarketColumns { get; set; }

		public int Height { get; set; }

		public int LocationX { get; set; }

		public int LocationY { get; set; }

		public int Width { get; set; }

		public FormWindowState WindowState { get; set; }
	}
}
