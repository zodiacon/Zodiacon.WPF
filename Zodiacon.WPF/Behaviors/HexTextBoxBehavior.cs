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
	public sealed class HexTextBoxBehavior : Behavior<TextBox> {
		protected override void OnAttached() {
			base.OnAttached();

			AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
			AssociatedObject.TextChanged += AssociatedObject_TextChanged;
		}

		private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e) {
			Value = Convert.ToUInt64(AssociatedObject.Text, 16);
		}

		protected override void OnDetaching() {
			AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
			AssociatedObject.TextChanged -= AssociatedObject_TextChanged;

			base.OnDetaching();
		}

		private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.Left:
				case Key.Right:
				case Key.Delete:
				case Key.Back:
					return;
			}
			
			if (e.Key < Key.D0 && e.Key > Key.D9 || e.Key < Key.A && e.Key > Key.F) {
				e.Handled = true;
			}
		}

		public ulong Value {
			get { return (ulong)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(ulong), typeof(HexTextBoxBehavior), 
				new PropertyMetadata((ulong)0, (s, e) => ((HexTextBoxBehavior)s).OnValueChanged(e)));

		private void OnValueChanged(DependencyPropertyChangedEventArgs e) {
			AssociatedObject.Text = ((ulong)e.NewValue).ToString("X");
		}

	}
}
