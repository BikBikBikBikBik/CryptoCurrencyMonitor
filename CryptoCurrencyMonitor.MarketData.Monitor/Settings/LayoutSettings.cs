/*
Copyright (C) 2017 BikBikBikBikBik

This file is part of CryptoCurrencyMonitor.

CryptoCurrencyMonitor is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

CryptoCurrencyMonitor is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with CryptoCurrencyMonitor.  If not, see <http://www.gnu.org/licenses/>.
*/
using System.Collections.Generic;
using System.Windows.Forms;

namespace CryptoCurrencyMonitor.MarketData.Monitor.Settings {
	internal class LayoutSettings {
		public LayoutSettings() {
			GridHoldingsColumns = new List<HoldingsDataGridViewColumnSettings>();
			GridMarketColumns = new List<MarketDataGridViewColumnSettings>();
		}

		public int GridContainerSplitterPosition { get; set; }

		public List<HoldingsDataGridViewColumnSettings> GridHoldingsColumns { get; set; }

		public List<MarketDataGridViewColumnSettings> GridMarketColumns { get; set; }

		public int Height { get; set; }

		public int LocationX { get; set; }

		public int LocationY { get; set; }

		public int Width { get; set; }

		public FormWindowState WindowState { get; set; }
	}
}
