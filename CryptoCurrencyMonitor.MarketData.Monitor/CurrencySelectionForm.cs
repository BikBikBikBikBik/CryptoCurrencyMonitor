using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CryptoCurrencyMonitor.MarketData.Client;

namespace CryptoCurrencyMonitor.MarketData.Monitor {
	public partial class CurrencySelectionForm : Form {
		public CurrencySelectionForm(ICollection<int> selectedCurrencyTypes) {
			InitializeComponent();

			_selectedCurrencyTypes = selectedCurrencyTypes;

			InitializeCurrencyLists();
		}

		public ICollection<int> SelectedCurrencyIds => _lstSelectedCurrencies.Items.Cast<Currency>().Select(c => c.Id).ToList();

		public ICollection<int> UnselectedCurrencyIds => _lstUnselectedCurrencies.Items.Cast<Currency>().Select(c => c.Id).ToList();

		private void InitializeCurrencyLists() {
			var currencySelectionGrouping = AllCurrencies.GroupBy(c => _selectedCurrencyTypes.Contains(c.Id)).ToList();
			_selectedCurrenciesDataSource = new BindingList<Currency>(currencySelectionGrouping.Where(g => g.Key).SelectMany(c => c).ToList());
			_unselectedCurrenciesDataSource = new BindingList<Currency>(currencySelectionGrouping.Where(g => !g.Key).SelectMany(c => c).ToList());

			_lstSelectedCurrencies.DisplayMember = "NameAndSymbol";
			_lstSelectedCurrencies.Sorted = true;
			_lstSelectedCurrencies.ValueMember = "Id";
			_lstSelectedCurrencies.DataSource = _selectedCurrenciesDataSource;

			_lstUnselectedCurrencies.DisplayMember = "NameAndSymbol";
			_lstUnselectedCurrencies.Sorted = true;
			_lstUnselectedCurrencies.ValueMember = "Id";
			_lstUnselectedCurrencies.DataSource = _unselectedCurrenciesDataSource;
		}

		#region Event Handlers
		private void OnBtnAddSelectedCurrenciesClick(object sender, System.EventArgs e) {
			foreach (var item in _lstUnselectedCurrencies.SelectedItems.Cast<Currency>().ToList()) {
				_selectedCurrenciesDataSource.Add(item);
				_unselectedCurrenciesDataSource.Remove(item);
			}
		}

		private void OnBtnRemoveSelectedCurrenciesClick(object sender, System.EventArgs e) {
			foreach (var item in _lstSelectedCurrencies.SelectedItems.Cast<Currency>().ToList()) {
				_selectedCurrenciesDataSource.Remove(item);
				_unselectedCurrenciesDataSource.Add(item);
			}
		}
		#endregion

		private readonly ICollection<int> _selectedCurrencyTypes;
		private BindingList<Currency> _selectedCurrenciesDataSource;
		private BindingList<Currency> _unselectedCurrenciesDataSource;
		private static readonly IReadOnlyCollection<Currency> AllCurrencies = CurrencyTypeRegistry.CurrencyTypeMap.Select(kv => new Currency { Id = kv.Key, Name = kv.Value.Item2, Symbol = kv.Value.Item1 }).ToList();
	}
}
