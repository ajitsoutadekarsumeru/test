namespace ENCollect.Dyna.Examples;

public static class Demo8_PlaceholderExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 8: Placeholder usage with c0.40 ==");
        Console.WriteLine("Property: 'ThresholdRecommendationAmount >= $SettAmount'");

        var settlement = new Settlement { SettlementAmount = 120m };
        var recommenders = new List<Recommender>
        {
            new Recommender { Branch = "PNB777", ThresholdRecommendationAmount = 75 },
            new Recommender { Branch = "MUM999", ThresholdRecommendationAmount = 150 },
            new Recommender { Branch = "BLR101", ThresholdRecommendationAmount = 200 }
        };

        // The filter uses a placeholder "$SettAmount" instead of a numeric constant
        var filter = new FilterDefinition(
            "ThresholdRecommendationAmount",
            FilterOperator.GreaterOrEqual,
            "$SettAmount"
        );

        var criteria = new SearchCriteria(filter);

        // We create a ParameterContext to supply "$SettAmount" => settlement.SettlementAmount
        var paramCtx = new ParameterContext();
        paramCtx.Set("$SettAmount", settlement.SettlementAmount);

        // Option A: Overload
        var expr = criteria.Build<Recommender>(paramCtx);

        // Now run the query
        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine($"Placeholder: $SettAmount => {settlement.SettlementAmount}");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, Threshold={r.ThresholdRecommendationAmount}");
        }

        Console.WriteLine();
    }
}