namespace WinFormsTablePanel.Factories;

public interface IControlFactory
{
    Control CreatePanel(TablePanelRow row, DockStyle dockStyle);
    Control CreateSplitter(TablePanelRow row);
}