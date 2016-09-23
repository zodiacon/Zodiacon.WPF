using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileExplorer.ViewModels {
    class MainViewModel : BindableBase {
        ObservableCollection<TreeItemViewModel> _drives;

        public MainViewModel() {
        }

        public IList<TreeItemViewModel> Drives => _drives ?? (_drives = BuildDrives());

        private ObservableCollection<TreeItemViewModel> BuildDrives() {
            var drives = new ObservableCollection<TreeItemViewModel>(DriveInfo.GetDrives().
                Where(drive => drive.IsReady).
                Select(drive => new TreeItemViewModel(this, TreeItemType.Drive) {
                Text = drive.Name,
                Icon = DriveToIcon(drive),
                FullPath = drive.RootDirectory.Name
            }));
            return drives;
        }

        private TreeItemViewModel _selectedItem;

        private string _searchText;

        public string SearchText {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private bool _isBusy;

        public bool IsBusy {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

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
                    yield break;

                foreach(var filepath in Directory.EnumerateFiles(SelectedItem.FullPath)) {
                    var file = new FileInfo(filepath);
                    var vm = new FileViewModel(filepath) {
                        Name = file.Name,
                        Size = file.Length,
                        Modified = file.LastWriteTime,
                    };
                    SetIconAsync(vm);
                    yield return vm;
                }
            }
        }


        private async static void SetIconAsync(FileViewModel vm) {
            vm.Icon = await ExtractIconAsync(vm.Path);
        }

        private async static Task<ImageSource> ExtractIconAsync(string path) {
            var hExtractedIcon = await Task.Run(() => {
                var hIcon = NativeMethods.ExtractIcon(IntPtr.Zero, path, 0);
                return hIcon;
            });

            if(hExtractedIcon == IntPtr.Zero || hExtractedIcon == new IntPtr(1))
                return null;

            var image = Imaging.CreateBitmapSourceFromHIcon(hExtractedIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            if(image != null)
                image.Freeze();

            NativeMethods.DestroyIcon(hExtractedIcon);
            return image;
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
