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



    public void AddColumn(TablePanelEntityStyle style, float width, bool visible = true)
    {
        var newColumn = new TablePanelColumn(style, width, visible);
        columns.Add(newColumn);
        foreach (var row in rows)
        {
            row.Cells.Add(null);
        }
    }

    public void ApplyStructure(TablePanelStructure structure)
    {
        foreach (var row in structure.Rows)
        {
            AddRow(row.Style, row.Height, row.Visible, row.Cells);
        }

        BuildLayout();
    }


    public void AddRow(TablePanelEntityStyle style, float height, bool visible, List<TablePanelCell> cells)
    {
        var rowIndex = rows.Count;
        var newRow = new TablePanelRow(style, height, visible)
        {
            Cells = cells ?? new List<TablePanelCell>()
        };
        rows.Add(newRow);

        // Добавляем ячейки в строку
        for (int columnIndex = 0; columnIndex < newRow.Cells.Count; columnIndex++)
        {
            var cell = newRow.Cells[columnIndex];
            if (cell != null)
            {
                Panel cellPanel = new Panel
                {
                    BackColor = cell.BackColor,
                    Dock = DockStyle.Left,
                    Visible = cell.Visible
                };

                if (!string.IsNullOrEmpty(cell.Text))
                {
                    Label label = new Label
                    {
                        Text = cell.Text,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    cellPanel.Controls.Add(label);
                }

                AddCell(rowIndex, columnIndex, cellPanel);
            }
        }
    }
    public void AddCell(int row, int column, Control control)
    {
        if (row >= rows.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row index is out of range.");
        }

        var targetRow = rows[row];

        // Убедимся, что количество ячеек в строке соответствует количеству столбцов
        while (targetRow.Cells.Count <= column)
        {
            targetRow.Cells.Add(new TablePanelCell()); // Добавляем пустые ячейки, если их не хватает
        }

        // Устанавливаем Control в правильную ячейку
        targetRow.Cells[column].Control = control;
    }


    private void BuildLayout()
    {
        Controls.Clear(); // Очищаем все предыдущие элементы

        // Проходим по строкам в обратном порядке и создаём их
        for (int rowIndex = rows.Count - 1; rowIndex >= 0; rowIndex--)
        {
            var row = rows[rowIndex];
            BuildRow(row); // Строим строку
        }
    }


    private void BuildRow(TablePanelRow row)
    {
        Panel rowPanel = null;

        switch (row.Style)
        {
            case TablePanelEntityStyle.Absolute:
                rowPanel = new Panel
                {
                    Height = (int)row.Height,
                    Dock = DockStyle.Top,
                    BackColor = row.Visible ? Color.LightBlue : Color.Transparent
                };
                Controls.Add(rowPanel);
                break;

            case TablePanelEntityStyle.Relative:
                rowPanel = new Panel
                {
                    Dock = DockStyle.Top,
                    BackColor = row.Visible ? Color.LightGreen : Color.Transparent
                };
                Controls.Add(rowPanel);
                break;

            case TablePanelEntityStyle.Fill:
                rowPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = row.Visible ? Color.LightGreen : Color.Transparent
                };
                Controls.Add(rowPanel);
                break;

            case TablePanelEntityStyle.Separator:
                Splitter splitter = new Splitter
                {
                    Dock = DockStyle.Top,
                    Height = 6,
                    BackColor = Color.Gray
                };
                Controls.Add(splitter);
                return; // Пропускаем создание ячеек для разделителей
        }

        // Добавляем ячейки в строку
        BuildCells(row, rowPanel);
    }

    private void BuildCells(TablePanelRow row, Panel rowPanel)
    {
        bool previousWasSeparator = false;

        foreach (var cell in row.Cells.OfType<TablePanelCell>())
        {
            // Если предыдущий элемент был разделителем, добавляем его
            if (previousWasSeparator)
            {
                var splitter = new Splitter
                {
                    Dock = DockStyle.Left,
                    Width = 6,
                    BackColor = Color.Gray
                };
                rowPanel.Controls.Add(splitter);
                splitter.BringToFront(); // Убедимся, что сплиттер отображается поверх
                previousWasSeparator = false;
            }

            // Если это разделитель, отмечаем, но не строим панель
            if (cell.Style == TablePanelEntityStyle.Separator)
            {
                previousWasSeparator = true;
                continue;
            }

            // Если это обычная ячейка, строим панель и добавляем её
            var cellPanel = BuildCell(cell);
            rowPanel.Controls.Add(cellPanel);
            cellPanel.BringToFront(); // Убедимся, что панель отображается поверх
        }
    }

    private Panel BuildCell(TablePanelCell cell)
    {
        Panel cellPanel = new Panel
        {
            BackColor = cell.BackColor,
            Dock = DockStyle.Left,
            Visible = cell.Visible
        };

        if (!string.IsNullOrEmpty(cell.Text))
        {
            cellPanel.Controls.Add(new Label
            {
                Text = cell.Text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            });
        }
        else if (cell.Control != null)
        {
            cellPanel.Controls.Add(cell.Control);
        }

        return cellPanel;
    }
}
