namespace ENCollect.Dyna.Examples;

public static class Demo3_DynamicThreeExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 3: Dynamic AND Filters (3 parameters) ==");

        var settlement = new Settlement { SettlementAmount = 120 };
        var recommenders = new List<Recommender>
        {
            new Recommender
                { Branch = "BLR101", Region = "Karnataka", Product = "Loans", ThresholdRecommendationAmount = 100 },
            new Recommender
                { Branch = "BLR102", Region = "Karnataka", Product = "Loans", ThresholdRecommendationAmount = 200 },
            new Recommender
                { Branch = "NDL450", Region = "Delhi", Product = "Insurance", ThresholdRecommendationAmount = 200 }
        };

        // Let's do: (Region == 'Karnataka') AND (Product == 'Loans') AND (ThresholdRecommendationAmount >= 120)
        var criteria = new SearchCriteria();
        criteria
            .And(new FilterDefinition("Region", FilterOperator.Equal, "Karnataka"))
            .And(new FilterDefinition("Product", FilterOperator.Equal, "Loans"))
            .And(new FilterDefinition("ThresholdRecommendationAmount", FilterOperator.GreaterOrEqual,
                settlement.SettlementAmount));

        var expr = criteria.Build<Recommender>();
        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine("Filters: Region='Karnataka' AND Product='Loans' AND Threshold>=120");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine(
                $"  Branch={r.Branch}, Region={r.Region}, Product={r.Product}, Threshold={r.ThresholdRecommendationAmount}");
        }
        Console.WriteLine();
    }
}