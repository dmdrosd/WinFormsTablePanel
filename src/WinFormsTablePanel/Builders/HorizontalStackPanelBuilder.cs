using WinFormsTablePanel.Factories;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class HorizontalStackPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly TablePanelHelper _helper = new();

    public PanelBuildResult Build(List<TablePanelCell> cells)
    {
        var result = new PanelBuildResult();

        var (leftCells, fillCell, rightCells) = _helper.SplitCellsByDock(cells);

        result.Controls.AddRange(leftCells.Select(cell => CreatePanelOrNestedTable(cell, DockStyle.Left, result)));

        var rightControls = rightCells.Select(cell => CreatePanelOrNestedTable(cell, DockStyle.Right, result)).Reverse();
        result.Controls.AddRange(rightControls);

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

            // Добавляем панели и контейнеры из вложенной структуры в текущий результат
            result.NamedContainers[cell.Name] = nestedPanel;

            // Добавляем вложенные элементы с помощью Union
            result.NamedContainers = result.NamedContainers.Union(nestedResult.NamedContainers).ToDictionary(x => x.Key, x => x.Value);
            result.NamedCells = result.NamedCells.Union(nestedResult.NamedCells).ToDictionary(x => x.Key, x => x.Value);

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
