using Key = Gdk.Key;
using Gtk;
namespace GTK_app;

public class Client
{
    private GameWindow? _gameWindow;
    private ChooseFour? _chooseFourGame;
    private KeyPressEventHandler? _keyPressHandler;

    private void StartGame()
    {
        var taskSelector = new GameDialog("Select Task", null,
            "Select game to start", "Animation", "Choose Four");

        taskSelector.Response += (_, args) =>
        {
            if (args.ResponseId == ResponseType.Yes)
            {
                _gameWindow = new GameWindow("Animation");
                new Animation(_gameWindow);
                
                AttachKeyPressHandlerLight();
            }
            else if (args.ResponseId == ResponseType.No)
            {
                _gameWindow = new GameWindow("Choose Four");
                _chooseFourGame = new ChooseFour(_gameWindow);

                _chooseFourGame.GameOverEvent += OnGameOver;
                AttachKeyPressHandler();
                
                _chooseFourGame.HandleInitialTurn();
            }
            taskSelector.Destroy();
        };
        taskSelector.Run();
    }

    private void AttachKeyPressHandler()
    {
        if (_gameWindow != null)
        {
            if (_keyPressHandler != null) _gameWindow.KeyPressEvent -= _keyPressHandler;

            _keyPressHandler = (_, args2) =>
            {
                var keyVal = args2.Event.KeyValue;

                if (keyVal is >= (uint)Key.Key_1 and <= (uint)Key.Key_9)
                {
                    _chooseFourGame?.PlayerMove((int)(keyVal - (uint)Key.Key_1));
                }

                if (keyVal == (uint)Key.Escape)
                {
                    Application.Quit();
                }
            };
            _gameWindow.KeyPressEvent += _keyPressHandler;
        }
    }

    private void AttachKeyPressHandlerLight()
    {
        if (_gameWindow != null)
        {
            _gameWindow.KeyPressEvent -= _keyPressHandler;
            
            _keyPressHandler = (_, args2) =>
            {
                var keyVal = args2.Event.KeyValue;
                if (keyVal == (uint)Key.Escape)
                {
                    Application.Quit();
                }
            };
            _gameWindow.KeyPressEvent += _keyPressHandler;
        }
    }
    private void OnGameOver(string message)
    {
        if (_chooseFourGame == null) return;
        _chooseFourGame.GameOverEvent -= OnGameOver;
        _chooseFourGame.ShowResult(message);
        _chooseFourGame = new ChooseFour(_gameWindow);
        _chooseFourGame.GameOverEvent += OnGameOver;
        AttachKeyPressHandler();
        _chooseFourGame.HandleInitialTurn();
    }

    public static void Main()
    {
        Application.Init();
        var client = new Client();
        client.StartGame();
        Application.Run();
    }
}
