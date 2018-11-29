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

namespace CanvasForML
{
    /// <summary>
    /// Logique d'interaction pour LinearRegression.xaml
    /// </summary>
    public partial class LinearRegression : Window
    {
        private List<Point> points;
        private double m = 1;
        private double b = 0;

        public LinearRegression()
        {
            InitializeComponent();
            points = new List<Point>();
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
                OrdinaryLeastSquares();
                DrawLine();
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

        private void OrdinaryLeastSquares()
        {
            int numbPoints = points.Count();
            double xSum = 0;
            double ySum = 0;

            for(var i = 0; i < numbPoints; i++)
            {
                xSum += points[i].X;
                ySum += points[i].Y;
            }

            double xMoy = xSum / numbPoints; // moyenne des x
            double yMoy = ySum / numbPoints; // moyenne des y

            double num = 0;
            double den = 0;

            for(var i = 0; i < numbPoints; i++)
            {
                var x = points[i].X;
                var y = points[i].Y;

                num += (x - xMoy) * (y - yMoy); // Pour chacun des points on ajoute à num la diff entre x et la moyenne des x * la diff entre y et la moyenne des y
                den += (x - xMoy) * (x - xMoy); // Pour chacun des points on ajoute à den la diff entre x et la somme des x * elle-même
            }

            m = num / den;
            b = yMoy - m * xMoy;
        }

        /*
         * Exemple régression linéaire avec deux points ([0] = {x0,075;y0,075} [1] = {x0,931;y0,939}) originellement placé en bas à gauche et en haut à droite
         * xSum = 1.006 ; ySum = 1.014
         * xMoy = 0.503 (1.006 / 2) ; yMoy = 0.507 (1.014 / 2)
         * num = 0 ; den = 0
         * Pour chacun des deux points :
         * {
         *  x = 0.075 ; y = 0.075
         *  num += (0.075 - 0.503) * (0.075 - 0.507) => (num += 0.184)
         *  den += (0.075 - 0.503) * (0.075 - 0.503) => (den += 0.183)
         * }
         * {
         *  x = 0.931 ; y = 0.939
         *  num += (0.931 - 0.503) * (0.939 - 0.507) => (num += 0.184)
         *  den += (0.931 - 0.503) * (0.931 - 0.503) => (den += 0.183)
         * }
         * num = 0.369 ; den = 0.366
         * m = 0.369 / 0.366 = 1.008
         * b = 0.507 - 1.008 * 0.503 = 0.507 - 0.507 = 0
         * => vu qu'on a presque cliqué sur des points équivalent à une ligne droite de bas gauche jusqu'en haut droit, on se
         * retrouve sensiblement avec les mêmes valeurs qu'au début.
         */

        private double Map(double unknown, double start1, double stop1, double start2, double stop2)
        {
            return start2 + (stop2 - start2) * ((unknown - start1) / (stop1 - start1));
        }
    }
}
