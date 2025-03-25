using Color = Cairo.Color;
using Timeout = GLib.Timeout;

namespace GTK_app;

public class Animation
{
    private readonly GameWindow _gameWindow;
    private readonly int _rows;
    private readonly int _columns;
    private readonly Random _random = new();
    private readonly List<(int row, int col)> _cells;

    public Animation(GameWindow gameWindow, int rows, int columns)
    {
        _gameWindow = gameWindow;
        _rows = rows;
        _columns = columns;
        _cells = new List<(int row, int col)>();
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                _cells.Add((r, c));
            }
        }

        Timeout.Add(1000, AnimationStep);
    }

    private bool AnimationStep()
    {
        if (_cells.Count > 0)
        {
            var index = _random.Next(_cells.Count);
            var (row, col) = _cells[index];
            _cells.RemoveAt(index);

            _gameWindow.Grid.DrawCircle(row, col, new Color(0, 1, 0)); // Green circle
        }
        else
        {
            _gameWindow.Grid.ClearGrid();
            _cells.AddRange(GetCellList(_rows,_columns));
        }

        return true;
    }

    private static List<(int row, int col)> GetCellList(int rows, int columns)
    {
        var cells = new List<(int row, int col)>();
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                cells.Add((r, c));
            }
        }
        return cells;

    }
}