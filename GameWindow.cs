using Color = Cairo.Color;
using Timeout = GLib.Timeout;
using Window = Gtk.Window;
using Gtk;
namespace GTK_app;

public class GameWindow 
    : Window
{
    public GridWidget Grid { get; private set; }

    public GameWindow(string title) : base(title)
    {
        SetDefaultSize(360, 360);
        DeleteEvent += delegate { Application.Quit(); };

        var backgroundColor = new Color(0.9, 0.9, 0.9);
        var lineColor = new Color(0, 0, 0);

        Grid = new GridWidget(9, 9, 40, backgroundColor, lineColor);
        Add(Grid);
        ShowAll();

        CanFocus = true; 

        FocusInEvent += (_, _) => {
            Timeout.Add(1, () => {
                Focus = Grid;
                return false;
            });
        };
    }
}