using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Zodiacon.WPF.Behaviors {
	public static class TreeViewItemHelper {
		public static bool GetBringIntoViewWhenSelected(DependencyObject obj) {
			return (bool)obj.GetValue(BringIntoViewWhenSelectedProperty);
		}

		public static void SetBringIntoViewWhenSelected(DependencyObject obj, bool value) {
			obj.SetValue(BringIntoViewWhenSelectedProperty, value);
		}

		public static readonly DependencyProperty BringIntoViewWhenSelectedProperty =
			DependencyProperty.RegisterAttached("BringIntoViewWhenSelected", typeof(bool), typeof(TreeViewItemHelper), 
				new PropertyMetadata(false, OnBringIntoViewWhenSelected));

		private static void OnBringIntoViewWhenSelected(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var item = d as TreeViewItem;
			if (item == null)
				return;

			if (item.IsSelected)
				item.BringIntoView();
		}

	}
}
