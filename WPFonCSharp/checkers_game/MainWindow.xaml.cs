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

namespace checkers_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private variables
        //Игровая доска в виде массива перечеслений 
        private CheckerType[,] Board_array;
        // булевая переменная 1го игрока, используется для проверки очередности хода 
        private bool player_one_turn;
        //булевая 2го игрока, используется для проверки очередности хода 
        private bool players_second_click;
        // лист кнопок
        private List<Button> buttonList;
        // переменная запоминающая кнопку
        private Button prevButton;
        // переменные для запоминания нажатой кнопки и предидущей кнопки
        private int row, column, prevRow, prevCol;
        private int p1_check_count, p2_check_count;
        // цвета шашек
        private Brush p1_color;
        private Brush p2_color;
        #endregion
        public MainWindow(Brush p1_color, Brush p2_color)
        {
            InitializeComponent();
            this.p1_color = p1_color;
            this.p2_color = p2_color;
            p1_identifier.Foreground = p1_color;
            p2_identifier.Foreground = p2_color;
            NewGame();
        }
        private void NewGame()
        {
            buttonList = Board.Children.Cast<Button>().ToList();
            // создание игрового поля
            Board_array = new CheckerType[8, 8];
            // расстановка фигурок на доске 
            for (int row = 0; row < 8; row++)
            {
                if (row == 0 || row == 2 || row == 6)
                {
                    for (int col = 0; col < 7; col += 2)
                    {
                        if (row == 0 || row == 2) { Board_array[row, col] = CheckerType.P2_check; }
                        else { Board_array[row, col] = CheckerType.P1_check; }
                    }
                }
                if (row == 1 || row == 5 || row == 7)
                {
                    for (int col = 1; col < 8; col += 2)
                    {
                        if (row == 5 || row == 7) { Board_array[row, col] = CheckerType.P1_check; } // row 5 and 7 are filled with player 1 checkers

                        else { Board_array[row, col] = CheckerType.P2_check; }

                    }

                }
            } //окончание расстановки
            // Инициализация переменных
            player_one_turn = true; //при старте всегда ходит первым игрок 1
            players_second_click = false;
            row = -1;
            column = 0;
            prevRow = 0;
            prevCol = 0;
            p1_check_count = 12;
            p2_check_count = 12;
            int counter = 0;
            // лямбда функция для настройки и отрисовки кнопок (шашек) на доске
            buttonList.ForEach(button =>
            // три верхних ряда заполняются для игрока 2
            {
                if (counter < 12)
                {
                    button.Content = "•";
                    button.Foreground = p2_color;
                    counter++;
                }
                // три нижних ряда для игрока 1
                else if (counter >= 20 && counter < 32)
                {
                    button.Content = "•";
                    button.Foreground = p1_color;
                    counter++;
                }
                else
                {
                    button.Content = string.Empty;
                    counter++;
                }
            }
            );
        }
        /* вспомогательные функции */
        private void borderChangeOnCLick(Button button)
        {
            //меняет границы нажатой кнопки для лучшей видимости
            button.BorderThickness = new Thickness(3, 3, 3, 3);
            button.BorderBrush = Brushes.Snow;
        }
        private void borderChangeBack(Button button)
        {
            // сброс нажатой кнопки
            button.BorderThickness = new Thickness(1, 1, 1, 1);
            button.BorderBrush = Brushes.SlateGray;
        }
        private void updateBoardGui()
        {
            /*Обновление графического интерфейса после хода*/
            buttonList.ForEach(button =>
            {
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);
                if (Board_array[row, col] == CheckerType.P1_check)
                {
                    button.Content = "•";
                    button.Foreground = p1_color;
                }
                else if (Board_array[row, col] == CheckerType.P1_king)
                {
                    button.Content = "♛";
                    button.Foreground = p1_color;
                }
                else if (Board_array[row, col] == CheckerType.P2_check)
                {
                    button.Content = "•";
                    button.Foreground = p2_color;
                }
                else if (Board_array[row, col] == CheckerType.P2_king)
                {
                    button.Content = "♚";
                    button.Foreground = p2_color;
                }
                else
                {
                    button.Content = "";
                }
            });
        }
        // функции определяющие доступен ли дополнительный ход (убрать более одной шашки противника) 
        private bool p1_jump_available()
        {
            if (row - 2 >= 0 && column - 2 >= 0 && Board_array[row - 2, column - 2] == CheckerType.Free && (Board_array[row - 1, column - 1] == CheckerType.P2_check || Board_array[row - 1, column - 1] == CheckerType.P2_king))
            {
                return true;
            }
            else if (row - 2 >= 0 && column + 2 <= 7 && Board_array[row - 2, column + 2] == CheckerType.Free && (Board_array[row - 1, column + 1] == CheckerType.P2_check || Board_array[row - 1, column + 1] == CheckerType.P2_king))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool p2_jump_available()
        {
            if (row + 2 <= 7 && column + 2 <= 7 && Board_array[row + 2, column + 2] == CheckerType.Free && (Board_array[row + 1, column + 1] == CheckerType.P1_check || Board_array[row + 1, column + 1] == CheckerType.P1_king))
            {
                return true;
            }
            else if (row + 2 <= 7 && column - 2 >= 0 && Board_array[row + 2, column - 2] == CheckerType.Free && (Board_array[row + 1, column - 1] == CheckerType.P1_check || Board_array[row + 1, column - 1] == CheckerType.P1_king))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool more_king_jump_available()
        {
            if (player_one_turn)
            {
                if (row - 2 >= 0 && column - 2 >= 0 && Board_array[row - 2, column - 2] == CheckerType.Free && (Board_array[row - 1, column - 1] == CheckerType.P2_check || Board_array[row - 1, column - 1] == CheckerType.P2_king))
                {
                    return true;
                }
                else if (row - 2 >= 0 && column + 2 <= 7 && Board_array[row - 2, column + 2] == CheckerType.Free && (Board_array[row - 1, column + 1] == CheckerType.P2_check || Board_array[row - 1, column + 1] == CheckerType.P2_king))
                {
                    return true;
                }
                else if (row + 2 <= 7 && column - 2 >= 0 && Board_array[row + 2, column - 2] == CheckerType.Free && (Board_array[row + 1, column - 1] == CheckerType.P2_check || Board_array[row + 1, column - 1] == CheckerType.P2_king))
                {
                    return true;
                }
                else if (row + 2 <= 7 && column + 2 <= 7 && Board_array[row + 2, column + 2] == CheckerType.Free && (Board_array[row + 1, column + 1] == CheckerType.P2_check || Board_array[row + 1, column + 1] == CheckerType.P2_king))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (row - 2 >= 0 && column - 2 >= 0 && Board_array[row - 2, column - 2] == CheckerType.Free && (Board_array[row - 1, column - 1] == CheckerType.P1_check || Board_array[row - 1, column - 1] == CheckerType.P1_king))
                {
                    return true;
                }
                else if (row - 2 >= 0 && column + 2 <= 7 && Board_array[row - 2, column + 2] == CheckerType.Free && (Board_array[row - 1, column + 1] == CheckerType.P1_check || Board_array[row - 1, column + 1] == CheckerType.P1_king))
                {
                    return true;
                }
                else if (row - 2 >= 0 && column - 2 >= 0 && Board_array[row + 2, column - 2] == CheckerType.Free && (Board_array[row + 1, column - 1] == CheckerType.P1_check || Board_array[row + 1, column - 1] == CheckerType.P1_king))
                {
                    return true;
                }
                else if (row - 2 >= 0 && column + 2 <= 7 && Board_array[row + 2, column + 2] == CheckerType.Free && (Board_array[row + 1, column + 1] == CheckerType.P1_check || Board_array[row + 1, column + 1] == CheckerType.P1_king))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        // Функция определяющая возможность превращения шашки в короля
        private bool Is_kinged()
        {
            System.Console.WriteLine("Королевская линия " + row);

            if (row == 0 && Board_array[prevRow, prevCol] == CheckerType.P1_check)
            {
                System.Console.WriteLine("Коронная шашка");
                Board_array[row, column] = CheckerType.P1_king;
                Board_array[prevRow, prevCol] = CheckerType.Free;
                updateBoardGui();
                return true;
            }
            else if (row == 7 && Board_array[prevRow, prevCol] == CheckerType.P2_check)
            {
                Board_array[row, column] = CheckerType.P2_king;
                Board_array[prevRow, prevCol] = CheckerType.Free;
                updateBoardGui();
                return true;
            }
            else
            {
                return false;
            }
        }
        // ниже функции проверки хода шашки короля
        private bool is_normal_king_move()
        {
            if (Board_array[row, column] == CheckerType.Free && (row - prevRow == 1 || row - prevRow == -1) && (column - prevCol == 1 || column - prevCol == -1))
            {
                Board_array[row, column] = Board_array[prevRow, prevCol];
                Board_array[prevRow, prevCol] = CheckerType.Free;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool is_valid_king_jump()
        {
            if (player_one_turn)
            {
                if (Board_array[row, column] == CheckerType.Free && row - prevRow == 2 && column - prevCol == 2)
                {
                    if (Board_array[row - 1, column - 1] == CheckerType.P2_check || Board_array[row - 1, column - 1] == CheckerType.P2_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Board_array[row, column] == CheckerType.Free && row - prevRow == 2 && column - prevCol == -2)
                {
                    if (Board_array[row - 1, column + 1] == CheckerType.P2_check || Board_array[row - 1, column + 1] == CheckerType.P2_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Board_array[row, column] == CheckerType.Free && row - prevRow == -2 && column - prevCol == 2)
                {
                    if (Board_array[row + 1, column - 1] == CheckerType.P2_check || Board_array[row + 1, column - 1] == CheckerType.P2_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    }
                else if (Board_array[row, column] == CheckerType.Free && row - prevRow == -2 && column - prevCol == -2)
                {
                    if (Board_array[row + 1, column + 1] == CheckerType.P2_check || Board_array[row + 1, column + 1] == CheckerType.P2_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // условия для второго игрока 
                if (Board_array[row, column] == CheckerType.Free && row - prevRow == 2 && column - prevCol == 2)
                {
                    if (Board_array[row - 1, column - 1] == CheckerType.P1_check || Board_array[row - 1, column - 1] == CheckerType.P1_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else if (Board_array[row, column] == CheckerType.Free && row - prevRow == 2 && column - prevCol == -2)
                {
                    if (Board_array[row - 1, column + 1] == CheckerType.P1_check || Board_array[row - 1, column + 1] == CheckerType.P1_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Board_array[row, column] == CheckerType.Free && row - prevRow == -2 && column - prevCol == 2)
                {
                    if (Board_array[row + 1, column - 1] == CheckerType.P1_check || Board_array[row + 1, column - 1] == CheckerType.P1_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Board_array[row, column] == CheckerType.Free && row - prevRow == -2 && column - prevCol == -2)
                {
                    if (Board_array[row + 1, column + 1] == CheckerType.P1_check || Board_array[row + 1, column + 1] == CheckerType.P1_king)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        private void End_turn()
        {
            Is_kinged(); // проверка является ли последний ход фишкой короля
            players_second_click = !players_second_click;
            player_one_turn = !player_one_turn;
            // выводим текст чей сейчас ход
            if (player_one_turn)
            {
                current_player.Text = "Ход первого игрока";
            }
            else
            {
                current_player.Text = "Ход второго игрока";
            }
        }
        private void invalid_choice()
        {
            players_second_click = false;
            borderChangeBack(prevButton);
        }
        private bool game_over()
        {
            if (p1_check_count == 0 || p2_check_count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         События нажатых кнопок и логика игры, описаны ниже  
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (game_over())
            {
                if (p1_check_count > 0) 
                {
                    MessageBoxResult result = MessageBox.Show("Игра окончена!", "Победил первый игрок!");
                }
                else 
                {
                    MessageBoxResult result = MessageBox.Show("Игра окончена!", "Победил второй игрок!");
                }
                Window1 window = new Window1();
                this.Visibility = Visibility.Collapsed; // скрывает текущее окно
                window.Show(); // показывает главный экран
                this.Close();
            }
            // определение нажатой кнопки
            var button = (Button)sender;
            column = Grid.GetColumn(button);
            row = Grid.GetRow(button);
            if (player_one_turn)
            {
                if (players_second_click)
                {
                    prevRow = Grid.GetRow(prevButton);
                    prevCol = Grid.GetColumn(prevButton);
                    if (Board_array[prevRow, prevCol] == CheckerType.P1_check)
                    {
                        if (Board_array[row, column] == CheckerType.Free && (row - prevRow == -1) && (column - prevCol == -1 || column - prevCol == 1))
                        {
                            if (!Is_kinged())
                            {
                                Board_array[row, column] = CheckerType.P1_check;
                                Board_array[prevRow, prevCol] = CheckerType.Free;
                                button.Content = "•";
                                button.Foreground = p1_color;
                                borderChangeBack(prevButton);
                                prevButton.Content = "";
                            }
                            End_turn();
                            borderChangeBack(prevButton);
                        }
                        else if (Board_array[row, column] == CheckerType.Free && (row - prevRow == -2) && (column - prevCol == -2))
                        {
                            if (Board_array[row + 1, column + 1] == CheckerType.P2_check || Board_array[row + 1, column + 1] == CheckerType.P2_king)
                            {
                                p2_check_count--;
                                Board_array[row + 1, column + 1] = CheckerType.Free;
                                if (Is_kinged())
                                {
                                    End_turn();
                                    borderChangeBack(prevButton);
                                }
                                else
                                {
                                    Board_array[row, column] = CheckerType.P1_check;
                                    Board_array[prevRow, prevCol] = CheckerType.Free;
                                    borderChangeBack(prevButton);
                                    updateBoardGui();
                                    if (p1_jump_available())
                                    {
                                        prevButton = button;
                                        borderChangeOnCLick(button);
                                    }
                                    else
                                    {
                                        End_turn();
                                        borderChangeBack(prevButton);
                                    }
                                }
                            }
                        }
                        else if (Board_array[row, column] == CheckerType.Free && (row - prevRow == -2) && (column - prevCol == 2))
                        {
                            if (Board_array[row + 1, column - 1] == CheckerType.P2_check || Board_array[row + 1, column - 1] == CheckerType.P2_king)
                            {
                                p2_check_count--;
                                Board_array[row + 1, column - 1] = CheckerType.Free;
                                if (Is_kinged())
                                {
                                    End_turn();
                                    borderChangeBack(prevButton);
                                }
                                else
                                { 
                                    Board_array[row, column] = CheckerType.P1_check;
                                    Board_array[prevRow, prevCol] = CheckerType.Free;
                                    borderChangeBack(prevButton);
                                    updateBoardGui();
                                    if (p1_jump_available())
                                    {
                                        prevButton = button;
                                        borderChangeOnCLick(button);
                                    }
                                    else
                                    {
                                        End_turn();
                                        borderChangeBack(prevButton);
                                    }
                                }
                            }
                        }
                        else
                        {
                            invalid_choice();
                        }
                    }
                    else
                    {
                        if (is_normal_king_move())
                        {
                            button.Content = "♛";
                            button.Foreground = p1_color;
                            prevButton.Content = "";
                            borderChangeBack(prevButton);
                            End_turn();
                        }
                        else if (is_valid_king_jump())
                        {
                            p2_check_count--; 
                            int jumped_row = (int)(row + ((row - prevRow) * -.5));
                            int jumped_col = (int)(column + ((column - prevCol) * -.5));
                            Board_array[row, column] = CheckerType.P1_king;
                            System.Console.WriteLine("Количество пройденых клеток " + (jumped_row) + "  " + (jumped_col));
                            Board_array[jumped_row, jumped_col] = CheckerType.Free;
                            Board_array[prevRow, prevCol] = CheckerType.Free;
                            borderChangeBack(prevButton);
                            updateBoardGui();
                            if (more_king_jump_available())
                            {
                                prevButton = button;
                                borderChangeOnCLick(button);
                            }
                            else
                            {
                                End_turn();
                                borderChangeBack(prevButton);
                            }
                        }
                        else
                        {
                            invalid_choice();
                        }
                    }
                }
                else
                {
                    if (Board_array[row, column] == CheckerType.P1_check || Board_array[row, column] == CheckerType.P1_king)
                    {
                        prevButton = button;
                        borderChangeOnCLick(button);
                        players_second_click = true;
                    }
                }
            }
            else
            {
                if (players_second_click)
                {
                    prevRow = Grid.GetRow(prevButton);
                    prevCol = Grid.GetColumn(prevButton);
                    if (Board_array[prevRow, prevCol] == CheckerType.P2_check)
                    {
                        if (Board_array[row, column] == CheckerType.Free && (row - prevRow == 1) && (column - prevCol == -1 || column - prevCol == 1))
                        {
                            if (!Is_kinged())
                            {
                                Board_array[row, column] = CheckerType.P2_check;
                                Board_array[prevRow, prevCol] = CheckerType.Free;
                                button.Content = "•";
                                button.Foreground = p2_color;
                                borderChangeBack(prevButton);
                                prevButton.Content = "";
                            }
                            End_turn();
                            borderChangeBack(prevButton);
                        }
                        else if (Board_array[row, column] == CheckerType.Free && (row - prevRow == 2) && column - prevCol == -2)
                        {
                            if (Board_array[row - 1, column + 1] == CheckerType.P1_check || Board_array[row - 1, column + 1] == CheckerType.P1_king)
                            {
                                p1_check_count--; 
                                Board_array[row - 1, column + 1] = CheckerType.Free;
                                if (Is_kinged())
                                {
                                    End_turn();
                                    borderChangeBack(prevButton);
                                }
                                else
                                {
                                    Board_array[row, column] = CheckerType.P2_check;
                                    Board_array[prevRow, prevCol] = CheckerType.Free;
                                    borderChangeBack(prevButton);
                                    updateBoardGui();
                                    if (p2_jump_available())
                                    {
                                        borderChangeOnCLick(button);
                                        prevButton = button;
                                    }
                                    else
                                    {
                                        End_turn();
                                        borderChangeBack(prevButton);
                                    }
                                }
                            }
                        }
                        else if (Board_array[row, column] == CheckerType.Free && (row - prevRow == 2) && column - prevCol == 2)
                        {

                            p1_check_count--; 
                            Board_array[row - 1, column - 1] = CheckerType.Free;
                            if (Is_kinged())
                            {
                                End_turn();
                                borderChangeBack(prevButton);
                            }
                            else
                            {
                                Board_array[row, column] = CheckerType.P2_check;
                                Board_array[prevRow, prevCol] = CheckerType.Free;
                                borderChangeBack(prevButton);
                                updateBoardGui();
                                if (p2_jump_available())
                                {
                                    borderChangeOnCLick(button);
                                    prevButton = button;
                                }
                                else
                                {
                                    End_turn();
                                    borderChangeBack(prevButton);
                                }
                            }

                        }
                        else
                        {
                            invalid_choice();
                        }
                    }
                    else
                    {
                        if (is_normal_king_move())
                        {
                            button.Content = "♚";
                            button.Foreground = p2_color;
                            prevButton.Content = "";
                            borderChangeBack(prevButton);
                            End_turn();
                        }
                        else if (is_valid_king_jump())
                        {
                            p1_check_count--;
                            int jumped_row = (int)(row + ((row - prevRow) * -.5));
                            int jumped_col = (int)(column + ((column - prevCol) * -.5));
                            Board_array[row, column] = CheckerType.P2_king;
                            System.Console.WriteLine("Количество пройденных клеток" + (row + jumped_row) + "  " + (column + jumped_col));
                            Board_array[jumped_row, jumped_col] = CheckerType.Free;
                            Board_array[prevRow, prevCol] = CheckerType.Free;
                            borderChangeBack(prevButton);
                            updateBoardGui();
                            if (more_king_jump_available())
                            {
                                prevButton = button;
                                borderChangeOnCLick(button);
                            }
                            else
                            {
                                End_turn();
                                borderChangeBack(prevButton);
                            }
                        }
                        else
                        {
                            invalid_choice();
                        }
                    }
                }
                else
                {
                    current_player.Text = "Ход второго игрока";
                    if (Board_array[row, column] == CheckerType.P2_check || Board_array[row, column] == CheckerType.P2_king)
                    {
                        prevButton = button;  
                        players_second_click = true;
                        borderChangeOnCLick(button);
                    }
                }
            }
        } 
    }
}
