using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class TablePanelBuilder(TablePanelStructure structure) : IPanelBuilder
{
    public IEnumerable<Control> Build()
    {
        // Предполагаем вертикальную ориентацию
        var builder = new VerticalStackPanelBuilder();

        return builder.Build(structure.Rows);
    }
}