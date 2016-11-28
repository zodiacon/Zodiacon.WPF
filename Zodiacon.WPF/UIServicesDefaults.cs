using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://zodiacon.wpf/2016", "Zodiacon.WPF")]
[assembly: XmlnsDefinition("http://zodiacon.wpf/2016", "Zodiacon.WPF.Behaviors")]

namespace Zodiacon.WPF {
	[Export]
	public sealed class UIServicesDefaults {
		public readonly IDialogService DialogService = DefaultDialogService.Instance;

		public readonly IFileDialogService FileDialogService = SimpleFileDialogService.Instance;

		public readonly IMessageBoxService MessageBoxService = SimpleMessageBox.Instance;

		public void Publish(CompositionContainer container) {
			container.ComposeExportedValue(DialogService);
			container.ComposeExportedValue(FileDialogService);
			container.ComposeExportedValue(MessageBoxService);
		}
	}
}
