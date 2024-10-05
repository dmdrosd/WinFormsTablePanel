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
            var tablePanel = new TablePanel();

            // Добавляем строки
            tablePanel.AddRow(TablePanelEntityStyle.Absolute, 100); // Фиксированная высота
            tablePanel.AddRow(TablePanelEntityStyle.Relative, 0);   // Относительная высота
            tablePanel.AddRow(TablePanelEntityStyle.Separator, 0);  // Разделитель
            tablePanel.AddRow(TablePanelEntityStyle.Fill, 0);   // Автоматический размер

            // Добавляем колонки
            tablePanel.AddColumn(TablePanelEntityStyle.Absolute, 100); // Фиксированная ширина
            tablePanel.AddColumn(TablePanelEntityStyle.Relative, 0);   // Относительная ширина
            tablePanel.AddColumn(TablePanelEntityStyle.Separator, 0);  // Разделитель
            tablePanel.AddColumn(TablePanelEntityStyle.Fill, 0);   // Автоматическая ширина

            tablePanel.BuildLayout();
            this.Controls.Add(tablePanel);
        }
    }
}
