namespace WinFormsTablePanel.Builders
{
    public class TablePanelRowHelper
    {
        public IEnumerable<(TablePanelRow PanelRow, bool SplitterOn, string SplitterName)> GetRowPairs(ICollection<TablePanelRow> rows)
        {
            return rows
                .Where(row => row.Style != TablePanelEntityStyle.Separator) // Исключаем строки с разделителями
                .Select((currentRow, index) =>
                {
                    var nextRow = rows.ElementAtOrDefault(index * 2 + 1); // Каждая вторая строка - это сплиттер
                    var splitterOn = nextRow?.Style == TablePanelEntityStyle.Separator;
                    var splitterName = splitterOn ? nextRow?.Name ?? $"Splitter_{currentRow.Name}" : string.Empty;
                    return (currentRow, splitterOn, splitterName);
                });
        }

        public (List<TablePanelRow> TopRows, TablePanelRow? FillRow, List<TablePanelRow> BottomRows) SplitRowsByFill(ICollection<TablePanelRow> rows)
        {
            var fillRow = rows.FirstOrDefault(row => row.Style == TablePanelEntityStyle.Fill);

            var topRows = rows.TakeWhile(row => row != fillRow).ToList();
            var bottomRows = rows.SkipWhile(row => row != fillRow).Skip(1).ToList();

            return (topRows, fillRow, bottomRows);
        }
    }
}