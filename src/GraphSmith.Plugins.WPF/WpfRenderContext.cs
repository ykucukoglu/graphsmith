using GraphSmith.Core.Rendering.Interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphSmith.Plugins.WPF
{
    public class WpfRenderContext : IRenderContext
    {
        private readonly Canvas _canvas;

        public WpfRenderContext(UserControl ctrl)
        {
            _canvas = new Canvas();
            ctrl.Content = _canvas;
        }

        public void Clear(string color)
        {
            _canvas.Children.Clear();
            _canvas.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        }

        public void DrawRectangle(double x, double y, double width, double height, string fillColor, string borderColor = "#000000", double borderWidth = 1)
        {
            var rect = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fillColor)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(borderColor)),
                StrokeThickness = borderWidth
            };
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            _canvas.Children.Add(rect);
        }

        public void DrawCircle(double x, double y, double radius, string fillColor, string borderColor = "#000000", double borderWidth = 1)
        {
            var ellipse = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fillColor)),
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(borderColor)),
                StrokeThickness = borderWidth
            };
            Canvas.SetLeft(ellipse, x - radius);
            Canvas.SetTop(ellipse, y - radius);
            _canvas.Children.Add(ellipse);
        }

        public void DrawText(double x, double y, string text, string color = "#000000", double fontSize = 12)
        {
            var tb = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)),
                FontSize = fontSize
            };
            Canvas.SetLeft(tb, x);
            Canvas.SetTop(tb, y);
            _canvas.Children.Add(tb);
        }

        public void DrawLine(double x1, double y1, double x2, double y2, string color = "#000000", double thickness = 1, double opacity = 1)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)),
                StrokeThickness = thickness,
                Opacity = opacity
            };
            _canvas.Children.Add(line);
        }

        public void DrawGrid(double startX, double startY, double endX, double endY, int divisions, string color = "#cccccc", double thickness = 0.5, double opacity = 0.3)
        {
            double stepX = (endX - startX) / divisions;
            double stepY = (endY - startY) / divisions;

            for (int i = 0; i <= divisions; i++)
            {
                // Dikey çizgi
                DrawLine(startX + i * stepX, startY, startX + i * stepX, endY, color, thickness, opacity);
                // Yatay çizgi
                DrawLine(startX, startY + i * stepY, endX, startY + i * stepY, color, thickness, opacity);
            }
        }
    }
}
