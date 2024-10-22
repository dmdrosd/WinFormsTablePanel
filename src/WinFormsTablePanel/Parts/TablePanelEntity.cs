namespace WinFormsTablePanel.Parts;

public class TablePanelEntity(string name, TablePanelEntityStyle style, bool visible = true)
{
    public string Name { get; set; } = name;
    public TablePanelEntityStyle Style { get; set; } = style;
    public bool Visible { get; set; }
}