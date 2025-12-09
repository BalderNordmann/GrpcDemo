using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrpcClient.Themes.UserControls
{
    /// <summary>
    /// Interaction logic for BookItem.xaml
    /// </summary>
    public partial class BookItem : UserControl
    {
        public BookItem()
        {
            InitializeComponent();
        }

        public string BookTitle
        {
            get => (string)GetValue(BookTitleProperty);
            set => SetValue(BookTitleProperty, value);
        }

        public static readonly DependencyProperty BookTitleProperty =
            DependencyProperty.Register(nameof(BookTitle), typeof(string), typeof(ShoppingCartItem));

        public string BookAuthor
        {
            get => (string)GetValue(BookAuthorProperty);
            set => SetValue(BookAuthorProperty, value);
        }

        public static readonly DependencyProperty BookAuthorProperty =
            DependencyProperty.Register(nameof(BookAuthor), typeof(string), typeof(ShoppingCartItem));
    }
}
