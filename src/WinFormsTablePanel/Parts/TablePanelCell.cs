using WinFormsTablePanel;
using WinFormsTablePanel.Parts;

public class TablePanelCell : TablePanelEntity
{
    public float Width { get; set; }
    public Control Control { get; set; }
    public TablePanelStructure ChildStructure { get; set; }

    public TablePanelCell(TablePanelEntityStyle style, float width, bool visible, string name)
        : base(style, visible, name)
    {
        Width = width;
    }
}