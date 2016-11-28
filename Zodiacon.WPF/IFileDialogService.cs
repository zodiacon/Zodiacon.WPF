using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public class FolderBrowserOptions {
		public string Title { get; set; } = "Select Folder";
		public string InitialDirectory { get; set; }
		public bool ShowHidden { get; set; }
		public bool ShowPlaces { get; set; } = true;
	}

    public interface IFileDialogService {
        string GetFileForOpen(string filter = "All Files|*.*", string title = null);
        string GetFileForSave(string filter = "All Files|*.*", string title = null);
		string GetFolder(FolderBrowserOptions options = null);
    }
}
