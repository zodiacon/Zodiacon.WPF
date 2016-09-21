using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zodiacon.WPF;

namespace FileExplorer.ViewModels {
    enum TreeItemType {
        Drive,
        Folder
    }

    class TreeItemViewModel : TreeViewItemBase {
        public TreeItemType Type { get; }
        public TreeItemViewModel(TreeItemType type) {
            Type = type;
        }

        public string FullPath { get; set; }

        private string _icon;

        public string Icon {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        IList<ITreeViewItem> _items;
        public override IList<ITreeViewItem> SubItems {
            get {
                if(_items == null) {
                    try {
                        _items = Directory.EnumerateDirectories(FullPath).Select(dir => new TreeItemViewModel(TreeItemType.Folder) {
                            Text = Path.GetFileName(dir),
                            FullPath = dir,
                            Icon = "/images/folder_closed.ico"
                        }).Cast<ITreeViewItem>().ToList();
                    }
                    catch {
                    }
                }
                return _items;
            }
        }

        protected override void OnSelected(bool selected) {
            if(Type == TreeItemType.Folder) {
                Icon = selected ? "/images/folder.ico" : "/images/folder_closed.ico";
            }
        }
    }
}
