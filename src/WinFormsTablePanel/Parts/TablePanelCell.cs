using WinFormsTablePanel;
using WinFormsTablePanel.Parts;

public class TablePanelCell
{
    public string Name { get; set; }
    public string Text { get; set; }
    public TablePanelEntityStyle Style { get; set; }
    public bool Visible { get; set; } = true;
    public double Width { get; set; }
    public DockStyle Dock { get; set; }
    public Color BackColor { get; set; } = Color.Transparent;
    public TablePanelStructure ChildStructure { get; set; }
    public Control Control { get; set; }
    public bool HasSplitter { get; set; }
    public int SplitterHeight { get; set; }

    public TablePanelCell()
    {
        // Значения по умолчанию можно оставить здесь
    }
}