using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrencyMonitor.Common.Http;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	public class ApiClient : ClientBase {
		public ApiClient(String apiBaseAddress) : base(apiBaseAddress) {
		}

		public async Task<ICollection<CurrencyTicker>> GetTicker() {
			var responseString = await GetHttpResponseStringAsync($"{_apiBaseAddress}ticker/");
			var currencyTicker = JsonConvert.DeserializeObject<List<CurrencyTickerDto>>(responseString);

			return currencyTicker.Select(TinyMapper.Map<CurrencyTicker>).ToList();
		}
	}
}
