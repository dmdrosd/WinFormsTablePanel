using WinFormsTablePanel.Factories;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class HorizontalStackPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly TablePanelHelper _helper = new();

    public IEnumerable<Control> Build(List<TablePanelCell> cells)
    {
        var controls = new List<Control>();

        // Разделяем ячейки на левые, Fill и правые
        var (leftCells, fillCell, rightCells) = _helper.SplitCellsByDock(cells);

        // Обрабатываем левые панели (DockStyle.Left)
        controls.AddRange(leftCells.Select(cell =>
            cell.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(cell, DockStyle.Left)
                : CreatePanelOrNestedTable(cell, DockStyle.Left)));

        // Обрабатываем правые панели (DockStyle.Right), добавляем в обратном порядке
        var rightControls = rightCells.Select(cell =>
            cell.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(cell, DockStyle.Right)
                : CreatePanelOrNestedTable(cell, DockStyle.Right)).ToList();

        rightControls.Reverse();
        controls.AddRange(rightControls);

        // Добавляем Fill панель (DockStyle.Fill)
        if (fillCell != null)
        {
            controls.Add(fillCell.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(fillCell, DockStyle.Fill)
                : CreatePanelOrNestedTable(fillCell, DockStyle.Fill));
        }

        return controls;
    }

    private Control CreatePanelOrNestedTable(TablePanelCell cell, DockStyle dockStyle)
    {
        // Если ячейка содержит вложенную таблицу (childStructure), строим её с помощью TablePanelBuilder
        if (cell.ChildStructure != null)
        {
            var nestedBuilder = new TablePanelBuilder(cell.ChildStructure);
            var nestedPanel = new Panel
            {
                Dock = dockStyle,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Добавляем все элементы вложенной таблицы в новую панель
            var nestedControls = nestedBuilder.Build();
            foreach (var control in nestedControls)
            {
                nestedPanel.Controls.Add(control);
            }

            return nestedPanel;
        }
        else
        {
            // Иначе создаём обычную панель для ячейки
            return _controlFactory.CreatePanel(cell, dockStyle);
        }
    }
}
