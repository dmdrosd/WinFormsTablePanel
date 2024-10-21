using WinFormsTablePanel;
using WinFormsTablePanel.Builders;

public class VerticalStackPanelBuilder : IPanelBuilder
{
    private readonly List<TablePanelRow> _rows;
    private readonly TablePanelRowHelper _helper;
    private readonly ControlFactory _controlFactory;

    public VerticalStackPanelBuilder(List<TablePanelRow> rows)
    {
        _rows = rows;
        _helper = new TablePanelRowHelper();
        _controlFactory = new ControlFactory();
    }

    public IEnumerable<Control> Build()
    {
        var controls = new List<Control>();

        // Разделяем строки на верхние, Fill и нижние
        var (topRows, fillRow, bottomRows) = _helper.SplitRowsByFill(_rows);

        // Обрабатываем верхние панели (DockStyle.Top)
        controls.AddRange(topRows.Select(row =>
            row.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(row, DockStyle.Top)
                : _controlFactory.CreatePanel(row, DockStyle.Top)));

        // Обрабатываем нижние панели (DockStyle.Bottom), добавляем в обратном порядке
        var bottomControls = bottomRows.Select(row =>
            row.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(row, DockStyle.Bottom)
                : _controlFactory.CreatePanel(row, DockStyle.Bottom)).ToList();

        bottomControls.Reverse();
        controls.AddRange(bottomControls);

        // Добавляем Fill панель (DockStyle.Fill)
        if (fillRow != null)
        {
            controls.Add(fillRow.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(fillRow, DockStyle.Fill)
                : _controlFactory.CreatePanel(fillRow, DockStyle.Fill));
        }

        controls.Reverse();

        return controls;
    }
}