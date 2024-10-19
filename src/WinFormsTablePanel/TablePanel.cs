using WinFormsTablePanel.Parts;

public class TablePanel : UserControl
{
    public void ApplyStructure(TablePanelStructure structure)
    {
        Controls.Clear();

        var builder = new TablePanelBuilder(structure);
        var controls = builder.Build();

        foreach (var control in controls)
        {
            Controls.Add(control);
        }
    }
}