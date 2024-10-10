using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsTablePanel
{
    // Перечисления и классы структуры остаются без изменений

    public class TablePanel : UserControl
    {
        // Поля
        private TablePanelStructure _structure;

        // Конструктор
        public TablePanel()
        {
            Dock = DockStyle.Fill;
        }

        // Свойства
        public DefaultBoolean ShowGrid { get; set; }

        // Методы
        public void ApplyStructure(TablePanelStructure structure)
        {
            _structure = structure;
            BuildLayout();
        }

        private void BuildLayout()
        {
            Controls.Clear();

            // Создаем корневой контрол
            Control rootControl = BuildControlHierarchy(_structure);

            if (rootControl != null)
            {
                rootControl.Dock = DockStyle.Fill;
                Controls.Add(rootControl);
            }
        }

        private Control BuildControlHierarchy(TablePanelStructure structure)
        {
            if (structure.Rows == null || structure.Rows.Count == 0)
                return null;

            // Проверяем, есть ли в структуре разделитель
            int separatorIndex = structure.Rows.FindIndex(r => r.Style == TablePanelEntityStyle.Separator);

            if (separatorIndex == -1)
            {
                // Нет разделителя, используем TableLayoutPanel
                return CreateTableLayoutPanel(structure);
            }
            else
            {
                // Есть разделитель, используем SplitContainer
                var firstPart = new TablePanelStructure { Rows = structure.Rows.Take(separatorIndex).ToList() };
                var secondPart = new TablePanelStructure { Rows = structure.Rows.Skip(separatorIndex + 1).ToList() };

                var splitContainer = new SplitContainer
                {
                    Dock = DockStyle.Fill,
                    Orientation = Orientation.Horizontal,
                    SplitterWidth = (int)(structure.Rows[separatorIndex].Height > 0 ? structure.Rows[separatorIndex].Height : 6),
                    BackColor = Color.Gray
                };

                // Рекурсивно создаем панели для Panel1 и Panel2
                var panel1 = BuildControlHierarchy(firstPart);
                var panel2 = BuildControlHierarchy(secondPart);

                if (panel1 != null)
                    splitContainer.Panel1.Controls.Add(panel1);

                if (panel2 != null)
                    splitContainer.Panel2.Controls.Add(panel2);

                return splitContainer;
            }
        }

        private TableLayoutPanel CreateTableLayoutPanel(TablePanelStructure structure)
        {
            if (structure.Rows == null || structure.Rows.Count == 0)
                return null;

            var tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = structure.Rows.Count,
                ColumnCount = 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < structure.Rows.Count; i++)
            {
                var row = structure.Rows[i];

                // Устанавливаем размер строки
                if (row.Style == TablePanelEntityStyle.Absolute)
                {
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, row.Height));
                }
                else if (row.Style == TablePanelEntityStyle.Relative)
                {
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, row.Height));
                }
                else if (row.Style == TablePanelEntityStyle.Fill)
                {
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                }
                else if (row.Style == TablePanelEntityStyle.Separator)
                {
                    // Разделители внутри TableLayoutPanel отображаются как фиксированные панели
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, row.Height > 0 ? row.Height : 6));

                    // Добавляем разделитель как панель с заданным цветом
                    var separatorPanel = new Panel
                    {
                        BackColor = Color.Gray,
                        Dock = DockStyle.Fill
                    };
                    tableLayoutPanel.Controls.Add(separatorPanel, 0, i);
                    continue;
                }

                // Проверяем, есть ли в строке разделитель
                int cellSeparatorIndex = row.Cells.FindIndex(c => c.Style == TablePanelEntityStyle.Separator);

                Control rowControl;

                if (cellSeparatorIndex == -1)
                {
                    // Нет разделителя, создаем TableLayoutPanel
                    rowControl = CreateRowTableLayoutPanel(row);
                }
                else
                {
                    // Есть разделитель, используем SplitContainer
                    var firstPartCells = row.Cells.Take(cellSeparatorIndex).ToList();
                    var secondPartCells = row.Cells.Skip(cellSeparatorIndex + 1).ToList();

                    var cellSplitContainer = new SplitContainer
                    {
                        Dock = DockStyle.Fill,
                        Orientation = Orientation.Vertical,
                        SplitterWidth = (int)(row.Cells[cellSeparatorIndex].Width > 0 ? row.Cells[cellSeparatorIndex].Width : 6),
                        BackColor = Color.Gray
                    };

                    var firstCellsRow = new TablePanelRow(row.Style, row.Height, row.Visible) { Cells = firstPartCells };
                    var secondCellsRow = new TablePanelRow(row.Style, row.Height, row.Visible) { Cells = secondPartCells };

                    var panel1 = CreateRowControl(firstCellsRow);
                    var panel2 = CreateRowControl(secondCellsRow);

                    if (panel1 != null)
                        cellSplitContainer.Panel1.Controls.Add(panel1);

                    if (panel2 != null)
                        cellSplitContainer.Panel2.Controls.Add(panel2);

                    rowControl = cellSplitContainer;
                }

                tableLayoutPanel.Controls.Add(rowControl, 0, i);
            }

            return tableLayoutPanel;
        }

        private Control CreateRowControl(TablePanelRow row)
        {
            if (row.Cells == null || row.Cells.Count == 0)
                return new Panel { Dock = DockStyle.Fill };

            // Проверяем, есть ли в ячейках разделитель
            int separatorIndex = row.Cells.FindIndex(c => c.Style == TablePanelEntityStyle.Separator);

            if (separatorIndex == -1)
            {
                // Нет разделителя, создаем TableLayoutPanel
                return CreateRowTableLayoutPanel(row);
            }
            else
            {
                // Есть разделитель, используем SplitContainer
                var firstPartCells = row.Cells.Take(separatorIndex).ToList();
                var secondPartCells = row.Cells.Skip(separatorIndex + 1).ToList();

                var splitContainer = new SplitContainer
                {
                    Dock = DockStyle.Fill,
                    Orientation = Orientation.Vertical,
                    SplitterWidth = (int)(row.Cells[separatorIndex].Width > 0 ? row.Cells[separatorIndex].Width : 6),
                    BackColor = Color.Gray
                };

                var firstCellsRow = new TablePanelRow(row.Style, row.Height, row.Visible) { Cells = firstPartCells };
                var secondCellsRow = new TablePanelRow(row.Style, row.Height, row.Visible) { Cells = secondPartCells };

                var panel1 = CreateRowControl(firstCellsRow);
                var panel2 = CreateRowControl(secondCellsRow);

                if (panel1 != null)
                    splitContainer.Panel1.Controls.Add(panel1);

                if (panel2 != null)
                    splitContainer.Panel2.Controls.Add(panel2);

                return splitContainer;
            }
        }

        private TableLayoutPanel CreateRowTableLayoutPanel(TablePanelRow row)
        {
            var tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = row.Cells.Count,
                RowCount = 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Устанавливаем стили для столбцов
            for (int i = 0; i < row.Cells.Count; i++)
            {
                var cell = row.Cells[i];

                if (cell.Style == TablePanelEntityStyle.Absolute)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cell.Width));
                }
                else if (cell.Style == TablePanelEntityStyle.Relative)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, cell.Width));
                }
                else if (cell.Style == TablePanelEntityStyle.Fill)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                }
                else if (cell.Style == TablePanelEntityStyle.Separator)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cell.Width > 0 ? cell.Width : 6));

                    // Добавляем разделитель как панель с заданным цветом
                    var separatorPanel = new Panel
                    {
                        BackColor = Color.Gray,
                        Dock = DockStyle.Fill
                    };
                    tableLayoutPanel.Controls.Add(separatorPanel, i, 0);
                    continue;
                }
            }

            // Добавляем ячейки
            for (int i = 0; i < row.Cells.Count; i++)
            {
                var cell = row.Cells[i];

                if (cell.Style == TablePanelEntityStyle.Separator)
                {
                    // Разделитель уже добавлен
                    continue;
                }

                Control cellControl = CreateCellControl(cell);
                tableLayoutPanel.Controls.Add(cellControl, i, 0);
            }

            return tableLayoutPanel;
        }

        private Control CreateCellControl(TablePanelCell cell)
        {
            Control cellControl;

            if (cell.ChildStructure != null)
            {
                var nestedPanel = new TablePanel();
                nestedPanel.ApplyStructure(cell.ChildStructure);
                cellControl = nestedPanel;
            }
            else if (!string.IsNullOrEmpty(cell.Text))
            {
                cellControl = new Label
                {
                    Text = cell.Text,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = cell.BackColor
                };
            }
            else if (cell.Control != null)
            {
                cellControl = cell.Control;
                cellControl.Dock = DockStyle.Fill;
            }
            else
            {
                cellControl = new Panel
                {
                    BackColor = cell.BackColor,
                    Dock = DockStyle.Fill
                };
            }

            return cellControl;
        }
    }
}
