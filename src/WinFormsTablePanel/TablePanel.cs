// TablePanel.cs
using System.ComponentModel;

namespace WinFormsTablePanel;

public class TablePanel : UserControl
{
    private readonly TablePanelRowCollection rows;
    private readonly TablePanelColumnCollection columns;

    public TablePanel()
    {
        rows = new TablePanelRowCollection();
        columns = new TablePanelColumnCollection();
        Dock = DockStyle.Fill;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TablePanelRowCollection Rows => rows;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TablePanelColumnCollection Columns => columns;

    public DefaultBoolean ShowGrid { get; set; } = DefaultBoolean.Default;

    public void SetCell(Control control, int row, int column)
    {
        if (row >= rows.Count || column >= columns.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row or column index is out of range.");
        }

        rows[row].Cells[column] = new TablePanelCell { Control = control };
    }

    public void BuildLayout(TablePanelStructure structure)
    {
        rows.Clear();
        columns.Clear();

        foreach (var row in structure.Rows)
        {
            rows.Add(row);
        }

        Controls.Clear();

        // Создание строк и ячеек
        for (int i = rows.Count - 1; i >= 0; i--)
        {
            var row = rows[i];
            Panel? rowPanel = null;

            if (row.Style == TablePanelEntityStyle.Absolute)
            {
                rowPanel = new()
                {
                    Height = (int)row.Height,
                    Dock = DockStyle.Top,
                    BackColor = row.Visible ? Color.CornflowerBlue : Color.Transparent
                };
                Controls.Add(rowPanel);
            }
            else if (row.Style == TablePanelEntityStyle.Relative)
            {
                rowPanel = new()
                {
                    Dock = DockStyle.Top,
                    BackColor = row.Visible ? Color.MediumSeaGreen : Color.Transparent
                };
                Controls.Add(rowPanel);
            }
            else if (row.Style == TablePanelEntityStyle.Fill)
            {
                rowPanel = new()
                {
                    Dock = DockStyle.Fill,
                    BackColor = row.Visible ? Color.LightSlateGray : Color.Transparent
                };
                Controls.Add(rowPanel);
            }
            else if (row.Style == TablePanelEntityStyle.Separator && i != rows.Count - 1)
            {
                Splitter splitter = new()
                {
                    Dock = DockStyle.Top,
                    Height = 6,
                    BackColor = Color.Gray
                };
                Controls.Add(splitter);
                continue;
            }

            if (rowPanel != null)
            {
                // Добавление ячеек в строки
                foreach (var cell in row.Cells)
                {
                    var cellControl = cell?.Control;
                    if (cellControl != null)
                    {
                        cellControl.Dock = DockStyle.Left;
                        rowPanel.Controls.Add(cellControl);
                    }
                }
            }
        }
    }
}
