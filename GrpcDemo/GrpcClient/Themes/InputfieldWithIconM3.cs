using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GrpcClient.Themes
{
    public class InputfieldWithIconM3 : TextBox
    {
        static InputfieldWithIconM3()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputfieldWithIconM3),
                new FrameworkPropertyMetadata(typeof(InputfieldWithIconM3)));
        }

        public static readonly DependencyProperty IconImageDataProperty =
            DependencyProperty.Register(
                nameof(IconImageData),
                typeof(Geometry),
                typeof(InputfieldWithIconM3),
                new PropertyMetadata(null));

        public Geometry IconImageData
        {
            get => (Geometry)GetValue(IconImageDataProperty);
            set => SetValue(IconImageDataProperty, value);
        }
    }
}