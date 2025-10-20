using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Interfaces;

namespace GraphSmith.Core.Rendering.Renderers
{
    public class LollipopBarRenderer : IBarRenderer
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
            double maxValue = model.Series.Max(s => s.Items.Max(i => i.Value));

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

            for (int c = 0; c < categoryCount; c++)
            {
                double xPos = baseX + c * xStep + xStep / 2 - 2;
                double value = model.Series[0].Items[c].Value;
                context.DrawLine(xPos, baseY, xPos, baseY - value, "#000000");
                context.DrawCircle(xPos, baseY - value, 5, model.Series[0].Color);

                if (model.ShowTooltips)
                    context.DrawText(xPos - 10, baseY + 5, model.Series[0].Items[c].Label, "#000000", 12);
            }
        }
    }
}
