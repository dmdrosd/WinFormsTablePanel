using WinFormsTablePanel;

public class RowBuilder : IPanelBuilder
{
    private readonly TablePanelRow _row;
    private readonly DockStyle _dockStyle;

    public RowBuilder(TablePanelRow row, DockStyle dockStyle)
    {
        _row = row;
        _dockStyle = dockStyle;
    }

    public IEnumerable<Control> Build()
    {
        var controls = new List<Control>();

        if (_row.Style == TablePanelEntityStyle.Separator)
        {
            // Создаем сплиттер
            var splitter = new Splitter
            {
                Name = _row.Name,
                Height = (int)(_row.Height > 0 ? _row.Height : 6),
                Dock = _dockStyle,
                BackColor = Color.Gray
            };
            controls.Add(splitter);
        }
        else
        {
            // Создаем панель для строки
            var panel = new Panel
            {
                Name = _row.Name,
                Dock = _dockStyle,
                BackColor = GetRowColor(_row),
                BorderStyle = BorderStyle.FixedSingle // Добавим рамку для наглядности
            };

            // Устанавливаем высоту для Absolute
            if (_row.Style == TablePanelEntityStyle.Absolute)
            {
                panel.Height = (int)_row.Height;
            }

            // Добавляем метку с названием панели
            var label = new Label
            {
                Text = _row.Name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);

            controls.Add(panel);
        }

        return controls;
    }

    private Color GetRowColor(TablePanelRow row)
    {
        // Устанавливаем цвет в зависимости от стиля строки
        return row.Style switch
        {
            TablePanelEntityStyle.Absolute => Color.LightBlue,
            TablePanelEntityStyle.Relative => Color.LightGreen,
            TablePanelEntityStyle.Fill => Color.LightYellow,
            _ => Color.LightGray
        };
    }
}
