using WinFormsTablePanel.Parts;

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

    public Control CreateSplitter(TablePanelEntity entity, DockStyle dockStyle)
    {
        return new Splitter
        {
            Name = entity.Name,
            Dock = dockStyle,
            BackColor = Color.Gray,
            Width = dockStyle is DockStyle.Left or DockStyle.Right ? 6 : 0,
            Height = dockStyle is DockStyle.Top or DockStyle.Bottom ? 6 : 0
        };
    }

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
