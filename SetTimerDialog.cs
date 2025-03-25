using Gtk;

namespace GTK_app;

public class SetTimerDialog 
    : Dialog
{
    private readonly SpinButton _spinButton = new SpinButton(10, 3600, 10);

    public SetTimerDialog() : base("Set Timer", null,
        DialogFlags.Modal | DialogFlags.DestroyWithParent, "OK", ResponseType.Apply)
    {
        _spinButton.Value = 60;
        ContentArea.PackStart(_spinButton, true, true, 5);
        ContentArea.SetSizeRequest(250, 100);
        var grid = new Grid();
        var label = new Label("Set Timer in seconds");
        label.Halign = Align.End;
        grid.Attach(label, 0, 1, 1, 1);
        grid.Attach(_spinButton, 1, 0, 1, 1);
        grid.ColumnSpacing = 10;
        grid.RowSpacing = 5;
        grid.Margin = 5;
        ContentArea.Add(grid);
        ShowAll();
    }
    public uint GetTimer() => (uint)_spinButton.Value * 1000;
}