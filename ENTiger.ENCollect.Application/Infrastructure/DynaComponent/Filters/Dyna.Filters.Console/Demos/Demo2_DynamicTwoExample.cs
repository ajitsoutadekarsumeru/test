namespace ENCollect.Dyna.Examples;
public static class Demo2_DynamicTwoExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 2: Dynamic AND Filters with 2 parameters ==");

        var settlement = new Settlement { SettlementAmount = 120 };
        var recommenders = new List<Recommender>
        {
            new Recommender { Branch = "MUM999", Region = "Maharashtra", ThresholdRecommendationAmount = 120 },
            new Recommender { Branch = "PNB777", Region = "Punjab", ThresholdRecommendationAmount = 175 }
        };

        // Suppose we want: (Region == 'Maharashtra') AND (ThresholdRecommendationAmount >= 120)
        var filters = new List<IFilterDefinition>
        {
            new FilterDefinition("Branch", FilterOperator.Equal, "PNB777"),
            new FilterDefinition("ThresholdRecommendationAmount", FilterOperator.GreaterOrEqual,
                settlement.SettlementAmount)
        };

        var criteria = new SearchCriteria(filters);
        var expr = criteria.Build<Recommender>(); // all constants, no placeholders here

        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine("Filter: (Region == 'Maharashtra') AND (ThresholdRecommendationAmount >= 120)");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, Region={r.Region}, Threshold={r.ThresholdRecommendationAmount}");
        }

        Console.WriteLine();
    }
}