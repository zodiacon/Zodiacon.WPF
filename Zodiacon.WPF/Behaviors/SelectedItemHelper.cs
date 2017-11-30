using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Zodiacon.WPF.Behaviors {
	public static class SelectedItemHelper {
		public static bool GetBringIntoViewWhenSelected(DependencyObject obj) {
			return (bool)obj.GetValue(BringIntoViewWhenSelectedProperty);
		}

		public static void SetBringIntoViewWhenSelected(DependencyObject obj, bool value) {
			obj.SetValue(BringIntoViewWhenSelectedProperty, value);
		}

		public static readonly DependencyProperty BringIntoViewWhenSelectedProperty =
			DependencyProperty.RegisterAttached("BringIntoViewWhenSelected", typeof(bool), typeof(SelectedItemHelper), 
				new PropertyMetadata(false, OnBringIntoViewWhenSelected));

		private static void OnBringIntoViewWhenSelected(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (!(bool)e.NewValue) {
				if (d is DataGrid dg) {
					dg.SelectionChanged -= DataGridSelectionChanged;
				}
				return;
			}

			switch (d) {
				case TreeViewItem item:
					if (item.IsSelected)
						item.BringIntoView();
					break;

				case ListBoxItem item:
					if (item.IsSelected)
						item.BringIntoView();
					break;

				case DataGridRow item:
					if (item.IsSelected)
						item.BringIntoView();
					break;

				case DataGrid dg:
					dg.SelectionChanged += DataGridSelectionChanged;
					break;

				case FrameworkElement item:
					if (Selector.GetIsSelected(item))
						item.BringIntoView();
					break;

			}
		}

		private static void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e) {
			var dg = sender as DataGrid;
			Debug.Assert(dg != null);

			if (dg.SelectedItem != null)
				dg.ScrollIntoView(dg.SelectedItem);
		}
	}
}
