using System.ComponentModel;
using WinFormsTablePanel;


    public class TablePanelRow : TablePanelEntity
    {
        public TablePanelRow(TablePanelEntityStyle style, float height, bool visible)
            : base(style, visible)
        {
            this.Height = height;
            this.Cells = new List<TablePanelCell>();
        }

        public float Height { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<TablePanelCell> Cells { get; set; }

        /// <summary>
        /// Убедимся, что количество ячеек соответствует количеству столбцов.
        /// Добавляем null, если ячеек меньше, чем столбцов.
        /// </summary>
        public void EnsureCells(int columnCount)
        {
            while (Cells.Count < columnCount)
            {
                Cells.Add(null); // Добавляем пустые ячейки
            }
        }
    }