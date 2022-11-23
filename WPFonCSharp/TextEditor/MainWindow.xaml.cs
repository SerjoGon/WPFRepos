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

        private void main_mi_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void main_mi_print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_mi_settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_mi_saveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void main_mi_save_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("document.txt", false, Encoding.UTF8);
            foreach (Paragraph par in rtb_editor.Document.Blocks)
            {
                foreach (Run r in par.Inlines)
                {
                    if (r.Text.Contains("\t"))
                    {

                    }
                    sw.WriteLine(r.Text);
                }
            }
            sw.Close();
        }

        private void main_mi_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowDocument fd = new FlowDocument();
                StreamReader sr = new StreamReader("document.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    fd.Blocks.Add(new Paragraph(new Run(line)));
                }
                rtb_editor.Document = fd;
                sr.Close();
            }
            catch
            {

            }
        }

        private void main_mi_create_Click(object sender, RoutedEventArgs e)
        {
            rtb_editor.Document.Blocks.Clear();
            //Run run = new Run("New Dock");
            //Paragraph paragraph = new Paragraph(new Run("New Dock"));
            // rtb_editor.Document.Blocks.Add(new Paragraph(new Run("New Dock")));

        }
        private void rtb_editor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
