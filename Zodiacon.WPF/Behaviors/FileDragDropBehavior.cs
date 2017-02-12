using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Zodiacon.WPF.Behaviors {
	public sealed class FileDragDropBehavior : Behavior<FrameworkElement> {
		protected override void OnAttached() {
			base.OnAttached();

			AssociatedObject.AllowDrop = true;
			AssociatedObject.DragEnter += AssociatedObject_DragEnter;
			AssociatedObject.Drop += AssociatedObject_Drop;
		}

		private void AssociatedObject_Drop(object sender, DragEventArgs e) {
			if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
				var drop = e.Data.GetData(DataFormats.FileDrop) as string[];
				Debug.Assert(drop != null);
				if(drop.Length > 0) {
					if(Command != null)
						Command.Execute(drop);
				}
			}
		}

		private void AssociatedObject_DragEnter(object sender, DragEventArgs e) {
			if(e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effects = DragDropEffects.Move;
			else
				e.Effects = DragDropEffects.None;
		}

		public ICommand Command {
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly DependencyProperty CommandProperty =
			 DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FileDragDropBehavior), new PropertyMetadata(null));


		protected override void OnDetaching() {
			AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
			AssociatedObject.Drop -= AssociatedObject_Drop;

			base.OnDetaching();
		}
	}
}
