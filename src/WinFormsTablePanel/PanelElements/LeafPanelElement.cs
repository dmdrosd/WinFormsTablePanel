using WinFormsTablePanel.PanelElements;

public class LeafPanelElement : PanelElement
{
    private readonly Control _control;

    public LeafPanelElement(Control control)
    {
        _control = control;
    }

    public override IEnumerable<Control> GetAllControls()
    {
        return new List<Control> { _control };
    }
}