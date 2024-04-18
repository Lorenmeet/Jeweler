using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jeweler.View
{
 
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private readonly Random random;
        private readonly string captchaSymbols = "QAZWSXEDCRFVTGBYHNUJMIK1234567890";
        private readonly Database.TradeData tradeData;
        private Database.User user;
        private bool isCaptcha;
        private string captchaCode;


        public Authorization()
        {
            InitializeComponent();
            random = new Random(Environment.TickCount);

            tradeData = new Database.TradeData();

        }

        private void OnSignIn(object sender, RoutedEventArgs e)
        {
            if (isCaptcha && captchaCode.ToLower() != tbCaptcha.Text.Trim().ToLower())
            {
                MessageBox.Show("ляляляляля");
                return;
            }
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();

            if (login.Length < 1)
            {
                MessageBox.Show("Необходимо ввести логин");
                return;
            }
            if (password.Length < 1)
            {
                MessageBox.Show("Необходимо ввести пароль");
                return;
            }

            user = tradeData.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Некорректный логин и(или) пароль");
                generateCaptcha();
                return;
            }
            else
            {
               
            }
        }


        private void generateCaptcha()
        {
            captchaCode = getNewCaptchaCode();

            for (int i = 0; i < captchaCode.Length; i++)
            {
                AddCharToCanvas(i, captchaCode[i]);
            }
            GenerateNoize();

        }
        private string getNewCaptchaCode()
        {
            canvas.Children.Clear();
            string code = "";
            for (int i = 0; i < 4; i++)
            {
                code += captchaSymbols[random.Next(captchaSymbols.Length)];
            }
            return code;
        }

        private void AddCharToCanvas(int index, char ch)
        {
            Label label = new Label();
            label.Content = ch.ToString();
            label.FontSize = random.Next(24, 38);
            label.Width = 30;
            label.Height = 60;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.RenderTransformOrigin = new Point(0.5, 0.5);
            label.RenderTransform = new RotateTransform(random.Next(-20, 15));


            canvas.Children.Add(label);
            int startPosition = (int)((canvas.ActualWidth / 2) - (30 * 4 / 2));

            Canvas.SetLeft(label, startPosition + (index * 30));
            Canvas.SetTop(label, random.Next(0, 10));
        }

        private void GenerateNoize()
        {
            for (int i = 1; i < 100; i++)
            {
                // Не знаю какая высота и ширина, по этому так
                double x = random.NextDouble() * canvas.ActualWidth;
                double y = random.NextDouble() * canvas.ActualHeight;

                int radius = random.Next(2, 5);
                Ellipse ellipse = new Ellipse
                {
                 Width = radius,
                Height = radius,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                 }; 

               canvas.Children.Add(ellipse);

                 Canvas.SetLeft(ellipse, x);
                 Canvas.SetTop(ellipse, y);
             }

            int lineCount = random.Next(2, 5);
            for (int i = 0; i < lineCount; i++)
            {
                Line line = new Line();

                line.X1 = random.Next(100, 120);
                line.Y1 = random.Next(10, 54);
                line.X2 = random.Next(250, 280);
                line.Y2 = random.Next(10, 54);
                line.Stroke = Brushes.Black;
                line.StrokeThickness = random.Next(2, 4);

                canvas.Children.Add(line);
            }
        }
       
    }

}
     
