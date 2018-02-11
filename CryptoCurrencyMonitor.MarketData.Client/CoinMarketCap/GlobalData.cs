/*
Copyright (C) 2018 BikBikBikBikBik

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
using System;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	public class GlobalData {
		public int ActiveAssetCount { get; set; }

		public int ActiveCurrencyCount { get; set; }

		public int ActiveMarketCount { get; set; }

		public double BitcoinDominancePercent { get; set; }

		public DateTime LastUpdatedOn { get; set; }

		public float TotalMarketCapInUsd { get; set; }

		public float TotalVolumeInUsd24H { get; set; }
	}
}
