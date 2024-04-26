using Jeweler.Database;
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
using System.Windows.Shapes;

namespace Jeweler.View
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private readonly Database.TradeData tradeData;
        private Database.User user;
        public AdminPanel(TradeData tradeData)
        {
            InitializeComponent();

            this.tradeData = tradeData;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           Product product = new Product(tradeData, user);
            product.Owner = this;
            product.Show();
            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(tradeData);
            addProduct.Owner = this;
            addProduct.Show();
            Hide();
        }
    }
}
