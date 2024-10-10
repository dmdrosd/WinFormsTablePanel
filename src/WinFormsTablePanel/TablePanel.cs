namespace WinFormsTablePanel
{
    public class TablePanel : UserControl
    {
        // Поля
        private readonly List<TablePanelRow> _rows = new();

        // Конструктор
        public TablePanel()
        {
            Dock = DockStyle.Fill;
        }

        // Свойства
        public List<TablePanelRow> Rows => _rows;
        public DefaultBoolean ShowGrid { get; set; }

        // Методы
        public void ApplyStructure(TablePanelStructure structure)
        {
            _rows.Clear();
            _rows.AddRange(structure.Rows);

            BuildLayout();
        }

        private void BuildLayout()
        {
            Controls.Clear();

            foreach (var row in _rows)
            {
                if (row.Style == TablePanelEntityStyle.Separator)
                {
                    // Добавляем горизонтальный сплиттер
                    var splitter = new Splitter
                    {
                        Dock = DockStyle.Top,
                        Height = 6,
                        BackColor = Color.Gray
                    };
                    Controls.Add(splitter);
                    splitter.BringToFront();
                    continue;
                }

                var rowPanel = new Panel
                {
                    Dock = row.Style == TablePanelEntityStyle.Fill ? DockStyle.Fill : DockStyle.Top,
                    Height = row.Style == TablePanelEntityStyle.Absolute ? (int)row.Height : 0,
                    Visible = row.Visible
                };

                Controls.Add(rowPanel);
                BuildCells(row, rowPanel);
            }
        }

        private void BuildCells(TablePanelRow row, Panel rowPanel)
        {
            foreach (var cell in row.Cells)
            {
                if (cell.Style == TablePanelEntityStyle.Separator)
                {
                    // Добавляем вертикальный сплиттер
                    var splitter = new Splitter
                    {
                        Dock = DockStyle.Left,
                        Width = 6,
                        BackColor = Color.Gray
                    };
                    rowPanel.Controls.Add(splitter);
                    splitter.BringToFront();
                    continue;
                }

                var cellPanel = new Panel
                {
                    BackColor = cell.BackColor,
                    Visible = cell.Visible,
                    Dock = cell.Dock,
                    Width = cell.Style == TablePanelEntityStyle.Absolute ? (int)cell.Width : 0
                };

                if (cell.ChildStructure != null)
                {
                    var nestedTablePanel = new TablePanel
                    {
                        Dock = DockStyle.Fill
                    };
                    nestedTablePanel.ApplyStructure(cell.ChildStructure);
                    cellPanel.Controls.Add(nestedTablePanel);
                }
                else if (!string.IsNullOrEmpty(cell.Text))
                {
                    cellPanel.Controls.Add(new Label
                    {
                        Text = cell.Text,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter
                    });
                }
                else if (cell.Control != null)
                {
                    cell.Control.Dock = DockStyle.Fill;
                    cellPanel.Controls.Add(cell.Control);
                }

                rowPanel.Controls.Add(cellPanel);
                cellPanel.BringToFront();
            }
        }
    }

}
