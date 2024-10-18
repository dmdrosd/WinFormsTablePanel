using WinFormsTablePanel.Builders;
using WinFormsTablePanel;

public class HorizontalStackPanelBuilder : IPanelBuilder
{
    private readonly List<TablePanelCell> _cells;

    public HorizontalStackPanelBuilder(List<TablePanelCell> cells)
    {
        _cells = cells;
    }

    public IEnumerable<Control> Build()
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill
        };

        foreach (var cell in _cells)
        {
            Control control = CreateControlFromCell(cell);

            if (cell.Style == TablePanelEntityStyle.Absolute)
            {
                control.Width = (int)cell.Width;
                control.Dock = DockStyle.Left;
            }
            else if (cell.Style == TablePanelEntityStyle.Fill)
            {
                control.Dock = DockStyle.Fill;
            }

            panel.Controls.Add(control);

            // Добавляем разделитель, если ячейка является Separator
            if (cell.Style == TablePanelEntityStyle.Separator)
            {
                var splitter = new Splitter
                {
                    Dock = DockStyle.Left,
                    Width = (int)(cell.Width > 0 ? cell.Width : 6),
                    BackColor = Color.Gray
                };
                panel.Controls.Add(splitter);
            }
        }

        return new List<Control> { panel };
    }

    private Control CreateControlFromCell(TablePanelCell cell)
    {
        if (cell.Style == TablePanelEntityStyle.Separator)
        {
            var splitter = new Splitter
            {
                Dock = DockStyle.Left,
                Width = (int)(cell.Width > 0 ? cell.Width : 6),
                BackColor = Color.Gray
            };
            return splitter;
        }
        else if (cell.ChildStructure != null)
        {
            var nestedBuilder = new TablePanelBuilder(cell.ChildStructure);
            var nestedControls = nestedBuilder.Build();

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = cell.BackColor
            };

            foreach (var nestedControl in nestedControls)
            {
                panel.Controls.Add(nestedControl);
            }

            return panel;
        }
        else if (!string.IsNullOrEmpty(cell.Name))
        {
            return new Label
            {
                Text = cell.Name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = cell.BackColor
            };
        }
        else if (cell.Control != null)
        {
            cell.Control.Dock = DockStyle.Fill;
            return cell.Control;
        }
        else
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = cell.BackColor
            };
        }
    }
}