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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoCurrencyMonitor.Common.Http;
using Newtonsoft.Json.Linq;

namespace CryptoCurrencyMonitor.MarketData.Client.CryptoCompare {
	public class ApiClient : ClientBase {
		public ApiClient(String apiBaseAddress) : base(apiBaseAddress) {
		}

		public async Task<IDictionary<String, decimal>> GetPriceAsync(String sourceCurrencyType, params String[] destCurrencyTypes) {
			var responseString = await GetHttpResponseStringAsync($"{_apiBaseAddress}data/price?fsym={sourceCurrencyType}&tsyms={String.Join(",", destCurrencyTypes)}");
			var prices = JObject.Parse(responseString);

			var currencyPriceMap = destCurrencyTypes.ToDictionary(d => d, d => prices[d].Value<decimal>());
			currencyPriceMap[sourceCurrencyType] = -1;

			return currencyPriceMap;
		}
	}
}
