using WinFormsTablePanel.Parts;

public class TablePanelBuilder : IPanelBuilder
{
    private readonly TablePanelStructure _structure;

    public TablePanelBuilder(TablePanelStructure structure)
    {
        _structure = structure;
    }

    public IEnumerable<Control> Build()
    {
        // Предполагаем вертикальную ориентацию
        var builder = new VerticalStackPanelBuilder(_structure.Rows);
        return builder.Build();
    }
}