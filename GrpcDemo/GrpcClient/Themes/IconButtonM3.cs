using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GrpcClient.Themes
{
    public class IconButtonM3 : Button
    {
        static IconButtonM3()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButtonM3),
                new FrameworkPropertyMetadata(typeof(IconButtonM3)));
        }

        public static readonly DependencyProperty IconImageDataProperty =
            DependencyProperty.Register(
                nameof(IconImageData),
                typeof(Geometry),
                typeof(IconButtonM3),
                new PropertyMetadata(null));

        public Geometry IconImageData
        {
            get => (Geometry)GetValue(IconImageDataProperty);
            set => SetValue(IconImageDataProperty, value);
        }
    }
}