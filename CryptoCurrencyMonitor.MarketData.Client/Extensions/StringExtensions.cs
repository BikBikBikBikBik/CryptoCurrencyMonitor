using System;

namespace CryptoCurrencyMonitor.MarketData.Client.Extensions {
	public static class StringExtensions {
		public static CurrencyType ToCurrencyType(this String symbol) {
			CurrencyType currencyType;
			if (Enum.TryParse(symbol, true, out currencyType)) {
				return currencyType;
			}

			return CurrencyType.UNKNOWN;
		}
	}
}
