using WinFormsTablePanel;
using WinFormsTablePanel.Parts;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly TablePanelStructure _tablePanelRowsOnlyStructure = new()
        {
            Rows =
            [
                new TablePanelRow(TablePanelEntityStyle.Absolute, 100, true, "Panel1_Absolute_100"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter1"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 4, true, "Panel2_Relative_4"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter2"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 5, true, "Panel3_Relative_5"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter3"),
                // Fill панель
                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Panel4_Fill"),
                // Нижние панели
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter5"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 3, true, "Panel5_Relative_3"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter6"),
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Panel6_Absolute_50")
            ]
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tablePanel = new TablePanel
            {
                Dock = DockStyle.Fill,
            };

            // Главная структура
            var structure = _tablePanelRowsOnlyStructure;

            // Применяем структуру и строим макет
            tablePanel.ApplyStructure(structure);

            // Добавляем TablePanel на форму
            Controls.Add(tablePanel);
        }
    }
}
