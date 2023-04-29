using MathLib;
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
using System.Windows.Shapes;

namespace WpfApp.View
{
    /// <summary>
    /// Логика взаимодействия для GraphView.xaml
    /// </summary>
    public partial class GraphView : Window
    {
        Double _maxGHeight; // Высота окна
        Double _minGHeight;
        Double _maxGWidth; // Ширина окна
        Double _minGWidth;

        public GraphView(double[] X, double[] Y)
        {
            InitializeComponent();

            _minGHeight = Height;
            _maxGHeight = 0;

            _maxGWidth = Width;
            _minGWidth = 0;

            Double minY = MathClass.GetMinElem(Y);
            Double maxY = MathClass.GetMaxElem(Y);

            Double minX = MathClass.GetMinElem(X);
            Double maxX = MathClass.GetMaxElem(X);

            Double coefHeight = (_maxGHeight - _minGHeight) / (maxY - minY);
            Double coefWidth = (_maxGWidth - _minGWidth) / (maxX - minX);

            Double xG = _minGWidth + ((0 - minX) * coefWidth);
            Double yG = _minGHeight + ((0 - minY) * coefHeight);

            Line lineX = new Line()
            {
                X1 = _minGWidth,
                Y1 = yG,
                X2 = _maxGWidth,
                Y2 = yG,
                Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#E0E0E0"),
                Opacity = 0.3,
                StrokeThickness = 2
            };

            Line lineY = new Line()
            {
                X1 = xG,
                Y1 = _minGHeight,
                X2 = xG,
                Y2 = _maxGHeight,
                Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#E0E0E0"),
                Opacity = 0.3,
                StrokeThickness = 2
            };

            Canvas.Children.Add(lineX);
            Canvas.Children.Add(lineY);

            Double[] sortY = MathClass.BubbleSort(Y);

            ShowGraph(X, Y, Colors.Red, Colors.Red);
            ShowGraph(X, sortY, Colors.Blue, Colors.Blue);
        }

        public void ShowGraph(Double[] X, Double[] Y, Color pointColor, Color lineColor)
        {
            Double minY = MathClass.GetMinElem(Y);
            Double maxY = MathClass.GetMaxElem(Y);

            Double minX = MathClass.GetMinElem(X);
            Double maxX = MathClass.GetMaxElem(X);

            Double coefHeight = (_maxGHeight - _minGHeight) / (maxY - minY);
            Double coefWidth = (_maxGWidth - _minGWidth) / (maxX - minX);

            Polyline polyline = new Polyline
            {
                Stroke = new SolidColorBrush(lineColor),
                StrokeThickness = 2,
                StrokeLineJoin = PenLineJoin.Round
            };

            Canvas.Children.Add(polyline);

            for (Int32 i = 0; i < X.Length; i++)
            {
                Point point = new Point
                {
                    X = _minGWidth + ((X[i] - minX) * coefWidth),
                    Y = _minGHeight + ((Y[i] - minY) * coefHeight)
                };

                polyline.Points.Add(point);

                Ellipse pointG1 = CreatePoint(point.X, point.Y, 7, new SolidColorBrush(lineColor));

                ToolTip toolTip = new ToolTip
                {
                    Background = Brushes.Blue,
                    Opacity = 0.7,
                };

                StackPanel stackPanel = new StackPanel();
                stackPanel.Children.Add(new Label
                {
                    Content = $"X[{i}] = {X[i]}",
                    Foreground = Brushes.White
                });
                stackPanel.Children.Add(new Label
                {
                    Content = $"Y[{i}] = {Y[i]}",
                    Foreground = Brushes.White
                });

                toolTip.Content = stackPanel;
                pointG1.ToolTip = toolTip;

                Canvas.Children.Add(pointG1);
            }
        }
        public Ellipse CreatePoint(Double x, Double y, Double size, Brush pointColor)
        {
            Ellipse point = new Ellipse
            {
                Width = size,
                Height = size,
                Fill = pointColor
            };

            Canvas.SetLeft(point, x - size / 2);
            Canvas.SetTop(point, y - size / 2);

            return point;
        }
    }
}
