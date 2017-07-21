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
using System;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	public class CurrencyTicker {
		public int Id { get; set; }

		public DateTime LastUpdatedOn { get; set; }

		public decimal MarketCapInUsd { get; set; }

		public String Name { get; set; }

		public int Rank { get; set; }

		public decimal PercentChange1H { get; set; }

		public decimal PercentChange24H { get; set; }

		public decimal PercentChange7D { get; set; }

		public decimal PriceInBtc { get; set; }

		public decimal PriceInUsd { get; set; }

		public String Symbol { get; set; }

		public decimal VolumeInUsd24H { get; set; }
	}
}
