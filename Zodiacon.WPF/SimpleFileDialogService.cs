using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Zodiacon.WPF {
    [Export("default", typeof(IFileDialogService))]
    sealed class SimpleFileDialogService : IFileDialogService {
        public string GetFileForOpen(string filter, string title) {
            var dlg = new OpenFileDialog {
                Filter = filter,
                Title = title ?? null
            };
            return dlg.ShowDialog() == true ? dlg.FileName : null;
        }

        public string GetFileForSave(string filter, string title) {
            var dlg = new SaveFileDialog {
                Filter = filter,
                Title = title ?? null
            };
            return dlg.ShowDialog() == true ? dlg.FileName : null;
        }

        public static readonly IFileDialogService Instance = new SimpleFileDialogService();
    }
}
