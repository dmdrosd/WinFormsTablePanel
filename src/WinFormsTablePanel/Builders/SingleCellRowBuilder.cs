namespace WinFormsTablePanel.Builders
{
    public class SingleCellRowBuilder : IPanelBuilder
    {
        private readonly TablePanelRow _row;

        public SingleCellRowBuilder(TablePanelRow row)
        {
            _row = row;
        }

        public IEnumerable<Control> Build()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top
            };

            foreach (var cell in _row.Cells)
            {
                var control = CreateControlFromCell(cell);
                panel.Controls.Add(control);
            }

            return new List<Control> { panel };
        }

        private Control CreateControlFromCell(TablePanelCell cell)
        {
            var control = new Label
            {
                Text = cell.Text,
                Dock = DockStyle.Fill,
                BackColor = cell.BackColor
            };

            return control;
        }
    }
}