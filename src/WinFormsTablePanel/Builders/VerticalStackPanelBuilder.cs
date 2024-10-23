using WinFormsTablePanel.Factories;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class VerticalStackPanelBuilder() : IPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly Helpers.TablePanelHelper _helper = new();

    public IEnumerable<Control> Build(List<TablePanelRow> rows)
    {
        var controls = new List<Control>();

        // Разделяем строки на верхние, Fill и нижние
        var (topRows, fillRow, bottomRows) = _helper.SplitRowsByFill(rows);

        // Обрабатываем верхние строки (DockStyle.Top)
        controls.AddRange(BuildSection(topRows, DockStyle.Top, reverse: false));

        // Обрабатываем нижние строки (DockStyle.Bottom), с инверсией
        controls.AddRange(BuildSection(bottomRows, DockStyle.Bottom, reverse: true));

        // Добавляем Fill панель (DockStyle.Fill)
        if (fillRow != null)
        {
            controls.AddRange(BuildSection([fillRow], DockStyle.Fill, reverse: false));
        }

        // Инвертируем для правильного порядка отображения
        controls.Reverse();

        return controls;
    }

    // Общий метод для обработки секций, включая создание горизонтальных ячеек
    private IEnumerable<Control> BuildSection(List<TablePanelRow> rows, DockStyle dockStyle, bool reverse)
    {
        if (reverse)
        {
            rows.Reverse(); // Инвертируем, если необходимо
        }

        var sectionControls = new List<Control>();
        foreach (var row in rows)
        {
            // Если у строки есть ячейки, используем HorizontalStackPanelBuilder для построения горизонтальных ячеек
            if (row.Cells != null && row.Cells.Any())
            {
                var horizontalBuilder = new HorizontalStackPanelBuilder();
                sectionControls.AddRange(horizontalBuilder.Build(row.Cells));
            }
            else
            {
                // Создаем панель или сплиттер для строки
                sectionControls.Add(row.Style == TablePanelEntityStyle.Separator
                    ? _controlFactory.CreateSplitter(row, dockStyle)
                    : _controlFactory.CreatePanel(row, dockStyle));
            }
        }

        return sectionControls;
    }
}