using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zodiacon.WPF {
	public interface IUIServices {
		IDialogService DialogService { get; }
		IFileDialogService FileDialogService { get; }
		IMessageBoxService MessageBoxService { get; }
	}
}