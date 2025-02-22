using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        private const int CellSize = 20;
        private const int Width = 20;
        private const int Height = 20;

        private List<Point> snake;
        private Point food;
        private DispatcherTimer timer;
        private Vector direction;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            snake = new List<Point> { new Point(Width / 2, Height / 2) };
            direction = new Vector(1, 0); // Начальное направление вправо
            GenerateFood();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            Draw();
        }

        private void MoveSnake()
        {
            var head = snake.First();
            var newHead = new Point(head.X + direction.X, head.Y + direction.Y);
            snake.Insert(0, newHead);

            // Проверка на поедание еды
            if (newHead == food)
            {
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void GenerateFood()
        {
            Random random = new Random();
            food = new Point(random.Next(0, Width) * CellSize, random.Next(0, Height) * CellSize);
        }

        private void CheckCollision()
        {
            var head = snake.First();

            // Проверка на столкновение со стенами
            if (head.X < 0 || head.X >= Width * CellSize || head.Y < 0 || head.Y >= Height * CellSize)
            {
                GameOver();
            }

            // Проверка на столкновение с собой
            if (snake.Skip(1).Contains(head))
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Игра окончена! Нажмите ОК для перезапуска.");
            InitializeGame();
        }

        private void Draw()
        {
            GameCanvas.Children.Clear();
            foreach (var segment in snake)
            {
                var rect = new Rectangle
                {
                    Width = CellSize,
                    Height = CellSize,
                    Fill = Brushes.Green
                };
                Canvas.SetLeft(rect, segment.X * CellSize);
                Canvas.SetTop(rect, segment.Y * CellSize);
                GameCanvas.Children.Add(rect);
            }

            var foodRect = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(foodRect, food.X);
            Canvas.SetTop(foodRect, food.Y);
            GameCanvas.Children.Add(foodRect);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (direction.Y != 1) direction = new Vector(0, -1);
                    break;
                case Key.Down:
                    if (direction.Y != -1) direction = new Vector(0, 1);
                    break;
                case Key.Left:
                    if (direction.X != 1) direction = new Vector(-1, 0);
                    break;
                case Key.Right:
                    if (direction.X != -1) direction = new Vector(1, 0);
                    break;
            }
        }
    }
}
