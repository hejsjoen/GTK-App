using Gtk;
using Key = Gdk.Key;
namespace GTK_app;

public class Client
{
    private GameWindow? _chooseFourWindow;
    private ChooseFour? _chooseFourGame;
    private KeyPressEventHandler? _keyPressHandler;

    private void StartGame()
    {
        var taskSelector = new GameDialog("Select Task", null, "Select game to start", "Animation", "Choose Four");

        taskSelector.Response += (o, args) =>
        {
            if (args.ResponseId == ResponseType.Yes)
            {
                var animWindow = new GameWindow(10, 10, "Animation");
                new Animation(animWindow, 10, 10);
            }
            else if (args.ResponseId == ResponseType.No)
            {
                _chooseFourWindow = new GameWindow(9, 9, "Choose Four");
                _chooseFourGame = new ChooseFour(_chooseFourWindow);

                AttachKeyPressHandler();
            }
            taskSelector.Destroy();
        };

        taskSelector.Run();
    }

    private void AttachKeyPressHandler()
    {
        if (_chooseFourWindow != null)
        {
            _chooseFourWindow.KeyPressEvent -= _keyPressHandler;

            _keyPressHandler = (_, args2) =>
            {
                var keyVal = args2.Event.KeyValue;

                if (keyVal >= (uint)Key.Key_1 && keyVal <= (uint)Key.Key_9)
                {
                    _chooseFourGame?.Play((int)(keyVal - (uint)Key.Key_1));
                }

                if (keyVal == (uint)Key.Escape)
                {
                    Application.Quit();
                }

                if (_chooseFourGame is { GameOver: true })
                {
                    string message;
                    if (_chooseFourGame.CheckWin(1))
                    {
                        message = "Player Wins!";
                    }
                    else if (_chooseFourGame.CheckWin(2))
                    {
                        message = "Computer Wins!";
                    }
                    else
                    {
                        message = "Draw!";
                    }

                    _chooseFourGame.GameOver = false;
                    _chooseFourGame.ShowResult(message);
                    _chooseFourGame = new ChooseFour(_chooseFourWindow); // Reset the game
                    AttachKeyPressHandler();
                }
            };

            _chooseFourWindow.KeyPressEvent += _keyPressHandler;
        }
    }

    public static void Main()
    {
        Application.Init();
        var client = new Client();
        client.StartGame();
        Application.Run();
    }
}
