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
using CryptoCurrencyMonitor.MarketData.Monitor.Settings;
using LayoutSettings = CryptoCurrencyMonitor.MarketData.Monitor.Settings.LayoutSettings;
using Timer = System.Timers.Timer;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();

			_coinMarketCapClient = new ApiClient(ConfigurationManager.AppSettings["COINMARKETCAP_API_BASE_ADDRESS"]);
			_desiredHoldingsCurrencyTypes = ConfigurationManager.AppSettings["HOLDINGS_CURRENCY_LIST"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c.Trim())).ToList();
			_desiredMarketCurrencyTypes = ConfigurationManager.AppSettings["CURRENCY_LIST"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c.Trim())).ToList();
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
			var completeSettings = _settingsManager.LoadCompleteSettings();

			var holdings = completeSettings?.Holdings;
			if (holdings != null) {
				foreach (var row in holdings) {
					var correspondingRow = _holdingsDataGridViewRows.SingleOrDefault(r => row.RowTag.Equals(r.Tag));
					if (correspondingRow != null) {
						correspondingRow.Cells[_clmnHoldingsQuantity.Index].Value = row.Value;
					}
				}
			}

			var layoutSettings = completeSettings?.Layout;
			if (layoutSettings != null) {
				Size = new Size(layoutSettings.Width, layoutSettings.Height);
				Location = new Point(layoutSettings.LocationX, layoutSettings.LocationY);
				_cntnrGridData.SplitterDistance = layoutSettings.GridContainerSplitterPosition;

				foreach (var column in layoutSettings.GridHoldingsColumns) {
					var correspondingColumn = _holdingsDataGridViewColumns.SingleOrDefault(c => column.Tag.Equals(c.Tag));
					if (correspondingColumn != null) {
						correspondingColumn.FillWeight = column.FillWeight;
						correspondingColumn.Width = column.Width;
					}
				}

				foreach (var column in layoutSettings.GridMarketColumns) {
					var correspondingColumn = _marketDataGridViewColumns.SingleOrDefault(c => column.Tag.Equals(c.Tag));
					if (correspondingColumn != null) {
						correspondingColumn.FillWeight = column.FillWeight;
						correspondingColumn.Width = column.Width;
					}
				}

				WindowState = layoutSettings.WindowState;
			}
		}

		private String FormatPercentChange(decimal percentChange) {
			//Use Math.abs() to avoid showing the 'built in' minus sign for negative numbers
			return $"{(percentChange == 0 ? " " : (percentChange > 0 ? "+" : "-"))} {Math.Abs(percentChange)}";
		}

		private void InitializeDataGridViewFields() {
			_holdingsDataGridViewColumns = _holdingsDataGridViewColumns ?? _gridHoldingsData.Columns.Cast<DataGridViewColumn>();
			_holdingsDataGridViewRows = _holdingsDataGridViewRows ?? _gridHoldingsData.Rows.Cast<DataGridViewRow>();
			_marketDataGridViewColumns = _marketDataGridViewColumns ?? _gridMarketData.Columns.Cast<DataGridViewColumn>();
			_marketDataGridViewRows = _marketDataGridViewRows != null ? (_marketDataGridViewRows.Any() ? _marketDataGridViewRows : _gridMarketData.Rows.Cast<DataGridViewRow>()) : _gridMarketData.Rows.Cast<DataGridViewRow>();
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
				newRow.SetValues(CurrencyTypeRegistry.GetNameAndSymbolFromId(desiredCurrency).Item1, "0.0", "0.00", "0.00000000");
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
				case MarketDataColumnType.PriceInUsd:
				case MarketDataColumnType.Rank:
				case MarketDataColumnType.Satoshi:
				case MarketDataColumnType.VolumeInUsd24H:
					e.SortResult = decimal.Parse(e.CellValue1.ToString().Replace(",", String.Empty)).CompareTo(decimal.Parse(e.CellValue2.ToString().Replace(",", String.Empty)));
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

				case HoldingsDataColumnType.PriceInBtc:
				case HoldingsDataColumnType.PriceInUsd:
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

		private async Task RefreshAllData() {
			_btnPauseRefreshTimer.Enabled = false;
			_globalRefreshTimer.Stop();
			_lblLastUpdatedValue.Text = $"{_lblLastUpdatedValue.Text} [IN PROGRESS]";
			_ntfyMain.Text = "Updating...";

			try {
				await RefreshMarketData();
				RefreshHoldingsData();
			} catch (Exception e) {
				//TODO: Coming soon...
			}

			_lblLastUpdatedValue.Text = DateTime.Now.ToLongTimeString();
			_ntfyMain.Text = $"{_lblLastUpdated.Text} {_lblLastUpdatedValue.Text}";
			_globalRefreshTimer.Start();
			_btnPauseRefreshTimer.Enabled = true;
		}

		private void RefreshHoldingsData() {
			InitializeDataGridViewFields();
			decimal overallPriceInBtc = 0, overallPriceInUsd = 0;
			
			foreach (var row in _holdingsDataGridViewRows) {
				var correspondingMarketRow = _marketDataGridViewRows.SingleOrDefault(r => r.Tag.Equals(row.Tag));
				if (correspondingMarketRow != null) {
					var priceInBtc = decimal.Parse(correspondingMarketRow.Cells[_clmnMarketCurrentBtcPrice.Index].Value.ToString());
					var priceInUsd = decimal.Parse(correspondingMarketRow.Cells[_clmnMarketCurrentUsdPrice.Index].Value.ToString());

					//Use EditedFormattedValue here because if this was called from the DataCellValidating event then the Value
					// field will not yet have the new value.
					var quantity = decimal.Parse(row.Cells[_clmnHoldingsQuantity.Index].EditedFormattedValue.ToString());
					var totalPriceInBtc = quantity * priceInBtc;
					var totalPriceInUsd = quantity * priceInUsd;

					overallPriceInBtc += totalPriceInBtc;
					overallPriceInUsd += totalPriceInUsd;

					row.Cells[_clmnHoldingsPriceInUsd.Index].Value = $"{totalPriceInUsd:N}";
					row.Cells[_clmnHoldingsPriceInBtc.Index].Value = $"{totalPriceInBtc:N8}";
				}
			}

			_lblTotalValBtcValue.Text = $"{overallPriceInBtc:N8}";
			_lblTotalValUsdValue.Text = $"{overallPriceInUsd:N}";
		}

		private async Task RefreshMarketData() {
			var tickerData = await _coinMarketCapClient.GetTicker();

			_gridMarketData.Rows.Clear();
			foreach (var desiredCurrency in _desiredMarketCurrencyTypes) {
				var currencyTicker = tickerData.SingleOrDefault(c => c.Id == desiredCurrency);

				if (currencyTicker != null) {
					var newRow = new DataGridViewRow();
					newRow.CreateCells(_gridMarketData);
					newRow.Tag = desiredCurrency;
					newRow.SetValues(currencyTicker.Symbol, $"{currencyTicker.PriceInUsd:N6}", $"{currencyTicker.PriceInBtc:N8}", $"{currencyTicker.PriceInBtc / 0.00000001m:N0}", $"{FormatPercentChange(currencyTicker.PercentChange1H)}", $"{FormatPercentChange(currencyTicker.PercentChange24H)}", $"{FormatPercentChange(currencyTicker.PercentChange7D)}", $"{currencyTicker.VolumeInUsd24H:N}", $"{currencyTicker.MarketCapInUsd:N}", currencyTicker.Rank);

					_gridMarketData.Rows.Add(newRow);
				}
			}
		}

		private void SaveMainFormSettings() {
			var completeSettings = new CompleteSettings {
				Holdings = new List<HoldingsDataGridViewCellSettings>(),
				Layout = new LayoutSettings {
					GridContainerSplitterPosition = _cntnrGridData.SplitterDistance,
					GridHoldingsColumns = new List<HoldingsDataGridViewColumnSettings>(),
					GridMarketColumns = new List<MarketDataGridViewColumnSettings>(),
					Height = Size.Height,
					LocationX = Location.X,
					LocationY = Location.Y,
					Width = Size.Width,
					WindowState = WindowState
				}
			};

			foreach (var column in _holdingsDataGridViewColumns) {
				var settings = new HoldingsDataGridViewColumnSettings {
					FillWeight = column.FillWeight,
					Tag = (HoldingsDataColumnType)column.Tag,
					Width = column.Width
				};
				completeSettings.Layout.GridHoldingsColumns.Add(settings);
			}

			foreach (var row in _holdingsDataGridViewRows) {
				var settings = new HoldingsDataGridViewCellSettings {
					RowTag = (int)row.Tag,
					Value = row.Cells[_clmnHoldingsQuantity.Index].Value.ToString()
				};
				completeSettings.Holdings.Add(settings);
			}

			foreach (var column in _marketDataGridViewColumns) {
				var settings = new MarketDataGridViewColumnSettings {
					FillWeight = column.FillWeight,
					Tag = (MarketDataColumnType)column.Tag,
					Width = column.Width
				};
				completeSettings.Layout.GridMarketColumns.Add(settings);
			}

			_settingsManager.SaveCompleteSettings(completeSettings);
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
		private readonly ICollection<int> _desiredMarketCurrencyTypes;
		private readonly ICollection<int> _desiredHoldingsCurrencyTypes;
		private Timer _globalRefreshTimer;
		private IEnumerable<DataGridViewColumn> _holdingsDataGridViewColumns;
		private IEnumerable<DataGridViewRow> _holdingsDataGridViewRows;
		private IEnumerable<DataGridViewColumn> _marketDataGridViewColumns;
		private IEnumerable<DataGridViewRow> _marketDataGridViewRows;
		private readonly SettingsManager _settingsManager;
	}
}
