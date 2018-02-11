/*
Copyright (C) 2017-2018 BikBikBikBikBik

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
using Nelibur.ObjectMapper;
using Newtonsoft.Json;

namespace CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap {
	public class ApiClient : ClientBase {
		public ApiClient(String apiBaseAddress) : base(apiBaseAddress) {
		}

		public async Task<GlobalData> GetGlobalDataAsync() {
			var responseString = await GetHttpResponseStringAsync($"{_apiBaseAddress}global/");
			var globalData = JsonConvert.DeserializeObject<GlobalDataDto>(responseString);

			return TinyMapper.Map<GlobalData>(globalData);
		}

		public async Task<ICollection<CurrencyTicker>> GetTickerAsync() {
			var responseString = await GetHttpResponseStringAsync($"{_apiBaseAddress}ticker/?limit=0");
			var currencyTicker = JsonConvert.DeserializeObject<List<CurrencyTickerDto>>(responseString);

			return currencyTicker.Select(TinyMapper.Map<CurrencyTicker>).ToList();
		}
	}
}
