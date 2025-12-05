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
    /// Interaction logic for ShoppingCartItem.xaml
    /// </summary>
    public partial class ShoppingCartItem : UserControl
    {
        public ShoppingCartItem()
        {
            InitializeComponent();
        }

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register(nameof(ItemName), typeof(string), typeof(ShoppingCartItem));

        public int Quantity
        {
            get => (int)GetValue(QuantityProperty);
            set => SetValue(QuantityProperty, value);
        }

        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register(nameof(Quantity), typeof(int), typeof(ShoppingCartItem),
                new PropertyMetadata(1));

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Quantity++;
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (Quantity > 0)
                Quantity--;
        }
    }
}