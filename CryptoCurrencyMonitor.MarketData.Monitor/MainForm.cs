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
using CryptoCurrencyMonitor.MarketData.Monitor.Settings;
using Timer = System.Timers.Timer;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();

			_coinMarketCapClient = new ApiClient(ConfigurationManager.AppSettings["COINMARKETCAP_API_BASE_ADDRESS"]);
			_cryptoCompareClient = new Client.CryptoCompare.ApiClient(ConfigurationManager.AppSettings["CRYPTOCOMPARE_API_BASE_ADDRESS"]);
			_desiredHoldingsCurrencyTypes = ConfigurationManager.AppSettings["HOLDINGS_CURRENCY_LIST"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToCurrencyType()).ToList();
			_desiredMarketCurrencyTypes = ConfigurationManager.AppSettings["CURRENCY_LIST"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToCurrencyType()).ToList();
			_settingsManager = new SettingsManager(ConfigurationManager.AppSettings["SETTINGS_FILE_LOCATION"]);

			_lblLastUpdatedValue.Text = String.Empty;
			_lblTotalValBtcValue.Text = "0.00000000";
			_lblTotalValUsdValue.Text = "0.00";
			_ntfyMain.Text = String.Empty;
			
			SetMarketDataColumnTags();
			SetHoldingsDataColumnType();
			InitializeGlobalRefreshTimer();
			InitializeHoldingsData();
		}

		private void ApplyMainFormSettings() {
			var settings = _settingsManager.LoadCompleteSettings()?.MainForm;
			if (settings == null) {
				return;
			}
			
			Size = new Size(settings.Width, settings.Height);
			Location = new Point(settings.LocationX, settings.LocationY);
			_cntnrGridData.SplitterDistance = settings.GridContainerSplitterPosition;

			foreach (var column in settings.GridHoldingsColumns) {
				var correspondingColumn = _holdingsDataGridViewColumns.SingleOrDefault(c => column.Tag.Equals(c.Tag));
				if (correspondingColumn != null) {
					correspondingColumn.FillWeight = column.FillWeight;
					correspondingColumn.Width = column.Width;
				}
			}

			foreach (var row in settings.GridHoldingsQuantities) {
				var correspondingRow = _holdingsDataGridViewRows.SingleOrDefault(r => row.Tag.Equals(r.Tag));
				if (correspondingRow != null) {
					correspondingRow.Cells[_clmnHoldingsQuantity.Index].Value = row.Value;
				}
			}

			foreach (var column in settings.GridMarketColumns) {
				var correspondingColumn = _marketDataGridViewColumns.SingleOrDefault(c => column.Tag.Equals(c.Tag));
				if (correspondingColumn != null) {
					correspondingColumn.FillWeight = column.FillWeight;
					correspondingColumn.Width = column.Width;
				}
			}

			WindowState = settings.WindowState;
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

		private void InitializeDataGridViewFields() {
			_holdingsDataGridViewColumns = _holdingsDataGridViewColumns ?? _gridHoldingsData.Columns.Cast<DataGridViewColumn>().ToList();
			_holdingsDataGridViewRows = _holdingsDataGridViewRows ?? _gridHoldingsData.Rows.Cast<DataGridViewRow>().ToList();
			_marketDataGridViewColumns = _marketDataGridViewColumns ?? _gridMarketData.Columns.Cast<DataGridViewColumn>().ToList();
			_marketDataGridViewRows = _marketDataGridViewRows != null ? (_marketDataGridViewRows.Count > 0 ? _marketDataGridViewRows : _gridMarketData.Rows.Cast<DataGridViewRow>().ToList()) : _gridMarketData.Rows.Cast<DataGridViewRow>().ToList();
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

		private void OnFormMainClosed(object sender, FormClosedEventArgs e) {
			SaveMainFormSettings();
		}

		private async void OnFormMainLoad(object sender, EventArgs e) {
			InitializeDataGridViewFields();
			ApplyMainFormSettings();
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

		private void OnGridMarketDataCellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
			switch ((MarketDataColumnType)_gridMarketData.Columns[e.ColumnIndex].Tag) {
				case MarketDataColumnType.PercentChange1H:
				case MarketDataColumnType.PercentChange24H:
				case MarketDataColumnType.PercentChange7D:
					var initialChar = e.Value?.ToString()[0];

					switch (initialChar) {
						case '+':
							_gridMarketData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
						break;

						case '-':
							_gridMarketData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
						break;

						default:
							_gridMarketData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = _gridMarketData.Columns[e.ColumnIndex].DefaultCellStyle;
						break;
					}
				break;
			}
		}

		private void OnGridMarketDataSortCompare(object sender, DataGridViewSortCompareEventArgs e) {
			switch ((MarketDataColumnType)e.Column.Tag) {
				case MarketDataColumnType.Coin:
					e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
				break;
				
				case MarketDataColumnType.PercentChange1H:
				case MarketDataColumnType.PercentChange24H:
				case MarketDataColumnType.PercentChange7D:
					e.SortResult = decimal.Parse(e.CellValue1.ToString().Replace(" ", String.Empty)).CompareTo(decimal.Parse(e.CellValue2.ToString().Replace(" ", String.Empty)));
				break;

				case MarketDataColumnType.MarketCapInUsd:
				case MarketDataColumnType.PriceInBtc:
				case MarketDataColumnType.VolumeInUsd24H:
					e.SortResult = decimal.Parse(e.CellValue1.ToString().Replace(",", String.Empty)).CompareTo(decimal.Parse(e.CellValue2.ToString().Replace(",", String.Empty)));
				break;
				
				case MarketDataColumnType.PriceInUsd:
					e.SortResult = HandlePriceInUsdColumnSort(e.CellValue1, e.CellValue2);
				break;

				case MarketDataColumnType.Rank:
				case MarketDataColumnType.Satoshi:
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
			await RefreshMarketData();
			RefreshHoldingsData();
		}

		private void RefreshHoldingsData() {
			InitializeDataGridViewFields();
			decimal overallPriceInBtc = 0, overallPriceInUsd1 = 0, overallPriceInUsd2 = 0;
			
			foreach (var row in _gridHoldingsData.Rows.Cast<DataGridViewRow>()) {
				var correspondingMarketRow = _marketDataGridViewRows.SingleOrDefault(r => r.Tag.Equals(row.Tag));
				if (correspondingMarketRow != null) {
					var priceInBtc = decimal.Parse(correspondingMarketRow.Cells[_clmnMarketCurrentBtcPrice.Index].Value.ToString());
					var priceInUsd = ParsePriceInUsdCell(correspondingMarketRow.Cells[_clmnMarketCurrentUsdPrice.Index].Value);
					var price1 = priceInUsd.Item1 ?? 0;
					var price2 = priceInUsd.Item2 ?? 0;

					//Use EditedFormattedValue here because if this was called from the DataCellValidating event then the Value
					// field will not yet have the new value.
					var quantity = decimal.Parse(row.Cells[_clmnHoldingsQuantity.Index].EditedFormattedValue.ToString());
					var totalPriceInBtc = quantity * priceInBtc;
					var totalPriceInUsd1 = quantity * price1;
					var totalPriceInUsd2 = quantity * price2;

					overallPriceInBtc += totalPriceInBtc;
					overallPriceInUsd1 += totalPriceInUsd1;
					overallPriceInUsd2 += totalPriceInUsd2;

					row.Cells[_clmnHoldingsPriceInUsd.Index].Value = $"{totalPriceInUsd1:N} / {totalPriceInUsd2:N}";
					row.Cells[_clmnHoldingsPriceInBtc.Index].Value = $"{totalPriceInBtc:N8}";
				}
			}

			_lblTotalValBtcValue.Text = $"{overallPriceInBtc:N8}";
			_lblTotalValUsdValue.Text = $"{overallPriceInUsd1:N} / {overallPriceInUsd2:N}";
		}

		private async Task RefreshMarketData() {
			_btnPauseRefreshTimer.Enabled = false;
			_globalRefreshTimer.Stop();
			_lblLastUpdatedValue.Text = $"{_lblLastUpdatedValue.Text} [IN PROGRESS]";
			_ntfyMain.Text = "Updating...";
			try {
				await RetrieveAndDisplayMarketData();
			} catch (Exception e) {
				//TODO: Coming soon...
			}
			_lblLastUpdatedValue.Text = DateTime.Now.ToLongTimeString();
			_ntfyMain.Text = $"{_lblLastUpdated.Text} {_lblLastUpdatedValue.Text}";
			_globalRefreshTimer.Start();
			_btnPauseRefreshTimer.Enabled = true;
		}

		private async Task RetrieveAndDisplayMarketData() {
			var marketRateData = await _cryptoCompareClient.GetPriceAsync(CurrencyType.USD, _desiredMarketCurrencyTypes.ToArray());
			var tickerData = await _coinMarketCapClient.GetTicker();

			_gridMarketData.Rows.Clear();
			foreach (var desiredCurrency in _desiredMarketCurrencyTypes) {
				var currencyTicker = tickerData.SingleOrDefault(c => c.Symbol == desiredCurrency);
				var marketData = marketRateData.ContainsKey(desiredCurrency) ? RoundPriceInUsd(1m / marketRateData[desiredCurrency]) : (decimal?)null;

				var newRow = new DataGridViewRow();
				newRow.CreateCells(_gridMarketData);
				newRow.Tag = desiredCurrency;

				if (currencyTicker != null) {
					var priceInUsd = $"{RoundPriceInUsd(currencyTicker.PriceInUsd)} / {marketData?.ToString() ?? "???"}";

					newRow.SetValues(desiredCurrency, priceInUsd, currencyTicker.PriceInBtc, $"{currencyTicker.PriceInBtc / 0.00000001m:N0}", $"{FormatPercentChange(currencyTicker.PercentChange1H)}", $"{FormatPercentChange(currencyTicker.PercentChange24H)}", $"{FormatPercentChange(currencyTicker.PercentChange7D)}", $"{currencyTicker.VolumeInUsd24H:N}", $"{currencyTicker.MarketCapInUsd:N}", currencyTicker.Rank);
				} else if (marketData != null) {
					newRow.SetValues(desiredCurrency, $"??? / {marketData}");
				} else {
					newRow.Tag = null;
				}

				if (newRow.Tag != null) {
					_gridMarketData.Rows.Add(newRow);
				}
			}
		}

		private decimal RoundPriceInUsd(decimal priceInUsd) {
			return Math.Round(priceInUsd, 6);
		}

		private void SaveMainFormSettings() {
			var mainFormSettings = new MainFormSettings {
				GridContainerSplitterPosition = _cntnrGridData.SplitterDistance,
				GridHoldingsColumns = new List<HoldingsDataGridViewColumnSettings>(),
				GridHoldingsQuantities = new List<HoldingsDataGridViewCellSettings>(),
				GridMarketColumns = new List<MarketDataGridViewColumnSettings>(),
				Height = Size.Height,
				LocationX = Location.X,
				LocationY = Location.Y,
				Width = Size.Width,
				WindowState = WindowState
			};

			foreach (var column in _holdingsDataGridViewColumns) {
				var settings = new HoldingsDataGridViewColumnSettings {
					FillWeight = column.FillWeight,
					Tag = (HoldingsDataColumnType)column.Tag,
					Width = column.Width
				};
				mainFormSettings.GridHoldingsColumns.Add(settings);
			}

			foreach (var row in _holdingsDataGridViewRows) {
				var settings = new HoldingsDataGridViewCellSettings {
					Tag = (CurrencyType)row.Tag,
					Value = row.Cells[_clmnHoldingsQuantity.Index].Value.ToString()
				};
				mainFormSettings.GridHoldingsQuantities.Add(settings);
			}

			foreach (var column in _marketDataGridViewColumns) {
				var settings = new MarketDataGridViewColumnSettings {
					FillWeight = column.FillWeight,
					Tag = (MarketDataColumnType)column.Tag,
					Width = column.Width
				};
				mainFormSettings.GridMarketColumns.Add(settings);
			}

			_settingsManager.SaveCompleteSettings(new CompleteSettings { MainForm = mainFormSettings });
		}

		private void SetHoldingsDataColumnType() {
			_clmnHoldingsCoin.Tag = HoldingsDataColumnType.Coin;
			_clmnHoldingsPriceInBtc.Tag = HoldingsDataColumnType.PriceInBtc;
			_clmnHoldingsPriceInUsd.Tag = HoldingsDataColumnType.PriceInUsd;
			_clmnHoldingsQuantity.Tag = HoldingsDataColumnType.Quantity;
		}

		private void SetMarketDataColumnTags() {
			_clmnMarketCoin.Tag = MarketDataColumnType.Coin;
			_clmnMarketCurrentBtcPrice.Tag = MarketDataColumnType.PriceInBtc;
			_clmnMarketCurrentUsdPrice.Tag = MarketDataColumnType.PriceInUsd;
			_clmnMarketMarketCapUsd.Tag = MarketDataColumnType.MarketCapInUsd;
			_clmnMarketPercentChange1h.Tag = MarketDataColumnType.PercentChange1H;
			_clmnMarketPercentChange24h.Tag = MarketDataColumnType.PercentChange24H;
			_clmnMarketPercentChange7D.Tag = MarketDataColumnType.PercentChange7D;
			_clmnMarketRank.Tag = MarketDataColumnType.Rank;
			_clmnMarketSatoshi.Tag = MarketDataColumnType.Satoshi;
			_clmnMarketVolumeUsd24h.Tag = MarketDataColumnType.VolumeInUsd24H;
		}

		private readonly ApiClient _coinMarketCapClient;
		private readonly Client.CryptoCompare.ApiClient _cryptoCompareClient;
		private readonly ICollection<CurrencyType> _desiredMarketCurrencyTypes;
		private readonly ICollection<CurrencyType> _desiredHoldingsCurrencyTypes;
		private Timer _globalRefreshTimer;
		private ICollection<DataGridViewColumn> _holdingsDataGridViewColumns;
		private ICollection<DataGridViewRow> _holdingsDataGridViewRows;
		private ICollection<DataGridViewColumn> _marketDataGridViewColumns;
		private ICollection<DataGridViewRow> _marketDataGridViewRows;
		private readonly SettingsManager _settingsManager;
	}
}
