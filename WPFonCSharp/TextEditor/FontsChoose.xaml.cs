﻿using System;
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

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for FontsChoose.xaml
    /// </summary>
    public partial class FontsChoose : Window
    {
        public FontsChoose(double fontsize)
        {
            InitializeComponent();
            FontSize.Text = fontsize.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
