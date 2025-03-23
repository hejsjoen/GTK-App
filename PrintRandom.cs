using Cairo;
using Timeout = GLib.Timeout;

namespace GTK_app;
public class PrintRandom(WidgetGrid grid) : Game
{
    public void Run()
    {
        var size = grid.Size();
        var cells = new List<(int col, int row)>();
        for (var col = 0; col < size.col; col++)
        {
            for (var row = 0; row < size.row; row++)
            {
                cells.Add((col, row));
            }
        }
        Timeout.Add(1000, delegate
        {
            if (cells.Count > 0)
            {
                var index = _random.Next(cells.Count);
                var (col, row) = cells[index];
                cells.RemoveAt(index);

                grid.DrawCircle(col, row, _color);
            }
            else
            {
                grid.ClearGrid();
                cells.AddRange(GetCellList(size.row, size.col));
            }

            return true;
        });
    }
    
    private readonly Random _random = new();
    private readonly Color _color = new(1, 0, 0);
}