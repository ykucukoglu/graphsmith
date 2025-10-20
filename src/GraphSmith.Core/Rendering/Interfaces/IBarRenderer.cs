using GraphSmith.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSmith.Core.Rendering.Interfaces
{
    public interface IBarRenderer
    {
        void Render(IRenderContext context, BarChartModel model);
    }
}
