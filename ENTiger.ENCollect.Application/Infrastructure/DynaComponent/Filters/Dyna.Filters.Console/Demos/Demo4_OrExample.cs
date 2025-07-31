namespace ENCollect.Dyna.Examples;

public static class Demo4_OrExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 4: .Or(...) usage ==");

        var recommenders = new List<Recommender>
        {
            new Recommender { Branch = "BLR101", Region = "Karnataka" },
            new Recommender { Branch = "MUM999", Region = "Maharashtra" },
            new Recommender { Branch = "NDL450", Region = "Delhi" }
        };

        // If we use empty constructor, we must do .And(...) first
        var criteria = new SearchCriteria(new FilterDefinition("Region", FilterOperator.Equal, "Karnataka"));
        criteria
            .Or(new FilterDefinition("Region", FilterOperator.Equal, "Maharashtra"));

        var expr = criteria.Build<Recommender>();
        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine("Filter: (Region=='Karnataka') OR (Region=='Maharashtra')");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, Region={r.Region}");
        }

        Console.WriteLine();
    }
}