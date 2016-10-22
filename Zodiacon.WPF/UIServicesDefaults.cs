using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://zodiacon.wpf/2016", "Zodiacon.WPF")]
[assembly: XmlnsDefinition("http://zodiacon.wpf/2016", "Zodiacon.WPF.Behaviors")]

namespace Zodiacon.WPF {
	[Export]
	public sealed class UIServicesDefaults {
		[Export(typeof(IDialogService))]
		public readonly IDialogService DialogService = DefaultDialogService.Instance;

		[Export(typeof(IFileDialogService))]
		public readonly IFileDialogService FileDialogService = SimpleFileDialogService.Instance;

		[Export(typeof(IMessageBoxService))]
		public readonly IMessageBoxService MessageBoxService = SimpleMessageBox.Instance;
	}
}
