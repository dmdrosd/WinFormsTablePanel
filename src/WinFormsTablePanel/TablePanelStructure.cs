namespace WinFormsTablePanel;

public class TablePanelStructure
{
    public List<TablePanelRow> Rows { get; set; }

    public TablePanelStructure()
    {
        Rows = new List<TablePanelRow>();
    }
}