using WinFormsTablePanel.Parts;
using WinFormsTablePanel;

public class TablePanelRow : TablePanelEntity
{
    public float Height { get; set; }
    public List<TablePanelCell> Cells { get; set; }

    public TablePanelRow(TablePanelEntityStyle style, float height, bool visible, string name)
        : base(style, visible, name)
    {
        Height = height;
        Cells = new List<TablePanelCell>();
    }
}