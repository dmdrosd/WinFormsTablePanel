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
            tablePanel.AddRow(TablePanelEntityStyle.Absolute, 100); // Фиксированная высота
            tablePanel.AddRow(TablePanelEntityStyle.Relative, 0);   // Относительная высота
            tablePanel.AddRow(TablePanelEntityStyle.Separator, 0);  // Разделитель
            tablePanel.AddRow(TablePanelEntityStyle.Fill, 0);       // Заполняющая высота

            // Добавляем колонки
            tablePanel.AddColumn(TablePanelEntityStyle.Absolute, 100); // Фиксированная ширина
            tablePanel.AddColumn(TablePanelEntityStyle.Relative, 200); // Относительная ширина
            tablePanel.AddColumn(TablePanelEntityStyle.Separator, 0);  // Разделитель
            tablePanel.AddColumn(TablePanelEntityStyle.Fill, 0);       // Заполняющая ширина

            // Добавляем ячейки в строки и столбцы
            var panel1 = new Panel { BackColor = Color.LightYellow, Width = 100 };
            var panel2 = new Panel { BackColor = Color.LightBlue, Width = 200 };
            var panel3 = new Panel { BackColor = Color.LightGray, Width = 300 };

            tablePanel.AddCell(0, 0, panel1);
            tablePanel.AddCell(1, 1, panel2);
            tablePanel.AddCell(3, 2, panel3);

            tablePanel.BuildLayout();

            // Добавляем TablePanel на форму
            Controls.Add(tablePanel);
        }

    }
}
