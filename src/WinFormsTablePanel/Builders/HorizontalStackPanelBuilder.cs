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
            var cellBuilder = new CellBuilder(cell);
            var cellControls = cellBuilder.Build();

            foreach (var control in cellControls)
            {
                if (cell.Style == TablePanelEntityStyle.Absolute)
                {
                    control.Width = (int)cell.Width;
                }

                control.Dock = DockStyle.Left;
                panel.Controls.Add(control);
            }
        }

        return new List<Control> { panel };
    }
}