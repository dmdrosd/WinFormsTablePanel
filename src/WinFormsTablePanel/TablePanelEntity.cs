namespace WinFormsTablePanel;

public abstract class TablePanelEntity
{
    protected TablePanelEntity(TablePanelEntityStyle style, bool visible)
    {
        this.Style = style;
        this.Visible = visible;
    }

    public TablePanelEntityStyle Style { get; set; }
    public bool Visible { get; set; }
}