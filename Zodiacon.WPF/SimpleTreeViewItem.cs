using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public class SimpleTreeViewItem : BindableBase, ITreeViewItem {
		public ITreeViewItem Parent { get; }

		public SimpleTreeViewItem(ITreeViewItem parent = null) {
			Parent = parent;
		}

		private bool _isExpanded;

		public virtual bool IsExpanded {
			get { return _isExpanded; }
			set {
				if(SetProperty(ref _isExpanded, value))
					OnExpanded(value);
			}
		}

		private string _icon;

		public string Icon {
			get { return _icon; }
			set { SetProperty(ref _icon, value); }
		}

		private string _text;

		public string Text {
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		IList<ITreeViewItem> _items;
		public virtual IList<ITreeViewItem> SubItems {
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

		bool _isSelected;
		public bool IsSelected {
			get { return _isSelected; }
			set {
				if(SetProperty(ref _isSelected, value))
					OnSelected(value);
			}
		}

		protected virtual void OnSelected(bool selected) { }

		protected virtual void OnExpanded(bool expanded) { }

	}
}
