using System.ComponentModel;

namespace WinFormsTablePanel;

public class TablePanel : UserControl
{
    private TablePanelRowCollection rows;
    private TablePanelColumnCollection columns;

    public TablePanel()
    {
        this.rows = new TablePanelRowCollection();
        this.columns = new TablePanelColumnCollection();
        this.Dock = DockStyle.Fill;
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
        columns.Add(new TablePanelColumn(style, width, visible));
        foreach (var row in rows)
        {
            row.Cells.Add(null);
        }
    }

    public void SetCell(Control control, int row, int column)
    {
        if (row >= rows.Count || column >= columns.Count)
            throw new ArgumentOutOfRangeException("Row or column index is out of range.");

        var targetRow = rows[row];
        targetRow.Cells[column] = control;
        this.Controls.Add(control);
    }

    public void BuildLayout()
    {
        this.Controls.Clear();

        foreach (var row in rows)
        {
            if (this.Controls.Count > 0)
            {
                Splitter splitter = new Splitter
                {
                    Dock = DockStyle.Top,
                    Height = 6,
                    BackColor = Color.Gray
                };
                this.Controls.Add(splitter);
            }

            Panel rowPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = row.Style == TablePanelEntityStyle.Absolute ? (int)row.Height : 0,
                BackColor = row.Visible ? Color.LightBlue : Color.Transparent,
                AutoSize = row.Style == TablePanelEntityStyle.AutoSize
            };
            this.Controls.Add(rowPanel);

            foreach (var cell in row.Cells)
            {
                if (cell != null)
                {
                    cell.Dock = DockStyle.Left;
                    rowPanel.Controls.Add(cell);
                }
            }
        }
    }
}