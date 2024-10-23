public class PanelBuildResult
{
    public List<Control> Controls { get; set; } = new();
    public Dictionary<string, Control> NamedContainers { get; set; } = new();
    public Dictionary<string, Control> NamedCells { get; set; } = new();
}