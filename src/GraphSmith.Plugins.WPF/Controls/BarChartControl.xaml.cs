using System.Windows.Controls;
using GraphSmith.Core.Models;
using GraphSmith.Core.Rendering.Factories;

namespace GraphSmith.Plugins.WPF.Controls
{
    public partial class BarChartControl : UserControl
    {
        public BarChartModel Model { get; set; }

        private readonly WpfRenderContext _context;

        public BarChartControl()
        {
            _context = new WpfRenderContext(this); // UserControl üzerine çizim
        }

        public void Render()
        {
            if (Model == null) return;

            var renderer = BarRendererFactory.CreateRenderer(Model);
            renderer.Render(_context, Model);
        }
    }
}
