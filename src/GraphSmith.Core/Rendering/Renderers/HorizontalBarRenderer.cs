using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Interfaces;

namespace GraphSmith.Core.Rendering.Renderers
{
    public class HorizontalBarRenderer : IBarRenderer
    {
        public void Render(IRenderContext context, BarChartModel model)
        {
            double padding = 50;
            double chartWidth = model.Width - padding * 2;
            double chartHeight = model.Height - padding * 2;
            double baseX = padding;
            double baseY = padding + chartHeight;

            int categoryCount = model.Series[0].Items.Count;
            int seriesCount = model.Series.Count;
            double maxValue = model.Series.Max(s => s.Items.Max(i => i.Value));
            maxValue = Math.Ceiling(maxValue / 10) * 10;

            double yStep = chartHeight / categoryCount;
            double xStep = chartWidth / 5;

            if (model.ShowXAxis) context.DrawLine(baseX, baseY, baseX + chartWidth, baseY, "#000000", 2);
            if (model.ShowYAxis) context.DrawLine(baseX, padding, baseX, baseY, "#000000", 2);

            if (model.ShowGridLines)
            {
                for (int i = 0; i <= categoryCount; i++)
                {
                    double yPos = padding + i * yStep;
                    context.DrawLine(baseX, yPos, baseX + chartWidth, yPos, "#cccccc", 1, model.GridLineOpacity);
                }
                for (int i = 0; i <= 5; i++)
                {
                    double xPos = baseX + i * xStep;
                    context.DrawLine(xPos, padding, xPos, baseY, "#cccccc", 1, model.GridLineOpacity);
                }
            }

            double barStartY = padding + yStep / 4;
            foreach (var series in model.Series)
            {
                double innerY = barStartY;
                foreach (var item in series.Items)
                {
                    double barLength = item.Value / maxValue * chartWidth;
                    context.DrawRectangle(baseX, innerY, barLength, model.BarWidth, series.Color);
                    if (model.ShowTooltips) context.DrawText(baseX + barLength + 5, innerY, item.Label);
                    innerY += yStep;
                }
            }

            if (model.ShowXLabels)
            {
                for (int i = 0; i <= 5; i++)
                {
                    double value = maxValue / 5 * i;
                    double xPos = baseX + i * xStep;
                    context.DrawText(xPos - 10, baseY + 5, value.ToString("0"), "#000000", 12);
                }
            }

            if (model.ShowYLabels)
            {
                double labelY = padding + yStep / 4;
                foreach (var item in model.Series[0].Items)
                {
                    context.DrawText(baseX - 40, labelY + model.BarWidth / 2 - 6, item.Label, "#000000", 12);
                    labelY += yStep;
                }
            }
        }
    }
}
