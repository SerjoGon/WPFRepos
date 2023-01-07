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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace WPF2048Game
{
    public partial class MainWindow : Window
    {
        
        Block[,] blks = new Block[4, 4];
        Block[,] OldBlks = new Block[4, 4];
        int score, PrevScore;
        Button[,] Btns = new Button[4, 4];
        DoubleAnimation DAnimation;
        Storyboard SBoard;

        public MainWindow()
        {
            InitializeComponent();
            DAnimation = new DoubleAnimation();
            DAnimation.From = 80;
            DAnimation.To = 40;
            DAnimation.Duration = TimeSpan.FromMilliseconds(250);
            SBoard = new Storyboard();
            SBoard.Children.Add(DAnimation);
            NewGame();
        }
        private void NewGame()
        {
            //Статистика по умолчанию
            score = 0;
            //Создание и добавление нового блока на игровое поле после каждого хода
            Block.InitNewGameBlocks(ref blks);
            Block.InitBlocks(ref OldBlks);
            Block.CoppyBlock(ref OldBlks, ref blks);
            //Вывод статистики
            Score.Text = score.ToString();
            DrawNewBlock();
        }
        private void BackGame()
        {
            // изменение статистики м игрового поля при нажатии клавиши Шаг назад
            score = PrevScore;
            Block.CoppyBlock(ref blks, ref OldBlks);
            Score.Text = score.ToString();
            DrawNewBlock();
        }
        /*private void LoadGame(Block[,] NewBlocks, int NewScore)
        {
        //Еще не реализованная функция загрузки прошлой игры
            score = NewScore;
            Block.CoppyBlock(ref blks, ref NewBlocks);
            Score.Text = NewScore.ToString();
            DrawNewBlock();
        }*/
        private bool GameOver(Block[,] blks)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blks[row, col].num == 0)
                        return false;
                }
            }
            
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (blks[row, col].num == blks[row, col + 1].num)
                        return false;
                }
            }
            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (blks[row + 1, col].num == blks[row, col].num)
                        return false;
                }
            }
            return true;
        }
        private void GridClear()
        {
            for (int i = 0; i < mgrid.Children.Count; i++)
            {
                if ((Grid.GetColumn(mgrid.Children[i]) != 4))
                {
                    mgrid.Children.Remove(mgrid.Children[i]);
                }
            }
        }
        private void DrawNewBlock()
        {
            GridClear();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    //Настройка блоков
                    Btns[row, col] = new Button();
                    Btns[row, col].BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
                    Btns[row, col].BorderThickness = new Thickness(3);
                    Btns[row, col].FontStretch = new FontStretch();
                    if (blks[row, col].num != 0) 
                    {
                        Btns[row, col].Background = Block.GetTitleBlocksColor(blks[row, col]);
                        Btns[row, col].Content = blks[row, col].num.ToString();
                        Btns[row, col].FontSize = 40;

                        //Анимация слияния блоков
                        if (blks[row, col].Combined == true || blks[row, col].NewBlock == true)
                        {
                            Storyboard.SetTarget(DAnimation, Btns[row, col]);
                            Storyboard.SetTargetProperty(DAnimation, new PropertyPath(Button.FontSizeProperty));
                            SBoard.Begin(Btns[row, col]);
                        }
                    }
                    else
                    {
                        Btns[row, col].Background = Block.GetBlocksNoneTitleColor();
                    }
                    Grid.SetColumn(Btns[row, col], col);
                    Grid.SetRow(Btns[row, col], row);
                    mgrid.Children.Add(Btns[row, col]);
                }
            }
        }

        private void MoveUp()
        {
            PrevScore = score;
            if (Block.TryUp(ref blks, ref OldBlks, ref score) == true)  
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }
        private void MoveDown()
        {
            PrevScore = score;
            if (Block.TryDown(ref blks, ref OldBlks, ref score) == true)  
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }
        private void MoveLeft()
        {
            PrevScore = score;
            if (Block.TryLeft(ref blks, ref OldBlks, ref score) == true)
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }
        private void MoveRight()
        {
            PrevScore = score;
            if (Block.TryRight(ref blks, ref OldBlks, ref score) == true) 
            {
                Block.GenerateABlock(ref blks);
                DrawNewBlock();
                Score.Text = score.ToString();
            }
        }

        private void Mgrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    MoveUp();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
                default:
                    break;
            }
            if (GameOver(blks))
            {
                MessageBox.Show("Игра окончена! Результат: " + score.ToString(), "Уведомление!");
            }
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BackGame();
        }
    }
}