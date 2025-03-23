using Cairo;
using Gtk;
namespace GTK_app;

public class WidgetGrid : DrawingArea
{
    public WidgetGrid()
    {
        _rows = 9;
        _columns = 9;
        _size = 40;
        _backgroundColor = new Color(0.9, 0.9, 0.9);
        _foregroundColor = new Color(0, 0, 0);
        _circles = new Dictionary<(int col, int row), Color>();
        Drawn += OnDraw;
    }

    public (int col, int row) Size() => (_columns, _rows);
    public void DrawCircle(int col, int row, Color color)
    {
        _circles[(col, row)] = color;
        QueueDraw();
    }

    public void ClearGrid()
    {
        _circles.Clear();
        QueueDraw();
    } 
    private void OnDraw(object sender, DrawnArgs args)
    {
        var cr = args.Cr;
        
        cr.SetSourceColor(_backgroundColor);
        cr.Rectangle(0,0, _columns * _size, _rows * _size);
        cr.Fill();
        
        cr.SetSourceColor(_foregroundColor);
        cr.LineWidth = 1;

        for (var i = 0; i <= _columns; i++)
        {
            cr.MoveTo(i * _size, 0);
            cr.LineTo(i * _size, _columns * _size);
        }

        for (var i = 0; i <= _rows; i++)
        {
            cr.MoveTo(0, i * _size);
            cr.LineTo(_rows * _size, i * _size);
        }
        
        cr.Stroke();

        foreach (var circle in _circles)
        {
            var col = circle.Key.col;
            var rows = circle.Key.row;
            var color = circle.Value;
            
            var centerX = col * _size + _size / 2;
            var centerY = rows * _size + _size / 2;
            var rad = _size / 3;
            
            cr.SetSourceColor(color);
            cr.Arc(centerX, centerY, rad, 0,   2 * Math.PI);
            cr.Fill();
        }
    }
    
    private readonly int _rows;
    private readonly int _columns;
    private readonly int _size;
    private readonly Color _backgroundColor;
    private readonly Color _foregroundColor;
    private readonly Dictionary<(int col, int row), Color> _circles;
}