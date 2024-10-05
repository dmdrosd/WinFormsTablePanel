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

            // ��������� ������
            tablePanel.AddRow(TablePanelEntityStyle.Absolute, 100); // ������������� ������
            tablePanel.AddRow(TablePanelEntityStyle.Relative, 0);   // ������������� ������
            tablePanel.AddRow(TablePanelEntityStyle.Separator, 0);  // �����������
            tablePanel.AddRow(TablePanelEntityStyle.Fill, 0);   // �������������� ������

            // ��������� �������
            tablePanel.AddColumn(TablePanelEntityStyle.Absolute, 100); // ������������� ������
            tablePanel.AddColumn(TablePanelEntityStyle.Relative, 0);   // ������������� ������
            tablePanel.AddColumn(TablePanelEntityStyle.Separator, 0);  // �����������
            tablePanel.AddColumn(TablePanelEntityStyle.Fill, 0);   // �������������� ������

            tablePanel.BuildLayout();
            this.Controls.Add(tablePanel);
        }
    }
}
