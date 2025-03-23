using Gtk;
using Cairo;
namespace GTK_app;

public class StartWindow : Window
{
    public StartWindow() : base("Welcome")
    {        
        SetDefaultSize(300, 150);
        DeleteEvent += delegate { Application.Quit(); };

        var vbox = new VBox(false, 10);
        vbox.BorderWidth = 10;

        var task12Button = new Button("Run Task 1.2 (Grid with Circles)");
        task12Button.Clicked += OnTask12ButtonClicked;
        vbox.PackStart(task12Button, false, false, 0);

        var task13Button = new Button("Run Task 1.3 (Connect Four)");
        task13Button.Clicked += OnTask13ButtonClicked;
        vbox.PackStart(task13Button, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    private void OnTask12ButtonClicked(object sender, EventArgs args)
    {
        // Code to run Task 1.2 (Grid with Circles)
        // Copy the GridWidgetDemo class from your previous code here.
        // Or create a new instance of the window with the grid widget.
        //Example:
        
        Application.Quit();
        Application.Init();
        var grid = new WidgetGrid();
        var game = new PrintRandom(grid);
        var window = new Window("Welcome");
        window.DeleteEvent += delegate { Application.Quit(); };
        window.SetDefaultSize(360, 360);
        window.Resizable = false;
        window.Add(grid);
        window.ShowAll();
        game.Run();
        Application.Run();
    }

    private void OnTask13ButtonClicked(object sender, EventArgs args)
    {
        // Code to run Task 1.3 (Connect Four)
        // copy the ConnectFourApp class here.
        Application.Quit();
        Application.Init();
        Program.Main();
    }
    
        /*_backgroundColor = new Color(0.9, 0.9, 0.9);
        _foregroundColor = new Color(0, 0, 0);
        Drawn += OnDraw;
    }*/

    /*
    public int Run()
    {
        /*var start = new StartWindow();
        var window = new Window("Welcome");
        window.DeleteEvent += delegate { Application.Quit(); };
        window.SetDefaultSize(300, 100);
        window.Resizable = false;
        Fixed container = new Fixed();
        Label label = new Label("Please choose what to start");
        container.Put(label, 70, 10);
        var button1 = new Button("Game");
        var button2 = new Button("Random");
        
        button1.SetSizeRequest(100, 50);
        
        button2.SetSizeRequest(100, 50);
        container.Put(button1, 20, 50);
        container.Put(button2, 180, 50);
        window.Add(container);
        window.Add(start);
        window.ShowAll();
        if (button1.Clicked += ) return 1;
        button2.Clicked += 
        return 1;#1#
    }
    */
}