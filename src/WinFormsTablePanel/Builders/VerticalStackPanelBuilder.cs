using WinFormsTablePanel.Builders;

public class VerticalStackPanelBuilder : IPanelBuilder
{
    private readonly List<TablePanelRow> _rows;
    private readonly TablePanelRowHelper _helper;

    public VerticalStackPanelBuilder(List<TablePanelRow> rows)
    {
        _rows = rows;
        _helper = new TablePanelRowHelper();
    }

    public IEnumerable<Control> Build()
    {
        var controls = new List<Control>();

        // Разделяем строки на верхние, Fill и нижние
        var (topRows, fillRow, bottomRows) = _helper.SplitRowsByFill(_rows);

        // Обрабатываем верхние панели (DockStyle.Top)
        foreach (var row in topRows)
        {
            var rowBuilder = new RowBuilder(row, DockStyle.Top);
            controls.AddRange(rowBuilder.Build());
        }

        // Обрабатываем нижние панели (DockStyle.Bottom), добавляем в обратном порядке
        var bottomControls = new List<Control>();
        foreach (var row in bottomRows)
        {
            var rowBuilder = new RowBuilder(row, DockStyle.Bottom);
            bottomControls.AddRange(rowBuilder.Build());
        }
        bottomControls.Reverse(); // Инвертируем порядок для правильного отображения
        controls.AddRange(bottomControls);

        // Добавляем Fill панель (DockStyle.Fill)
        if (fillRow != null)
        {
            var rowBuilder = new RowBuilder(fillRow, DockStyle.Fill);
            controls.AddRange(rowBuilder.Build());
        }

        // Инвертируем всю последовательность контролов, чтобы при добавлении в Controls
        // панели с DockStyle.Top и DockStyle.Bottom отображались правильно.
        // Это связано с особенностями WinForms, где порядок наложения контролов зависит
        // от порядка их добавления в Controls.
        controls.Reverse();


        return controls;
    }
}