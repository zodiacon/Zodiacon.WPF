using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zodiacon.WPF {
    [Export("default", typeof(IMessageBoxService))]
    sealed class SimpleMessageBox : IMessageBoxService {
        Window _owner;
        private SimpleMessageBox() {
        }

        public MessageBoxResult ShowMessage(string message, string caption, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None) {
            return MessageBox.Show(_owner, message, caption, buttons, image);
        }

        public void SetOwner(Window ownerWindow) {
            _owner = ownerWindow;
        }

        public static readonly IMessageBoxService Instance = new SimpleMessageBox();
    }
}
