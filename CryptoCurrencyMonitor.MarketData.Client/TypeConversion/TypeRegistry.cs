using Nelibur.ObjectMapper;

namespace CryptoCurrencyMonitor.MarketData.Client.TypeConversion {
	internal static class TypeRegistry {
		static TypeRegistry() {
			TinyMapper.Bind<CoinMarketCap.CurrencyTickerDto, CoinMarketCap.CurrencyTicker>();
		}
	}
}
