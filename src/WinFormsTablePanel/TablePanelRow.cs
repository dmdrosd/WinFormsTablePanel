using System.ComponentModel;
using WinFormsTablePanel;

public class TablePanelRow : TablePanelEntity
{
    public TablePanelRow(TablePanelEntityStyle style, float height, bool visible)
        : base(style, visible)
    {
        this.Height = height;
        this.Cells = new List<Control>();
    }

    public float Height { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<Control> Cells { get; }
}
