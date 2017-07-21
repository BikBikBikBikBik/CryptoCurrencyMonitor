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
