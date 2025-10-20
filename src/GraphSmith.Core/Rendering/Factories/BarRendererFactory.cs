using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Interfaces;
using GraphSmith.Core.Rendering.Renderers;


namespace GraphSmith.Core.Rendering.Factories
{
    public static class BarRendererFactory
    {
        public static IBarRenderer CreateRenderer(BarChartModel model)
        {
            return model.Type switch
            {
                BarType.Simple => model.Orientation == BarOrientation.Vertical
                                  ? new VerticalBarRenderer()
                                  : new HorizontalBarRenderer(),
                BarType.Grouped => new GroupedBarRenderer(),
                BarType.Stacked => new StackedBarRenderer(),
                BarType.Stacked100 => new Stacked100BarRenderer(),
                BarType.Lollipop => new LollipopBarRenderer(),
                _ => throw new NotSupportedException("Unknown BarType")
            };
        }

    }
}
