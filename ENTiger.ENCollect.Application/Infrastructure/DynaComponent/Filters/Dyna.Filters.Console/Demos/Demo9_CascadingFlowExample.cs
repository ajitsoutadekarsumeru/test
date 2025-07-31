namespace ENCollect.Dyna.Examples;

/// <summary>
/// Demonstrates using the CascadingFlow with multiple IDynaCondition
/// and then applying the final aggregator to filter a list of ApprovalGrid objects.
/// Now includes Console.WriteLine statements for clarity.
/// </summary>
public static class Demo9_CascadingFlowExample
{
    public static void Run()
    {
        Console.WriteLine("=== Demo 9: CascadingFlow & IDynaCondition ===");
        Console.WriteLine();

        // 1) Prepare some sample ApprovalGrids
        Console.WriteLine("[Step 1] Preparing sample ApprovalGrid data...");
        var allGrids = new List<ApprovalGrid>
        {
            new ApprovalGrid
            {
                Name = "GridA",
                NpaAllowed = true,
                MinPrincipalWaiver = 5000,
                MaxPrincipalWaiver = 20000,
                Product = "Loans",
                Designation = "Business Head"
            },
            new ApprovalGrid
            {
                Name = "GridB",
                NpaAllowed = true,
                MinPrincipalWaiver = 500,
                MaxPrincipalWaiver = 10000,
                Product = "Loans",
                Designation = "Credit Head"
            },
            new ApprovalGrid
            {
                Name = "GridC",
                NpaAllowed = false,
                MinInterestWaiver = 50,
                MaxInterestWaiver = 500,
                Product = "CreditCards",
                Designation = "Business Head"
            },
            new ApprovalGrid
            {
                Name = "GridD",
                NpaAllowed = true,
                MinPrincipalWaiver = 0,
                MaxPrincipalWaiver = 0,
                MinInterestWaiver = 0,
                MaxInterestWaiver = 1000,
                Product = "Loans",
                Designation = "Regional Manager"
            }
            // etc.
        };
        Console.WriteLine($"Total sample grids: {allGrids.Count}");

        // 2) Suppose we have a domain context
        Console.WriteLine();
        Console.WriteLine("[Step 2] Creating the domain context:");
        var settlementAccountContext = new SettlementWorkflowContext
        {
            IsNpa = true,
            PrincipalWaiver = 8000,
            InterestWaiver = 0,
            Product = "Loans"
        };
        Console.WriteLine(
            $"DomainContext => IsNpa={settlementAccountContext.IsNpa}, Principal={settlementAccountContext.PrincipalWaiver}, Interest={settlementAccountContext.InterestWaiver}, Product={settlementAccountContext.Product}");

        // 3) Build the flow, add conditions
        Console.WriteLine();
        Console.WriteLine("[Step 3] Building the CascadingFlow, adding conditions");
        var flow = new CascadingFlow<SettlementWorkflowContext>();
        flow.AddConditions(
            new ProductCondition(),
            new NpaCondition(),
            new PrincipalCondition(),
            new InterestCondition()
        );
        Console.WriteLine("Conditions successfully added to flow.");

        // 4) Evaluate => get final aggregator
        Console.WriteLine();
        Console.WriteLine("[Step 4] Evaluating flow => producing a final ISearchCriteria...");
        var finalCriteria = flow.Evaluate(settlementAccountContext);
        Console.WriteLine("[Debug] Final aggregated search criteria: " + finalCriteria);

        // 5) We must set placeholders for $PrincipalWaiver, $InterestWaiver if relevant
        Console.WriteLine();
        Console.WriteLine("[Step 5] Setting parameter placeholders for finalCriteria...");
        var paramCtx = new ParameterContext();
        paramCtx.Set("$Product", settlementAccountContext.Product);
        paramCtx.Set("$PrincipalWaiver", settlementAccountContext.PrincipalWaiver);
        paramCtx.Set("$InterestWaiver", settlementAccountContext.InterestWaiver);
        Console.WriteLine(
            $"Param placeholders => $PrincipalWaiver={settlementAccountContext.PrincipalWaiver}, $InterestWaiver={settlementAccountContext.InterestWaiver}");

        // 6) Build an expression for ApprovalGrid
        Console.WriteLine();
        Console.WriteLine("[Step 6] Building a LINQ expression from finalCriteria for 'ApprovalGrid'...");
        var expr = finalCriteria.Build<ApprovalGrid>(paramCtx);
        Console.WriteLine("Expression built. Now applying to the sample data...");

        // 7) Filter the sample data
        Console.WriteLine();
        var matched = allGrids.Where(expr.Compile()).ToList();

        // 8) Print results
        Console.WriteLine($"[Step 7/8] Matched {matched.Count} grids after applying final expression:");
        foreach (var g in matched)
        {
            Console.WriteLine($"  - {g.Designation}");
        }

        Console.WriteLine();
        Console.WriteLine("=== End of CascadingFlow demo (Demo9) ===");
    }
}