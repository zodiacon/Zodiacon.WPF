using System;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Zodiacon.WPF {
    [MarkupExtensionReturnType(typeof(Image))]
    public sealed class ImageExtension : MarkupExtension {
        public string Uri { get; set; }

        public double Width { get; set; } = double.NaN;
        public double Height { get; set; } = double.NaN;
        public Stretch Stretch { get; set; } = Stretch.Uniform;

        public ImageExtension(string uri) {
            Uri = uri;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new Image {
                Source = new BitmapImage(new Uri(Uri, UriKind.RelativeOrAbsolute)),
                Width = Width,
                Height = Height,
                Stretch = Stretch
            };
        }
    }
}
