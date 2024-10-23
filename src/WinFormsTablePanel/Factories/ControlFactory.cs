using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Factories;

public class ControlFactory
{
    private static readonly Random Random = new Random();

    public Panel CreatePanel(TablePanelRow row, DockStyle dockStyle)
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

    public Panel CreatePanel(TablePanelCell cell, DockStyle dockStyle)
    {
        var panel = new Panel
        {
            Name = cell.Name,
            Dock = dockStyle,
            BackColor = cell.BackColor != Color.Empty ? cell.BackColor : GetRandomColor(),
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

    private Color GetRowColor(TablePanelRow row) =>
        row.Style switch
        {
            TablePanelEntityStyle.Absolute => GetRandomColor(),
            TablePanelEntityStyle.Relative => GetRandomColor(),
            TablePanelEntityStyle.Fill => Color.LightYellow,
            _ => GetRandomColor()
        };

    private Color GetRandomColor()
    {
        return Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
    }
}
