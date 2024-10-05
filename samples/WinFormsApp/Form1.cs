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

            // ��������� ������
            tablePanel.AddRow(TablePanelEntityStyle.Absolute, 100); // ������������� ������
            tablePanel.AddRow(TablePanelEntityStyle.Relative, 0);   // ������������� ������
            tablePanel.AddRow(TablePanelEntityStyle.Separator, 0);  // �����������
            tablePanel.AddRow(TablePanelEntityStyle.Fill, 0);       // ����������� ������

            // ��������� �������
            tablePanel.AddColumn(TablePanelEntityStyle.Absolute, 100); // ������������� ������
            tablePanel.AddColumn(TablePanelEntityStyle.Relative, 200); // ������������� ������
            tablePanel.AddColumn(TablePanelEntityStyle.Separator, 0);  // �����������
            tablePanel.AddColumn(TablePanelEntityStyle.Fill, 0);       // ����������� ������

            // ��������� ������ � ������ � �������
            var panel1 = new Panel { BackColor = Color.LightYellow, Width = 100 };
            var panel2 = new Panel { BackColor = Color.LightBlue, Width = 200 };
            var panel3 = new Panel { BackColor = Color.LightGray, Width = 300 };

            tablePanel.AddCell(0, 0, panel1);
            tablePanel.AddCell(1, 1, panel2);
            tablePanel.AddCell(3, 2, panel3);

            tablePanel.BuildLayout();

            // ��������� TablePanel �� �����
            Controls.Add(tablePanel);
        }

    }
}
