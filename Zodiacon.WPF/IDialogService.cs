using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zodiacon.WPF {
    public interface IDialogService {
        TViewModel CreateDialog<TViewModel, TDialog>(params object[] args) where TDialog : Window, new() where TViewModel : DialogViewModelBase;
		TViewModel CreateDialog<TViewModel, TDialog>(TViewModel vm) where TDialog : Window, new() where TViewModel : DialogViewModelBase;
	}
}
