using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileExplorer.ViewModels {
    class FileViewModel : BindableBase {
        public string Path { get; }

        public FileViewModel(string path) {
            Path = path;
        }

        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime Modified { get; set; }

        static readonly ImageSource _genericIcon = new BitmapImage(new Uri("pack://application:,,,/images/app.ico"));

        ImageSource _icon = _genericIcon;
        public ImageSource Icon {
            get { return _icon; }
            set {
                if(value != null)
                    SetProperty(ref _icon, value);
            }
        }
    }
}
