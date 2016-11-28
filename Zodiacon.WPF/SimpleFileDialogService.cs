using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using WPFFolderBrowser;

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

		public string GetFolder(FolderBrowserOptions options = null) {
			if (options == null)
				options = new FolderBrowserOptions();
			var dlg = new WPFFolderBrowserDialog {
				Title = options.Title,
				InitialDirectory = options.InitialDirectory,
				ShowHiddenItems = options.ShowHidden,
				ShowPlacesList = options.ShowPlaces
			};
			if (dlg.ShowDialog() == true) {
				return dlg.FileName;
			}
			return null;
		}

		public static readonly IFileDialogService Instance = new SimpleFileDialogService();
    }
}
