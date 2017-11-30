using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Zodiacon.WPF.Behaviors {
    public sealed class EventToCommand : Behavior<DependencyObject> {
        EventInfo _event;
        Delegate _handler;

        protected override void OnAttached() {
            base.OnAttached();

            if (EventName != null) {
                _event = AssociatedObject.GetType().GetEvent(EventName);
                if (_event != null) {
                    _handler = Delegate.CreateDelegate(_event.EventHandlerType, this, GetType().GetMethod(nameof(OnEvent), 
                        BindingFlags.NonPublic | BindingFlags.Instance));
                    _event.AddEventHandler(AssociatedObject, _handler);
                }
            }
        }

        void OnEvent(object sender, EventArgs args) {
            if (Command.CanExecute(args))
                Command.Execute(args);
        }

        protected override void OnDetaching() {
            if (_event != null) {
                _event.RemoveEventHandler(AssociatedObject, _handler);
            }

            base.OnDetaching();
        }

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventToCommand), new PropertyMetadata(null));


        public string EventName {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.Register(nameof(EventName), typeof(string), typeof(EventToCommand), new PropertyMetadata(null));


    }
}
