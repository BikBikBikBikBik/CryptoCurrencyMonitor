using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using CryptoCurrencyMonitor.MarketData.Client.Extensions;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	internal class CurrencyTickerConverter : TypeConverter {
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return destinationType == typeof(CurrencyTicker);
		}

		public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType) {
			var concreteValue = (CurrencyTickerDto)value;
			var result = new CurrencyTicker {
				Id = concreteValue.Id,
				LastUpdatedOn = DateTimeOffset.FromUnixTimeSeconds(SafeParseLong(concreteValue.Last_updated)).UtcDateTime,
				MarketCapInUsd = SafeParseDecimal(concreteValue.Market_cap_usd),
				Name = concreteValue.Name,
				Rank = SafeParseInt(concreteValue.Rank),
				PercentChange1H = SafeParseDecimal(concreteValue.Percent_change_1h),
				PercentChange24H = SafeParseDecimal(concreteValue.Percent_change_24h),
				PercentChange7D = SafeParseDecimal(concreteValue.Percent_change_7d),
				PriceInBtc = SafeParseDecimal(concreteValue.Price_btc),
				PriceInUsd = SafeParseDecimal(concreteValue.Price_usd),
				Symbol = FixSymbolName(concreteValue.Symbol).ToCurrencyType(),
				VolumeInUsd24H = SafeParseDecimal(concreteValue.VolumeUsd24h)
			};

			return result;
		}

		private String FixSymbolName(String symbol) {
			if (_currencySymbolMap.ContainsKey(symbol)) {
				return _currencySymbolMap[symbol];
			}

			return symbol;
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

		private static readonly IDictionary<String, String> _currencySymbolMap = new Dictionary<String, String> {
			{ "MIOTA", "IOT" }
		};
	}
}
