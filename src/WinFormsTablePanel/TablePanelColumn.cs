namespace WinFormsTablePanel;

public class TablePanelColumn
{
    public TablePanelEntityStyle Style { get; set; }
    public float Width { get; set; }
    public bool Visible { get; set; }

    public TablePanelColumn(TablePanelEntityStyle style, float width, bool visible)
    {
        Style = style;
        Width = width;
        Visible = visible;
    }
}