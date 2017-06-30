using CryptoCurrencyMonitor.Common.Attributes;

namespace CryptoCurrencyMonitor.MarketData.Client {
	public enum CurrencyType {
		[EnumDisplayName("Aeon")]
		AEON,

		[EnumDisplayName("AntShares")]
		ANS,

		[EnumDisplayName("Ardor")]
		ARDR,

		[EnumDisplayName("Bitcoin")]
		BTC,

		[EnumDisplayName("Espers")]
		ESP,

		[EnumDisplayName("Ethereum")]
		ETH,

		[EnumDisplayName("Golem Network Tokens")]
		GNT,

		[EnumDisplayName("GridCoin")]
		GRC,

		[EnumDisplayName("Iota")]
		IOT,

		[EnumDisplayName("LoMoCoin")]
		LMC,

		[EnumDisplayName("Mysterium")]
		MYST,

		[EnumDisplayName("Nxt")]
		NXT,

		[EnumDisplayName("Quantum Resistant Ledger")]
		QRL,

		[EnumDisplayName("Siacoin")]
		SC,

		[EnumDisplayName("Storjcoin X")]
		SJCX,

		[EnumDisplayName("Status Network Token")]
		SNT,

		[EnumDisplayName("Ubiq")]
		UBQ,

		[EnumDisplayName("USD")]
		USD,

		[EnumDisplayName("UNKNOWN_DISPLAY_NAME")]
		UNKNOWN,

		[EnumDisplayName("DigitalNote")]
		XDN,

		[EnumDisplayName("Monero")]
		XMR
	}
}
