using WinFormsTablePanel;
using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Parts;

public class RowHeightCalculator
{
    private readonly int _totalHeight;
    private readonly List<TablePanelRow> _rows;

    public RowHeightCalculator(List<TablePanelRow> rows, int totalHeight)
    {
        _rows = rows;
        _totalHeight = totalHeight;
    }

    public PanelHeightInfo CalculateHeights()
    {
        var heightInfo = new PanelHeightInfo();
        var remainingHeight = _totalHeight;
        var totalRelativeWeight = _rows.Where(r => r.Style == TablePanelEntityStyle.Relative).Sum(r => r.Height);

        // Сначала уменьшаем высоту на все абсолютные строки
        foreach (var row in _rows)
        {
            if (row.Style == TablePanelEntityStyle.Absolute)
            {
                remainingHeight -= (int)row.Height;
            }
        }

        // Распределяем оставшуюся высоту между Relative строками
        var distributedHeight = 0;
        foreach (var row in _rows.Where(r => r.Style == TablePanelEntityStyle.Relative))
        {
            var relativeHeight = (int)((row.Height / totalRelativeWeight) * remainingHeight);
            distributedHeight += relativeHeight;
            heightInfo.SetHeightForRow(row, relativeHeight);
        }

        // Если осталась нераспределенная высота из-за округлений, добавляем её к последней Relative строке
        var unallocatedHeight = remainingHeight - distributedHeight;
        if (unallocatedHeight > 0 && _rows.Any(r => r.Style == TablePanelEntityStyle.Relative))
        {
            var lastRelativeRow = _rows.Last(r => r.Style == TablePanelEntityStyle.Relative);
            var currentHeight = heightInfo.GetHeightForRow(lastRelativeRow);
            heightInfo.SetHeightForRow(lastRelativeRow, currentHeight + unallocatedHeight);
        }

        return heightInfo;
    }
}