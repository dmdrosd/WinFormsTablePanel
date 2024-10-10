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
        var newRow = new TablePanelRow(style, height, visible)
        {
            Cells = cells ?? new List<TablePanelCell>()
        };

        // Добавляем новую строку перед пересчетом статусов
        rows.Add(newRow);

        // Пересчитываем статусы строк
        RecalculateRowStatuses();

        // Остальная часть вашего кода
    }

    private void RecalculateRowStatuses()
    {
        bool foundFill = false;

        foreach (var row in rows)
        {
            if (row.Style == TablePanelEntityStyle.Fill)
            {
                row.Status = RowStatus.Fill;
                foundFill = true;
            }
            else if (foundFill)
            {
                row.Status = RowStatus.Bottom;
            }
            else
            {
                row.Status = RowStatus.Top;
            }
        }
    }


    private void BuildLayout()
    {
        if (ClientSize.Height == 0)
        {
            // Контрол еще не измерен
            return;
        }

        Controls.Clear(); // Очищаем предыдущие элементы

        int totalAbsoluteHeight = 0;
        float totalRelativeWeight = 0f;

        List<TablePanelRow> topRows = new List<TablePanelRow>();
        List<TablePanelRow> bottomRows = new List<TablePanelRow>();
        TablePanelRow fillRow = null;

        // Разделяем строки по стилю и статусу
        foreach (var row in rows)
        {
            if (row.Style == TablePanelEntityStyle.Absolute)
            {
                totalAbsoluteHeight += (int)row.Height;
                if (row.Status == RowStatus.Bottom)
                    bottomRows.Add(row);
                else
                    topRows.Add(row);
            }
            else if (row.Style == TablePanelEntityStyle.Relative)
            {
                totalRelativeWeight += row.Height;
                if (row.Status == RowStatus.Bottom)
                    bottomRows.Add(row);
                else
                    topRows.Add(row);
            }
            else if (row.Style == TablePanelEntityStyle.Fill)
            {
                fillRow = row;
            }
        }

        // Вычисляем доступную высоту для относительных строк
        int availableHeight = ClientSize.Height - totalAbsoluteHeight;

        // Вычисляем высоту для относительных строк
        foreach (var row in topRows.Concat(bottomRows))
        {
            if (row.Style == TablePanelEntityStyle.Relative)
            {
                row.CalculatedHeight = (int)(availableHeight * (row.Height / totalRelativeWeight));
            }
        }

        // Добавляем нижние строки в обратном порядке
        for (int i = bottomRows.Count - 1; i >= 0; i--)
        {
            BuildRow(bottomRows[i], true);
        }

        // Добавляем Fill-строку
        if (fillRow != null)
        {
            BuildRow(fillRow, false);
        }

        // Добавляем верхние строки в прямом порядке
        for (int i = 0; i < topRows.Count; i++)
        {
            BuildRow(topRows[i], false);
        }
    }


    private void BuildRow(TablePanelRow row, bool isBottom)
    {
        Panel rowPanel = new Panel
        {
            BackColor = row.Visible ? Color.LightBlue : Color.Transparent
        };

        switch (row.Style)
        {
            case TablePanelEntityStyle.Absolute:
                rowPanel.Height = (int)row.Height;
                rowPanel.Dock = isBottom ? DockStyle.Bottom : DockStyle.Top;
                break;
            case TablePanelEntityStyle.Relative:
                rowPanel.Height = row.CalculatedHeight;
                rowPanel.Dock = isBottom ? DockStyle.Bottom : DockStyle.Top;
                break;
            case TablePanelEntityStyle.Fill:
                rowPanel.Dock = DockStyle.Fill;
                break;
            default:
                rowPanel.Dock = isBottom ? DockStyle.Bottom : DockStyle.Top;
                break;
        }

        Controls.Add(rowPanel);
        BuildCells(row, rowPanel);
    }


    private void BuildCells(TablePanelRow row, Panel rowPanel)
    {
        // Проверяем, является ли текущий уровень вертикальным или горизонтальным
        bool isHorizontalLayout = rowPanel.Dock == DockStyle.Left || rowPanel.Dock == DockStyle.Right || rowPanel.Dock == DockStyle.Fill;

        // Список элементов для добавления в правильном порядке
        var elements = new List<Control>();

        for (int i = 0; i < row.Cells.Count; i++)
        {
            var cell = row.Cells[i];

            // Если это разделитель, создаем сплиттер и добавляем в список
            if (cell.Style == TablePanelEntityStyle.Separator)
            {
                var splitter = CreateSplitter(cell, isHorizontalLayout);
                elements.Add(splitter);
                continue;
            }

            // Создаем панель ячейки
            var cellPanel = BuildCell(cell);
            elements.Add(cellPanel);
        }

        // Добавляем элементы в rowPanel.Controls в правильном порядке
        foreach (var element in elements)
        {
            rowPanel.Controls.Add(element);
            element.BringToFront();
        }
    }

    private Splitter CreateSplitter(TablePanelCell cell, bool isHorizontalLayout)
    {
        var splitter = new Splitter
        {
            BackColor = Color.Gray
        };

        if (isHorizontalLayout)
        {
            // Вертикальный сплиттер
            splitter.Dock = DockStyle.Left;
            splitter.Width = 6;
        }
        else
        {
            // Горизонтальный сплиттер
            splitter.Dock = DockStyle.Top;
            splitter.Height = 6;
        }

        return splitter;
    }




    private Panel BuildCell(TablePanelCell cell)
    {
        Panel cellPanel = new Panel
        {
            BackColor = cell.BackColor,
            Visible = cell.Visible,
            Dock = cell.Dock // Используем свойство Dock ячейки
        };

        if (cell.ChildStructure != null)
        {
            // Создаем вложенный TablePanel и применяем структуру
            var nestedTablePanel = new TablePanel
            {
                Dock = DockStyle.Fill
            };
            nestedTablePanel.ApplyStructure(cell.ChildStructure);
            cellPanel.Controls.Add(nestedTablePanel);
        }
        else if (!string.IsNullOrEmpty(cell.Text))
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
