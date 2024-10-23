using Shouldly;
using WinFormsTablePanel.Builders;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Tests.Builders
{
    public class VerticalStackPanelBuilderTests
    {
        [Fact]
        public void Build_GivenComplexConfiguration_ShouldReturnControlsInCorrectOrder()
        {
            // Arrange
            var structure = new TablePanelStructure
            {
                Rows =
                [
                    new TablePanelRow(TablePanelEntityStyle.Absolute, "Panel1_Absolute_100", 100, true),
                    new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter1", 6, true),
                    new TablePanelRow(TablePanelEntityStyle.Relative, "Panel2_Relative_4", 4, true),
                    new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter2", 6, true),
                    new TablePanelRow(TablePanelEntityStyle.Relative, "Panel3_Relative_5", 5, true),
                    new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter3", 6, true),
                    // Fill panel
                    new TablePanelRow(TablePanelEntityStyle.Fill, "Panel4_Fill", 0, true),
                    // Bottom panels
                    new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter5", 6, true),
                    new TablePanelRow(TablePanelEntityStyle.Relative, "Panel5_Relative_3", 3, true),
                    new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter6", 6, true),
                    new TablePanelRow(TablePanelEntityStyle.Absolute, "Panel6_Absolute_50", 50, true)
                ]
            };

            var builder = new VerticalStackPanelBuilder();

            // Act
            var controls = builder.Build(structure.Rows).Controls.ToList();

            // Check Fill panel
            controls[0].Name.ShouldBe("Panel4_Fill");

            // Check bottom panels and splitters
            controls[1].Name.ShouldBe("Splitter5");
            controls[2].Name.ShouldBe("Panel5_Relative_3");
            controls[3].Name.ShouldBe("Splitter6");
            controls[4].Name.ShouldBe("Panel6_Absolute_50");

            // Check top panels and splitters
            controls[5].Name.ShouldBe("Splitter3");
            controls[6].Name.ShouldBe("Panel3_Relative_5");
            controls[7].Name.ShouldBe("Splitter2");
            controls[8].Name.ShouldBe("Panel2_Relative_4");
            controls[9].Name.ShouldBe("Splitter1");
            controls[10].Name.ShouldBe("Panel1_Absolute_100");
        }
    }
}
