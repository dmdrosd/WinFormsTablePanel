using WinFormsTablePanel.PanelElements;

public interface IPanelBuilder
{
    IEnumerable<PanelElement> Build();
}