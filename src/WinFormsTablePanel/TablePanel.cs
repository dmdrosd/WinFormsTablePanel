using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Contracts;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel;

public class TablePanel : UserControl, ITablePanel
{
    public void ApplyStructure(TablePanelStructure structure)
    {
        Controls.Clear();

        var builder = new TablePanelBuilder(structure);
        var controls = builder.Build();

        Controls.AddRange(controls.ToArray());
    }
}