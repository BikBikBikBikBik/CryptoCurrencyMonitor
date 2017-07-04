using System;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	internal class Currency {
		public int Id { get; set; }

		public String Name { get; set; }

		public String NameAndSymbol => ToString();

		public String Symbol { get; set; }

		public override string ToString() {
			return $"{Symbol} ({Name})";
		}
	}
}
