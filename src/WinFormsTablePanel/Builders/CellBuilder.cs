using WinFormsTablePanel;
using WinFormsTablePanel.Parts;

public class CellBuilder : IPanelBuilder
{
    private readonly TablePanelCell _cell;

    public CellBuilder(TablePanelCell cell)
    {
        _cell = cell;
    }

    public IEnumerable<Control> Build()
    {
        var controls = new List<Control>();

        // Создаем контрол ячейки
        Control control;

        // Создаем панель для ячейки
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = GetCellColor(_cell),
            BorderStyle = BorderStyle.FixedSingle // Добавим рамку для наглядности
        };

        // Добавляем метку с названием панели
        var label = new Label
        {
            Text = _cell.Name,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(label);

        control = panel;

        controls.Add(control);
        return controls;
    }

    private Color GetCellColor(TablePanelCell cell)
    {
        // Устанавливаем цвет в зависимости от стиля ячейки
        return cell.Style switch
        {
            TablePanelEntityStyle.Absolute => Color.LightBlue,
            TablePanelEntityStyle.Relative => Color.LightGreen,
            TablePanelEntityStyle.Fill => Color.LightYellow,
            _ => Color.LightGray
        };
    }
}