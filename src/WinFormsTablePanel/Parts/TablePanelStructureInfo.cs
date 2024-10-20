namespace WinFormsTablePanel.Parts;

public class TablePanelStructureInfo
{
    public IEnumerable<TablePanelElementInfo> Elements { get; }
    public bool HasFillElement { get; }

    public TablePanelStructureInfo(IEnumerable<TablePanelElementInfo> elements, bool hasFillElement)
    {
        Elements = elements;
        HasFillElement = hasFillElement;
    }
}