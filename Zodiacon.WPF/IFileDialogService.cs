using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
    public interface IFileDialogService {
        string GetFileForOpen(string filter = "All Files|*.*", string title = null);
        string GetFileForSave(string filter = "All Files|*.*", string title = null);
    }
}
