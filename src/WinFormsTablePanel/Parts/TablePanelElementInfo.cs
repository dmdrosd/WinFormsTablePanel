using WinFormsTablePanel;

public class TablePanelElementInfo
{
    public string Name { get; set; }
    public Color BackColor { get; set; } = Color.Empty;
    public bool HasSplitter { get; set; }
    public int SplitterHeight { get; set; }
    public DockStyle Dock { get; set; } = DockStyle.Fill;
    public bool IsFillPanel { get; set; } = false;
    public int Height { get; set; }
    public TablePanelEntityStyle Style { get; set; }

    public TablePanelElementInfo() { }
}