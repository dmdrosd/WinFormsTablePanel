using WinFormsTablePanel;
using WinFormsTablePanel.Parts;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly TablePanelStructure _tablePanelRowsStructure = new()
        {
            Rows =
            [
                // Header
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Header")
                {
                    BackColor = Color.IndianRed
                },

                // Main Content Row
                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Main Content")
                {
                    Cells =
                    [
                        // SideMenu Cell
                        new TablePanelCell("SideMenu", TablePanelEntityStyle.Absolute, 200)
                        {
                            BackColor = Color.LightBlue
                        },

                        // Separator between SideMenu and Nested Structure
                        new TablePanelCell("Separator1", TablePanelEntityStyle.Separator, 0),

                        // Cell containing nested rows
                        new TablePanelCell("NestedRows", TablePanelEntityStyle.Fill, 0)
                        {
                            ChildStructure = new TablePanelStructure
                            {
                                Rows =
                                [
                                    // Row 1: MainMenu
                                    new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "MainMenu")
                                    {
                                        Cells =
                                        [
                                            new TablePanelCell("MainMenuCell", TablePanelEntityStyle.Fill, 0)
                                            {
                                                BackColor = Color.LightCoral
                                            }
                                        ]
                                    },

                                    // Row 2: Content and Properties
                                    new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Content")
                                    {
                                        Cells =
                                        [
                                            // Content Cell
                                            new TablePanelCell("ContentCell", TablePanelEntityStyle.Fill, 0)
                                            {
                                                BackColor = Color.White,
                                                ChildStructure = new TablePanelStructure
                                                {
                                                    Rows =
                                                    [
                                                        // Master Row
                                                        new TablePanelRow(TablePanelEntityStyle.Absolute, 150, true, "Master")
                                                        {
                                                            Cells =
                                                            [
                                                                new TablePanelCell("MasterCell", TablePanelEntityStyle.Fill, 0)
                                                                {
                                                                    BackColor = Color.LightYellow
                                                                }
                                                            ]
                                                        },

                                                        // Separator between Master and Detail
                                                        new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Master-Detail Splitter"),

                                                        // Detail Row
                                                        new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Detail")
                                                        {
                                                            Cells =
                                                            [
                                                                new TablePanelCell("DetailCell", TablePanelEntityStyle.Fill, 0)
                                                                {
                                                                    BackColor = Color.White
                                                                }
                                                            ]
                                                        }
                                                    ]
                                                }
                                            },

                                            // Separator between Content and Properties
                                            new TablePanelCell("Separator2", TablePanelEntityStyle.Separator, 0),

                                            // Properties Cell
                                            new TablePanelCell("Properties", TablePanelEntityStyle.Absolute, 200)
                                            {
                                                BackColor = Color.LightGray,
                                                ChildStructure = new TablePanelStructure
                                                {
                                                    Rows =
                                                    [
                                                        new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "PropertiesRow")
                                                        {
                                                            Cells =
                                                            [
                                                                new TablePanelCell("PropertiesCell", TablePanelEntityStyle.Fill, 0)
                                                                {
                                                                    BackColor = Color.LightGray
                                                                }
                                                            ]
                                                        }
                                                    ]
                                                }
                                            }
                                        ]
                                    },

                                    // Row 3: Footer (Optional)
                                    new TablePanelRow(TablePanelEntityStyle.Absolute, 30, true, "Inner Footer")
                                    {
                                        Cells =
                                        [
                                            new TablePanelCell("InnerFooterCell", TablePanelEntityStyle.Fill, 0)
                                            {
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
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Footer")
                {
                    BackColor = Color.LightGreen
                }
            ]
        };

        public Form1()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tablePanel = new TablePanel
            {
                Dock = DockStyle.Fill,
            };

            tablePanel.ApplyStructure(_tablePanelRowsStructure);

            Controls.Add(tablePanel);
        }
    }
}