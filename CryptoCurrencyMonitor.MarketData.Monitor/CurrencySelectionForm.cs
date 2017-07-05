using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CryptoCurrencyMonitor.MarketData.Client;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	public partial class CurrencySelectionForm : Form {
		public CurrencySelectionForm(ICollection<int> selectedCurrencyTypes) {
			InitializeComponent();

			_selectedCurrencyTypes = (IReadOnlyCollection<int>)selectedCurrencyTypes;

			InitializeCurrencyLists();
		}

		public ICollection<int> SelectedCurrencyIds => _lstSelectedCurrencies.Items.Cast<Currency>().Select(c => c.Id).ToList();

		public ICollection<int> UnselectedCurrencyIds => _lstUnselectedCurrencies.Items.Cast<Currency>().Select(c => c.Id).ToList();

		private void InitializeCurrencyLists() {
			var currencySelectionGrouping = AllCurrencies.GroupBy(c => _selectedCurrencyTypes.Contains(c.Id)).ToList();
			_selectedCurrenciesDataSource = new BindingList<Currency>(currencySelectionGrouping.Where(g => g.Key).SelectMany(c => c).ToList());
			_unselectedCurrenciesDataSource = new BindingList<Currency>(currencySelectionGrouping.Where(g => !g.Key).SelectMany(c => c).ToList());

			_lstSelectedCurrencies.DisplayMember = "NameAndSymbol";
			_lstSelectedCurrencies.ValueMember = "Id";
			_lstSelectedCurrencies.DataSource = _selectedCurrenciesDataSource;

			_lstUnselectedCurrencies.DisplayMember = "NameAndSymbol";
			_lstUnselectedCurrencies.ValueMember = "Id";
			_lstUnselectedCurrencies.DataSource = _unselectedCurrenciesDataSource;
		}

		#region Event Handlers
		private void OnBtnAddSelectedCurrenciesClick(object sender, System.EventArgs e) {
			MoveSelectedCurrencies(_lstUnselectedCurrencies, ref _unselectedCurrenciesDataSource, _lstSelectedCurrencies, ref _selectedCurrenciesDataSource);
		}

		private void OnBtnRemoveSelectedCurrenciesClick(object sender, System.EventArgs e) {
			MoveSelectedCurrencies(_lstSelectedCurrencies, ref _selectedCurrenciesDataSource, _lstUnselectedCurrencies, ref _unselectedCurrenciesDataSource);
		}
		#endregion

		private void MoveSelectedCurrencies(ListBox sourceList, ref BindingList<Currency> sourceDataSource, ListBox destinationList, ref BindingList<Currency> destinationDataSource) {
			var selectedIndices = sourceList.SelectedIndices.Cast<int>().ToArray();

			if (selectedIndices.Length > 0) {
				SetButtonStates(false);
			
				var tempDestination = new List<Currency>(destinationDataSource);
				var tempSource = new List<Currency>(sourceDataSource);

				for(var i = selectedIndices.Length - 1; i >= 0; i--) {
					var currency = sourceDataSource[selectedIndices[i]];

					tempSource.RemoveAt(selectedIndices[i]);
					tempDestination.Add(currency);
				}

				destinationDataSource = new BindingList<Currency>(tempDestination.OrderBy(c => c.NameAndSymbol).ToList());
				destinationList.DataSource = destinationDataSource;
				sourceDataSource = new BindingList<Currency>(tempSource);
				sourceList.DataSource = sourceDataSource;
				sourceList.SelectedIndices.Clear();
				sourceList.SelectedIndices.Add(selectedIndices[0] > 0 ? selectedIndices[0] - 1 : 0);

				SetButtonStates(true);
			}
		} 

		private void SetButtonStates(bool isEnabled) {
			_btnAddSelectedCurrencies.Enabled = isEnabled;
			_btnCancel.Enabled = isEnabled;
			_btnOk.Enabled = isEnabled;
			_btnRemoveSelectedCurrencies.Enabled = isEnabled;
		}

		private readonly IReadOnlyCollection<int> _selectedCurrencyTypes;
		private BindingList<Currency> _selectedCurrenciesDataSource;
		private BindingList<Currency> _unselectedCurrenciesDataSource;
		private static readonly IReadOnlyCollection<Currency> AllCurrencies = CurrencyTypeRegistry.CurrencyTypeMap.Select(kv => new Currency(kv.Key) { Name = kv.Value.Item2, Symbol = kv.Value.Item1 }).OrderBy(c => c.NameAndSymbol).ToList();
	}
}
