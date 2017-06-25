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
