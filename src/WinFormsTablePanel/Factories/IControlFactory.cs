namespace WinFormsTablePanel.Factories;

public interface IControlFactory
{
    Control CreateControl(global::TablePanelEntity entity);
}