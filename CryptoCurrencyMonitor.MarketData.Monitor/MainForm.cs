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
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CryptoCurrencyMonitor.MarketData.Client;
using CryptoCurrencyMonitor.MarketData.Client.CoinMarketCap;
using CryptoCurrencyMonitor.MarketData.Monitor.Extensions;
using CryptoCurrencyMonitor.MarketData.Monitor.Settings;
using LayoutSettings = CryptoCurrencyMonitor.MarketData.Monitor.Settings.LayoutSettings;
using Timer = System.Timers.Timer;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	internal partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();

			_coinMarketCapClient = new ApiClient(ConfigurationManager.AppSettings["COINMARKETCAP_API_BASE_ADDRESS"]);
			_settingsManager = new SettingsManager(ConfigurationManager.AppSettings["SETTINGS_FILE_LOCATION"]);
			_completeSettings = _settingsManager.LoadCompleteSettings();

			_globalMarketData = new GlobalData();
			_lblLastUpdatedValue.Text = String.Empty;
			_lblTotalValBtcValue.Text = "0";
			_lblTotalValUsdValue.Text = "0.00";
			_ntfyMain.ContextMenu = new ContextMenu(new []{ new MenuItem("E&xit", (s, a) => Application.Exit()) });
			_ntfyMain.Text = String.Empty;
			_tickerData = new List<CurrencyTicker>();
			
			SetMarketDataColumnTags();
			SetHoldingsDataColumnType();
			InitializeGlobalRefreshTimer();
		}

		private void ApplyCompleteSettings() {
			ApplyLayoutSettings(_completeSettings.Layout);
			ApplyMonitoringSettings(_completeSettings.Monitoring);
		}

		private void ApplyLayoutSettings(LayoutSettings layoutSettings) {
			if (layoutSettings != null) {
				Size = new Size(layoutSettings.Width, layoutSettings.Height);
				Location = new Point(layoutSettings.LocationX, layoutSettings.LocationY);
				_cntnrGridData.SplitterDistance = layoutSettings.GridContainerSplitterPosition > 0 ? layoutSettings.GridContainerSplitterPosition : _cntnrGridData.SplitterDistance;

				if (layoutSettings.GridHoldingsColumns != null) {
					foreach (var column in layoutSettings.GridHoldingsColumns) {
						var correspondingColumn = _holdingsDataGridViewColumns.SingleOrDefault(c => column.Tag.Equals(c.Tag));
						if (correspondingColumn != null) {
							correspondingColumn.FillWeight = column.FillWeight;
							correspondingColumn.Width = column.Width;
						}
					}
				}

				if (layoutSettings.GridMarketColumns != null) {
					foreach (var column in layoutSettings.GridMarketColumns) {
						var correspondingColumn = _marketDataGridViewColumns.SingleOrDefault(c => column.Tag.Equals(c.Tag));
						if (correspondingColumn != null) {
							correspondingColumn.FillWeight = column.FillWeight;
							correspondingColumn.Width = column.Width;
						}
					}
				}

				WindowState = layoutSettings.WindowState;
			}
		}

		private void ApplyMonitoringHoldingsSettings(MonitoringSettings monitoringSettings) {
			_gridHoldingsData.Rows.Clear();
			foreach (var desiredCurrency in monitoringSettings.Holdings) {
				var newRow = new DataGridViewRow();
				newRow.CreateCells(_gridHoldingsData);
				newRow.SetValues(FormatCurrencyForDisplay(desiredCurrency.RowTag, monitoringSettings.CurrencyDisplayType), decimal.Parse(desiredCurrency.Value), "0.00", 0);
				newRow.Tag = desiredCurrency.RowTag;

				_gridHoldingsData.Rows.Add(newRow);
			}
		}

		private void ApplyMonitoringMarketSettings(MonitoringSettings monitoringSettings) {
			_gridMarketData.Rows.Clear();
			foreach (var desiredCurrency in monitoringSettings.MarketCurrencyTypes) {
				var newRow = new DataGridViewRow();
				newRow.CreateCells(_gridMarketData);
				newRow.Tag = desiredCurrency;
				newRow.SetValues(FormatCurrencyForDisplay(desiredCurrency, monitoringSettings.CurrencyDisplayType), "0.00", 0, 0, 0, 0, 0, "0.00", "0.00", 0);

				_gridMarketData.Rows.Add(newRow);
			}
		}

		private void ApplyMonitoringSettings(MonitoringSettings monitoringSettings) {
			ApplyMonitoringMarketSettings(monitoringSettings);
			ApplyMonitoringHoldingsSettings(monitoringSettings);
			UpdateRefreshInterval(monitoringSettings.RefreshInterval);
		}

		private String FormatCurrencyForDisplay(int currencyId, CurrencyDisplayType displayType) {
			var currency = CurrencyTypeRegistry.GetNameAndSymbolFromId(currencyId);

			switch (displayType) {
				case CurrencyDisplayType.Name:
					return currency.Item2;
				
				case CurrencyDisplayType.NameAndSymbol:
					return $"{currency.Item2} ({currency.Item1})";
				
				case CurrencyDisplayType.Symbol:
					return currency.Item1;
				
				case CurrencyDisplayType.SymbolAndName:
					return $"{currency.Item1} ({currency.Item2})";
			}

			return String.Empty;
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
			_globalRefreshTimer = new Timer(1000);
			_globalRefreshTimer.Elapsed += OnGlobalRefreshTimerElapsed;
			_globalRefreshTimer.SynchronizingObject = this;
			_globalRefreshTimer.Start();
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

		private void OnContextMenuHoldingsSumSelectedClick(object sender, EventArgs e) {
			var selectedRows = _gridHoldingsData.SelectedRows.Cast<DataGridViewRow>().ToList();

			var totalPriceInBtc = selectedRows.Sum(d => decimal.Parse(d.Cells[_clmnHoldingsPriceInBtc.Index].Value.ToString()));
			var totalPriceInUsd = selectedRows.Sum(d => decimal.Parse(d.Cells[_clmnHoldingsPriceInUsd.Index].Value.ToString()));

			MessageBox.Show(this, $"BTC: {totalPriceInBtc:0.########}{Environment.NewLine}USD: {totalPriceInUsd:N}", "Sum", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void OnFormMainClosed(object sender, FormClosedEventArgs e) {
			SaveCompleteSettings();
		}

		private async void OnFormMainLoad(object sender, EventArgs e) {
			InitializeDataGridViewFields();
			ApplyCompleteSettings();
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

		private void OnGridHoldingsDataCellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
			var columnTag = _gridHoldingsData.Columns[e.ColumnIndex].Tag;
			switch ((HoldingsDataColumnType)columnTag) {
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
						RefreshHoldingsData(_tickerData);
					}
				break;
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

		private void OnGridMarketDataCellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
			if (_globalMarketData.TotalVolumeInUsd24H > 0 && e.RowIndex >= 0) {
				var backgroundColor = Color.White;
				var cellText = String.Empty;
				var fillWidth = -1;
				var partialFillColor = Color.Gray;
				var textColor = Color.Black;

				switch ((MarketDataColumnType)_gridMarketData.Columns[e.ColumnIndex].Tag) {
					case MarketDataColumnType.VolumeInUsd24H:
						backgroundColor = Color.DarkGray;
						cellText = _gridMarketData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
						var volumePercent = double.Parse(cellText) / _globalMarketData.TotalVolumeInUsd24H;
						fillWidth = (int)Math.Floor(e.CellBounds.Width * volumePercent);
						partialFillColor = Color.Gray;
						textColor = Color.White;
					break;
				}

				if (fillWidth >= 0) {
					if (e.RowIndex % 2 == 0) {
						PaintPartialCellBackground(e, backgroundColor, partialFillColor, textColor, fillWidth, cellText);
					} else {
						PaintPartialCellBackground(e, backgroundColor.Darken(0.6), partialFillColor.Darken(0.4), textColor, fillWidth, cellText);
					}

					e.Handled = true;
				}
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

		private void OnMenuItemCurrencyListHoldingsClick(object sender, EventArgs e) {
			using (var currencySelectionForm = new CurrencySelectionForm(_completeSettings.Monitoring.Holdings.Select(h => h.RowTag).ToList())) {
				if (currencySelectionForm.ShowDialog(this) == DialogResult.OK) {
					_completeSettings.Monitoring.Holdings = currencySelectionForm.SelectedCurrencyIds.Select(c => {
						var quantityString = _holdingsDataGridViewRows.SingleOrDefault(r => (int)r.Tag == c)?.Cells[_clmnHoldingsQuantity.Index].Value.ToString();

						return new HoldingsDataGridViewCellSettings { RowTag = c, Value = String.IsNullOrWhiteSpace(quantityString) ? "0" : quantityString};
					}).ToList();

					ApplyMonitoringHoldingsSettings(_completeSettings.Monitoring);
					RefreshHoldingsData(_tickerData);
				}
			}
		}

		private async void OnMenuItemCurrencyListMarketClick(object sender, EventArgs e) {
			using (var currencySelectionForm = new CurrencySelectionForm(_completeSettings.Monitoring.MarketCurrencyTypes)) {
				if (currencySelectionForm.ShowDialog(this) == DialogResult.OK) {
					_completeSettings.Monitoring.MarketCurrencyTypes = currencySelectionForm.SelectedCurrencyIds;

					ApplyMonitoringMarketSettings(_completeSettings.Monitoring);
					await RefreshAllData();
				}
			}
		}

		private void OnMenuItemFileQuitClick(object sender, EventArgs e) {
			Application.Exit();
		}

		private void OnMenuItemFileSaveClick(object sender, EventArgs e) {
			SaveCompleteSettings();
		}

		private void OnMenuItemFileSettingsClick(object sender, EventArgs e) {
			using (var settingsForm = new SettingsForm(_completeSettings)) {
				if (settingsForm.ShowDialog(this) == DialogResult.OK) {
					_completeSettings.Monitoring.CurrencyDisplayType = settingsForm.CurrencyDisplayType;
					_completeSettings.Monitoring.RefreshInterval = settingsForm.RefreshInterval;

					UpdateCurrencyDisplayType(_completeSettings.Monitoring.CurrencyDisplayType);
					UpdateRefreshInterval(_completeSettings.Monitoring.RefreshInterval);
				}
			}
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

		private void PaintPartialCellBackground(DataGridViewCellPaintingEventArgs e, Color backgroundColor, Color partialFillColor, Color textColor, int partialFillWidth, String cellText) {
			e.Graphics.FillRectangle(new SolidBrush(partialFillColor), new Rectangle(e.CellBounds.Left, e.CellBounds.Top, partialFillWidth, e.CellBounds.Height));
			e.Graphics.FillRectangle(new SolidBrush(backgroundColor), new Rectangle(e.CellBounds.Left + partialFillWidth, e.CellBounds.Top, e.CellBounds.Width - partialFillWidth, e.CellBounds.Height));
			e.Graphics.DrawString(cellText, Font, new SolidBrush(textColor), new Point(e.CellBounds.Left, e.CellBounds.Top + 2));
			e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width - 1, e.CellBounds.Height - 1));
		}

		private async Task RefreshAllData() {
			_btnPauseRefreshTimer.Enabled = false;
			_globalRefreshTimer.Stop();
			_lblLastUpdatedValue.Text = $"{_lblLastUpdatedValue.Text} [IN PROGRESS]";

			try {
				_globalMarketData = await _coinMarketCapClient.GetGlobalDataAsync();
				_tickerData = await RefreshMarketData();
				RefreshHoldingsData(_tickerData);
			} catch (Exception e) {
				//TODO: Coming soon...
			}

			_lblLastUpdatedValue.Text = DateTime.Now.ToLongTimeString();
			_globalRefreshTimer.Start();
			_btnPauseRefreshTimer.Text = "Pause";
			_btnPauseRefreshTimer.Enabled = true;
		}

		private void RefreshHoldingsData(ICollection<CurrencyTicker> tickerData) {
			decimal overallPriceInBtc = 0, overallPriceInUsd = 0;

			foreach (var row in _holdingsDataGridViewRows) {
				var desiredCurrency = tickerData.SingleOrDefault(c => c.Id == (int)row.Tag);
				if (desiredCurrency != null) {
					//Use EditedFormattedValue here because if this was called from the DataCellValidating event then the Value
					// field will not yet have the new value.
					var quantity = decimal.Parse(row.Cells[_clmnHoldingsQuantity.Index].EditedFormattedValue.ToString());
					var totalPriceInBtc = quantity * desiredCurrency.PriceInBtc;
					var totalPriceInUsd = quantity * desiredCurrency.PriceInUsd;

					overallPriceInBtc += totalPriceInBtc;
					overallPriceInUsd += totalPriceInUsd;

					row.Cells[_clmnHoldingsPriceInUsd.Index].Value = $"{totalPriceInUsd:N}";
					row.Cells[_clmnHoldingsPriceInBtc.Index].Value = $"{totalPriceInBtc:0.########}";
				}
			}

			_lblTotalValBtcValue.Text = $"{overallPriceInBtc:0.########}";
			_lblTotalValUsdValue.Text = $"{overallPriceInUsd:N}";
			_ntfyMain.Text = $"${_lblTotalValUsdValue.Text} / {_lblTotalValBtcValue.Text} BTC";
		}

		private async Task<ICollection<CurrencyTicker>> RefreshMarketData() {
			var tickerData = await _coinMarketCapClient.GetTickerAsync();

			foreach (var row in _marketDataGridViewRows) {
				var desiredCurrency = tickerData.SingleOrDefault(c => c.Id == (int)row.Tag);
				if (desiredCurrency != null) {
					row.SetValues(row.Cells[0].Value, $"{desiredCurrency.PriceInUsd:0.00####}", $"{desiredCurrency.PriceInBtc:0.########}", $"{desiredCurrency.PriceInBtc / 0.00000001m:N0}", $"{FormatPercentChange(desiredCurrency.PercentChange1H)}", $"{FormatPercentChange(desiredCurrency.PercentChange24H)}", $"{FormatPercentChange(desiredCurrency.PercentChange7D)}", $"{desiredCurrency.VolumeInUsd24H:N}", $"{desiredCurrency.MarketCapInUsd:N}", desiredCurrency.Rank);
				}
			}

			return tickerData;
		}

		private void SaveCompleteSettings() {
			var completeSettings = new CompleteSettings();

			SaveLayoutSettings(completeSettings);
			SaveMonitoringSettings(completeSettings);

			_settingsManager.SaveCompleteSettings(completeSettings);
		}

		private void SaveLayoutSettings(CompleteSettings completeSettings) {
			completeSettings.Layout = new LayoutSettings {
				GridHoldingsColumns = new List<HoldingsDataGridViewColumnSettings>(),
				GridMarketColumns = new List<MarketDataGridViewColumnSettings>(),
				GridContainerSplitterPosition = _cntnrGridData.SplitterDistance,
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
				completeSettings.Layout.GridHoldingsColumns.Add(settings);
			}

			foreach (var column in _marketDataGridViewColumns) {
				var settings = new MarketDataGridViewColumnSettings {
					FillWeight = column.FillWeight,
					Tag = (MarketDataColumnType)column.Tag,
					Width = column.Width
				};
				completeSettings.Layout.GridMarketColumns.Add(settings);
			}
		}

		private void SaveMonitoringSettings(CompleteSettings completeSettings) {
			completeSettings.Monitoring = new MonitoringSettings {
				CurrencyDisplayType = _completeSettings.Monitoring.CurrencyDisplayType,
				Holdings = _holdingsDataGridViewRows.Select(r => new HoldingsDataGridViewCellSettings { RowTag = (int)r.Tag, Value = r.Cells[_clmnHoldingsQuantity.Index].Value.ToString() } ).ToList(),
				MarketCurrencyTypes = _marketDataGridViewRows.Select(r => (int)r.Tag).ToList(),
				RefreshInterval = _prgrssGlobalRefresh.Maximum
			};
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

		private void UpdateCurrencyDisplayType(CurrencyDisplayType displayType) {
			foreach (var row in _holdingsDataGridViewRows) {
				row.Cells[_clmnHoldingsCoin.Index].Value = FormatCurrencyForDisplay((int)row.Tag, displayType);
			}

			foreach (var row in _marketDataGridViewRows) {
				row.Cells[_clmnMarketCoin.Index].Value = FormatCurrencyForDisplay((int)row.Tag, displayType);
			}
		}

		private void UpdateRefreshInterval(int refreshInterval) {
			_prgrssGlobalRefresh.Maximum = refreshInterval > 0 ? refreshInterval : DEFAULT_REFRESH_INTERVAL;
		}

		private const int DEFAULT_REFRESH_INTERVAL = 120;
		private readonly ApiClient _coinMarketCapClient;
		private readonly CompleteSettings _completeSettings;
		private GlobalData _globalMarketData;
		private Timer _globalRefreshTimer;
		private IEnumerable<DataGridViewColumn> _holdingsDataGridViewColumns;
		private IEnumerable<DataGridViewRow> _holdingsDataGridViewRows;
		private IEnumerable<DataGridViewColumn> _marketDataGridViewColumns;
		private IEnumerable<DataGridViewRow> _marketDataGridViewRows;
		private readonly SettingsManager _settingsManager;
		private ICollection<CurrencyTicker> _tickerData;
	}
}
