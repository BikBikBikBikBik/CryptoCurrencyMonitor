using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrencyMonitor.Common.Http;
using Newtonsoft.Json.Linq;

namespace CryptoCurrencyMonitor.MarketData.Client.CryptoCompare {
	public class ApiClient : ClientBase {
		public ApiClient(String apiBaseAddress) : base(apiBaseAddress) {
		}

		public async Task<IDictionary<CurrencyType, decimal>> GetPriceAsync(CurrencyType sourceCurrencyType, params CurrencyType[] destCurrencyTypes) {
			var responseString = await GetHttpResponseStringAsync($"{_apiBaseAddress}data/price?fsym={sourceCurrencyType}&tsyms={String.Join(",", destCurrencyTypes)}");
			var prices = JObject.Parse(responseString);

			var currencyPriceMap = destCurrencyTypes.ToDictionary(d => d, d => prices[d.ToString()].Value<decimal>());
			currencyPriceMap[sourceCurrencyType] = -1;

			return currencyPriceMap;
		}
	}
}
