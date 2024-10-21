namespace WinFormsTablePanel.Factories;

public interface IControlFactory
{
    Control CreateControl(global::WinFormsTablePanel.Parts.TablePanelEntity entity);
}