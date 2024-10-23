using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Helpers;

public class PanelHeightCalculator
{
    public PanelHeightInfo CalculateHeights(ICollection<TablePanelRow> rows, int totalHeight)
    {
        var heightInfo = new PanelHeightInfo();
        var remainingHeight = totalHeight;
        var totalRelativeWeight = rows.Where(r => r.Style == TablePanelEntityStyle.Relative).Sum(r => r.Height);

        // Сначала уменьшаем высоту на все абсолютные строки
        foreach (var row in rows)
        {
            if (row.Style == TablePanelEntityStyle.Absolute)
            {
                remainingHeight -= (int)row.Height;
            }
        }

        // Распределяем оставшуюся высоту между Relative строками
        var distributedHeight = 0;
        foreach (var row in rows.Where(r => r.Style == TablePanelEntityStyle.Relative))
        {
            // Расчет высоты на основе общего веса
            var relativeHeight = (int)(row.Height / totalRelativeWeight * remainingHeight);
            distributedHeight += relativeHeight;

            // Сохраняем рассчитанную высоту для строки
            heightInfo.SetHeightForRow(row, relativeHeight);
        }

        // Если осталась нераспределенная высота из-за округлений, добавляем её к последней Relative строке
        var unallocatedHeight = remainingHeight - distributedHeight;
        if (unallocatedHeight > 0 && rows.Any(r => r.Style == TablePanelEntityStyle.Relative))
        {
            var lastRelativeRow = rows.Last(r => r.Style == TablePanelEntityStyle.Relative);
            var currentHeight = heightInfo.GetHeightForRow(lastRelativeRow);
            heightInfo.SetHeightForRow(lastRelativeRow, currentHeight + unallocatedHeight);
        }

        // Устанавливаем абсолютные высоты для абсолютных строк
        foreach (var row in rows)
        {
            if (row.Style == TablePanelEntityStyle.Absolute)
            {
                heightInfo.SetHeightForRow(row, (int)row.Height);
            }
        }

        return heightInfo;
    }

}