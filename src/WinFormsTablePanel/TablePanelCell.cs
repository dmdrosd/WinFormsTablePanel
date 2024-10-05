namespace WinFormsTablePanel;

// TablePanelCell.cs
public class TablePanelCell
{
    public Control? Control { get; set; }
    public TablePanel? NestedTable { get; set; }  // Возможность вложенной таблицы
    public string? Text { get; set; }  // Текст для отображения внутри ячейки
    public Color BackColor { get; set; } = Color.Transparent;  // Цвет фона ячейки
    public TablePanelEntityStyle Style { get; set; } = TablePanelEntityStyle.Relative;
    public float Size { get; set; } = 0;  // Размер ячейки в зависимости от стиля
    public bool Visible { get; set; } = true;
}