using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMaster.Common;
using Zodiacon.WPF;

namespace SystemMaster.MainViewModels {
	class MainViewModel : BindableBase {
		ObservableCollection<SimpleTreeViewItem> _items;

		public IList<SimpleTreeViewItem> Items => _items;

		public string Title => Constants.Title + (Helpers.IsAdmin ? " (Administrator)" : string.Empty);
		public MainViewModel() {
			_items = new ObservableCollection<SimpleTreeViewItem> {
				new SimpleTreeViewItem { Text = "Local System", IsExpanded = true }
			};
		}
	}
}
