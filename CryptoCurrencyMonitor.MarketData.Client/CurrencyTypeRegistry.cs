using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CryptoCurrencyMonitor.Common.Attributes;

namespace CryptoCurrencyMonitor.MarketData.Client {
	internal static class CurrencyTypeRegistry {
		internal static String GetNameFromSymbl(CurrencyType currencyType) {
			if (!CurrencyTypeMap.ContainsKey(currencyType)) {
				return null;
			}
			
			return CurrencyTypeMap[currencyType];
		}

		private static IDictionary<CurrencyType, String> GenerateCurrencyTypeMap() {
			var asm = Assembly.GetExecutingAssembly();
			var cts = asm.GetType("CryptoMiningPool.Exchange.Client.CurrencyType");
			var members = cts.GetMembers(BindingFlags.Public | BindingFlags.Static);
			var dict = new Dictionary<CurrencyType, String>();
			foreach (var member in members) {
				var atr = member.GetCustomAttributes(typeof(EnumDisplayNameAttribute)).Cast<EnumDisplayNameAttribute>().SingleOrDefault();
				if (atr != null) {
					dict[(CurrencyType)Enum.Parse(typeof(CurrencyType), member.Name)] = atr.DisplayName;
				}
			}

			return dict;
		}

		private static readonly IDictionary<CurrencyType, String> CurrencyTypeMap = GenerateCurrencyTypeMap();
	}
}
