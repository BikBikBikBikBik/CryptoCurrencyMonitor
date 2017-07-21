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
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CryptoCurrencyMonitor.Common.Http {
	public abstract class ClientBase {
		protected ClientBase(String apiBaseAddress) {
			_apiBaseAddress = apiBaseAddress;
		}

		protected async Task<String> GetHttpResponseStringAsync(String requestUrl) {
			var request = WebRequest.CreateHttp(requestUrl);
			request.Accept = "application/json";
			request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			request.Method = "GET";
			request.Timeout = 30 * 1000;

			using (var response = await request.GetResponseAsync()) {
				using (var responseStream = response.GetResponseStream()) {
					using (var responseStreamReader = new StreamReader(responseStream)) {
						return await responseStreamReader.ReadToEndAsync();
					}
				}
			}
		}

		protected Task<String> PostAndGetHttpResponseStringAsync(String requestUrl, String requestBody) {
			var webClient = new WebClient();

			return webClient.UploadStringTaskAsync(requestUrl, "POST", requestBody);
		}

		protected readonly String _apiBaseAddress;
	}
}
