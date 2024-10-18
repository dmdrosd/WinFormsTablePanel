namespace WinFormsTablePanel.Builders;

public class ControlFactory
{
    public Control CreatePanel(TablePanelRow row, DockStyle dockStyle)
    {
        var panel = new Panel
        {
            Name = row.Name,
            Dock = dockStyle,
            Height = row.Style == TablePanelEntityStyle.Absolute ? (int)row.Height : 0,
            BackColor = row.BackColor ?? Color.LightGray
        };

        if (row.Cells?.Any() != true)
        {
            return panel;
        }

        foreach (var cell in row.Cells)
        {
            panel.Controls.Add(CreateCellControl(cell));
        }

        return panel;
    }

    public Control CreateSplitter(string splitterName, DockStyle dockStyle, int height)
    {
        return new Splitter
        {
            Name = splitterName,
            Dock = dockStyle,
            Height = height > 0 ? height : 6,
            BackColor = Color.Gray
        };
    }

    private Control CreateCellControl(TablePanelCell cell)
    {
        var panel = new Panel
        {
            BackColor = cell.BackColor,
            Dock = DockStyle.Fill,
            Visible = cell.Visible
        };

        if (!string.IsNullOrEmpty(cell.Text))
        {
            panel.Controls.Add(new Label
            {
                Text = cell.Text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            });
        }
        else if (cell.Control != null)
        {
            panel.Controls.Add(cell.Control);
        }

        return panel;
    }
}