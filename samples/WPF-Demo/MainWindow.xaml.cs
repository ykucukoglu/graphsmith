using System.Windows;
using GraphSmith.Core.Models;
using GraphSmith.Plugins.WPF.Controls;

namespace WPF_Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RenderAllBarCharts();
        }

        private void RenderAllBarCharts()
        {
            //// Örnek veri
            //var series1 = new BarSeries("Ürün A", new List<BarItem> { new BarItem("Ocak", 10), new BarItem("Şubat", 15), new BarItem("Mart", 20) });
            //var series2 = new BarSeries("Ürün B", new List<BarItem> { new BarItem("Ocak", 20), new BarItem("Şubat", 10), new BarItem("Mart", 30) });
            //var series3 = new BarSeries("Ürün C", new List<BarItem> { new BarItem("Ocak", 15), new BarItem("Şubat", 25), new BarItem("Mart", 10) });

            //var allSeries = new List<BarSeries> { series1, series2, series3 };

            var simpleVerticalSeries = new List<BarSeries>
{
    new BarSeries("Ürün A", new List<BarItem> { new BarItem("Ocak", 30), new BarItem("Şubat", 50), new BarItem("Mart", 20) }),
    new BarSeries("Ürün B", new List<BarItem> { new BarItem("Ocak", 20), new BarItem("Şubat", 40), new BarItem("Mart", 60) })
};

            var simpleHorizontalSeries = new List<BarSeries>
{
    new BarSeries("Ürün X", new List<BarItem> { new BarItem("Q1", 80), new BarItem("Q2", 55), new BarItem("Q3", 70) }),
    new BarSeries("Ürün Y", new List<BarItem> { new BarItem("Q1", 40), new BarItem("Q2", 90), new BarItem("Q3", 50) })
};

            var groupedSeries = new List<BarSeries>
{
    new BarSeries("Ürün A", new List<BarItem> { new BarItem("Ocak", 25), new BarItem("Şubat", 35), new BarItem("Mart", 45) }),
    new BarSeries("Ürün B", new List<BarItem> { new BarItem("Ocak", 15), new BarItem("Şubat", 25), new BarItem("Mart", 55) }),
    new BarSeries("Ürün C", new List<BarItem> { new BarItem("Ocak", 20), new BarItem("Şubat", 40), new BarItem("Mart", 30) })
};

            var stackedSeries = new List<BarSeries>
{
    new BarSeries("Segment A", new List<BarItem> { new BarItem("Ocak", 20), new BarItem("Şubat", 30), new BarItem("Mart", 50) }),
    new BarSeries("Segment B", new List<BarItem> { new BarItem("Ocak", 40), new BarItem("Şubat", 20), new BarItem("Mart", 30) }),
    new BarSeries("Segment C", new List<BarItem> { new BarItem("Ocak", 30), new BarItem("Şubat", 25), new BarItem("Mart", 20) })
};

            var stacked100Series = new List<BarSeries>
{
    new BarSeries("A", new List<BarItem> { new BarItem("Ocak", 40), new BarItem("Şubat", 20), new BarItem("Mart", 50) }),
    new BarSeries("B", new List<BarItem> { new BarItem("Ocak", 30), new BarItem("Şubat", 50), new BarItem("Mart", 25) }),
    new BarSeries("C", new List<BarItem> { new BarItem("Ocak", 30), new BarItem("Şubat", 30), new BarItem("Mart", 25) })
};

            var lollipopSeries = new List<BarSeries>
{
    new BarSeries("Skor", new List<BarItem> { new BarItem("Takım A", 75), new BarItem("Takım B", 50), new BarItem("Takım C", 90), new BarItem("Takım D", 65) })
};

            // Farklı BarChart türlerini oluştur
            var chartTypes = new List<(string Title, BarChartModel Model)>
            {
                ("Simple Vertical", new BarChartModel(simpleVerticalSeries, BarOrientation.Vertical, BarType.Simple)),
                ("Simple Horizontal", new BarChartModel(simpleHorizontalSeries, BarOrientation.Horizontal, BarType.Simple)),
                ("Grouped", new BarChartModel(groupedSeries, BarOrientation.Vertical, BarType.Grouped)),
                ("Stacked", new BarChartModel(stackedSeries, BarOrientation.Vertical, BarType.Stacked)),
                ("100% Stacked", new BarChartModel(stacked100Series, BarOrientation.Vertical, BarType.Stacked100)),
                ("Lollipop", new BarChartModel(lollipopSeries, BarOrientation.Vertical, BarType.Lollipop))
            };

            // Her chart için kontrol oluştur ve render et
            foreach (var (title, model) in chartTypes)
            {
                var textBlock = new System.Windows.Controls.TextBlock
                {
                    Text = title,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 20, 0, 5)
                };
                ChartsPanel.Children.Add(textBlock);

                // Grafik kontrolünü Border içine al ve alt boşluk ekle
                var border = new System.Windows.Controls.Border
                {
                    Margin = new Thickness(0, 0, 0, 80) // Her grafik altına 20 birim boşluk
                };

                var chartControl = new BarChartControl
                {
                    Model = model,
                    Width = 700,
                    Height = 250
                };
                // Border içine ekle
                border.Child = chartControl;

                ChartsPanel.Children.Add(border);
                chartControl.Render();
            }
        }
    }
}
