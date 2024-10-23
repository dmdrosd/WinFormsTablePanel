using Shouldly;
using WinFormsTablePanel.Helpers;
using WinFormsTablePanel.Parts;

namespace WinFormsTablePanel.Tests.Builders;

public class TablePanelHelperTests
{
    private readonly TablePanelHelper _helper = new();

    [Fact]
    public void GetRowPairs_ShouldReturnCorrectRowPairs()
    {
        // Arrange
        var rows = new List<TablePanelRow>
        {
            new TablePanelRow(TablePanelEntityStyle.Absolute, "Panel1_Absolute_100", 100, true),
            new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter1", 6, true),
            new TablePanelRow(TablePanelEntityStyle.Relative, "Panel2_Relative_4", 4, true),
            new TablePanelRow(TablePanelEntityStyle.Separator, "Splitter2", 6, true),
            new TablePanelRow(TablePanelEntityStyle.Relative, "Panel3_Relative_5", 5, true)
        };

        // Act
        var rowPairs = _helper.GetRowPairs(rows).ToList();

        // Assert
        rowPairs.ShouldSatisfyAllConditions(
            () => rowPairs.Count.ShouldBe(3),
            () => rowPairs[0].PanelRow.Name.ShouldBe("Panel1_Absolute_100"),
            () => rowPairs[0].HasSplitter.ShouldBeTrue(),

            () => rowPairs[1].PanelRow.Name.ShouldBe("Panel2_Relative_4"),
            () => rowPairs[1].HasSplitter.ShouldBeTrue(),

            () => rowPairs[2].PanelRow.Name.ShouldBe("Panel3_Relative_5")
        );
    }

    [Fact]
    public void SplitRowsByFill_ShouldCorrectlySplitRows()
    {
        // Arrange
        var rows = new List<TablePanelRow>
        {
            new TablePanelRow(TablePanelEntityStyle.Absolute, "Panel1_Absolute_100", 100, true),
            new TablePanelRow(TablePanelEntityStyle.Relative, "Panel2_Relative_4", 4, true),
            new TablePanelRow(TablePanelEntityStyle.Fill, "Panel3_Fill", 0, true),
            new TablePanelRow(TablePanelEntityStyle.Relative, "Panel4_Relative_5", 5, true),
            new TablePanelRow(TablePanelEntityStyle.Absolute, "Panel5_Absolute_50", 50, true)
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