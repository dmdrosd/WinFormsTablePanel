using WinFormsTablePanel;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly TablePanelStructure _tablePanelStructure = new()
        {
            Rows =
            [
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true)
                {
                    Cells =
                    [
                        new TablePanelCell
                        {
                            Text = "Header",
                            BackColor = Color.IndianRed,
                            Style = TablePanelEntityStyle.Fill,
                            Visible = true,
                            Dock = DockStyle.Fill
                        }
                    ]
                },
                // Main Content Row
                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                {
                    Cells =
                    [
                        new TablePanelCell
                        {
                            Text = "SideMenu",
                            Style = TablePanelEntityStyle.Absolute,
                            Width = 200,
                            Visible = true,
                            BackColor = Color.LightBlue
                        },
                        // Separator between SideMenu and Nested Structure
                        new TablePanelCell
                        {
                            Style = TablePanelEntityStyle.Separator
                        },
                        // Cell containing nested rows
                        new TablePanelCell
                        {
                            Style = TablePanelEntityStyle.Fill,
                            Visible = true,
                            Dock = DockStyle.Fill,
                            ChildStructure = new TablePanelStructure
                            {
                                Rows =
                                [
                                    new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true)
                                    {
                                        Cells =
                                        [
                                            new TablePanelCell
                                            {
                                                Text = "MainMenu",
                                                Style = TablePanelEntityStyle.Fill,
                                                Dock = DockStyle.Fill,
                                                Visible = true,
                                                BackColor = Color.LightCoral
                                            }
                                        ]
                                    },
                                    // Row 2: Content and Properties
                                    new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                    {
                                        Cells =
                                        [
                                            new TablePanelCell
                                            {
                                                Style = TablePanelEntityStyle.Fill,
                                                Dock = DockStyle.Fill,
                                                Visible = true,
                                                BackColor = Color.White,
                                                ChildStructure = new TablePanelStructure
                                                {
                                                    Rows =
                                                    [
                                                        new TablePanelRow(TablePanelEntityStyle.Absolute, 150, true)
                                                        {
                                                            Cells =
                                                            [
                                                                new TablePanelCell
                                                                {
                                                                    Text = "Master",
                                                                    Style = TablePanelEntityStyle.Fill,
                                                                    Dock = DockStyle.Fill,
                                                                    Visible = true,
                                                                    BackColor = Color.LightYellow
                                                                }
                                                            ]
                                                        },
                                                        // Separator between Master and Detail (Horizontal)
                                                        new TablePanelRow(TablePanelEntityStyle.Separator, 6, true),
                                                        // Detail Row
                                                        new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                                        {
                                                            Cells =
                                                            [
                                                                new TablePanelCell
                                                                {
                                                                    Text = "Detail",
                                                                    Style = TablePanelEntityStyle.Fill,
                                                                    Dock = DockStyle.Fill,
                                                                    Visible = true,
                                                                    BackColor = Color.White
                                                                }
                                                            ]
                                                        }
                                                    ]
                                                }
                                            },
                                            // Separator between Content and Properties (Vertical)
                                            new TablePanelCell
                                            {
                                                Style = TablePanelEntityStyle.Separator
                                            },
                                            // Properties Cell
                                            new TablePanelCell
                                            {
                                                Text = "Properties",
                                                Style = TablePanelEntityStyle.Absolute,
                                                Width = 200,
                                                Dock = DockStyle.Right,
                                                Visible = true,
                                                BackColor = Color.LightGray
                                            }
                                        ]
                                    },
                                    // Row 3: Footer (Optional)
                                    // Если этот футер не нужен, вы можете удалить этот блок
                                    new TablePanelRow(TablePanelEntityStyle.Absolute, 30, true)
                                    {
                                        Cells =
                                        [
                                            new TablePanelCell
                                            {
                                                Text = "Inner Footer",
                                                Style = TablePanelEntityStyle.Fill,
                                                Dock = DockStyle.Fill,
                                                Visible = true,
                                                BackColor = Color.Green
                                            }
                                        ]
                                    }
                                ]
                            }
                        }
                    ]
                },
                // Footer
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true)
                {
                    Cells =
                    [
                        new TablePanelCell
                        {
                            Text = "Footer",
                            BackColor = Color.LightGreen,
                            Style = TablePanelEntityStyle.Fill,
                            Visible = true,
                            Dock = DockStyle.Fill
                        }
                    ]
                }
            ]
        };
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
                ShowGrid = DefaultBoolean.True
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
