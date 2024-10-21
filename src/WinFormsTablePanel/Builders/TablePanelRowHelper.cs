namespace WinFormsTablePanel.Builders
{
    public class TablePanelRowHelper
    {
        public IEnumerable<(TablePanelRow PanelRow, bool SplitterOn, string SplitterName)> GetRowPairs(ICollection<TablePanelRow> rows)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                var currentRow = rows.ElementAt(i);
                if (currentRow.Style == TablePanelEntityStyle.Separator)
                    continue;

                var nextRow = i + 1 < rows.Count ? rows.ElementAt(i + 1) : null;
                var splitterOn = nextRow?.Style == TablePanelEntityStyle.Separator;
                var splitterName = splitterOn ? nextRow.Name : string.Empty;

                yield return (currentRow, splitterOn, splitterName);
            }
        }

        public (List<TablePanelRow> TopRows, TablePanelRow? FillRow, List<TablePanelRow> BottomRows) SplitRowsByFill(ICollection<TablePanelRow> rows)
        {
            var fillIndex = rows.ToList().FindIndex(row => row.Style == TablePanelEntityStyle.Fill);

            if (fillIndex == -1)
            {
                return (rows.ToList(), null, new List<TablePanelRow>());
            }

            var topRows = rows.Take(fillIndex).ToList();
            var fillRow = rows.ElementAt(fillIndex);
            var bottomRows = rows.Skip(fillIndex + 1).ToList();

            return (topRows, fillRow, bottomRows);
        }
    }

}