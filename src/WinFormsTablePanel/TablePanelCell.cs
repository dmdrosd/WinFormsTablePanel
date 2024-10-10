using WinFormsTablePanel;

public class TablePanelCell
{
    // Существующие свойства
    public string Text { get; set; }
    public Control Control { get; set; }
    public Color BackColor { get; set; }
    public bool Visible { get; set; }
    public TablePanelEntityStyle Style { get; set; }
    public float Width { get; set; }

    // Новое свойство для докинга
    public DockStyle Dock { get; set; } = DockStyle.Left;

    // Свойство для вложенной структуры
    public TablePanelStructure ChildStructure { get; set; }
}
