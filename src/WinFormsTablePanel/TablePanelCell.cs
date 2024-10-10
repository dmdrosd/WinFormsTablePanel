namespace WinFormsTablePanel;

public class TablePanelCell
{
    public string Text { get; set; }
    public Control Control { get; set; }
    public Color BackColor { get; set; }
    public bool Visible { get; set; }
    public TablePanelEntityStyle Style { get; set; }
    public float Width { get; set; }
    public DockStyle Dock { get; set; } = DockStyle.Left;
    public TablePanelStructure ChildStructure { get; set; }
    public CellStatus Status { get; set; }
}