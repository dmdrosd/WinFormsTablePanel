namespace WinFormsTablePanel.Parts;

public class TablePanelProcessor
{
    public TablePanelStructureInfo ProcessStructure(TablePanelStructure structure, int totalHeight)
    {
        var processedElements = new List<TablePanelElementInfo>();
        var rowHelper = new TablePanelRowHelper();

        // Разбиваем строки на верхние и нижние, оставляя Fill панель для последующей обработки
        var (topRows, fillRow, bottomRows) = rowHelper.SplitRowsByFill(structure.Rows);

        // Высчитываем высоты для строк
        var heightCalculator = new RowHeightCalculator(structure.Rows, totalHeight);
        var panelHeights = heightCalculator.CalculateHeights();

        // Обрабатываем верхние панели (с Dock = DockStyle.Top)
        foreach (var (row, hasSplitter) in rowHelper.GetRowPairs(topRows))
        {
            processedElements.Add(new TablePanelElementInfo
            {
                Name = row.Name,
                BackColor = row.BackColor,
                HasSplitter = hasSplitter,
                SplitterHeight = hasSplitter ? row.Height : 0,
                Dock = DockStyle.Top, // Верхние панели крепим сверху
                Height = panelHeights.GetHeightForRow(row),
                Style = row.Style
            });
        }

        // Обрабатываем нижние панели (с Dock = DockStyle.Bottom), в обратном порядке
        foreach (var (row, hasSplitter) in rowHelper.GetRowPairs(bottomRows).Reverse())
        {
            processedElements.Add(new TablePanelElementInfo
            {
                Name = row.Name,
                BackColor = row.BackColor,
                HasSplitter = hasSplitter,
                SplitterHeight = hasSplitter ? row.Height : 0,
                Dock = DockStyle.Bottom, // Нижние панели крепим снизу
                Height = panelHeights.GetHeightForRow(row),
                Style = row.Style
            });
        }

        // Добавляем Fill панель в самом конце, если она существует
        if (fillRow != null)
        {
            processedElements.Add(new TablePanelElementInfo
            {
                Name = fillRow.Name,
                BackColor = fillRow.BackColor,
                HasSplitter = false, // У Fill панели не может быть сплиттера
                SplitterHeight = 0,
                Dock = DockStyle.Fill, // Fill панель заполняет оставшееся пространство
                Height = panelHeights.GetHeightForRow(fillRow),
                Style = fillRow.Style,
                IsFillPanel = true
            });
        }

        //processedElements.Reverse();

        return new TablePanelStructureInfo(processedElements, fillRow != null);
    }
}