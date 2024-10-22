using WinFormsTablePanel.Factories;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Builders;

public class HorizontalStackPanelBuilder
{
    private readonly ControlFactory _controlFactory = new();
    private readonly Helpers.TablePanelHelper _helper = new();

    public IEnumerable<Control> Build(List<TablePanelCell> cells)
    {
        var controls = new List<Control>();

        // Разделяем ячейки на левые, Fill и правые
        var (leftCells, fillCell, rightCells) = _helper.SplitCellsByDock(cells);

        controls.AddRange(leftCells.Select(cell =>
            cell.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(cell, DockStyle.Left)
                : _controlFactory.CreatePanel(cell, DockStyle.Left)));

        var rightControls = rightCells.Select(cell =>
            cell.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(cell, DockStyle.Right)
                : _controlFactory.CreatePanel(cell, DockStyle.Right)).ToList();

        rightControls.Reverse();
        controls.AddRange(rightControls);

        if (fillCell != null)
        {
            controls.Add(fillCell.Style == TablePanelEntityStyle.Separator
                ? _controlFactory.CreateSplitter(fillCell, DockStyle.Fill)
                : _controlFactory.CreatePanel(fillCell, DockStyle.Fill));
        }

        return controls;
    }
}