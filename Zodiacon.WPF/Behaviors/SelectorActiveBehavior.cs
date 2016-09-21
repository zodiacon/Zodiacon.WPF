using Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace Zodiacon.WPF.Behaviors {
    /// <summary>
    /// Notifies of active item change in a selector
    /// </summary>
    public sealed class SelectorActiveBehavior : Behavior<Selector> {
        protected override void OnAttached() {
            base.OnAttached();

            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching() {
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;

            base.OnDetaching();
        }

        public object ActiveItem {
            get { return (object)GetValue(ActiveItemProperty); }
            set { SetValue(ActiveItemProperty, value); }
        }

        public static readonly DependencyProperty ActiveItemProperty =
            DependencyProperty.Register(nameof(ActiveItem), typeof(object), typeof(SelectorActiveBehavior), 
                new PropertyMetadata(null, (s, e) => ((SelectorActiveBehavior)s).OnActiveItemChanged(e)));

        private void OnActiveItemChanged(DependencyPropertyChangedEventArgs e) {
            var oldItem = e.OldValue;
            if(oldItem != null)
                SetActive(oldItem, false);
            var newItem = e.NewValue;
            if(newItem != null)
                SetActive(newItem, true);
        }

        private void AssociatedObject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if(e.RemovedItems.Count > 0) {
                SetActive(e.RemovedItems[0], false);
            }
            if(e.AddedItems.Count > 0) {
                SetActive(e.AddedItems[0], true);
            }
        }

        private void SetActive(object item, bool active) {
            var activeAware = item as IActiveAware;

            if(activeAware != null) {
                activeAware.IsActive = active;
            }
            if(active) {
                ActiveItem = item;
                AssociatedObject.SelectedItem = item;
            }
        }
    }
}
