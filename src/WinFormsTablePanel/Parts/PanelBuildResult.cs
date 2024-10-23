namespace WinFormsTablePanel.Parts;

public class PanelBuildResult
{
    public List<Control> Controls { get; } = [];
    public Dictionary<string, Panel> NamedContainers { get; set; } = new();
    public Dictionary<string, Panel> NamedCells { get; set; } = new();
}