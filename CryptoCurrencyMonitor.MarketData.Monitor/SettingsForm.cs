using System;
using System.ComponentModel;
using System.Windows.Forms;
using CryptoCurrencyMonitor.MarketData.Monitor.Settings;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	internal partial class SettingsForm : Form {
		public SettingsForm(CompleteSettings completeSettings) {
			InitializeComponent();

			_completeSettings = completeSettings;

			InitializeSettings();
		}

		public int RefreshInterval { get; private set; }

		private void InitializeSettings() {
			RefreshInterval = _completeSettings.Monitoring.RefreshInterval;
			_txtRefreshInterval.Text = _completeSettings.Monitoring.RefreshInterval.ToString();
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

		private readonly CompleteSettings _completeSettings;
	}
}
