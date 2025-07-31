namespace ENCollect.Dyna.Examples;

/// <summary>
/// Demonstrates using the Has operator for a single nested object
/// (e.g. "WorkflowState.Name").
/// </summary>
public static class Demo10_HasSingleObjectExample
{
    public static void Run()
    {
        Console.WriteLine("=== Demo 10: Has Operator for Single Nested Object ===");
        Console.WriteLine();

        // 1) Prepare sample data
        Console.WriteLine(
            "[Demo10] Step 1: Preparing sample 'ApprovalGrid10' data with a single nested WorkflowState object.");
        var agencies = new List<Agency>
        {
            new Agency
            {
                Name = "Agency 0",
                WorkflowState = new WorkflowState { Name = "Closed" },
                Product = "Loans"
            },
            new Agency
            {
                Name = "Agency 1",
                WorkflowState = new WorkflowState { Name = "Open" },
                Product = "CreditCards"
            },
            new Agency
            {
                Name = "Agency 2",
                WorkflowState = null!, // no state
                Product = "Loans"
            }
        };

        // 2) Domain context (optional for demonstration)
        Console.WriteLine(
            "[Demo10] Step 2: No special domain context needed here, just a direct filter definition using Has for 'WorkflowState.Name'.");

        // 3) Build a search criteria that uses Has for single object
        Console.WriteLine("[Demo10] Step 3: Creating a SearchCriteria with (WorkflowState.Name) Has 'Closed'...");
        var filter = new FilterDefinition("WorkflowState.Name", FilterOperator.Has, "Closed");
        var criteria = new SearchCriteria(filter);

        // 4) Build expression and filter
        Console.WriteLine("[Demo10] Step 4: Building expression and filtering data...");
        var expr = criteria.Build<Agency>();
        var matched = agencies.Where(expr.Compile()).ToList();

        // 5) Show results
        Console.WriteLine($"[Demo10] Step 5: Found {matched.Count} record(s) where WorkflowState.Name == 'Closed':");
        foreach (var rec in matched)
        {
            Console.WriteLine($"  - {rec.Name} (Product={rec.Product}, WorkflowState={rec.WorkflowState?.Name})");
        }

        Console.WriteLine();
        Console.WriteLine("=== End of Demo 10: Has Operator (SingleObject) ===");
    }
}

// Simple entity to illustrate single-object nested property
public class Agency
{
    public string Name { get; set; } = string.Empty;
    public WorkflowState? WorkflowState { get; set; }
    public string Product { get; set; } = string.Empty;
}

public class WorkflowState
{
    public string Name { get; set; } = string.Empty;
}