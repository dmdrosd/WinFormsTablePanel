using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Contracts;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel;

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

    public Control? GetNamedContainer(string name)
    {
        return _namedContainers.GetValueOrDefault(name);
    }

    public Control? GetNamedCell(string name)
    {
        return _namedCells.GetValueOrDefault(name);
    }
}