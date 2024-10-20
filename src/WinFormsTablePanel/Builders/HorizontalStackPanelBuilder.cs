using WinFormsTablePanel.PanelElements;

public class HorizontalStackPanelBuilder : IPanelBuilder
{
    private readonly List<TablePanelCell> _cells;
    private readonly ControlFactory _controlFactory;

    public HorizontalStackPanelBuilder(List<TablePanelCell> cells, ControlFactory controlFactory)
    {
        _cells = cells;
        _controlFactory = controlFactory;
    }

    public IEnumerable<PanelElement> Build()
    {
        throw new NotImplementedException();

        //var elements = new List<PanelElement>();

        //foreach (var cell in _cells)
        //{
        //    // Создаем панель для каждой ячейки через фабрику
        //    var panel = _controlFactory.CreatePanel(cell);

        //    if (cell.HasSplitter)
        //    {
        //        // Если у ячейки есть сплиттер, создаем композитный элемент (панель + сплиттер)
        //        var splitter = _controlFactory.CreateSplitterForCell(cell);
        //        var compositeElement = _controlFactory.CreateCompositePanel(new[] { panel, splitter });
        //        elements.Add(compositeElement);
        //    }
        //    else
        //    {
        //        // Если сплиттера нет, добавляем панель как leaf элемент
        //        elements.Add(panel);
        //    }
        //}

        //return elements;
    }
}