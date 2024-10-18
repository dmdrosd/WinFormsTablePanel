using WinFormsTablePanel;
using WinFormsTablePanel.Builders;

public class TablePanelBuilder : IPanelBuilder
{
    private readonly TablePanelStructure _structure;

    public TablePanelBuilder(TablePanelStructure structure)
    {
        _structure = structure;
    }

    public IEnumerable<Control> Build()
    {
        var controls = new List<Control>();

        foreach (var row in _structure.Rows)
        {
            IPanelBuilder rowBuilder;

            if (row.Cells.Count == 1)
            {
                rowBuilder = new SingleCellRowBuilder(row);
            }
            else
            {
                rowBuilder = new HorizontalStackPanelBuilder(row.Cells);
            }

            var rowControls = rowBuilder.Build();

            foreach (var control in rowControls)
            {
                if (row.Style == TablePanelEntityStyle.Absolute)
                {
                    control.Height = (int)row.Height;
                }

                controls.Add(control);
            }

            // Добавляем разделитель, если строка является Separator
            if (row.Style == TablePanelEntityStyle.Separator)
            {
                var splitter = new Splitter
                {
                    Dock = DockStyle.Top,
                    Height = (int)(row.Height > 0 ? row.Height : 6),
                    BackColor = Color.Gray
                };
                controls.Add(splitter);
            }
        }

        return controls;
    }
}