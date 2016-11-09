using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Zodiacon.WPF.Behaviors {
	public static class HexTextBoxBehavior {


		public static bool GetIsHexInput(DependencyObject obj) {
			return (bool)obj.GetValue(IsHexInputProperty);
		}

		public static void SetIsHexInput(DependencyObject obj, bool value) {
			obj.SetValue(IsHexInputProperty, value);
		}

		public static readonly DependencyProperty IsHexInputProperty =
			DependencyProperty.RegisterAttached("IsHexInput", typeof(bool), typeof(HexTextBoxBehavior), 
				new PropertyMetadata(false, OnIsHexInputChanged));

		private static void OnIsHexInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var tb = d as TextBox;
			if (tb == null)
				throw new InvalidOperationException("IsHexInput only valid on TextBox objects");

			if ((bool)e.NewValue)
				tb.PreviewKeyDown += OnKeyDown;
			else
				tb.PreviewKeyDown -= OnKeyDown;
		}

		private static void OnKeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.Left:
				case Key.Right:
				case Key.Delete:
				case Key.Back:
					return;
			}
			
			if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.A || e.Key > Key.F)) {
				e.Handled = true;
			}
		}
	}
}
