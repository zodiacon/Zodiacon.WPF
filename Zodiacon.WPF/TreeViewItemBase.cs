using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Zodiacon.WPF.Behaviors;

namespace Zodiacon.WPF {
    public abstract class TreeViewItemBase : BindableBase, ITreeViewItemMatch {
        protected virtual void OnExpanded(bool expanded) { }

        public ITreeViewItem Parent { get; }

        protected TreeViewItemBase(TreeViewItemBase parent = null) {
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


        private string _text;

        public string Text {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public virtual IList<ITreeViewItem> SubItems => null;

        bool _isSelected;
        public bool IsSelected {
            get { return _isSelected; }
            set {
                if(SetProperty(ref _isSelected, value))
                    OnSelected(value);
            }
        }

        bool _isVisible = true;
        public bool IsVisible {
            get { return _isVisible; }
            set {
                if(SetProperty(ref _isVisible, value))
                    OnVisible(value);
            }
        }

        protected virtual void OnVisible(bool visible) { }

        protected virtual void OnSelected(bool selected) { }

        public async Task<bool> IsMatchAsync(string searchText, int level, CancellationToken ct, SearchTextOptions options) {
            if(ct.IsCancellationRequested)
                return false;

            if(string.IsNullOrEmpty(searchText)) {
                IsExpanded = false;
                IsVisible = true;
                return true;
            }

            bool visible = false;
            bool any = false;
            var items = SubItems;

            if(Text.ToLower().Contains(searchText)) {
                visible = true;
                IsVisible = true;
                if(Parent != null)
                    Parent.IsExpanded = true;
                await Dispatcher.Yield(DispatcherPriority.DataBind);
            }

            if(level != 0 && items != null) {
                foreach(var item in items.Cast<ITreeViewItemMatch>()) {
                    if(ct.IsCancellationRequested)
                        return false;

                    await item.BuildSubItemsAsync(true, ct);

                    if(await item.IsMatchAsync(searchText, level - 1, ct, options)) {
                        any = visible = true;
                        item.IsVisible = true;
                        item.IsExpanded = !string.IsNullOrEmpty(searchText);
                    }
                }
            }

            if(!visible && !any) {
                IsVisible = false;
            }
            if(any)
                IsExpanded = true;

            return visible || any;
        }

        public virtual Task BuildSubItemsAsync(bool build, CancellationToken ct) => Task.FromResult<object>(null);
    }
}
