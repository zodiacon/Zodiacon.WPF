using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Zodiacon.WPF;

namespace FileExplorer.ViewModels {
    enum TreeItemType {
        Drive,
        Folder
    }

    class TreeItemViewModel : TreeViewItemBase {
        public TreeItemType Type { get; }
        public MainViewModel MainViewModel { get; }

        public TreeItemViewModel(MainViewModel mainViewModel, TreeItemType type, TreeViewItemBase parent = null) : base(parent) {
            Type = type;
            MainViewModel = mainViewModel;
        }

        public string FullPath { get; set; }

        private string _icon;

        public string Icon {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        ObservableCollection<ITreeViewItemMatch> _items;
        public override IList<ITreeViewItemMatch> SubItems {
            get {
                if(_items == null) {
                    Debug.WriteLine($"SubItems: {FullPath}");
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    BuildSubItemsAsync(true, CancellationToken.None);
                    //                    AddDirectories(_items, FullPath);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
                return _items;
            }
        }

        async Task AddDirectories(IList<ITreeViewItemMatch> items, string path, CancellationToken ct) {
            var dispatcher = Dispatcher.CurrentDispatcher;
            var searchText = MainViewModel.SearchText;
            var directories = await DirectoryHelper.EnumerateDirectoriesAsync(path);
            var list = new List<TreeItemViewModel>(directories.Length);

            await Task.Run(() => {
                foreach(var dir in directories) {
                    list.Add(new TreeItemViewModel(MainViewModel, TreeItemType.Folder, this) {
                        Text = Path.GetFileName(dir),
                        FullPath = dir,
                        Icon = "/images/folder_closed.ico",
                        IsExpanded = false
                    });
                }
            });

            await dispatcher.InvokeAsync(() => {
                foreach(var vm in list)
                    items.Add(vm);
            }, DispatcherPriority.Background);
        }

        public async override Task BuildSubItemsAsync(bool build, CancellationToken ct) {
            if(!build) {
                if(_items != null)
                    _items.Clear();
                _items = null;
                OnPropertyChanged(nameof(SubItems));
                return;
            }

            if(_items == null) {
                _items = new ObservableCollection<ITreeViewItemMatch>();
                OnPropertyChanged(nameof(SubItems));
            }
            else
                _items.Clear();

            await AddDirectories(_items, FullPath, ct);
            await Dispatcher.Yield(DispatcherPriority.Background);
        }


        protected override void OnSelected(bool selected) {
            if(Type == TreeItemType.Folder) {
                Icon = selected ? "/images/folder.ico" : "/images/folder_closed.ico";
            }
        }

        protected override void OnVisible(bool visible) {
            if(visible && _items != null && IsExpanded)
                foreach(var item in _items)
                    item.IsVisible = true;
        }

        protected override void OnExpanded(bool expanded) {
            if(expanded && Parent != null)
                Parent.IsExpanded = true;
        }
    }
}
