using Jeweler.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class SortItem
    {
      public  string Text { get; set; }
      public SortDescription Description { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        private readonly Database.TradeData trade;
        public readonly Database.Manufacturer allManufacturer;
       
        public Database.Manufacturer SelectedManufacturer { get; set; }
        public SortItem SelectedSort { get; set; }
        public  ObservableCollection<Database.Product> products { get; set; }
        public ObservableCollection<Database.Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<SortItem> SortItem { get; set; }

        public Product(Database.TradeData trade, Database.User user)
        {
            InitializeComponent();
            this.trade = trade;
            allManufacturer = new Manufacturer() { ID = 0, Name = "Все производители" };
            products = new ObservableCollection<Database.Product>(trade.Products);
            Manufacturers = new ObservableCollection<Database.Manufacturer>(trade.Manufacturers);
            Manufacturers.Insert(0, allManufacturer);
            SortItem = new ObservableCollection<SortItem>();
            SortItem.Add( new SortItem() { Text = "Цена по возрастанию", Description = new SortDescription() { PropertyName = "Cost", Direction = ListSortDirection.Ascending} } );
            SortItem.Add( new SortItem() { Text = "Цена по убыванию", Description = new SortDescription() { PropertyName = "Cost", Direction = ListSortDirection.Descending} } );
            
          
            DataContext = this;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var searchString = tbSearch.Text.Trim().ToLower();
            var view =  CollectionViewSource.GetDefaultView(lvProducts.ItemsSource);

            if (view == null) return;

            view.Filter = (object o) =>
               {
                var product =  o as Database.Product;
                   if (product == null) return false;

                   

                   if (searchString.Length > 0)
                   {
                      
                       if(!(product.Name.ToLower().Contains(searchString) ||
                       product.Description.ToLower().Contains(searchString) ||
                       product.Category1.Name.ToLower().Contains(searchString)||
                       product.Manufacturer1.Name.ToLower().Contains(searchString) ||
                       product.Provider1.Name.ToLower().Contains(searchString)))
                           {
                           return false;
                       }

                       
                   }
                   if(SelectedManufacturer != null && SelectedManufacturer != allManufacturer)
                   {
                       if(product.Manufacturer1 != SelectedManufacturer)
                       {
                           return false;
                       }
                   }
                   return true;
               };
        }

        private void ApplySort()
        {
            var view = CollectionViewSource.GetDefaultView(lvProducts.ItemsSource);

            if (view == null) return;

            view.SortDescriptions.Clear();

            if(SelectedSort != null)
            {
                view.SortDescriptions.Add(SelectedSort.Description);
            }

            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ApplySort();
        }
    }
}
