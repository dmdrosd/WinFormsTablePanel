namespace WinFormsTablePanel.Builders
{
    public class PanelHeightInfo
    {
        private readonly Dictionary<TablePanelRow, int> _heights = new();

        public void SetHeightForRow(TablePanelRow row, int height)
        {
            _heights[row] = height;
        }

        public int GetHeightForRow(TablePanelRow row)
        {
            return _heights.GetValueOrDefault(row, 0);
        }
    }
}