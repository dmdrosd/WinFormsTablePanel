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

        foreach (var row in rows)
        {
            Control control;

            if (row.Cells.Any())
            {
                var horizontalBuilder = new HorizontalStackPanelBuilder();
                var horizontalResult = horizontalBuilder.Build(row.Cells, (int)row.Height);
                control = new Panel { Dock = dockStyle, Height = (int)row.Height };

                foreach (var panel in horizontalResult.Controls)
                {
                    control.Controls.Add(panel);
                }

                foreach (var cell in horizontalResult.NamedCells)
                {
                    result.NamedCells[cell.Key] = cell.Value;
                }

                foreach (var container in horizontalResult.NamedContainers)
                {
                    result.NamedContainers[container.Key] = container.Value;
                }
            }
            else
            {
                control = row.Style == TablePanelEntityStyle.Separator
                    ? _controlFactory.CreateSplitter(row, dockStyle)
                    : _controlFactory.CreatePanel(row, dockStyle);

                result.NamedContainers[row.Name] = control as Panel;
                result.NamedCells[row.Name] = control as Panel;
            }

            yield return control;
        }
    }

}