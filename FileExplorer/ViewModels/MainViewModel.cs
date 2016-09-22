using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.ViewModels {
    class MainViewModel : BindableBase {
        ObservableCollection<TreeItemViewModel> _drives;

        public MainViewModel() {
        }

        public IList<TreeItemViewModel> Drives => _drives ?? (_drives = BuildDrives());

        private ObservableCollection<TreeItemViewModel> BuildDrives() {
            return new ObservableCollection<TreeItemViewModel>(DriveInfo.GetDrives().Select(drive => new TreeItemViewModel(TreeItemType.Drive) {
                Text = drive.Name,
                Icon = DriveToIcon(drive),
                FullPath = drive.RootDirectory.Name
            }));
        }

        private TreeItemViewModel _selectedItem;

        public TreeItemViewModel SelectedItem {
            get { return _selectedItem; }
            set {
                if(SetProperty(ref _selectedItem, value)) {
                    OnPropertyChanged(nameof(Files));
                }
            }
        }

        public IEnumerable<FileViewModel> Files {
            get {
                if(SelectedItem == null)
                    return null;
                try {
                    return from path in Directory.EnumerateFiles(SelectedItem.FullPath)
                            let file = new FileInfo(path)
                            select new FileViewModel {
                                Name = file.Name,
                                Size = file.Length,
                                Modified = file.LastWriteTime
                            };
                }
                catch {
                    return null;
                }
            }
        }

        private string DriveToIcon(DriveInfo drive) {
            switch(drive.DriveType) {
                case DriveType.CDRom:
                    return "/images/cd.ico";
                case DriveType.Fixed:
                    return "/images/harddisk.ico";
                case DriveType.Network:
                    return "/images/harddisk_network.ico";
                case DriveType.Removable:
                    return "/images/usb.ico";
                case DriveType.Ram:
                    return "/images/ram.ico";
                default:
                    return "/images/data.ico";
            }
        }
    }
}
