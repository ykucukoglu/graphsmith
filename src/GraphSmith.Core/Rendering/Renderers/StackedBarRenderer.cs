using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Interfaces;

namespace GraphSmith.Core.Rendering.Renderers
{
    public class StackedBarRenderer : IBarRenderer
    {
        public void Render(IRenderContext context, BarChartModel model)
        {
            double padding = 50;
            double chartWidth = model.Width - padding * 2;
            double chartHeight = model.Height - padding * 2;
            double baseX = padding;
            double baseY = padding + chartHeight;

            int categoryCount = model.Series[0].Items.Count;
            double maxValue = model.Series.Sum(s => s.Items.Max(i => i.Value));
            maxValue = Math.Ceiling(maxValue / 10) * 10;

            double xStep = chartWidth / categoryCount;
            double yStep = chartHeight / 5;

            if (model.ShowXAxis) context.DrawLine(baseX, baseY, baseX + chartWidth, baseY, "#000000", 2);
            if (model.ShowYAxis) context.DrawLine(baseX, padding, baseX, baseY, "#000000", 2);

            if (model.ShowGridLines)
            {
                for (int i = 0; i <= 5; i++)
                {
                    double yPos = baseY - i * yStep;
                    context.DrawLine(baseX, yPos, baseX + chartWidth, yPos, "#cccccc", 1, model.GridLineOpacity);
                }
                for (int i = 0; i <= categoryCount; i++)
                {
                    double xPos = baseX + i * xStep;
                    context.DrawLine(xPos, padding, xPos, baseY, "#cccccc", 1, model.GridLineOpacity);
                }
            }

            double barStartX = baseX + (xStep - model.BarWidth) / 2;
            for (int c = 0; c < categoryCount; c++)
            {
                double yOffset = 0;
                foreach (var series in model.Series)
                {
                    var item = series.Items[c];
                    double barHeight = item.Value / maxValue * chartHeight;
                    context.DrawRectangle(barStartX + c * xStep, baseY - yOffset - barHeight, model.BarWidth, barHeight, series.Color);
                    yOffset += barHeight;
                }
            }

            if (model.ShowXLabels)
            {
                for (int c = 0; c < categoryCount; c++)
                {
                    double labelX = barStartX + c * xStep + model.BarWidth / 2 - 10;
                    context.DrawText(labelX, baseY + 5, model.Series[0].Items[c].Label, "#000000", 12);
                }
            }

            if (model.ShowYLabels)
            {
                for (int i = 0; i <= 5; i++)
                {
                    double yPos = baseY - i * yStep;
                    double value = maxValue / 5 * i;
                    context.DrawText(baseX - 35, yPos - 5, value.ToString("0"), "#000000", 12);
                }
            }
        }
    }
}
