using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Interfaces;

namespace GraphSmith.Core.Rendering.Renderers
{
    public class Stacked100BarRenderer : IBarRenderer
    {
        public void Render(IRenderContext context, BarChartModel model)
        {
            double padding = 50;
            double chartWidth = model.Width - padding * 2;
            double chartHeight = model.Height - padding * 2;
            double baseX = padding;
            double baseY = padding + chartHeight;

            int categoryCount = model.Series[0].Items.Count;
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
                double total = model.Series.Sum(s => s.Items[c].Value);
                foreach (var series in model.Series)
                {
                    var item = series.Items[c];
                    double barHeight = total > 0 ? item.Value / total * chartHeight : 0;
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
                    context.DrawText(baseX - 35, yPos - 5, (100 / 5 * i).ToString("0") + "%", "#000000", 12);
                }
            }
        }
    }
}
