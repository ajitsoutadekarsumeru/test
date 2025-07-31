namespace ENCollect.Dyna.CascadingFlow.Tests;

public class SearchCriteriaAndCriteriaTests
{
    [Fact]
    public void AndCriteria_EmptyThis_JustCopiesOther()
    {
        var thisCrit = new SearchCriteria(); // empty
        var otherCrit = new SearchCriteria();
        var f1 = new FilterDefinition("Branch", FilterOperator.Equal, "NY");
        var f2 = new FilterDefinition("IsActive", FilterOperator.NotEqual, false);
        otherCrit.And(f1).Or(f2);

        thisCrit.AndCriteria(otherCrit);

        //var items = GetItems(thisCrit);
        //Assert.Equal(2, items.Count);

        //Assert.Same(f1, items[0].filter);
        //Assert.Equal(LogicConnector.And, items[0].connector);

        //Assert.Same(f2, items[1].filter);
        //Assert.Equal(LogicConnector.Or, items[1].connector);
    }

    [Fact]
    public void AndCriteria_NonEmptyThis_ForceFirstItemToAnd_KeepOthers()
    {
        // arrange
        var thisCrit = new SearchCriteria();
        var fA = new FilterDefinition("Branch", FilterOperator.Equal, "AAA");
        var fB = new FilterDefinition("IsActive", FilterOperator.Equal, true);
        thisCrit.And(fA).Or(fB);

        var otherCrit = new SearchCriteria();
        var fC = new FilterDefinition("Branch", FilterOperator.NotEqual, "ZZZ");
        var fD = new FilterDefinition("Amount", FilterOperator.GreaterThan, 100);

        // Instead of .Or(fC).And(fD), do:
        otherCrit.And(fC).Or(fD);

        // act
        thisCrit.AndCriteria(otherCrit);

        // assert
        //var items = GetItems(thisCrit);
        //Assert.Equal(4, items.Count);

        //// #1 aggregator items:
        //Assert.Same(fA, items[0].filter);
        //Assert.Equal(LogicConnector.And, items[0].connector);

        //Assert.Same(fB, items[1].filter);
        //Assert.Equal(LogicConnector.Or, items[1].connector);

        //// #2 aggregator items:
        //Assert.Same(fC, items[2].filter);
        //Assert.Equal(LogicConnector.And, items[2].connector); // forced AND for first item
        //Assert.Same(fD, items[3].filter);
        //Assert.Equal(LogicConnector.Or, items[3].connector); // original connector from aggregator #2
    }
    // ============ Helper to read 'Items' property from SearchCriteria ============

    //private static List<(IFilterDefinition filter, LogicConnector connector, bool negate)> GetItems(
    //    SearchCriteria sc)
    //{
    //    return sc.Items.ToList();
    //}
}