using Jeweler.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public byte[] photo { get; set; }
        private readonly Database.TradeData tradeData;
        private Database.Product currentProduct = new Database.Product();
        public ObservableCollection<Manufacturer> Manafacts { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
       
        public ObservableCollection<Category> Categories { get; set; }
        public AddProduct(TradeData tradeData)
        {
            InitializeComponent();
            this.tradeData = tradeData;
            Binding bindingManu = new Binding();
            Manafacts = new ObservableCollection<Manufacturer>(tradeData.Manufacturers);
            bindingManu.Source = Manafacts;
            cbManu.SetBinding(ComboBox.ItemsSourceProperty, bindingManu);

            Binding bindingCategory = new Binding();
            Categories = new ObservableCollection<Category>(tradeData.Categories);
            bindingCategory.Source = Categories;
            cbCategory.SetBinding(ComboBox.ItemsSourceProperty, bindingCategory);

            Binding bindingProvider = new Binding();
            Providers = new ObservableCollection<Provider>(tradeData.Providers);
            bindingProvider.Source = Providers;
            cbProvider.SetBinding(ComboBox.ItemsSourceProperty, bindingProvider);


        }

        private void AddPhotography(object sender, RoutedEventArgs e)
        {
            Database.Product product = new Database.Product();
            if (product == null) return;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы изображений|.jpg;.jpeg;*.png;";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                Stream fileStream = fileDialog.OpenFile();
                product.Photo = new byte[fileStream.Length];
                fileStream.Read(product.Photo, 0, (int)fileStream.Length);
                fileStream.Seek(0, SeekOrigin.Begin);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = fileStream;
                bitmap.EndInit();
                ImagePreview.Source = bitmap;
                if (currentProduct.Photo == null)
                {
                    photo = product.Photo;
                }
                else { photo = null; }


            }

        }
        //записать унит 
        private void SaveProduct(object sender, RoutedEventArgs e)
        {
            var article = tbArticle.Text.Trim();
            var name = tbName.Text.Trim();
            var desc = tbDesc.Text.Trim();
            var category = tradeData.Categories.Where(g => g.Name.Equals(cbCategory.Text)).FirstOrDefault();

            var Manu = tradeData.Manufacturers.Where(g => g.Name.Equals(cbManu.Text)).FirstOrDefault();
            var provider = tradeData.Providers.Where(g => g.Name.Equals(cbProvider.Text)).FirstOrDefault();
            var cost = int.Parse(tbPrice.Text.Trim());
            var discount = (byte)int.Parse(tbDiscount.Text.Trim());
            var discountMax = (byte)int.Parse(tbDiscountMax.Text.Trim());
            var quantity = int.Parse(tbQuantity.Text.Trim());
            var unit = "шт";

            var product = new Database.Product()
            {
                ArticleNumber = article,
                Name = name,
                Description = desc,
                Category = category.ID,
                Photo = photo,
                Manufacturer1 = Manu,
                Provider1 = provider,
                Cost = cost,
                DiscountAmount = discount,
                DiscountMaxAmount = discountMax,
                QuantityInStock = quantity,
                Unit = unit,


            };
            currentProduct = product;
            tradeData.Products.Add(currentProduct);
            tradeData.SaveChanges();
            

            AdminPanel panel = new AdminPanel(tradeData);
            panel.Owner = this;
            panel.Show();
            Hide();
        }
    }
}
