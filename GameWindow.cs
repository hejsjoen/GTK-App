using Gtk;
using Color = Cairo.Color;
using Timeout = GLib.Timeout;
using Window = Gtk.Window;
namespace GTK_app;

public class GameWindow : Window
{
    public GridWidget Grid { get; private set; }

    public GameWindow(int rows, int columns, string title) : base(title)
    {
        SetDefaultSize(columns * 100, rows * 100);
        DeleteEvent += delegate { Application.Quit(); };

        var backgroundColor = new Color(0.9, 0.9, 0.9);
        var lineColor = new Color(0, 0, 0);

        Grid = new GridWidget(rows, columns, 100, backgroundColor, lineColor);
        Add(Grid);
        ShowAll();

        CanFocus = true; // Added this line

        FocusInEvent += (o, args) => {
            Timeout.Add(1, () => {
                Focus = Grid;
                return false;
            });
        };
    }
}