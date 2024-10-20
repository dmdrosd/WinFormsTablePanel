using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsTablePanel.PanelElements
{
    public abstract class PanelElement
    {
        // Абстрактный метод для получения всех контролов
        public abstract IEnumerable<Control> GetAllControls();
        public Panel Control { get; set; }
    }
}