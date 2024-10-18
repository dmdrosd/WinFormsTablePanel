using WinFormsTablePanel;
using WinFormsTablePanel.Builders;

public class TablePanel : UserControl
{
    // Конструктор
    public TablePanel()
    {
        Dock = DockStyle.Fill;
    }

    public DefaultBoolean ShowGrid { get; set; }

    // Методы
    public void ApplyStructure(TablePanelStructure structure)
    {
        Controls.Clear();

        // Используем билдер для создания контролов на основе структуры
        var builder = new TablePanelBuilder(structure);
        var controls = builder.Build();

        // Добавляем контролы в TablePanel в порядке, заданном билдером
        foreach (var control in controls)
        {
            Controls.Add(control);
        }
    }
}