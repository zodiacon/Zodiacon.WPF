using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

[assembly:XmlnsDefinition("http://zodiacon.wpf/2016", "Zodiacon.WPF")]
[assembly:XmlnsDefinition("http://zodiacon.wpf/2016", "Zodiacon.WPF.Behaviors")]

namespace Zodiacon.WPF {
    public static class UIServicesDefaults {
        public static readonly IDialogService DialogService = DefaultDialogService.Instance;
        public static readonly IFileDialogService FileDialogService = SimpleFileDialogService.Instance;
        public static readonly IMessageBoxService MessageBoxService = SimpleMessageBox.Instance;
    }
}
