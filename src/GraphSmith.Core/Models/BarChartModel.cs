
namespace GraphSmith.Core.Models
{
    public enum BarOrientation { Vertical, Horizontal }

    public enum BarType { Simple, Grouped, Stacked, Stacked100, Lollipop, Gantt }

    public record BarItem(string Label, double Value);

    public record BarSeries(string Name, List<BarItem> Items, string Color = "#3498db");

    public record BarChartModel(
        List<BarSeries> Series,
        BarOrientation Orientation = BarOrientation.Vertical,
        BarType Type = BarType.Simple,
        double BarWidth = 40,
        bool ShowTooltips = true,
        bool Animate = true,
        bool ShowXAxis = true,
        bool ShowYAxis = true,
        bool ShowXLabels = true,
        bool ShowYLabels = true,
        bool ShowGridLines = true,
        double GridLineOpacity = 0.3,
        double Width = 600,   // Canvas genişliği
        double Height = 400   // Canvas yüksekliği
    );

}
