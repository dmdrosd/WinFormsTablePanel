using WinFormsTablePanel.PanelElements;
using WinFormsTablePanel.Parts;

public class VerticalStackPanelBuilder(TablePanelStructureInfo structureInfo, ControlFactory controlFactory) : IPanelBuilder
{
    public IEnumerable<PanelElement> Build()
    {
        return structureInfo
            .Elements
            .Select(element => controlFactory.CreateCompositePanel([element]))
            .Reverse();
    }
}