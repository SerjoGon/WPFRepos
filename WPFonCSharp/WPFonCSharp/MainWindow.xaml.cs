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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFonCSharp
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
        double firstnumber = 0.0, secondnumber = 0.0;
        string oper = "";
        private void btn_zero_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "0";
        }

        private void btn_one_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "1";
        }

        private void btn_two_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "2";
        }

        private void btn_three_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "3";
        }

        private void btn_four_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "4";
        }

        private void btn_five_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "5";
        }

        private void btn_six_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "6";
        }

        private void btn_seven_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "7";
        }

        private void btn_eight_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "8";
        }

        private void btn_nine_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text += "9";
        }

        private void btn_point_Click(object sender, RoutedEventArgs e)
        {
            if (tb_currentResult.Text.Length != 0 && !tb_currentResult.Text.Contains(","))
            {
                tb_currentResult.Text += ",";
            }
            else if(tb_currentResult.Text.Length == 0)
            {
                tb_currentResult.Text += "0,";
            }
        }

        private void btn_currentClear_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text = "";
            firstnumber = 0.0;
        }

        private void btn_globalClear_Click(object sender, RoutedEventArgs e)
        {
            tb_currentResult.Text = "";
            tb_globalResult.Text = "";
            firstnumber = 0.0;
            secondnumber = 0.0;
           

        }

        private void btn_lastcharDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tb_currentResult.Text.Length != 0)
                tb_currentResult.Text = tb_currentResult.Text.Substring(0, tb_currentResult.Text.Length - 1);
        }

        private void btn_divide_Click(object sender, RoutedEventArgs e)
        {
            if (tb_currentResult.Text.Length > 0 && btn_sub.Content.ToString() != oper)
                OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
            oper = "/";
            OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
        }

        private void btn_multiple_Click(object sender, RoutedEventArgs e)
        {
            if (tb_currentResult.Text.Length > 0 && btn_sub.Content.ToString() != oper)
                OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
            oper = "*";
            OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
        }

        private void btn_sub_Click(object sender, RoutedEventArgs e)
        {
            if(tb_currentResult.Text.Length > 0 && btn_sub.Content.ToString() != oper)
                OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
            oper = "-";
            OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_currentResult.Text.Length > 0 && btn_sub.Content.ToString() != oper)
                OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, false);
            oper = "+";
            OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult,false);
        }

        private void btn_equal_Click(object sender, RoutedEventArgs e)
        {
            OperationResult(ref firstnumber, ref secondnumber, oper, ref tb_currentResult, ref tb_globalResult, true);

        }
        static void OperationResult(ref double n1,ref double n2, string operation,ref TextBox current, ref TextBox global,bool end)
        {
            if (current.Text.Length != 0 && current.Text[current.Text.Length - 1] != ',')
            {
                if (n1 == 0.0)
                {
                    n1 = Double.Parse(current.Text);
                }
                else
                {
                    n2 = Double.Parse(current.Text);
                }
                if (n1 != 0.0 && n2 != 0.0)
                {
                    switch(operation)
                    {
                        case "+": n1 += n2; break;
                        case "-": n1 -= n2; break;
                        case "*": n1 *= n2; break;
                        case "/": n1 /= n2; break;
                        //case "=": n1 = n2; global.Text = n1.ToString(); break;
                    }
                    if(end) global.Text = n1.ToString(); 
                    if(!end) global.Text = n1 + operation;
                    n2 = 0.0;
                }
                else
                {
                    global.Text += n1 + operation;
                }
                current.Text = "";
            }
            else if(!global.Text.Contains(operation))
            {
                global.Text = n1 + operation;
            }
        }
    }
}
