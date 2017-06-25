using System;

namespace CryptoCurrencyMonitor.MarketData.Client.Extensions {
	public static class CurrencyTypeExtensions {
		public static String ToNameString(this CurrencyType currencyType) {
			var currencyName = CurrencyTypeRegistry.GetNameFromSymbl(currencyType);

			return !String.IsNullOrWhiteSpace(currencyName) ? currencyName : currencyType.ToString();
		}
	}
}
