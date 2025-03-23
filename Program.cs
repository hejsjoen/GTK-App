using Gtk;
using Application = Gtk.Application;
using Cairo;
using GLib;

namespace GTK_app;

internal static class Program
{
    public static void Main()
    {
        Application.Init();
        new StartWindow();
        /*var grid = new WidgetGrid();
        var game = new PrintRandom(grid);*/
        /*var start = new StartWindow();
        var window = new Window("Welcome");
        window.DeleteEvent += delegate { Application.Quit(); };
        window.SetDefaultSize(300, 100);
        window.Resizable = false;
        window.Add(start);
        window.ShowAll();*/
        // game.Run();
        Application.Run();
    }
}