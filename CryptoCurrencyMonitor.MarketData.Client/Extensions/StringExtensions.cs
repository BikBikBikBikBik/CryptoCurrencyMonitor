using System;

namespace CryptoCurrencyMonitor.MarketData.Client.Extensions {
	public static class StringExtensions {
		public static CurrencyType ToCurrencyType(this String symbol) {
			var currencyType = ToEnumType<CurrencyType>(symbol);
			if (currencyType.HasValue) {
				return currencyType.Value;
			}

			return CurrencyType.UNKNOWN;
		}

		public static T? ToEnumType<T>(this String symbol) where T : struct {
			if (!typeof(T).IsEnum) {
				throw new ArgumentException("T must be an enumerated type.");
			}

			T returnEnum;
			if (Enum.TryParse(symbol, true, out returnEnum)) {
				return returnEnum;
			}

			return null;
		}
	}
}
