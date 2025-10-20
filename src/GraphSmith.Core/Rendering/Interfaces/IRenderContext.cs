namespace GraphSmith.Core.Rendering.Interfaces
{
    public interface IRenderContext
    {
        void Clear(string color);
        void DrawRectangle(double x, double y, double width, double height, string fillColor, string borderColor = "#000000", double borderWidth = 1);
        void DrawCircle(double x, double y, double radius, string fillColor, string borderColor = "#000000", double borderWidth = 1);
        void DrawText(double x, double y, string text, string color = "#000000", double fontSize = 12);
        void DrawLine(double x1, double y1, double x2, double y2, string color = "#000000", double thickness = 1, double opacity = 1);
        void DrawGrid(double startX, double startY, double endX, double endY, int divisions, string color = "#cccccc", double thickness = 0.5, double opacity = 0.3);
    }
}
