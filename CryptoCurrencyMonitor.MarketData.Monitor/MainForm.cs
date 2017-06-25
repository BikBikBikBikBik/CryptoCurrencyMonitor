using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CryptoCurrencyMonitor.MarketData.Client;
using CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap;
using CryptoCurrencyMonitor.MarketData.Client.Extensions;
using Timer = System.Timers.Timer;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();

			_coinMarketCapClient = new ApiClient(ConfigurationManager.AppSettings["COINMARKETCAP_API_BASE_ADDRESS"]);
			_cryptoCompareClient = new Client.CryptoCompare.ApiClient(ConfigurationManager.AppSettings["CRYPTOCOMPARE_API_BASE_ADDRESS"]);
			_desiredExchangeCurrencyTypes = ConfigurationManager.AppSettings["CURRENCY_LIST"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToCurrencyType()).ToList();
			_desiredHoldingsCurrencyTypes = ConfigurationManager.AppSettings["HOLDINGS_CURRENCY_LIST"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToCurrencyType()).ToList();

			_lblLastUpdatedValue.Text = String.Empty;
			_lblTotalValBtcValue.Text = "0.00000000";
			_lblTotalValUsdValue.Text = "0.00";
			_ntfyMain.Text = String.Empty;
			//_txtbxErrors.Text = String.Empty;
			
			SetExchangeDataColumnTags();
			SetHoldingsDataColumnType();
			InitializeGlobalRefreshTimer();
			InitializeHoldingsData();
		}

		private String FormatPercentChange(decimal percentChange) {
			//Use Math.abs() to avoid showing the 'built in' minus sign for negative numbers
			return $"{(percentChange == 0 ? " " : (percentChange > 0 ? "+" : "-"))} {Math.Abs(percentChange)}";
		}

		private int HandlePriceInUsdColumnSort(Object cellValue1, Object cellValue2) {
			var row1Prices = ParsePriceInUsdCell(cellValue1);
			var row2Prices = ParsePriceInUsdCell(cellValue2);

			if (row1Prices.Item1.HasValue && row2Prices.Item1.HasValue) {
				return row1Prices.Item1.Value.CompareTo(row2Prices.Item1.Value);
			}
			if (row1Prices.Item2.HasValue && row2Prices.Item2.HasValue) {
				return row1Prices.Item2.Value.CompareTo(row2Prices.Item2.Value);
			}

			return 0;
		}

		private void InitializeGlobalRefreshTimer() {
			_globalRefreshTimer = new Timer(double.Parse(ConfigurationManager.AppSettings["REFRESH_INTERVAL"]) / 100d);
			_globalRefreshTimer.Elapsed += OnGlobalRefreshTimerElapsed;
			_globalRefreshTimer.SynchronizingObject = this;
			_globalRefreshTimer.Start();
		}

		private void InitializeHoldingsData() {
			_gridHoldingsData.Rows.Clear();

			for (var i = 0; i < _desiredHoldingsCurrencyTypes.Count; i++) {
				var desiredCurrency = _desiredHoldingsCurrencyTypes.ElementAt(i);
				var newRow = new DataGridViewRow();
				newRow.CreateCells(_gridHoldingsData);
				newRow.SetValues(desiredCurrency.ToString(), "0.0", "0.00 / 0.00", "0.00000000");
				newRow.Tag = desiredCurrency;

				_gridHoldingsData.Rows.Add(newRow);
			}
		}

		#region Event Handlers
		private void OnBtnPauseRefreshTimerClick(object sender, EventArgs e) {
			if (_globalRefreshTimer.Enabled) {
				_globalRefreshTimer.Stop();
				_btnPauseRefreshTimer.Text = "Resume";
			} else {
				_globalRefreshTimer.Start();
				_btnPauseRefreshTimer.Text = "Pause";
			}
		}

		private async void OnFormMainLoad(object sender, EventArgs e) {
			await RefreshAllData();
		}

		private void OnFormMainResize(object sender, EventArgs e) {
			if (WindowState == FormWindowState.Minimized) {
				Hide();
			}
		}

		private async void OnGlobalRefreshTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs) {
			if (_prgrssGlobalRefresh.Value == _prgrssGlobalRefresh.Maximum) {
				_prgrssGlobalRefresh.Value = 0;
				
				await RefreshAllData();
			}

			_prgrssGlobalRefresh.Value += 1;
		}

		private void OnGridExchangeDataCellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
			switch ((ExchangeDataColumnType)_gridExchangeData.Columns[e.ColumnIndex].Tag) {
				case ExchangeDataColumnType.PercentChange1H:
				case ExchangeDataColumnType.PercentChange24H:
				case ExchangeDataColumnType.PercentChange7D:
					var initialChar = e.Value?.ToString()[0];

					switch (initialChar) {
						case '+':
							_gridExchangeData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
						break;

						case '-':
							_gridExchangeData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
						break;

						default:
							_gridExchangeData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = _gridExchangeData.Columns[e.ColumnIndex].DefaultCellStyle;
						break;
					}
				break;
			}
		}

		private void OnGridExchangeDataSortCompare(object sender, DataGridViewSortCompareEventArgs e) {
			switch ((ExchangeDataColumnType)e.Column.Tag) {
				case ExchangeDataColumnType.Coin:
					e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
				break;
				
				case ExchangeDataColumnType.PercentChange1H:
				case ExchangeDataColumnType.PercentChange24H:
				case ExchangeDataColumnType.PercentChange7D:
					e.SortResult = decimal.Parse(e.CellValue1.ToString().Replace(" ", String.Empty)).CompareTo(decimal.Parse(e.CellValue2.ToString().Replace(" ", String.Empty)));
				break;

				case ExchangeDataColumnType.MarketCapInUsd:
				case ExchangeDataColumnType.PriceInBtc:
				case ExchangeDataColumnType.VolumeInUsd24H:
					e.SortResult = decimal.Parse(e.CellValue1.ToString().Replace(",", String.Empty)).CompareTo(decimal.Parse(e.CellValue2.ToString().Replace(",", String.Empty)));
				break;
				
				case ExchangeDataColumnType.PriceInUsd:
					e.SortResult = HandlePriceInUsdColumnSort(e.CellValue1, e.CellValue2);
				break;

				case ExchangeDataColumnType.Rank:
				case ExchangeDataColumnType.Satoshi:
					e.SortResult = int.Parse(e.CellValue1.ToString().Replace(",", String.Empty)).CompareTo(int.Parse(e.CellValue2.ToString().Replace(",", String.Empty)));
				break;
			}

			e.Handled = true;
		}

		private void OnGridHoldingsDataCellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
			var columnTag = _gridHoldingsData.Columns[e.ColumnIndex].Tag;
			if (columnTag is HoldingsDataColumnType) {
				var specificColumnTag = (HoldingsDataColumnType)columnTag;

				switch (specificColumnTag) {
					case HoldingsDataColumnType.Quantity:
						if (e.FormattedValue == null || String.IsNullOrWhiteSpace(e.FormattedValue.ToString())) {
							e.Cancel = true;
						} else {
							decimal quantity;
							if (!decimal.TryParse(e.FormattedValue.ToString(), out quantity)) {
								e.Cancel = true;
							}
						}

						if (!e.Cancel) {
							RefreshHoldingsData();
						}
					break;
				}
			}
		}

		private void OnGridHoldingsDataSortCompare(object sender, DataGridViewSortCompareEventArgs e) {
			switch ((HoldingsDataColumnType)e.Column.Tag) {
				case HoldingsDataColumnType.Coin:
					e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
				break;

				case HoldingsDataColumnType.PriceInUsd:
					e.SortResult = HandlePriceInUsdColumnSort(e.CellValue1, e.CellValue2);
				break;

				case HoldingsDataColumnType.PriceInBtc:
				case HoldingsDataColumnType.Quantity:
					e.SortResult = decimal.Parse(e.CellValue1.ToString().Replace(",", String.Empty)).CompareTo(decimal.Parse(e.CellValue2.ToString().Replace(",", String.Empty)));
				break;
			}

			e.Handled = true;
		}

		private void OnNtfyMainMouseDoubleClick(object sender, MouseEventArgs e) {
			if (WindowState == FormWindowState.Minimized) {
				Show();
				WindowState = FormWindowState.Normal;
			} else {
				Hide();
				WindowState = FormWindowState.Minimized;
			}
		}
		#endregion

		private Tuple<decimal?, decimal?> ParsePriceInUsdCell(Object cellValue) {
			var cellValues = cellValue.ToString().Split(new [] { " / " }, StringSplitOptions.RemoveEmptyEntries);

			decimal cellValue1, cellValue2;
			var cellValue1IsValid = decimal.TryParse(cellValues[0], out cellValue1);
			var cellValue2IsValid = decimal.TryParse(cellValues[1], out cellValue2);

			if (!cellValue1IsValid && !cellValue2IsValid) {
				return new Tuple<decimal?, decimal?>(null, null);
			}
			if (!cellValue1IsValid) {
				return new Tuple<decimal?, decimal?>(null, cellValue2);
			}
			if (!cellValue2IsValid) {
				return new Tuple<decimal?, decimal?>(cellValue1, null);
			}

			return new Tuple<decimal?, decimal?>(cellValue1, cellValue2);
		}

		private async Task RefreshAllData() {
			await RefreshExchangeData();
			RefreshHoldingsData();
		}

		private async Task RefreshExchangeData() {
			_btnPauseRefreshTimer.Enabled = false;
			_globalRefreshTimer.Stop();
			_lblLastUpdatedValue.Text = $"{_lblLastUpdatedValue.Text} [IN PROGRESS]";
			_ntfyMain.Text = "Updating...";
			try {
				await RetrieveAndDisplayExchangeData();
			} catch (Exception e) {
				var curException = e;
				var indentLevel = 0;
				while (curException != null) {
					var indent = new String(' ', indentLevel);
					var dateIndent = new String('-', indentLevel);
					//_txtbxErrors.AppendText($"{dateIndent}{DateTime.Now:F}{Environment.NewLine}{indent}{curException.GetType().FullName}{Environment.NewLine}{indent}{curException.Message}{Environment.NewLine}{indent}{curException.StackTrace}{Environment.NewLine}");

					curException = curException.InnerException;
					indentLevel += 2;
				}
				//_txtbxErrors.AppendText($"------------------------------------------------------------------------------------------{Environment.NewLine}");
			}
			_lblLastUpdatedValue.Text = DateTime.Now.ToLongTimeString();
			_ntfyMain.Text = $"{_lblLastUpdated.Text} {_lblLastUpdatedValue.Text}";
			_globalRefreshTimer.Start();
			_btnPauseRefreshTimer.Enabled = true;
		}

		private void RefreshHoldingsData() {
			var exchangeDataGridViewColumns = _gridExchangeData.Columns.Cast<DataGridViewColumn>().ToList();
			var exchangeDataGridViewRows = _gridExchangeData.Rows.Cast<DataGridViewRow>().ToList();
			var holdingsDataGridViewColumns = _gridHoldingsData.Columns.Cast<DataGridViewColumn>().ToList();
			var exchangePriceInBtcColumn = exchangeDataGridViewColumns.Single(c => ExchangeDataColumnType.PriceInBtc.Equals(c.Tag));
			var exchangePriceInUsdColumn = exchangeDataGridViewColumns.Single(c => ExchangeDataColumnType.PriceInUsd.Equals(c.Tag));
			var holdingsPriceInBtcColumn = holdingsDataGridViewColumns.Single(c => HoldingsDataColumnType.PriceInBtc.Equals(c.Tag));
			var holdingsPriceInUsdColumn = holdingsDataGridViewColumns.Single(c => HoldingsDataColumnType.PriceInUsd.Equals(c.Tag));
			var holdingsQuantityColumn = holdingsDataGridViewColumns.Single(c => HoldingsDataColumnType.Quantity.Equals(c.Tag));
			decimal overallPriceInBtc = 0, overallPriceInUsd1 = 0, overallPriceInUsd2 = 0;
			
			foreach (var row in _gridHoldingsData.Rows.Cast<DataGridViewRow>()) {
				var correspondingExchangeRow = exchangeDataGridViewRows.SingleOrDefault(r => r.Tag.Equals(row.Tag));
				if (correspondingExchangeRow != null) {
					var priceInBtc = decimal.Parse(correspondingExchangeRow.Cells[exchangePriceInBtcColumn.Index].Value.ToString());
					var priceInUsd = ParsePriceInUsdCell(correspondingExchangeRow.Cells[exchangePriceInUsdColumn.Index].Value);
					var price1 = priceInUsd.Item1 ?? 0;
					var price2 = priceInUsd.Item2 ?? 0;

					//Use EditedFormattedValue here because if this was called from the DataCellValidating event then the Value
					// field will not yet have the new value.
					var quantity = decimal.Parse(row.Cells[holdingsQuantityColumn.Index].EditedFormattedValue.ToString());
					var totalPriceInBtc = quantity * priceInBtc;
					var totalPriceInUsd1 = quantity * price1;
					var totalPriceInUsd2 = quantity * price2;

					overallPriceInBtc += totalPriceInBtc;
					overallPriceInUsd1 += totalPriceInUsd1;
					overallPriceInUsd2 += totalPriceInUsd2;

					row.Cells[holdingsPriceInUsdColumn.Index].Value = $"{totalPriceInUsd1:N} / {totalPriceInUsd2:N}";
					row.Cells[holdingsPriceInBtcColumn.Index].Value = $"{totalPriceInBtc:N8}";
				}
			}

			_lblTotalValBtcValue.Text = $"{overallPriceInBtc:N8}";
			_lblTotalValUsdValue.Text = $"{overallPriceInUsd1:N} / {overallPriceInUsd2:N}";
		}

		private async Task RetrieveAndDisplayExchangeData() {
			var exchangeRateData = await _cryptoCompareClient.GetPriceAsync(CurrencyType.USD, _desiredExchangeCurrencyTypes.ToArray());
			var tickerData = await _coinMarketCapClient.GetTicker();

			_gridExchangeData.Rows.Clear();
			foreach (var desiredCurrency in _desiredExchangeCurrencyTypes) {
				var currencyTicker = tickerData.SingleOrDefault(c => c.Symbol == desiredCurrency);
				var exchangeData = exchangeRateData.ContainsKey(desiredCurrency) ? RoundPriceInUsd(1m / exchangeRateData[desiredCurrency]) : (decimal?)null;

				var newRow = new DataGridViewRow();
				newRow.CreateCells(_gridExchangeData);
				newRow.Tag = desiredCurrency;

				if (currencyTicker != null) {
					var priceInUsd = $"{RoundPriceInUsd(currencyTicker.PriceInUsd)} / {exchangeData?.ToString() ?? "???"}";

					newRow.SetValues(desiredCurrency, priceInUsd, currencyTicker.PriceInBtc, $"{currencyTicker.PriceInBtc / 0.00000001m:N0}", $"{FormatPercentChange(currencyTicker.PercentChange1H)}", $"{FormatPercentChange(currencyTicker.PercentChange24H)}", $"{FormatPercentChange(currencyTicker.PercentChange7D)}", $"{currencyTicker.VolumeInUsd24H:N}", $"{currencyTicker.MarketCapInUsd:N}", currencyTicker.Rank);
				} else if (exchangeData != null) {
					newRow.SetValues(desiredCurrency, $"??? / {exchangeData}");
				} else {
					newRow.Tag = null;
				}

				if (newRow.Tag != null) {
					_gridExchangeData.Rows.Add(newRow);
				}
			}
		}

		private decimal RoundPriceInUsd(decimal priceInUsd) {
			return Math.Round(priceInUsd, 6);
		}

		private void SetExchangeDataColumnTags() {
			_clmnExchangeCoin.Tag = ExchangeDataColumnType.Coin;
			_clmnExchangeCurrentBtcPrice.Tag = ExchangeDataColumnType.PriceInBtc;
			_clmnExchangeCurrentUsdPrice.Tag = ExchangeDataColumnType.PriceInUsd;
			_clmnExchangeMarketCapUsd.Tag = ExchangeDataColumnType.MarketCapInUsd;
			_clmnExchangePercentChange1h.Tag = ExchangeDataColumnType.PercentChange1H;
			_clmnExchangePercentChange24h.Tag = ExchangeDataColumnType.PercentChange24H;
			_clmnExchangePercentChange7D.Tag = ExchangeDataColumnType.PercentChange7D;
			_clmnExchangeRank.Tag = ExchangeDataColumnType.Rank;
			_clmnExchangeSatoshi.Tag = ExchangeDataColumnType.Satoshi;
			_clmnExchangeVolumeUsd24h.Tag = ExchangeDataColumnType.VolumeInUsd24H;
		}

		private void SetHoldingsDataColumnType() {
			_clmnHoldingsCoin.Tag = HoldingsDataColumnType.Coin;
			_clmnHoldingsPriceInBtc.Tag = HoldingsDataColumnType.PriceInBtc;
			_clmnHoldingsPriceInUsd.Tag = HoldingsDataColumnType.PriceInUsd;
			_clmnHoldingsQuantity.Tag = HoldingsDataColumnType.Quantity;
		}

		private readonly ApiClient _coinMarketCapClient;
		private readonly Client.CryptoCompare.ApiClient _cryptoCompareClient;
		private Timer _globalRefreshTimer;
		private readonly ICollection<CurrencyType> _desiredExchangeCurrencyTypes;
		private readonly ICollection<CurrencyType> _desiredHoldingsCurrencyTypes;
	}
}
