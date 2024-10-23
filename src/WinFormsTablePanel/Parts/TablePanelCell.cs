namespace WinFormsTablePanel.Parts;

public class TablePanelCell(
    string name,
    TablePanelEntityStyle style,
    float width,
    Control? control = null,
    TablePanelStructure? childStructure = null,
    bool visible = true)
    : TablePanelEntity(name, style, visible)
{
    public float Width { get; set; } = width;
    public Control? Control { get; set; } = control;
    public TablePanelStructure? ChildStructure { get; init; } = childStructure;
    public Color BackColor { get; init; }
}