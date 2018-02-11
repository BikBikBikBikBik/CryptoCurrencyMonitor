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
using System.ComponentModel;
using System.Globalization;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	internal class GlobalDataConverter : TypeConverter {
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return destinationType == typeof(GlobalData);
		}

		public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType) {
			var concreteValue = (GlobalDataDto)value;
			var result = new GlobalData {
				ActiveAssetCount = concreteValue.Active_assets,
				ActiveCurrencyCount = concreteValue.Active_currencies,
				ActiveMarketCount = concreteValue.Active_markets,
				BitcoinDominancePercent = concreteValue.Bitcoin_percentage_of_market_cap,
				LastUpdatedOn = DateTimeOffset.FromUnixTimeSeconds(concreteValue.Last_updated).UtcDateTime,
				TotalMarketCapInUsd = concreteValue.Total_market_cap_usd,
				TotalVolumeInUsd24H = concreteValue.Total_24h_volume_usd
			};

			return result;
		}
	}
}
