using WinFormsTablePanel.Parts;
using WinFormsTablePanel;

public class TablePanelRow : TablePanelEntity
{
    public int Height { get; set; }
    public List<TablePanelCell> Cells { get; set; }
    public Color BackColor { get; set; }

    public TablePanelRow(TablePanelEntityStyle style, int height, bool visible, string name)
        : base(style, visible, name)
    {
        Height = height;
        Cells = new List<TablePanelCell>();
    }
}