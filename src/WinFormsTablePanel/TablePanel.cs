// TablePanel.cs
using System.ComponentModel;

namespace WinFormsTablePanel;

public sealed class TablePanel : UserControl
{
    private readonly TablePanelRowCollection _rows;
    private readonly TablePanelColumnCollection _columns;

    public TablePanel()
    {
        _rows = new TablePanelRowCollection();
        _columns = new TablePanelColumnCollection();
        Dock = DockStyle.Fill;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TablePanelRowCollection Rows => _rows;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TablePanelColumnCollection Columns => _columns;

    public DefaultBoolean ShowGrid { get; set; } = DefaultBoolean.Default;

    public void AddRow(TablePanelEntityStyle style, float height, bool visible = true)
    {
        var newRow = new TablePanelRow(style, height, visible);
        for (var i = 0; i < _columns.Count; i++)
        {
            newRow.Cells.Add(null);
        }
        _rows.Add(newRow);
    }

    public void AddColumn(TablePanelEntityStyle style, float width, bool visible = true)
    {
        var newColumn = new TablePanelColumn(style, width, visible);
        _columns.Add(newColumn);
        foreach (var row in _rows)
        {
            row.Cells.Add(null);
        }
    }

    public void SetCell(Control control, int row, int column)
    {
        if (row >= _rows.Count || column >= _columns.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row or column index is out of range.");
        }

        var targetRow = _rows[row];
        targetRow.Cells[column] = control;
        Controls.Add(control);
    }

    public void BuildLayout()
    {
        Controls.Clear();

        // Создание строк
        foreach (var row in _rows)
        {
            if (row.Style == TablePanelEntityStyle.Absolute)
            {
                Panel rowPanel = new()
                {
                    Height = (int)row.Height,
                    Dock = DockStyle.Top,
                    BackColor = row.Visible ? Color.LightBlue : Color.Transparent
                };
                Controls.Add(rowPanel);
            }
            else if (row.Style == TablePanelEntityStyle.Relative || row.Style == TablePanelEntityStyle.Fill)
            {
                Panel rowPanel = new()
                {
                    Dock = DockStyle.Top,
                    BackColor = row.Visible ? Color.LightGreen : Color.Transparent
                };
                Controls.Add(rowPanel);
            }
            else if (row.Style == TablePanelEntityStyle.Separator)
            {
                Splitter splitter = new()
                {
                    Dock = DockStyle.Top,
                    Height = 6,
                    BackColor = Color.Gray
                };
                Controls.Add(splitter);
            }
        }

        // Создание колонок
        foreach (var column in _columns)
        {
            switch (column.Style)
            {
                case TablePanelEntityStyle.Absolute:
                {
                    Panel colPanel = new()
                    {
                        Width = (int)column.Width,
                        Dock = DockStyle.Left,
                        BackColor = column.Visible ? Color.LightYellow : Color.Transparent
                    };
                    Controls.Add(colPanel);
                    break;
                }
                case TablePanelEntityStyle.Relative or TablePanelEntityStyle.Fill:
                {
                    Panel colPanel = new()
                    {
                        Dock = DockStyle.Left,
                        BackColor = column.Visible ? Color.LightCoral : Color.Transparent
                    };
                    Controls.Add(colPanel);
                    break;
                }
                case TablePanelEntityStyle.Separator:
                {
                    Splitter splitter = new()
                    {
                        Dock = DockStyle.Left,
                        Width = 6,
                        BackColor = Color.Gray
                    };
                    Controls.Add(splitter);
                    break;
                }
            }
        }
    }
}