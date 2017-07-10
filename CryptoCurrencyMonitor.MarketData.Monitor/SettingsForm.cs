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
