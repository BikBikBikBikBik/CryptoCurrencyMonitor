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
using System.Globalization;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	internal class CurrencyTickerConverter : TypeConverter {
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return destinationType == typeof(CurrencyTicker);
		}

		public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType) {
			var concreteValue = (CurrencyTickerDto)value;
			var result = new CurrencyTicker {
				Id = CurrencyTypeRegistry.GetIdFromName(concreteValue.Name),
				LastUpdatedOn = DateTimeOffset.FromUnixTimeSeconds(SafeParseLong(concreteValue.Last_updated)).UtcDateTime,
				MarketCapInUsd = SafeParseDecimal(concreteValue.Market_cap_usd),
				Name = concreteValue.Name,
				Rank = SafeParseInt(concreteValue.Rank),
				PercentChange1H = SafeParseDecimal(concreteValue.Percent_change_1h),
				PercentChange24H = SafeParseDecimal(concreteValue.Percent_change_24h),
				PercentChange7D = SafeParseDecimal(concreteValue.Percent_change_7d),
				PriceInBtc = SafeParseDecimal(concreteValue.Price_btc),
				PriceInUsd = SafeParseDecimal(concreteValue.Price_usd),
				Symbol = concreteValue.Symbol,
				VolumeInUsd24H = SafeParseDecimal(concreteValue.VolumeUsd24h)
			};

			return result;
		}

		private decimal SafeParseDecimal(String value) {
			if (String.IsNullOrWhiteSpace(value)) {
				return 0;
			}

			return decimal.Parse(value);
		}

		private int SafeParseInt(String value) {
			if (String.IsNullOrWhiteSpace(value)) {
				return 0;
			}

			return int.Parse(value);
		}

		private long SafeParseLong(String value) {
			if (String.IsNullOrWhiteSpace(value)) {
				return 0;
			}

			return long.Parse(value);
		}
	}
}
