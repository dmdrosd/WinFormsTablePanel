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

            // Главная структура
            var structure = new TablePanelStructure
            {
                Rows = new List<TablePanelRow>
        {
            // Header
            new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true)
            {
                Cells = new List<TablePanelCell>
                {
                    new TablePanelCell
                    {
                        Text = "Header",
                        BackColor = Color.IndianRed,
                        Style = TablePanelEntityStyle.Fill,
                        Visible = true,
                        Dock = DockStyle.Fill
                    }
                }
            },
            // Main Content Row
            new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
            {
                Cells = new List<TablePanelCell>
                {
                    // SideMenu Cell
                    new TablePanelCell
                    {
                        Style = TablePanelEntityStyle.Absolute,
                        Width = 200,
                        Visible = true,
                        Dock = DockStyle.Left,
                        BackColor = Color.LightBlue,
                        ChildStructure = new TablePanelStructure
                        {
                            Rows = new List<TablePanelRow>
                            {
                                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                {
                                    Cells = new List<TablePanelCell>
                                    {
                                        new TablePanelCell
                                        {
                                            Text = "SideMenu",
                                            Style = TablePanelEntityStyle.Fill,
                                            Dock = DockStyle.Fill,
                                            Visible = true,
                                            BackColor = Color.LightBlue
                                        }
                                    }
                                }
                            }
                        }
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
                            Rows = new List<TablePanelRow>
                            {
                                // Row 1: MainMenu
                                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true)
                                {
                                    Cells = new List<TablePanelCell>
                                    {
                                        new TablePanelCell
                                        {
                                            Text = "MainMenu",
                                            Style = TablePanelEntityStyle.Fill,
                                            Dock = DockStyle.Fill,
                                            Visible = true,
                                            BackColor = Color.LightCoral
                                        }
                                    }
                                },
                                // Row 2: Content and Properties
                                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                {
                                    Cells = new List<TablePanelCell>
                                    {
                                        // Content Cell
                                        new TablePanelCell
                                        {
                                            Style = TablePanelEntityStyle.Fill,
                                            Dock = DockStyle.Fill,
                                            Visible = true,
                                            BackColor = Color.White,
                                            ChildStructure = new TablePanelStructure
                                            {
                                                Rows = new List<TablePanelRow>
                                                {
                                                    new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                                    {
                                                        Cells = new List<TablePanelCell>
                                                        {
                                                            // Content Cell
                                                            new TablePanelCell
                                                            {
                                                                Style = TablePanelEntityStyle.Fill,
                                                                Dock = DockStyle.Fill,
                                                                Visible = true,
                                                                BackColor = Color.White,
                                                                ChildStructure = new TablePanelStructure
                                                                {
                                                                    Rows = new List<TablePanelRow>
                                                                    {
                                                                        // Master Row
                                                                        new TablePanelRow(TablePanelEntityStyle.Absolute, 150, true)
                                                                        {
                                                                            Cells = new List<TablePanelCell>
                                                                            {
                                                                                new TablePanelCell
                                                                                {
                                                                                    Text = "Master",
                                                                                    Style = TablePanelEntityStyle.Fill,
                                                                                    Dock = DockStyle.Top,
                                                                                    Visible = true,
                                                                                    BackColor = Color.LightYellow
                                                                                }
                                                                            }
                                                                        },
                                                                        // Splitter between Master and Detail (Horizontal)
                                                                        new TablePanelRow(TablePanelEntityStyle.Separator, 6, true)
                                                                        {
                                                                            Cells = new List<TablePanelCell>
                                                                            {
                                                                                new TablePanelCell
                                                                                {
                                                                                    Style = TablePanelEntityStyle.Separator,
                                                                                    Dock = DockStyle.Top
                                                                                }
                                                                            }
                                                                        },
                                                                        // Detail Row
                                                                        new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                                                        {
                                                                            Cells = new List<TablePanelCell>
                                                                            {
                                                                                new TablePanelCell
                                                                                {
                                                                                    Text = "Detail",
                                                                                    Style = TablePanelEntityStyle.Fill,
                                                                                    Dock = DockStyle.Fill,
                                                                                    Visible = true,
                                                                                    BackColor = Color.White
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        // Separator between Content and Properties
                                        new TablePanelCell
                                        {
                                            Style = TablePanelEntityStyle.Separator
                                        },
                                        // Properties Cell
                                        new TablePanelCell
                                        {
                                            Style = TablePanelEntityStyle.Absolute,
                                            Width = 200,
                                            Dock = DockStyle.Right,
                                            Visible = true,
                                            BackColor = Color.LightGray,
                                            ChildStructure = new TablePanelStructure
                                            {
                                                Rows = new List<TablePanelRow>
                                                {
                                                    new TablePanelRow(TablePanelEntityStyle.Fill, 0, true)
                                                    {
                                                        Cells = new List<TablePanelCell>
                                                        {
                                                            new TablePanelCell
                                                            {
                                                                Text = "Properties",
                                                                Style = TablePanelEntityStyle.Fill,
                                                                Dock = DockStyle.Fill,
                                                                Visible = true,
                                                                BackColor = Color.LightGray
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                // Row 3: Footer
                                new TablePanelRow(TablePanelEntityStyle.Absolute, 30, true)
                                {
                                    Cells = new List<TablePanelCell>
                                    {
                                        new TablePanelCell
                                        {
                                            Text = "Footer",
                                            Style = TablePanelEntityStyle.Fill,
                                            Dock = DockStyle.Fill,
                                            Visible = true,
                                            BackColor = Color.LightGreen
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            // Footer
            new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true)
            {
                Cells = new List<TablePanelCell>
                {
                    new TablePanelCell
                    {
                        Text = "Footer",
                        BackColor = Color.LightGreen,
                        Style = TablePanelEntityStyle.Fill,
                        Visible = true,
                        Dock = DockStyle.Fill
                    }
                }
            }
        }
            };

            // Применяем структуру и строим макет
            tablePanel.ApplyStructure(structure);

            // Добавляем TablePanel на форму
            Controls.Add(tablePanel);
        }







    }
}