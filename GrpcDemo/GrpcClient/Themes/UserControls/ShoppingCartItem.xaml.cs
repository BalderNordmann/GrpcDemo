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
            Loaded += shoppingCartItem_Loaded;
        }

        public static readonly RoutedEvent RemoveEvent =
            EventManager.RegisterRoutedEvent(name: "Remove", routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler), ownerType: typeof(ShoppingCartItem));

        public event RoutedEventHandler Remove
        {
            add { AddHandler(RemoveEvent, value); }
            remove { RemoveHandler(RemoveEvent, value); }
        }

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register(nameof(ItemName), typeof(string), typeof(ShoppingCartItem));

        public float ItemPrice
        {
            get => (float)GetValue(ItemPriceProperty);
            set
            {
                SetValue(ItemPriceProperty, value);
                updateItemPriceDisplay();
            }
        }

        public static readonly DependencyProperty ItemPriceProperty =
            DependencyProperty.Register(nameof(ItemPrice), typeof(float), typeof(ShoppingCartItem));       

        public string ItemPriceDisplay
        {
            get => (string)GetValue(ItemPriceDisplayProperty);
            set => SetValue(ItemPriceDisplayProperty, value);
        }

        public static readonly DependencyProperty ItemPriceDisplayProperty =
            DependencyProperty.Register(nameof(ItemPriceDisplay), typeof(string), typeof(ShoppingCartItem));

        public int Quantity
        {
            get => (int)GetValue(QuantityProperty);
            set
            {
                SetValue(QuantityProperty, value);
                updateItemPriceDisplay();
            }
        }

        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register(nameof(Quantity), typeof(int), typeof(ShoppingCartItem),
                new PropertyMetadata(1));

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            Quantity++;
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (Quantity > 1)
                Quantity--;
        }

        protected void RaiseRemoveEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(RemoveEvent);
            RaiseEvent(args);
        }

        private void removeShoppingCardItem_Click(object sender, RoutedEventArgs e)
        {
            RaiseRemoveEvent();
        }

        private void updateItemPriceDisplay()
        {
             float calcPrice = MathF.Round(ItemPrice * Quantity, 2);
            ItemPriceDisplay = $"{calcPrice.ToString("F2")}€";
        }

        private void shoppingCartItem_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.InvokeAsync(() =>
            {
                updateItemPriceDisplay();
            }, System.Windows.Threading.DispatcherPriority.DataBind);
        }
    }
}