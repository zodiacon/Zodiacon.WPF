using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
    public static class UIServicesDefaults {
        public static readonly IDialogService DialogService = DefaultDialogService.Instance;
        public static readonly IFileDialogService FileDialogService = SimpleFileDialogService.Instance;
        public static readonly IMessageBoxService MessageBoxService = SimpleMessageBox.Instance;
    }
}
