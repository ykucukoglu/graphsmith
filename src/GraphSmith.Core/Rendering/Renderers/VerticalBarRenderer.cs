using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSmith.Core.Rendering.Renderers
{
    public class VerticalBarRenderer : IBarRenderer
    {
        public void Render(IRenderContext context, BarChartModel model)
        {
            double padding = 50; // Eksen ve label için boşluk
            double chartWidth = model.Width - padding * 2;
            double chartHeight = model.Height - padding * 2;
            double baseY = padding + chartHeight;
            double baseX = padding;

            int categoryCount = model.Series[0].Items.Count;
            int seriesCount = model.Series.Count;

            // Maksimum değer ve bar yüksekliği
            double maxValue = model.Series.Max(s => s.Items.Max(i => i.Value));
            maxValue = Math.Ceiling(maxValue / 10) * 10;

            // Kare şeklinde grid için
            int divisions = 5; // hem x hem y için grid bölme sayısı
            double yStep = chartHeight / divisions;
            double xStep = chartWidth / categoryCount;

            // Eksenler
            if (model.ShowXAxis) context.DrawLine(baseX, baseY, model.Width - padding, baseY, "#000000", 2);
            if (model.ShowYAxis) context.DrawLine(baseX, padding, baseX, baseY, "#000000", 2);

            // Grid çizgileri
            if (model.ShowGridLines)
            {
                // Yatay
                for (int i = 0; i <= divisions; i++)
                {
                    double y = baseY - yStep * i;
                    context.DrawLine(baseX, y, baseX + chartWidth, y, "#cccccc", 1, model.GridLineOpacity);
                }

                // Dikey
                for (int i = 0; i <= categoryCount; i++)
                {
                    double x = baseX + xStep * i;
                    context.DrawLine(x, baseY, x, padding, "#cccccc", 1, model.GridLineOpacity);
                }
            }

            // Bar çizimi
            double barX = baseX + (xStep - model.BarWidth * seriesCount) / 2;
            for (int c = 0; c < categoryCount; c++)
            {
                double innerX = barX + c * xStep;
                foreach (var series in model.Series)
                {
                    var item = series.Items[c];
                    double barHeight = item.Value / maxValue * chartHeight;
                    context.DrawRectangle(innerX, baseY - barHeight, model.BarWidth, barHeight, series.Color);
                    innerX += model.BarWidth;
                }
            }

            // X label
            if (model.ShowXLabels)
            {
                for (int c = 0; c < categoryCount; c++)
                {
                    double labelX = baseX + xStep * c + xStep / 2 - 10;
                    context.DrawText(labelX, baseY + 5, model.Series[0].Items[c].Label, "#000000", 12);
                }
            }

            // Y label
            if (model.ShowYLabels)
            {
                for (int i = 0; i <= divisions; i++)
                {
                    double y = baseY - yStep * i;
                    double value = maxValue / divisions * i;
                    context.DrawText(baseX - 35, y - 5, value.ToString("0"), "#000000", 12);
                }
            }
        }
    }
}
