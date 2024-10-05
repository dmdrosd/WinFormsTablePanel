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

            // Добавляем строки
            tablePanel.AddRow(TablePanelEntityStyle.Relative, 1f);
            tablePanel.AddRow(TablePanelEntityStyle.Absolute, 150);
            tablePanel.AddRow(TablePanelEntityStyle.Absolute, 300);

            // Добавляем столбцы
            tablePanel.AddColumn(TablePanelEntityStyle.Relative, 1f);
            tablePanel.AddColumn(TablePanelEntityStyle.Relative, 1f);

            // Добавляем контролы в ячейки
            var panel1 = new Panel { BackColor = Color.LightYellow };
            var panel2 = new Panel { BackColor = Color.LightBlue };
            var panel3 = new Panel { BackColor = Color.LightGray };

            tablePanel.SetCell(panel1, 0, 0);
            tablePanel.SetCell(panel2, 1, 1);
            tablePanel.SetCell(panel3, 2, 0);

            // Добавляем TablePanel на форму
            this.Controls.Add(tablePanel);

            tablePanel.BuildLayout();
        }




    }
}
