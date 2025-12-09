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

        public static readonly RoutedEvent ClickEvent = 
            EventManager.RegisterRoutedEvent(name: "Click", routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler), ownerType: typeof(BookItem));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public string BookTitle
        {
            get => (string)GetValue(BookTitleProperty);
            set => SetValue(BookTitleProperty, value);
        }

        public static readonly DependencyProperty BookTitleProperty =
            DependencyProperty.Register(nameof(BookTitle), typeof(string), typeof(BookItem));

        public string BookAuthor
        {
            get => (string)GetValue(BookAuthorProperty);
            set => SetValue(BookAuthorProperty, value);
        }

        public static readonly DependencyProperty BookAuthorProperty =
            DependencyProperty.Register(nameof(BookAuthor), typeof(string), typeof(BookItem));

        public string BookPrice
        {
            get => (string)GetValue(BookPriceProperty);
            set => SetValue(BookPriceProperty, value);
        }

        public static readonly DependencyProperty BookPriceProperty =
            DependencyProperty.Register(nameof(BookPrice), typeof(string), typeof(BookItem));

        public string BookStatus
        {
            get => (string)GetValue(BookStatusProperty);
            set => SetValue(BookStatusProperty, value);
        }

        public static readonly DependencyProperty BookStatusProperty =
            DependencyProperty.Register(nameof(BookStatus), typeof(string), typeof(BookItem));

        public Brush BookStatusColor
        {
            get => (Brush)GetValue(BookStatusColorProperty);
            set => SetValue(BookStatusColorProperty, value);
        }

        public static readonly DependencyProperty BookStatusColorProperty =
            DependencyProperty.Register(nameof(BookStatusColor), typeof(Brush), typeof(BookItem));

        public string BookID
        {
            get => (string)GetValue(BookIDProperty);
            set => SetValue(BookIDProperty, value);
        }

        public static readonly DependencyProperty BookIDProperty =
            DependencyProperty.Register(nameof(BookID), typeof(string), typeof(BookItem));

        protected void RaiseClickEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
        }
    }
}
