using WinFormsTablePanel;

public class TablePanelEntity
{
    public TablePanelEntityStyle Style { get; set; }
    public bool Visible { get; set; } = true;
    public string Name { get; set; } // Для тестирования
    public TablePanelEntity(TablePanelEntityStyle style, bool visible, string name)
    {
        Style = style;
        Visible = visible;
        Name = name;
    }
}