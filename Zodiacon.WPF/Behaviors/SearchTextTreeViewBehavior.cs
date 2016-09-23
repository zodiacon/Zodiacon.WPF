using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace Zodiacon.WPF.Behaviors {
    public sealed class SearchTextTreeViewBehavior : Behavior<TreeView> {
        public IEnumerable<ITreeViewItemMatch> Items {
            get { return (IEnumerable<ITreeViewItemMatch>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(IEnumerable<ITreeViewItemMatch>), typeof(SearchTextTreeViewBehavior),
                new PropertyMetadata(null));

        public string SearchText {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(SearchTextTreeViewBehavior), new PropertyMetadata(null,
                (s, e) => ((SearchTextTreeViewBehavior)s).SearchTextChanged(e)));



        public ITreeViewItem SearchRoot {
            get { return (ITreeViewItem)GetValue(SearchRootProperty); }
            set { SetValue(SearchRootProperty, value); }
        }

        public static readonly DependencyProperty SearchRootProperty =
            DependencyProperty.Register(nameof(SearchRoot), typeof(ITreeViewItem), typeof(SearchTextTreeViewBehavior), new PropertyMetadata(null));

        public bool IsBusy {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(SearchTextTreeViewBehavior), new PropertyMetadata(false));

        public int MaxSearchLevel {
            get { return (int)GetValue(MaxSearchLevelProperty); }
            set { SetValue(MaxSearchLevelProperty, value); }
        }

        public static readonly DependencyProperty MaxSearchLevelProperty =
            DependencyProperty.Register(nameof(MaxSearchLevel), typeof(int), typeof(SearchTextTreeViewBehavior), new PropertyMetadata(-1));


        CancellationTokenSource _cts;
        private async void SearchTextChanged(DependencyPropertyChangedEventArgs e) {
            if(Items == null) return;

            var searchText = (string)e.NewValue;

            var items = SearchRoot == null ? Items : SearchRoot.SubItems;
            if(items == null) return;

            if(IsBusy && _cts != null) {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
                await Dispatcher.Yield(DispatcherPriority.Background);

                foreach(var item in items) {
                    await item.BuildSubItemsAsync(false, CancellationToken.None);
                    item.IsExpanded = false;
                    item.IsVisible = true;
                }
                IsBusy = false;
            }

            try {
                await DoSearch(items, searchText);
            }
            catch { }
        }

        private async Task<bool> DoSearch(IEnumerable<ITreeViewItemMatch> items, string searchText) {

            // clear search first

            foreach(var item in items) {
                await item.BuildSubItemsAsync(false, CancellationToken.None);
                item.IsExpanded = false;
                item.IsVisible = true;
            }

            if(string.IsNullOrEmpty(searchText))
                return true;

            _cts = new CancellationTokenSource();

            IsBusy = true;
            var maxLevel = MaxSearchLevel;
            var token = _cts.Token;

            foreach(var item in items) {
                if(token.IsCancellationRequested)
                    break;
                await item.BuildSubItemsAsync(true, token);
                if(await item.IsMatchAsync(searchText, maxLevel, token, SearchTextOptions.IgnoreCase)) {
                    item.IsVisible = true;
                    item.IsExpanded = !string.IsNullOrEmpty(searchText);
                }
                else {
                    item.IsVisible = false;
                }
            }
            await Dispatcher.Yield(DispatcherPriority.Background);
            _cts.Dispose();
            IsBusy = false;
            return true;
        }
    }
}


