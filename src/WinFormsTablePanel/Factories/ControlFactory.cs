using WinFormsTablePanel.PanelElements;

public class ControlFactory
{
    public PanelElement CreatePanelWithSplitter(TablePanelElementInfo elementInfo)
    {
        var panel = CreatePanel(elementInfo);

        if (elementInfo.HasSplitter)
        {
            var splitter = CreateSplitter(elementInfo);

            // Возвращаем композитный элемент, состоящий из панели и сплиттера
            return new CompositePanelElement([ panel, splitter ]);
        }

        return panel;
    }

    public PanelElement CreatePanel(TablePanelElementInfo elementInfo)
    {
        var panel = new Panel
        {
            Dock = elementInfo.Dock, // Устанавливаем Dock по умолчанию, если не задано иное
            BackColor = elementInfo.BackColor != Color.Empty ? elementInfo.BackColor : GenerateRandomColor(), // Генерация случайного цвета, если цвет не задан
            Name = elementInfo.Name ?? "UnnamedPanel",
            Height = 100,//elementInfo.Height,
            BorderStyle = BorderStyle.None
        };

        // Создаем метку для отображения имени панели
        var label = new Label
        {
            Text = elementInfo.Name?? "UnnamedPanel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };

        panel.Controls.Add(label);

        return new LeafPanelElement(panel);
    }


    public PanelElement CreateSplitter(TablePanelElementInfo elementInfo)
    {
        var splitter = new Splitter
        {
            Dock = DockStyle.Top, // Здесь Dock может быть изменен, если потребуется синхронизация с панелью
            Height = 6, //elementInfo.SplitterHeight > 0 ? elementInfo.SplitterHeight : 6,
            BackColor = Color.Gray,
            Name = elementInfo.Name + "_splitter" ?? "UnnamedSplitter"
        };

        return new LeafPanelElement(splitter);
    }

    public CompositePanelElement CreateCompositePanel(IEnumerable<TablePanelElementInfo> elementsInfo)
    {
        var panelElements = elementsInfo.Select(CreatePanelWithSplitter);

        return new CompositePanelElement(panelElements);
    }

    public Color GenerateRandomColor()
    {
        Random random = new Random();
        int red = random.Next(0, 256);
        int green = random.Next(0, 256);
        int blue = random.Next(0, 256);

        return Color.FromArgb(red, green, blue);
    }

}
