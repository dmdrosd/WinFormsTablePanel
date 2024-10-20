using Shouldly;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Tests.Parts;

public class TablePanelProcessorTests
{
    [Fact]
    public void ProcessStructure_GivenComplexConfiguration_ShouldReturnElementsInCorrectOrder()
    {
        // Arrange
        var structure = new TablePanelStructure
        {
            Rows =
            [
                new TablePanelRow(TablePanelEntityStyle.Absolute, 100, true, "Panel1_Absolute_100"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter1"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 4, true, "Panel2_Relative_4"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter2"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 5, true, "Panel3_Relative_5"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter3"),
                new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Panel4_Fill"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter5"),
                new TablePanelRow(TablePanelEntityStyle.Relative, 3, true, "Panel5_Relative_3"),
                new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter6"),
                new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Panel6_Absolute_50")
            ]
        };

        var processor = new TablePanelProcessor();

        // Act
        var processedStructure = processor.ProcessStructure(structure, 500);
        var elements = processedStructure.Elements.ToList();

        // Assert
        elements.Count.ShouldBe(6); // У нас 6 панелей

        // Проверяем верхние панели
        elements[0].Name.ShouldBe("Panel1_Absolute_100");
        elements[0].HasSplitter.ShouldBeTrue();  // За ним идет сплиттер
        elements[1].Name.ShouldBe("Panel2_Relative_4");
        elements[1].HasSplitter.ShouldBeTrue();  // За ним идет сплиттер
        elements[2].Name.ShouldBe("Panel3_Relative_5");
        elements[2].HasSplitter.ShouldBeTrue();  // За ним идет сплиттер

        // Проверяем нижние панели
        elements[3].Name.ShouldBe("Panel6_Absolute_50");
        elements[3].HasSplitter.ShouldBeTrue();  // За ним идет сплиттер
        elements[4].Name.ShouldBe("Panel5_Relative_3");
        elements[4].HasSplitter.ShouldBeTrue();  // За ним идет сплиттер

        // Проверяем Fill панель в самом конце
        elements[5].Name.ShouldBe("Panel4_Fill");
        elements[5].HasSplitter.ShouldBeFalse(); // Fill панель не должна иметь сплиттер
    }

}