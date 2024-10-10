using WinFormsTablePanel;

public class TablePanelRow
{
    public TablePanelEntityStyle Style { get; set; }
    public float Height { get; set; }
    public bool Visible { get; set; }
    public List<TablePanelCell> Cells { get; set; }
    public RowStatus Status { get; set; } // Новый статус строки
    public int CalculatedHeight { get; set; } // Для хранения вычисленной высоты относительных строк

    public TablePanelRow(TablePanelEntityStyle style, float height, bool visible)
    {
        Style = style;
        Height = height;
        Visible = visible;
        Cells = new List<TablePanelCell>();
    }
}