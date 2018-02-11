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
using System.ComponentModel;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	[TypeConverter(typeof(GlobalDataConverter))]
	internal class GlobalDataDto {
		public int Active_assets { get; set; }

		public int Active_currencies { get; set; }

		public int Active_markets { get; set; }

		public double Bitcoin_percentage_of_market_cap { get; set; }

		public long Last_updated { get; set; }

		public float Total_24h_volume_usd { get; set; }

		public float Total_market_cap_usd { get; set; }
	}
}
