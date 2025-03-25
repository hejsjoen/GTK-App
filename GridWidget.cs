using Gdk;
using Gtk;
using Color = Cairo.Color;

namespace GTK_app;

public class GridWidget : DrawingArea
{
    private readonly int _rows;
    private readonly int _columns;
    private readonly int _cellSize;
    private readonly Color _backgroundColor;
    private readonly Color _lineColor;
    private readonly Dictionary<(int row, int col), Color> _circles;

    public GridWidget(int rows, int columns, int cellSize, Color backgroundColor, Color lineColor)
    {
        _rows = rows;
        _columns = columns;
        _cellSize = cellSize;
        _backgroundColor = backgroundColor;
        _lineColor = lineColor;
        _circles = new Dictionary<(int row, int col), Color>();
        SetSizeRequest(columns * cellSize, rows * cellSize);
        CanFocus = true;
        //Events |= EventMask.KeyPressMask;

        Drawn += OnDrawn;
    }

    public void DrawCircle(int row, int col, Color color)
    {
        _circles[(row, col)] = color;
        QueueDraw();
    }

    public void ClearGrid()
    {
        _circles.Clear();
        QueueDraw();
    }

    private void OnDrawn(object sender, DrawnArgs args)
    {
        var cr = args.Cr;

        // Fill background
        cr.SetSourceColor(_backgroundColor);
        cr.Rectangle(0, 0, _columns * _cellSize, _rows * _cellSize);
        cr.Fill();

        // Draw grid lines
        cr.SetSourceColor(_lineColor);
        cr.LineWidth = 1;

        for (var i = 0; i <= _columns; i++)
        {
            cr.MoveTo(i * _cellSize, 0);
            cr.LineTo(i * _cellSize, _rows * _cellSize);
        }

        for (var j = 0; j <= _rows; j++)
        {
            cr.MoveTo(0, j * _cellSize);
            cr.LineTo(_columns * _cellSize, j * _cellSize);
        }

        cr.Stroke();

        // Draw circles
        foreach (var circle in _circles)
        {
            var row = circle.Key.row;
            var col = circle.Key.col;
            var color = circle.Value;

            var centerX = col * _cellSize + _cellSize / 2.0;
            var centerY = row * _cellSize + _cellSize / 2.0;
            var radius = _cellSize / 3.0;

            cr.SetSourceColor(color);
            cr.Arc(centerX, centerY, radius, 0, 2 * Math.PI);
            cr.Fill();
        }
    }
}