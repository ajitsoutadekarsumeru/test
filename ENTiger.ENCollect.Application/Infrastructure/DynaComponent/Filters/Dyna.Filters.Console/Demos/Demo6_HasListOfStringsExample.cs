namespace ENCollect.Dyna.Examples;
public static class Demo6_HasListOfStringsExample
{
    public static void Run()
    {
        Console.WriteLine("== Demo 6: .Has(...) with a List<string> ==");
        Console.WriteLine("Property: 'States' => a List<string>.");

        var recommenders = new List<Recommender>
        {
            new Recommender
            {
                Branch = "BLR101",
                States = new List<string> { "Karnataka", "TamilNadu" }
            },
            new Recommender
            {
                Branch = "MUM999",
                States = new List<string> { "Maharashtra" }
            }
        };

        var filter = new FilterDefinition("[States]", FilterOperator.Has, "Karnataka");
        var criteria = new SearchCriteria(filter);

        var expr = criteria.Build<Recommender>();
        var results = recommenders.Where(expr.Compile()).ToList();

        Console.WriteLine("(States HAS 'Karnataka')");
        Console.WriteLine($"Matched {results.Count} recommenders:");
        foreach (var r in results)
        {
            Console.WriteLine($"  Branch={r.Branch}, States=[{string.Join(", ", r.States ?? new List<string>())}]");
        }

        Console.WriteLine();
    }
}