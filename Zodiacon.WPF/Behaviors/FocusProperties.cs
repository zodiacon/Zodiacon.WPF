using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Zodiacon.WPF.Behaviors {
	public static class FocusProperties {
		public static bool GetFocusOnLoad(DependencyObject obj) {
			return (bool)obj.GetValue(FocusOnLoadProperty);
		}

		public static void SetFocusOnLoad(DependencyObject obj, bool value) {
			obj.SetValue(FocusOnLoadProperty, value);
		}

		public static readonly DependencyProperty FocusOnLoadProperty =
			DependencyProperty.RegisterAttached("FocusOnLoad", typeof(bool), typeof(FocusProperties), new PropertyMetadata(false, OnFocusOnLoadChanged));

		private static void OnFocusOnLoadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var c = d as Control;
			if (c == null)
				throw new InvalidOperationException("FocusOnLoad valid on Control-derived types only");

			c.Loaded += OnControlLoaded;
		}

		private static void OnControlLoaded(object sender, RoutedEventArgs e) {
			((Control)sender).Focus();
		}
	}
}
