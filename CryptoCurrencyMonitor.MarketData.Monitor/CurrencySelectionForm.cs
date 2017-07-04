using System.Collections.Generic;
using System.Windows.Forms;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	public partial class CurrencySelectionForm : Form {
		public CurrencySelectionForm(ICollection<int> selectedCurrencyTypes) {
			InitializeComponent();

			_selectedCurrencyTypes = selectedCurrencyTypes;
		}

		private readonly ICollection<int> _selectedCurrencyTypes;
	}
}
