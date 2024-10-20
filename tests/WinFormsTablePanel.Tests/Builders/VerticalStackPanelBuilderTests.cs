using Shouldly;
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
                    new TablePanelRow(TablePanelEntityStyle.Absolute, 100, true, "Panel1_Absolute_100"),
                    new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter1"),
                    new TablePanelRow(TablePanelEntityStyle.Relative, 4, true, "Panel2_Relative_4"),
                    new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter2"),
                    new TablePanelRow(TablePanelEntityStyle.Relative, 5, true, "Panel3_Relative_5"),
                    new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter3"),
                    // Fill panel
                    new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Panel4_Fill"),
                    // Bottom panels
                    new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter5"),
                    new TablePanelRow(TablePanelEntityStyle.Relative, 3, true, "Panel5_Relative_3"),
                    new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter6"),
                    new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Panel6_Absolute_50")
                ]
            };

            //var builder = new VerticalStackPanelBuilder(structure.Rows, new ControlFactory());

            //// Act
            //var controls = builder.Build().ToList();

            //// Check Fill panel
            //controls[0].Name.ShouldBe("Panel4_Fill");

            //// Check bottom panels and splitters
            //controls[1].Name.ShouldBe("Splitter5");
            //controls[2].Name.ShouldBe("Panel5_Relative_3");
            //controls[3].Name.ShouldBe("Splitter6");
            //controls[4].Name.ShouldBe("Panel6_Absolute_50");

            //// Check top panels and splitters
            //controls[5].Name.ShouldBe("Splitter3");
            //controls[6].Name.ShouldBe("Panel3_Relative_5");
            //controls[7].Name.ShouldBe("Splitter2");
            //controls[8].Name.ShouldBe("Panel2_Relative_4");
            //controls[9].Name.ShouldBe("Splitter1");
            //controls[10].Name.ShouldBe("Panel1_Absolute_100");
        }
    }
}
