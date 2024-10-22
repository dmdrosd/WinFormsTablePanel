using WinFormsTablePanel;
using WinFormsTablePanel.Parts;

public class TablePanelRow : TablePanelEntity
{
    public float Height { get; set; }
    public List<TablePanelCell> Cells { get; set; }
    public Color BackColor { get; set; }

    public TablePanelRow(TablePanelEntityStyle style, float height, bool visible, string name)
        : base(name, style, visible)
    {
        Height = height;
        Cells = new List<TablePanelCell>();
    }
}