using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

public class HorizontalStackPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly TablePanelHelper _helper = new();

    public PanelBuildResult Build(List<TablePanelCell> cells, int rowHeight = 0)
    {
        var result = new PanelBuildResult();
        var canvas = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

        if (rowHeight > 0)
        {
            canvas.Height = rowHeight;
        }

        var (leftCells, fillCell, rightCells) = _helper.SplitCellsByDock(cells);

        canvas.Controls.AddRange(BuildSection(leftCells, DockStyle.Left, false, result).ToArray());
        if (fillCell != null)
        {
            canvas.Controls.Add(CreatePanelOrNestedTable(fillCell, DockStyle.Fill, result));
        }
        canvas.Controls.AddRange(BuildSection(rightCells, DockStyle.Right, false, result).ToArray());

        result.Controls.Add(canvas);
        return result;
    }

    private IEnumerable<Control> BuildSection(List<TablePanelCell> cells, DockStyle dockStyle, bool reverse, PanelBuildResult result)
    {
        if (reverse)
        {
            cells.Reverse();
        }

        foreach (var cell in cells)
        {
            Control control;

            if (cell.Style == TablePanelEntityStyle.Separator)
            {
                control = _controlFactory.CreateSplitter(cell, dockStyle);
            }
            else
            {
                control = CreatePanelOrNestedTable(cell, dockStyle, result);
            }

            yield return control;
        }
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
