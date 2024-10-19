using WinFormsTablePanel;
using WinFormsTablePanel.Factories;

public class ControlFactory : IControlFactory
{
    public Control CreateControl(TablePanelEntity entity)
    {
        if (entity.Style == TablePanelEntityStyle.Separator)
        {
            // Создаем сплиттер
            var splitter = new Splitter
            {
                Name = entity.Name,
                Dock = DockStyle.Top,
                Height = (int)(entity is TablePanelRow row ? row.Height : 6),
                BackColor = Color.Gray
            };
            return splitter;
        }
        else
        {
            // Создаем панель
            var panel = new Panel
            {
                Name = entity.Name,
                BackColor = GetPanelColor(entity),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Добавляем метку с именем панели
            var label = new Label
            {
                Text = entity.Name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);

            return panel;
        }
    }

    private Color GetPanelColor(TablePanelEntity entity)
    {
        // Назначаем цвета в зависимости от стиля или имени панели
        return entity.Style switch
        {
            TablePanelEntityStyle.Absolute => Color.LightBlue,
            TablePanelEntityStyle.Relative => Color.LightGreen,
            TablePanelEntityStyle.Fill => Color.LightYellow,
            _ => Color.LightGray
        };
    }
}