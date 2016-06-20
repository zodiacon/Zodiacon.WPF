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
        Window _dialog;
        bool? _result;

        protected DialogViewModelBase(Window dialog) {
            _dialog = dialog;

            _okCommand = new DelegateCommand(() => Close(true), _canExecuteOKCommand);
        }

        protected DialogViewModelBase(bool? result) : this((Window)null) {
            _result = result;
        }

        protected void Close(bool? result = true) {
            if(_dialog != null) {
                _dialog.DialogResult = result;
                _dialog.Close();
            }
        }

        public bool? ShowDialog() {
            return _dialog != null ? _dialog.ShowDialog() : _result;
        }

        public void Show() {
            if(_dialog != null)
                _dialog.Show();
        }

        Func<bool> _canExecuteOKCommand = () => true;
        public Func<bool> CanExecuteOKCommand {
            get { return _canExecuteOKCommand; }
            set {
                if(SetProperty(ref _canExecuteOKCommand, value)) {
                    OKCommand = new DelegateCommand(() => Close(), value);
                    OKCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public Func<bool> CanExecuteCancelCommand { get; set; } = () => true;

        public DelegateCommand CancelCommand => new DelegateCommand(() => Close(false), CanExecuteCancelCommand);
        private DelegateCommand _okCommand;

        public DelegateCommand OKCommand {
            get { return _okCommand; }
            set { SetProperty(ref _okCommand, value); }
        }

    }
}
