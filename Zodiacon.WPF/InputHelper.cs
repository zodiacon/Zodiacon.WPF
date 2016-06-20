using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zodiacon.WPF {
    public static class InputHelper {
        public static bool GetAttachInputBindings(DependencyObject obj) {
            return (bool)obj.GetValue(AttachInputBindingsProperty);
        }

        public static void SetAttachInputBindings(DependencyObject obj, bool value) {
            obj.SetValue(AttachInputBindingsProperty, value);
        }

        public static readonly DependencyProperty AttachInputBindingsProperty =
            DependencyProperty.RegisterAttached(nameof(AttachInputBindings), typeof(bool), typeof(InputHelper),
                new PropertyMetadata(false, OnAttachInputBindingsChanged));

        private static void OnAttachInputBindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var menu = d as Menu;
            if(menu != null) {
                menu.Loaded += Menu_Loaded;
            }
        }

        private static void Menu_Loaded(object sender, RoutedEventArgs e) {
            var menu = (Menu)sender;
            var window = Window.GetWindow(menu);
            foreach(MenuItem item in menu.Items) {
                AttachInputBindings(item, window);
            }
        }

        static char[] _splitters = new char[] { '+' };

        private static void AttachInputBindings(MenuItem item, Window window) {
            var gestureText = item.InputGestureText;
            if(!string.IsNullOrWhiteSpace(gestureText) && item.Command != null) {
                var words = gestureText.Split(_splitters, StringSplitOptions.RemoveEmptyEntries);
                var keyIndex = 1;
                if(words.Length == 1)
                    keyIndex = 0;
                var key = (Key)Enum.Parse(typeof(Key), words[keyIndex]);
                var modifiers = ModifierKeys.None;
                if(keyIndex == 1) {
                    if(words[0] == "Ctrl")
                        modifiers = ModifierKeys.Control;
                    else
                        modifiers = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), words[0]);
                }
                window.InputBindings.Add(new KeyBinding(item.Command, new KeyGesture(key, modifiers)) { CommandParameter = item.CommandParameter });
            }

            foreach(MenuItem subItem in item.Items.OfType<MenuItem>())
                AttachInputBindings(subItem, window);
        }
    }
}
