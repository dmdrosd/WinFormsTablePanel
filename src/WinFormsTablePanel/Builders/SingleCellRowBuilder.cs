using WinFormsTablePanel.PanelElements;

public class SingleCellRowBuilder : IPanelBuilder
{
    private readonly TablePanelElementInfo _elementInfo;
    private readonly ControlFactory _controlFactory;

    public SingleCellRowBuilder(TablePanelElementInfo elementInfo, ControlFactory controlFactory)
    {
        _elementInfo = elementInfo;
        _controlFactory = controlFactory;
    }

    public IEnumerable<PanelElement> Build()
    {
        // Создаем только панель, без сплиттера
        var panel = _controlFactory.CreatePanel(_elementInfo);

        return new List<PanelElement> { panel };
    }
}