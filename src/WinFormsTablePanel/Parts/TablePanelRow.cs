namespace WinFormsTablePanel.Parts;

public class TablePanelRow(TablePanelEntityStyle style, string name, float height, bool visible = true)
    : TablePanelEntity(name, style, visible)
{
    public float Height { get; set; } = height;
    public List<TablePanelCell> Cells { get; init; } = [];
    public Color BackColor { get; set; }
}