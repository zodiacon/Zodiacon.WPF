using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace Zodiacon.WPF {
    public abstract class DialogViewModelBase : BindableBase {
        internal Window Dialog;
        bool? _result;

        protected DialogViewModelBase(Window dialog) {
            Dialog = dialog;

            _okCommand = new DelegateCommand(() => OnOK(), _canExecuteOKCommand);
        }

        protected virtual void OnOK() {
            Close(true);
        }

		protected virtual void OnCancel() {
			Close(false);
		}

        protected DialogViewModelBase(bool? result) : this((Window)null) {
            _result = result;
        }

		protected virtual void OnClose(bool? result) { }

        protected void Close(bool? result = true) {
			OnClose(result);
            if(Dialog != null) {
                Dialog.DialogResult = result;
                Dialog.Close();
            }
        }

        public bool? ShowDialog() {
            return Dialog != null ? Dialog.ShowDialog() : _result;
        }

        public void Show() {
            if(Dialog != null)
                Dialog.Show();
        }

        Func<bool> _canExecuteOKCommand = () => true;
        public Func<bool> CanExecuteOKCommand {
            get { return _canExecuteOKCommand; }
            set {
                if(SetProperty(ref _canExecuteOKCommand, value)) {
                    OKCommand = new DelegateCommand(() => OnOK(), value);
                    OKCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public Func<bool> CanExecuteCancelCommand { get; set; } = () => true;

        public DelegateCommand CancelCommand => new DelegateCommand(() => OnCancel(), CanExecuteCancelCommand);
        private DelegateCommand _okCommand;

        public DelegateCommand OKCommand {
            get { return _okCommand; }
            set { SetProperty(ref _okCommand, value); }
        }

    }
}
