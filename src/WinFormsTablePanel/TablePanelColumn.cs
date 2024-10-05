namespace WinFormsTablePanel;

public class TablePanelColumn : TablePanelEntity
{
    public TablePanelColumn(TablePanelEntityStyle style, float width, bool visible)
        : base(style, visible)
    {
        this.Width = width;
    }

    public float Width { get; set; }
}