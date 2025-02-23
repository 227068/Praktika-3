using System;
using System.Windows;

namespace WpfApp9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LinearToggle_Checked(object sender, RoutedEventArgs e)
        {
            LinearPanel.Visibility = Visibility.Visible;
            QuadraticPanel.Visibility = Visibility.Collapsed;
        }

        private void LinearToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            LinearPanel.Visibility = Visibility.Collapsed;
        }

        private void QuadraticToggle_Checked(object sender, RoutedEventArgs e)
        {
            QuadraticPanel.Visibility = Visibility.Visible;
            LinearPanel.Visibility = Visibility.Collapsed;
        }

        private void QuadraticToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            QuadraticPanel.Visibility = Visibility.Collapsed;
        }

        private void SolveLinear_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(LinearA.Text);
            double b = double.Parse(LinearB.Text);

            if (a == 0)
            {
                LinearResult.Text = "Уравнение не имеет решения.";
            }
            else
            {
                double x = -b / a;
                LinearResult.Text = $"Решение: x = {x}";
            }
        }

        private void SolveQuadratic_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(QuadraticA.Text);
            double b = double.Parse(QuadraticB.Text);
            double c = double.Parse(QuadraticC.Text);

            if (a == 0)
            {
                // Линейное уравнение, переадресуем к линейному
                SolveLinear_Click(sender, e);
            }
            else
            {
                double discriminant = b * b - 4 * a * c;
                if (discriminant < 0)
                {
                    QuadraticResult.Text = "Уравнение не имеет решений.";
                }
                else if (discriminant == 0)
                {
                    double x = -b / (2 * a);
                    QuadraticResult.Text = $"Решение: x = {x}";
                }
                else
                {
                    double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    QuadraticResult.Text = $"Решения: x1 = {x1}, x2 = {x2}");
                }
            else if (discriminant == 0)
                {
                    double x = -b / (2 * a);
                    Console.WriteLine($"Одно решение: x = {x}");
                }
                else
                {
                    Console.WriteLine("Нет действительных решений.");
                }
            }
        }
    }




