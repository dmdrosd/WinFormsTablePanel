using WinFormsTablePanel;
using WinFormsTablePanel.Builders;

public class VerticalStackPanelBuilder : IPanelBuilder
{
    private readonly ICollection<TablePanelRow> _rows;
    private readonly ControlFactory _controlFactory;
    private readonly PanelHeightCalculator _heightCalculator;
    private readonly TablePanelRowHelper _tablePanelRowHelper;
    private readonly int _totalHeight; // Добавляем поле для общей высоты

    public VerticalStackPanelBuilder(ICollection<TablePanelRow> rows, int totalHeight)
    {
        _rows = rows ?? Array.Empty<TablePanelRow>();
        _totalHeight = totalHeight; // Инициализируем общую высоту
        _controlFactory = new ControlFactory();
        _heightCalculator = new PanelHeightCalculator();
        _tablePanelRowHelper = new TablePanelRowHelper(); // Поле для работы с TablePanelRowHelper
    }

    public IEnumerable<Control> Build()
    {
        var (topRows, fillRow, bottomRows) = _tablePanelRowHelper.SplitRowsByFill(_rows);

        // Рассчитываем высоты для Relative строк, используя PanelHeightCalculator
        var heightInfo = _heightCalculator.CalculateHeights(_rows, _totalHeight);

        // Выдаём верхние строки
        foreach (var control in BuildRowsWithSplitters(topRows, DockStyle.Top, heightInfo))
        {
            yield return control;
        }

        // Выдаём нижние строки
        bottomRows.Reverse();
        foreach (var control in BuildRowsWithSplitters(bottomRows, DockStyle.Bottom, heightInfo))
        {
            yield return control;
        }

        // Добавляем Fill панель в конец
        if (fillRow != null)
        {
            yield return _controlFactory.CreatePanel(fillRow, DockStyle.Fill);
        }
    }

    private IEnumerable<Control> BuildRowsWithSplitters(ICollection<TablePanelRow> rows, DockStyle dockStyle, PanelHeightInfo heightInfo)
    {
        foreach (var (row, splitterOn, splitterName) in _tablePanelRowHelper.GetRowPairs(rows))
        {
            if (row.Style == TablePanelEntityStyle.Relative)
            {
                // Получаем высоту для Relative строки из PanelHeightInfo
                row.Height = heightInfo.GetHeightForRow(row);
            }

            yield return _controlFactory.CreatePanel(row, dockStyle);

            if (splitterOn)
            {
                yield return _controlFactory.CreateSplitter(splitterName, dockStyle, (int)row.Height);
            }
        }
    }
}