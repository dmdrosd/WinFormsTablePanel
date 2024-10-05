using WinFormsTablePanel;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tablePanel = new TablePanel
            {
                Dock = DockStyle.Fill,
                ShowGrid = DefaultBoolean.True
            };

            // Создаём структуру
            var structure = new TablePanelStructure
            {
                Rows = new List<TablePanelRow>
                {
                    new(TablePanelEntityStyle.Absolute, 200, true)
                    {
                        Cells = new List<TablePanelCell>
                        {
                            new() { Text = "Header 1", BackColor = Color.LightYellow, Style = TablePanelEntityStyle.Absolute, Size = 250, Visible = true },
                            new() { Style = TablePanelEntityStyle.Separator },
                            new() { Text = "Header 2", BackColor = Color.PaleTurquoise, Style = TablePanelEntityStyle.Relative, Size = 1f, Visible = true },
                            new() { Style = TablePanelEntityStyle.Separator },
                            new() { Text = "Header 3", BackColor = Color.SkyBlue, Style = TablePanelEntityStyle.Fill, Size = 0, Visible = true }
                        }
                    },
                    new(TablePanelEntityStyle.Separator, 3, true),
                    new(TablePanelEntityStyle.Relative, 2f, true)
                    {
                        Cells = new List<TablePanelCell>
                        {
                            new() { Text = "Content 1", BackColor = Color.LightCoral, Style = TablePanelEntityStyle.Relative, Visible = true },
                            new() { Style = TablePanelEntityStyle.Separator },
                            new() { Text = "Content 2", BackColor = Color.MediumSeaGreen, Style = TablePanelEntityStyle.Relative, Visible = true }
                        }
                    },
                    new(TablePanelEntityStyle.Separator, 3, true),
                    new(TablePanelEntityStyle.Fill, 0, true)
                    {
                        Cells = new List<TablePanelCell>
                        {
                            new() { Text = "Footer 1", BackColor = Color.LavenderBlush, Style = TablePanelEntityStyle.Fill, Visible = true },
                            new() { Style = TablePanelEntityStyle.Separator },
                            new() { Text = "Footer 2", BackColor = Color.LightSlateGray, Style = TablePanelEntityStyle.Fill, Visible = true }
                        }
                    }
                },
                Columns = new List<TablePanelColumn>
                {
                    new TablePanelColumn(TablePanelEntityStyle.Absolute, 100, true),
                    new TablePanelColumn(TablePanelEntityStyle.Relative, 200, true),
                    new TablePanelColumn(TablePanelEntityStyle.Separator, 0, true),
                    new TablePanelColumn(TablePanelEntityStyle.Fill, 0, true)
                }
            };

            // Применяем структуру и строим макет
            tablePanel.ApplyStructure(structure);

            // Добавляем TablePanel на форму
            Controls.Add(tablePanel);
        }



    }
}
