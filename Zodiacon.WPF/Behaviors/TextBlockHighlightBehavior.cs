using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Zodiacon.WPF.Behaviors {
    public enum SearchTextOptions {
        None,
        IgnoreCase = 1
    }

    /// <summary>
    /// Highlight parts of a TextBlock based on search text
    /// </summary>
    public sealed class TextBlockHighlightBehavior : Behavior<TextBlock> {
        protected override void OnAttached() {
            base.OnAttached();

            AssociatedObject.Loaded += delegate {
                if(!string.IsNullOrEmpty(SearchText))
                    OnSearchTextChanged();
            };

        }

        public SearchTextOptions SearchTextOptions {
            get { return (SearchTextOptions)GetValue(SearchTextOptionsProperty); }
            set { SetValue(SearchTextOptionsProperty, value); }
        }

        public static readonly DependencyProperty SearchTextOptionsProperty =
            DependencyProperty.Register(nameof(SearchTextOptions), typeof(SearchTextOptions), typeof(TextBlockHighlightBehavior), 
                new PropertyMetadata(SearchTextOptions.IgnoreCase));

        public Brush HighlightBackground {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register(nameof(HighlightBackground), typeof(Brush), typeof(TextBlockHighlightBehavior), new PropertyMetadata(Brushes.Yellow));


        public Brush HighlightForeground {
            get { return (Brush)GetValue(HighlightForegroundProperty); }
            set { SetValue(HighlightForegroundProperty, value); }
        }

        public static readonly DependencyProperty HighlightForegroundProperty =
            DependencyProperty.Register(nameof(HighlightForeground), typeof(Brush), typeof(TextBlockHighlightBehavior), new PropertyMetadata(null));


        public string SearchText {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(TextBlockHighlightBehavior),
                new PropertyMetadata(null, (s, e) => ((TextBlockHighlightBehavior)s).OnSearchTextChanged()));

        private void OnSearchTextChanged() {
            if(AssociatedObject == null)
                return;

            var inlines = AssociatedObject.Inlines;
            inlines.Clear();
            var value = AssociatedObject.Tag.ToString();

            if(string.IsNullOrEmpty(SearchText) || !value.ToLower().Contains(SearchText.ToLower())) {
                inlines.Add(value);
            }
            else {
                var foreground = HighlightForeground ?? AssociatedObject.Foreground;
                var background = HighlightBackground ?? AssociatedObject.Background;

                var regex = new Regex("(" + SearchText + ")", 
                    SearchTextOptions == SearchTextOptions.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                foreach(var substring in regex.Split(value)) {
                    if(string.IsNullOrEmpty(substring))
                        continue;

                    if(regex.Match(substring).Success)
                        inlines.Add(new Run(substring) { Background = background, Foreground = foreground });
                    else
                        inlines.Add(substring);
                }
            }
        }
    }
}
