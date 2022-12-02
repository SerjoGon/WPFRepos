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

namespace checkers_game
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       private Brush p1_color;
       private Brush p2_color;
       private Random random1;
        public Window1()
        {
            InitializeComponent();
            random1 = new Random();
            // Генерация случайного цвета фишек
            this.p1_color = new SolidColorBrush(Color.FromRgb((byte)random1.Next(0, 255), (byte)random1.Next(0, 255), (byte)random1.Next(0, 255)));
            this.p2_color = new SolidColorBrush(Color.FromRgb((byte)random1.Next(0, 255), (byte)random1.Next(0, 255), (byte)random1.Next(0, 255)));
        }
        public Window1(Brush p1_color, Brush p2_color )
        {
            InitializeComponent();
            this.p1_color = p1_color;
            this.p2_color = p2_color;
        }
        public void start_game(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this.p1_color, this.p2_color); // Создание нового главного окна
            this.Visibility = Visibility.Collapsed; 
            mainWindow.Show(); 
            this.Close();
        }
        public void options(object sender, RoutedEventArgs e)
        {
            Window2 propertyWindow = new Window2(); // создание окна настроек
            this.Visibility = Visibility.Collapsed; 
            propertyWindow.Show();
            this.Close();
        }
    }
}