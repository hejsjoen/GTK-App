using Gtk;
using Window = Gtk.Window;
namespace GTK_app;

public class GameDialog : Dialog
{
    public GameDialog(string title, Window? parent, string message, string button1, string button2) : base(title, parent, DialogFlags.Modal | DialogFlags.DestroyWithParent, button1, ResponseType.Yes, button2, ResponseType.No)
    {
        var label = new Label(message);
        ContentArea.PackStart(label, true, true, 0);
        label.Show();
        ShowAll();
    }
}