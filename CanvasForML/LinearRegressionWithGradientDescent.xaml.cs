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

namespace CanvasForML
{
    /// <summary>
    /// Logique d'interaction pour LinearRegressionWithGradientDescent.xaml
    /// </summary>
    public partial class LinearRegressionWithGradientDescent : Window
    {
        private List<Point> points;
        private bool canLineBeDrawed = false;
        private double m = 1;
        private double b = 0;
        private static bool closed = false;

        public LinearRegressionWithGradientDescent()
        {
            InitializeComponent();
            points = new List<Point>();
            Task.Run((Action) Draw);
        }

        private void Draw()
        {
            while (!closed)
            {
                if (canLineBeDrawed)
                {
                    Dispatcher.Invoke(new Action(() => {
                        GradientDescent();
                    }));
                }
                Thread.Sleep(30);
            }
        }

        private void Cnv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(cnv);

            points.Add(new Point(Map(p.X, 0, 500, 0, 1), Map(p.Y, 0, 500, 1, 0)));

            var ellipse = new Ellipse() { Width = 10, Height = 10, Stroke = new SolidColorBrush(Colors.Black), Fill = new SolidColorBrush(Colors.Black) };
            Canvas.SetLeft(ellipse, p.X - 5);
            Canvas.SetTop(ellipse, p.Y - 5);
            cnv.Children.Add(ellipse);

            if(points.Count() >= 2)
            {
                canLineBeDrawed = true;
            }
        }

        private void DrawLine()
        {
            foreach(var child in cnv.Children)
            {
                if(child is Line)
                {
                    cnv.Children.Remove((Line)child);
                    break;
                }
            }

            var line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;

            var x1 = 0;
            var y1 = m * x1 + b;
            var x2 = 1;
            var y2 = m * x2 + b;

            line.X1 = Map(x1, 0, 1, 0, 500);
            line.Y1 = Map(y1, 0, 1, 500, 0);
            line.X2 = Map(x2, 0, 1, 0, 500);
            line.Y2 = Map(y2, 0, 1, 500, 0);

            cnv.Children.Add(line);
        }

        private void GradientDescent()
        {
            var learningRate = 0.05;

            for(int i = 0; i < points.Count(); i++)
            {
                var x = points[i].X;
                var y = points[i].Y;

                var guess = m * x + b;

                var error = y - guess;

                m += (error * x) * learningRate;
                b += error * learningRate;

                DrawLine();
            }
        }

        private double Map(double unknown, double start1, double stop1, double start2, double stop2)
        {
            return start2 + (stop2 - start2) * ((unknown - start1) / (stop1 - start1));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            closed = true;
        }
    }
}
