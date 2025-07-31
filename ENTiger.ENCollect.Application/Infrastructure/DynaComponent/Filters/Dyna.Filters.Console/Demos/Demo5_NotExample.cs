namespace ENCollect.Dyna.Examples;
public static class Demo5_NotExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 5: .Not(...) usage ==");

        var recommenders = new List<Recommender>
        {
            new Recommender { Branch = "PNB777", Region = "Punjab", ThresholdRecommendationAmount = 75 },
            new Recommender { Branch = "MUM999", Region = "Maharashtra", ThresholdRecommendationAmount = 150 },
            new Recommender { Branch = "BLR101", Region = "Karnataka", ThresholdRecommendationAmount = 100 }
        };

        var criteria = new SearchCriteria();
        // NOT(Region == 'Punjab') AND (ThresholdRecommendationAmount >= 100)
        criteria
            .Not(new FilterDefinition("Region", FilterOperator.Equal, "Punjab"))
            .And(new FilterDefinition("ThresholdRecommendationAmount", FilterOperator.GreaterOrEqual, 100m));

        var expr = criteria.Build<Recommender>();
        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine("Filter: NOT(Region=='Punjab') AND Threshold>=100");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, Region={r.Region}, Threshold={r.ThresholdRecommendationAmount}");
        }

        Console.WriteLine();
    }
}