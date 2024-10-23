using WinFormsTablePanel.Factories;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class VerticalStackPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly TablePanelHelper _helper = new();

    public PanelBuildResult Build(List<TablePanelRow> rows)
    {
        var result = new PanelBuildResult();

        // Разделяем строки на верхние, Fill и нижние
        var (topRows, fillRow, bottomRows) = _helper.SplitRowsByFill(rows);

        // Обрабатываем верхние строки (DockStyle.Top)
        result.Controls.AddRange(BuildSection(topRows, DockStyle.Top, false, result));

        // Обрабатываем нижние строки (DockStyle.Bottom), с инверсией
        result.Controls.AddRange(BuildSection(bottomRows, DockStyle.Bottom, true, result));

        // Добавляем Fill панель (DockStyle.Fill)
        if (fillRow != null)
        {
            result.Controls.AddRange(BuildSection([fillRow], DockStyle.Fill, false, result));
        }

        result.Controls.Reverse();

        return result;
    }

    private IEnumerable<Control> BuildSection(List<TablePanelRow> rows, DockStyle dockStyle, bool reverse, PanelBuildResult result)
    {
        if (reverse)
        {
            rows.Reverse();
        }

        var sectionControls = new List<Control>();
        foreach (var row in rows)
        {
            if (row.Cells != null && row.Cells.Any())
            {
                var horizontalBuilder = new HorizontalStackPanelBuilder();
                var horizontalResult = horizontalBuilder.Build(row.Cells);
                sectionControls.AddRange(horizontalResult.Controls);

                // Добавляем именованные ячейки
                foreach (var cell in horizontalResult.NamedCells)
                {
                    result.NamedCells[cell.Key] = cell.Value;
                }

                // Добавляем именованные контейнеры, если есть
                foreach (var container in horizontalResult.NamedContainers)
                {
                    result.NamedContainers[container.Key] = container.Value;
                }
            }
            else
            {
                // Создаем панель или сплиттер для строки
                Control control;
                if (row.Style == TablePanelEntityStyle.Separator)
                {
                    control = _controlFactory.CreateSplitter(row, dockStyle);
                }
                else
                {
                    var panel = _controlFactory.CreatePanel(row, dockStyle);
                    result.NamedContainers[row.Name] = panel;
                    result.NamedCells[row.Name] = panel;

                    control = panel;
                }

                sectionControls.Add(control);
            }
        }

        return sectionControls;
    }
}