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
using System.ComponentModel;
using Newtonsoft.Json;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	[TypeConverter(typeof(CurrencyTickerConverter))]
	internal class CurrencyTickerDto {
		public String Id { get; set; }

		public String Last_updated { get; set; }

		public String Market_cap_usd { get; set; }

		public String Name { get; set; }

		public String Rank { get; set; }

		public String Percent_change_1h { get; set; }

		public String Percent_change_24h { get; set; }

		public String Percent_change_7d { get; set; }

		public String Price_btc { get; set; }

		public String Price_usd { get; set; }

		public String Symbol { get; set; }

		[JsonProperty("24h_volume_usd")]
		public String VolumeUsd24h { get; set; }
	}
}
