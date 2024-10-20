using WinFormsTablePanel.PanelElements;
using WinFormsTablePanel.Parts;

public class TablePanelBuilder : IPanelBuilder
{
    private readonly TablePanelStructureInfo _structureInfo;
    private readonly ControlFactory _controlFactory;

    public TablePanelBuilder(TablePanelStructureInfo structureInfo, ControlFactory controlFactory)
    {
        _structureInfo = structureInfo;
        _controlFactory = controlFactory;
    }

    public IEnumerable<PanelElement> Build()
    {
        var verticalBuilder = new VerticalStackPanelBuilder(_structureInfo, _controlFactory);
        var panelElements = verticalBuilder.Build();

        return panelElements;
    }
}