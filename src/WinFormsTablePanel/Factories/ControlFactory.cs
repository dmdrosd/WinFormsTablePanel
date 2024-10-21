using WinFormsTablePanel.Parts;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsTablePanel;

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

    private Color GetRowColor(TablePanelRow row) =>
        row.Style switch
        {
            TablePanelEntityStyle.Absolute => Color.LightBlue,
            TablePanelEntityStyle.Relative => Color.LightGreen,
            TablePanelEntityStyle.Fill => Color.LightYellow,
            _ => Color.LightGray
        };
}