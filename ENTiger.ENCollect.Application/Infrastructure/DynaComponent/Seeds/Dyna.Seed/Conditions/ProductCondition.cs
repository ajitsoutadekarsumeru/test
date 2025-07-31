namespace ENCollect.Dyna.Seed;
public class ProductCondition : IDynaCondition<SettlementWorkflowContext>
{
    public bool IsMatch(SettlementWorkflowContext settlementWorkflowContext)
    {
        // If domain says the account is NPA => we match
        bool matched = settlementWorkflowContext.Product == "Loans";
        Console.WriteLine(
            $"[ProductCondition] IsMatch => Product= {settlementWorkflowContext.Product}, matched= {matched}");
        return matched;
    }

    public ISearchCriteria GetSearchCriteria()
    {
        Console.WriteLine("[NpaCondition] Building SearchCriteria for 'NpaAllowed == true'...");

        var sc = new SearchCriteria();
        sc.And(new FilterDefinition("Product", FilterOperator.Equal, "$Product"));

        Console.WriteLine("[NpaCondition] SearchCriteria built => must have NpaAllowed == true.");
        return sc;
    }

    public bool StopAfterMatch => false;
}