namespace ENCollect.Dyna.Examples;
public static class Demo7_HasNestedScopeExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 7: .Has(...) with dot notation => 'ScopeOfWorks.City' ==");

        var recommenders = new List<Recommender>
        {
            new Recommender
            {
                Branch = "BLR101",
                ScopeOfWorks = new List<ScopeOfWork>
                {
                    new ScopeOfWork { City = "Bengaluru" },
                    new ScopeOfWork { City = "Mysuru" }
                }
            },
            new Recommender
            {
                Branch = "MUM999",
                ScopeOfWorks = new List<ScopeOfWork>
                {
                    new ScopeOfWork { City = "Mumbai" }
                }
            }
        };

        // "[ScopeOfWorks].City" + Has => .Any(s => s.City == "Bengaluru")
        var filter = new FilterDefinition("[ScopeOfWorks].City", FilterOperator.Has, "Bengaluru");
        var criteria = new SearchCriteria(filter);

        var expr = criteria.Build<Recommender>();
        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine("Filter: Recommender.ScopeOfWorks.Any(x => x.City=='Bengaluru')");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, " +
                              $"ScopeCities=[{string.Join(", ", r.ScopeOfWorks?.Select(s => s.City) ?? new List<string>())}]");
        }

        Console.WriteLine();
    }
}