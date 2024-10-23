using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Helpers;

public class TablePanelHelper
{
    // Для строк панели
    public IEnumerable<(TablePanelRow PanelRow, bool HasSplitter)> GetRowPairs(ICollection<TablePanelRow> rows)
    {
        for (int i = 0; i < rows.Count; i++)
        {
            var currentRow = rows.ElementAt(i);
            if (currentRow.Style == TablePanelEntityStyle.Separator)
                continue;

            var nextRow = i + 1 < rows.Count ? rows.ElementAt(i + 1) : null;
            var hasSplitter = nextRow?.Style == TablePanelEntityStyle.Separator;

            yield return (currentRow, hasSplitter);
        }
    }

    public (List<TablePanelRow> TopRows, TablePanelRow? FillRow, List<TablePanelRow> BottomRows) SplitRowsByFill(ICollection<TablePanelRow> rows)
    {
        var fillIndex = rows.ToList().FindIndex(row => row.Style == TablePanelEntityStyle.Fill);

        if (fillIndex == -1)
        {
            return (rows.ToList(), null, []);
        }

        var topRows = rows.Take(fillIndex).ToList();
        var fillRow = rows.ElementAt(fillIndex);
        var bottomRows = rows.Skip(fillIndex + 1).ToList();

        return (topRows, fillRow, bottomRows);
    }

    // Для ячеек панели
    public IEnumerable<(TablePanelCell Cell, bool HasSeparator)> GetCellPairs(ICollection<TablePanelCell> cells)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            var currentCell = cells.ElementAt(i);
            if (currentCell.Style == TablePanelEntityStyle.Separator)
                continue;

            var nextCell = i + 1 < cells.Count ? cells.ElementAt(i + 1) : null;
            var hasSeparator = nextCell?.Style == TablePanelEntityStyle.Separator;

            yield return (currentCell, hasSeparator);
        }
    }

    public (List<TablePanelCell> LeftCells, TablePanelCell? FillCell, List<TablePanelCell> RightCells) SplitCellsByDock(ICollection<TablePanelCell> cells)
    {
        var fillIndex = cells.ToList().FindIndex(cell => cell.Style == TablePanelEntityStyle.Fill);

        if (fillIndex == -1)
        {
            return (cells.ToList(), null, new List<TablePanelCell>());
        }

        var leftCells = cells.Take(fillIndex).ToList();
        var fillCell = cells.ElementAt(fillIndex);
        var rightCells = cells.Skip(fillIndex + 1).ToList();

        return (leftCells, fillCell, rightCells);
    }
}