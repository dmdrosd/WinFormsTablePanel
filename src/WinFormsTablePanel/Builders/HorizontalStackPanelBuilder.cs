using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Factories;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

public class HorizontalStackPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly TablePanelHelper _helper = new();

    public PanelBuildResult Build(List<TablePanelCell> cells)
    {
        var result = new PanelBuildResult();

        // Разделяем ячейки на левые, Fill и правые
        var (leftCells, fillCell, rightCells) = _helper.SplitCellsByDock(cells);

        // Обрабатываем левые панели (DockStyle.Left)
        result.Controls.AddRange(leftCells.Select(cell =>
            CreatePanelOrNestedTable(cell, DockStyle.Left, result)));

        // Обрабатываем правые панели (DockStyle.Right), добавляем в обратном порядке
        var rightControls = rightCells.Select(cell =>
            CreatePanelOrNestedTable(cell, DockStyle.Right, result)).ToList();

        rightControls.Reverse();
        result.Controls.AddRange(rightControls);

        // Добавляем Fill панель (DockStyle.Fill)
        if (fillCell != null)
        {
            result.Controls.Add(CreatePanelOrNestedTable(fillCell, DockStyle.Fill, result));
        }

        return result;
    }

    private Control CreatePanelOrNestedTable(TablePanelCell cell, DockStyle dockStyle, PanelBuildResult result)
    {
        if (cell.ChildStructure != null)
        {
            var nestedBuilder = new TablePanelBuilder(cell.ChildStructure);
            var nestedResult = nestedBuilder.Build();

            var nestedPanel = new Panel
            {
                Dock = dockStyle,
                BorderStyle = BorderStyle.FixedSingle
            };

            foreach (var control in nestedResult.Controls)
            {
                nestedPanel.Controls.Add(control);
            }

            result.NamedContainers[cell.Name] = nestedPanel;
            return nestedPanel;
        }
        else
        {
            var panel = _controlFactory.CreatePanel(cell, dockStyle);
            result.NamedCells[cell.Name] = panel;
            return panel;
        }
    }
}
