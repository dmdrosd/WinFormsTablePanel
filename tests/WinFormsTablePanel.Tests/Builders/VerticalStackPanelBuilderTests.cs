using Shouldly;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Tests.Builders;

public class VerticalStackPanelBuilderTests
{
    [Fact]
    public void Build_GivenComplexConfiguration_ShouldReturnControlsInCorrectOrder()
    {
        // Arrange
        var structure = new TablePanelStructure
        {
            Rows = new List<TablePanelRow>
            {
                // Верхние панели
                new TablePanelRow(TablePanelEntityStyle.Absolute, 100, true, "Panel1_Absolute_100"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter1"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 4, true, "Panel2_Relative_4"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter2"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 5, true, "Panel3_Relative_5"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter3"),
                // Fill панель
                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Panel4_Fill"),
                // Нижние панели
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter5"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 3, true, "Panel5_Relative_3"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter6"),
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Panel6_Absolute_50")
            }
        };

        var builder = new VerticalStackPanelBuilder(structure.Rows);

        // Act
        var controls = builder.Build().ToList();

        // Assert
        // Проверяем верхние панели и сплиттеры
        controls[0].Name.ShouldBe("Panel1_Absolute_100");
        controls[1].Name.ShouldBe("Splitter1");

        controls[2].Name.ShouldBe("Panel2_Relative_4");
        controls[3].Name.ShouldBe("Splitter2");

        controls[4].Name.ShouldBe("Panel3_Relative_5");
        controls[5].Name.ShouldBe("Splitter3");

        // Проверяем нижние панели и сплиттеры
        controls[6].Name.ShouldBe("Panel6_Absolute_50");
        controls[7].Name.ShouldBe("Splitter6");

        controls[8].Name.ShouldBe("Panel5_Relative_3");
        controls[9].Name.ShouldBe("Splitter5");

        // Проверяем Fill панель в конце
        controls[10].Name.ShouldBe("Panel4_Fill");
    }
}