using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Factories;

public class ControlFactory
{
    public Control CreatePanel(TablePanelRow row, DockStyle dockStyle)
    {
        var panel = new Panel
        {
            Name = row.Name,
            Dock = dockStyle,
            BackColor = GetRowColor(row),
            BorderStyle = BorderStyle.FixedSingle
        };

        if (row.Style == TablePanelEntityStyle.Absolute)
        {
            panel.Height = (int)row.Height;
        }

        var label = new Label
        {
            Text = row.Name,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(label);

        return panel;
    }

    public Control CreateSplitter(TablePanelRow row, DockStyle dockStyle) =>
        new Splitter
        {
            Name = row.Name,
            Height = (int)(row.Height > 0 ? row.Height : 6),
            Dock = dockStyle,
            BackColor = Color.Gray
        };

    public Control CreatePanel(TablePanelCell cell, DockStyle dockStyle)
    {
        var panel = new Panel
        {
            Name = cell.Name,
            Dock = dockStyle,
            BackColor = cell.BackColor != Color.Empty ? cell.BackColor : Color.LightGray,
            BorderStyle = BorderStyle.FixedSingle,
            Width = cell.Style == TablePanelEntityStyle.Absolute ? (int)cell.Width : 0
        };

        if (cell.Control != null)
        {
            panel.Controls.Add(cell.Control);
        }
        else
        {
            var label = new Label
            {
                Text = cell.Name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);
        }

        return panel;
    }

    public Control CreateSplitter(TablePanelCell cell, DockStyle dockStyle) =>
        new Splitter
        {
            Name = cell.Name,
            Width = (int)(cell.Width > 0 ? cell.Width : 6),
            Dock = dockStyle,
            BackColor = Color.Gray
        };

    private Color GetRowColor(TablePanelRow row) =>
        row.Style switch
        {
            TablePanelEntityStyle.Absolute => Color.LightBlue,
            TablePanelEntityStyle.Relative => Color.LightGreen,
            TablePanelEntityStyle.Fill => Color.LightYellow,
            _ => Color.LightGray
        };
}
