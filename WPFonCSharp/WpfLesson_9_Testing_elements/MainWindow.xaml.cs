using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfLesson_9_Testing_elements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void cb_firstitem_Click(object sender, RoutedEventArgs e)
        {
            tree_sample.Items.Add(((ListBoxItem)lb_sample.Items[0]).Content);
        }

        private void cb_seconditem_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tree_sample2 = new TreeViewItem { Header = "Tree level 2"};
            tree_sample2.Items.Add(((ListBoxItem)lb_sample.Items[1]).Content);
            tree_sample2.Items.Add(((ListBoxItem)lb_sample.Items[2]).Content);
            tree_sample.Items.Add(tree_sample2);
        }

        private void cb_thirditem_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tree_sample3 = new TreeViewItem { Header = "Tree level 3" };
            tree_sample3.Items.Add(((ListBoxItem)lb_sample.Items[3]).Content);
            tree_sample3.Items.Add(((ListBoxItem)lb_sample.Items[4]).Content);
            ((TreeViewItem)tree_sample.Items[1]).Items.Add(tree_sample3);
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            for(double i = Pb.Minimum; i < Pb.Maximum;i++)
            {
                Form.Height = i;
                Pb.Value = i;
            }
        }
    }
}
