using WinFormsTablePanel.Contracts;
using WinFormsTablePanel.Parts;

public class TablePanel : UserControl, ITablePanel
{
    private Dictionary<string, Panel> _namedContainers = new();
    private Dictionary<string, Panel> _namedCells = new();

    public void ApplyStructure(TablePanelStructure structure)
    {
        Controls.Clear();

        var builder = new TablePanelBuilder(structure);
        var result = builder.Build();

        _namedContainers = result.NamedContainers;
        _namedCells = result.NamedCells;

        Controls.AddRange(result.Controls.ToArray());
    }

    public Control GetNamedContainer(string name)
    {
        return _namedContainers.TryGetValue(name, out var container) ? container : null;
    }

    public Control GetNamedCell(string name)
    {
        return _namedCells.TryGetValue(name, out var cell) ? cell : null;
    }
}