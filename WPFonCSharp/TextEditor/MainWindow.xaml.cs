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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
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

        private void main_mi_create_Click(object sender, RoutedEventArgs e)
        {
            //rtb_editor.Document.Blocks.Clear();            
        }

        private void main_mi_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != null)
                {

                    StreamReader sr = new StreamReader(ofd.OpenFile());
                    FlowDocument fd = new FlowDocument();
                    string line;
                    if (sr != null)
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            fd.Blocks.Add(new Paragraph(new Run(line)));
                        }
                        rtb_editor.Document = fd;
                        sr.Close();
                    }
                }

            }
            catch
            {

            }
        }

        private void main_mi_save_Click(object sender, RoutedEventArgs e)
        {
            /* StreamWriter sw = new StreamWriter("document.txt", false ,Encoding.UTF8);
             foreach (Paragraph block in rtb_editor.Document.Blocks)
             {
                 foreach (Run r in block.Inlines)
                 {
                     sw.WriteLine(r.Text);
                 }
             }             

             sw.Close();*/
        }

        private void main_mi_saveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_mi_settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_mi_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printdialog = new PrintDialog();
            printdialog.ShowDialog();
        }

        private void main_mi_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void rtb_editor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void main_mi_about_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void main_mi_statusBarShow_Click(object sender, RoutedEventArgs e)
        {
            if (main_mi_statusBarShow.IsChecked != true)
            {
                statusBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                statusBar.Visibility = Visibility.Visible;
            }

        }

        private void main_mi_growScale_Click(object sender, RoutedEventArgs e)
        {
            rtb_scale.ScaleX = rtb_scale.ScaleX + 0.1;
            rtb_scale.ScaleY = rtb_scale.ScaleY + 0.1;
            sb_scale.Text = Math.Round(rtb_scale.ScaleX * 100).ToString() + "%";

        }

        private void main_mi_tinyScale_Click(object sender, RoutedEventArgs e)
        {
            rtb_scale.ScaleX = rtb_scale.ScaleX - 0.1;
            rtb_scale.ScaleY = rtb_scale.ScaleY - 0.1;
            sb_scale.Text = Math.Round(rtb_scale.ScaleX * 100).ToString() + "%";
        }

        private void main_mi_defaultScale_Click(object sender, RoutedEventArgs e)
        {
            rtb_scale.ScaleX = 1.0;
            rtb_scale.ScaleY = 1.0;
            sb_scale.Text = Math.Round(rtb_scale.ScaleX * 100).ToString() + "%";
        }

        private void main_mi_fontShow_Click(object sender, RoutedEventArgs e)
        {
            FontsChoose fonts = new FontsChoose(rtb_editor.FontSize);
            if (fonts.ShowDialog() == true)
            {
                rtb_editor.FontSize = Double.Parse(fonts.FontSize.Text);

                foreach (Paragraph block in rtb_editor.Document.Blocks)
                {
                    foreach (Run inline in block.Inlines)
                    {
                        inline.FontFamily = (FontFamily)fonts.FontFamily.SelectedItem;
                    }

                }
            }
            fonts.Close();
        }
    }
}
