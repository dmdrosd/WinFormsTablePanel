using Shouldly;
using WinFormsTablePanel.Builders;

namespace WinFormsTablePanel.Tests.Builders;

public class TablePanelRowHelperTests
{
    private readonly TablePanelRowHelper _helper = new();

    [Fact]
    public void GetRowPairs_ShouldReturnCorrectRowPairs()
    {
        // Arrange
        var rows = new List<TablePanelRow>
        {
            new TablePanelRow(TablePanelEntityStyle.Absolute, 100, true, "Panel1_Absolute_100"),
            new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter1"),
            new TablePanelRow(TablePanelEntityStyle.Relative, 4, true, "Panel2_Relative_4"),
            new TablePanelRow(TablePanelEntityStyle.Separator, 6, true, "Splitter2"),
            new TablePanelRow(TablePanelEntityStyle.Relative, 5, true, "Panel3_Relative_5")
        };

        // Act
        var rowPairs = _helper.GetRowPairs(rows).ToList();

        // Assert
        rowPairs.ShouldSatisfyAllConditions(
            () => rowPairs.Count.ShouldBe(3),
            () => rowPairs[0].PanelRow.Name.ShouldBe("Panel1_Absolute_100"),
            () => rowPairs[0].SplitterOn.ShouldBeTrue(),
            () => rowPairs[0].SplitterName.ShouldBe("Splitter1"),

            () => rowPairs[1].PanelRow.Name.ShouldBe("Panel2_Relative_4"),
            () => rowPairs[1].SplitterOn.ShouldBeTrue(),
            () => rowPairs[1].SplitterName.ShouldBe("Splitter2"),

            () => rowPairs[2].PanelRow.Name.ShouldBe("Panel3_Relative_5"),
            () => rowPairs[2].SplitterOn.ShouldBeFalse()
        );
    }

    [Fact]
    public void SplitRowsByFill_ShouldCorrectlySplitRows()
    {
        // Arrange
        var rows = new List<TablePanelRow>
        {
            new TablePanelRow(TablePanelEntityStyle.Absolute, 100, true, "Panel1_Absolute_100"),
            new TablePanelRow(TablePanelEntityStyle.Relative, 4, true, "Panel2_Relative_4"),
            new TablePanelRow(TablePanelEntityStyle.Fill, 0, true, "Panel3_Fill"),
            new TablePanelRow(TablePanelEntityStyle.Relative, 5, true, "Panel4_Relative_5"),
            new TablePanelRow(TablePanelEntityStyle.Absolute, 50, true, "Panel5_Absolute_50")
        };

        // Act
        var (topRows, fillRow, bottomRows) = _helper.SplitRowsByFill(rows);

        // Assert
        topRows.ShouldSatisfyAllConditions(
            () => topRows.Count.ShouldBe(2),
            () => topRows[0].Name.ShouldBe("Panel1_Absolute_100"),
            () => topRows[1].Name.ShouldBe("Panel2_Relative_4")
        );

        fillRow.ShouldNotBeNull();
        fillRow?.Name.ShouldBe("Panel3_Fill");

        bottomRows.ShouldSatisfyAllConditions(
            () => bottomRows.Count.ShouldBe(2),
            () => bottomRows[0].Name.ShouldBe("Panel4_Relative_5"),
            () => bottomRows[1].Name.ShouldBe("Panel5_Absolute_50")
        );
    }
}