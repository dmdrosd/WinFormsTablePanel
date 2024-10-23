using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Parts;

public class TablePanelBuilder
{
    private readonly TablePanelStructure _structure;

    public TablePanelBuilder(TablePanelStructure structure)
    {
        _structure = structure;
    }

    public PanelBuildResult Build()
    {
        var builder = new VerticalStackPanelBuilder();
        var result = builder.Build(_structure.Rows);

        return result;
    }
}