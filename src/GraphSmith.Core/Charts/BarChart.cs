using GraphSmith.Core.Base;
using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Factories;
using GraphSmith.Core.Rendering.Interfaces;

namespace GraphSmith.Core.Charts
{
    public class BarChart : ChartBase
    {
        private readonly BarChartModel _model;
        private readonly IBarRenderer _renderer;

        public BarChart(BarChartModel model) => (_model, _renderer) = (model, BarRendererFactory.CreateRenderer(model));

        public override void Render(IRenderContext context)
        {
            context.Clear("#FFFFFF");
            _renderer.Render(context, _model);
        }
    }
}
