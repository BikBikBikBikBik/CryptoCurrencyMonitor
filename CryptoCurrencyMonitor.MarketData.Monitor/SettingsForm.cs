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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CryptoCurrencyMonitor.MarketData.Monitor.Settings;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	internal partial class SettingsForm : Form {
		public SettingsForm(CompleteSettings completeSettings) {
			InitializeComponent();

			InitCurrencyDisplayTypeRadioMap();
			InitializeSettings(completeSettings);
		}

		public CurrencyDisplayType CurrencyDisplayType => _currencyDisplayTypeRadioMap.SingleOrDefault(kv => kv.Value.Checked).Key;

		public int RefreshInterval { get; private set; }

		private void InitializeSettings(CompleteSettings completeSettings) {
			//Refresh Interval
			RefreshInterval = completeSettings.Monitoring.RefreshInterval;
			_txtRefreshInterval.Text = completeSettings.Monitoring.RefreshInterval.ToString();

			//Currency Display
			_currencyDisplayTypeRadioMap[completeSettings.Monitoring.CurrencyDisplayType].Checked = true;
		}

		private void InitCurrencyDisplayTypeRadioMap() {
			_currencyDisplayTypeRadioMap = new Dictionary<CurrencyDisplayType, RadioButton> {
				{ CurrencyDisplayType.Name, _radioName },
				{ CurrencyDisplayType.NameAndSymbol, _radioNameAndSymbol },
				{ CurrencyDisplayType.Symbol, _radioSymbol },
				{ CurrencyDisplayType.SymbolAndName, _radioSymbolAndName }
			};
		}

		#region Event Handlers
		private void OnTxtRefreshIntervalValidating(object sender, CancelEventArgs e) {
			var input = (sender as TextBox)?.Text;

			if (String.IsNullOrWhiteSpace(input)) {
				e.Cancel = true;
			}

			int numericInput;
			if (int.TryParse(input, out numericInput)) {
				if (numericInput < 1) {
					e.Cancel = true;
				}

				RefreshInterval = numericInput;
			}
		}
		#endregion

		private IDictionary<CurrencyDisplayType, RadioButton> _currencyDisplayTypeRadioMap;
	}
}
