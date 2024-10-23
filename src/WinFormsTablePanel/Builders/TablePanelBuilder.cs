using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class TablePanelBuilder(TablePanelStructure structure)
{
    public PanelBuildResult Build()
    {
        var builder = new VerticalStackPanelBuilder();
        var result = builder.Build(structure.Rows);

        return result;
    }
}