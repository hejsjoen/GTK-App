namespace GTK_app;
public class Game
{
    protected static List<(int row, int col)> GetCellList(int rows, int columns)
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