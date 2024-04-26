using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public byte[] photo { get; set; }
        private Database.Product currentProduct = new Database.Product();
        public AddProduct()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
