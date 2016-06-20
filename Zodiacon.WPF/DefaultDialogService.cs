using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zodiacon.WPF {
    [Export(typeof(IDialogService))]
    sealed class DefaultDialogService : IDialogService {
        private DefaultDialogService() { }

        public TViewModel CreateDialog<TViewModel, TDialog>(params object[] args) where TDialog : Window, new() where TViewModel : DialogViewModelBase {
            var dlg = new TDialog();
            var vm = (TViewModel)Activator.CreateInstance(typeof(TViewModel), new object[] { dlg }.Concat(args).ToArray());
            dlg.DataContext = vm;

            return vm;
        }

        public static readonly IDialogService Instance = new DefaultDialogService();
    }
}
