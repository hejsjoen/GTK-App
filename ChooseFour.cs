using System.Diagnostics;
using Gtk;
using Color = Cairo.Color;
using Timeout = GLib.Timeout;
namespace GTK_app;

public class ChooseFour
{
    private readonly GameWindow? _gameWindow;
    private const int Rows = 9;
    private const int Columns = 9;
    private readonly Color _playerColor = new(1, 0, 0);
    private readonly Color _computerColor = new(0, 0, 1);
    private int[,]? _board;
    private int _currentPlayer;
    private readonly Random _random = new();
    public bool GameOver;

    public ChooseFour(GameWindow? gameWindow)
    {
        _gameWindow = gameWindow;
        InitializeGame();
    }

    private void InitializeGame()
    {
        _board = new int[Rows, Columns];
        _currentPlayer = _random.Next(1, 3);
        GameOver = false;
        _gameWindow?.Grid.ClearGrid();
    }

    public void Play(int column)
    {
        if (GameOver) return;

        if (_currentPlayer == 1)
        {
            if (MakeMove(column, 1))
            {
                if (CheckWin(1))
                {
                    GameOver = true;
                }
                else if (CheckDraw())
                {
                    GameOver = true;
                }
                else
                {
                    _currentPlayer = 2;
                    Timeout.Add(500, ComputerMove);
                }
            }
        }
    }

    private bool ComputerMove()
    {
        if (GameOver) return false;
        int column;
        do
        {
            column = _random.Next(Columns);
        } while (!MakeMove(column, 2));

        if (CheckWin(2))
        {
            GameOver = true;
        }
        else if (CheckDraw())
        {
            GameOver = true;
        }
        else
        {
            _currentPlayer = 1;
        }
        return false;
    }

    private bool MakeMove(int column, int player)
    {
        for (var row = Rows - 1; row >= 0; row--)
        {
            Debug.Assert(_board != null, nameof(_board) + " != null");
            if (_board[row, column] == 0)
            {
                _board[row, column] = player;
                _gameWindow?.Grid.DrawCircle(row, column, player == 1 ? _playerColor : _computerColor);
                return true;
            }
        }
        return false; // Column is full
    }

    public bool CheckWin(int player)
    {
        // Horizontal
        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c <= Columns - 4; c++)
            {
                Debug.Assert(_board != null, nameof(_board) + " != null");
                if (_board[r, c] == player && _board[r, c + 1] == player && _board[r, c + 2] == player && _board[r, c + 3] == player)
                    return true;
            }
        }

        // Vertical
        for (var c = 0; c < Columns; c++)
        {
            for (var r = 0; r <= Rows - 4; r++)
            {
                if (_board != null && _board[r, c] == player && _board[r + 1, c] == player && _board[r + 2, c] == player && _board[r + 3, c] == player)
                    return true;
            }
        }

        // Diagonal (down-right)
        for (var r = 0; r <= Rows - 4; r++)
        {
            for (var c = 0; c <= Columns - 4; c++)
            {
                if (_board != null && _board[r, c] == player && _board[r + 1, c + 1] == player && _board[r + 2, c + 2] == player && _board[r + 3, c + 3] == player)
                    return true;
            }
        }

        // Diagonal (down-left)
        for (var r = 0; r <= Rows - 4; r++)
        {
            for (var c = 3; c < Columns; c++)
            {
                if (_board != null && _board[r, c] == player && _board[r + 1, c - 1] == player && _board[r + 2, c - 2] == player && _board[r + 3, c - 3] == player)
                    return true;
            }
        }

        return false;
    }

    private bool CheckDraw()
    {
        for (var r = 0; r < Rows; r++)
        {
            for (var c = 0; c < Columns; c++)
            {
                if (_board != null && _board[r, c] == 0)
                    return false; // Found an empty cell
            }
        }
        return true; // All cells are filled
    }

    private void ResetGame()
    {
        InitializeGame(); // Reinitialize game state
    }

    public void ShowResult(string message)
    {
        var dialog = new GameDialog("Game Over", _gameWindow, message, "Play Again", "Quit");

        var response = (ResponseType)dialog.Run();

        if (response == ResponseType.Yes)
        {
            ResetGame();
        }
        else
        {
            Application.Quit();
        }
        dialog.Destroy();
    }
}