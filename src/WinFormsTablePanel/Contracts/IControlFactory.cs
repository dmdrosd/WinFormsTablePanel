namespace WinFormsTablePanel.Contracts;

public interface IControlFactory
{
    Control CreateControl(Parts.TablePanelEntity entity);
}