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

    public void AddRow(TablePanelEntityStyle style, float height, bool visible = true)
    {
        var newRow = new TablePanelRow(style, height, visible);
        for (int i = 0; i < columns.Count; i++)
        {
            newRow.Cells.Add(null);
        }
        rows.Add(newRow);
    }

    public void AddColumn(TablePanelEntityStyle style, float width, bool visible = true)
    {
        var newColumn = new TablePanelColumn(style, width, visible);
        columns.Add(newColumn);
        foreach (var row in rows)
        {
            row.Cells.Add(null);
        }
    }

    public void SetCell(Control control, int row, int column)
    {
        if (row >= rows.Count || column >= columns.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row or column index is out of range.");
        }

        var targetRow = rows[row];
        targetRow.Cells[column] = control;
        Controls.Add(control);
    }

    public void AddCell(int row, int column, Control control)
    {
        if (row >= rows.Count || column >= columns.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row or column index is out of range.");
        }

        rows[row].Cells[column] = control;
    }

    public void BuildLayout()
    {
        Controls.Clear();

        // Создание строк и ячеек
        foreach (var row in rows)
        {
            Panel rowPanel = null;

            switch (row.Style)
            {
                case TablePanelEntityStyle.Absolute:
                    rowPanel = new()
                    {
                        Height = (int)row.Height,
                        Dock = DockStyle.Top,
                        BackColor = row.Visible ? Color.LightBlue : Color.Transparent
                    };
                    Controls.Add(rowPanel);
                    break;
                case TablePanelEntityStyle.Relative:
                case TablePanelEntityStyle.Fill:
                    rowPanel = new()
                    {
                        Dock = DockStyle.Top,
                        BackColor = row.Visible ? Color.LightGreen : Color.Transparent
                    };
                    Controls.Add(rowPanel);
                    break;
                case TablePanelEntityStyle.Separator:
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
            }

            // Добавление ячеек в строки
            for (var columnIndex = 0; columnIndex < columns.Count; columnIndex++)
            {
                var cellControl = row.Cells[columnIndex];
                if (cellControl != null)
                {
                    cellControl.Dock = DockStyle.Left;
                    rowPanel.Controls.Add(cellControl);
                }
            }
        }

        // Создание колонок
        foreach (var column in columns)
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
                case TablePanelEntityStyle.Relative:
                case TablePanelEntityStyle.Fill:
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
