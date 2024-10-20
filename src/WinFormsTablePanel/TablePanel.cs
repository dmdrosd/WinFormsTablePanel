using WinFormsTablePanel.Parts;

public class TablePanel : UserControl
{
    private readonly ControlFactory _controlFactory;

    public TablePanel()
    {
        _controlFactory = new ControlFactory();
    }

    public void ApplyStructure(TablePanelStructure structure)
    {
        Controls.Clear();

        // Преобразуем структуру
        var panelProcessor = new TablePanelProcessor();
        var processedStructure = panelProcessor.ProcessStructure(structure, Height);

        // Создаём билдер с обработанной структурой
        var builder = new TablePanelBuilder(processedStructure, _controlFactory);

        // Добавляем композитные элементы
        var elements = builder.Build();

        var xx = new List<Control>();
        foreach (var element in elements)
        {
            var controls = element.GetAllControls().ToArray();

            xx.AddRange(controls); 
            Controls.AddRange(controls);
        }

        var xxx = xx.ToArray();
    }
}