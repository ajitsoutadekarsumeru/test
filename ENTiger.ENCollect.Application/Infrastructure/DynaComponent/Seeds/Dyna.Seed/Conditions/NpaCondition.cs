namespace ENCollect.Dyna.Seed;
public class NpaCondition : IDynaCondition<SettlementWorkflowContext>
{
    public bool IsMatch(SettlementWorkflowContext settlementWorkflowContext)
    {
        // If domain says the account is NPA => we match
        bool matched = settlementWorkflowContext.IsNpa;
        Console.WriteLine($"[NpaCondition] IsMatch => IsNpa= {settlementWorkflowContext.IsNpa}, matched= {matched}");
        return matched;
    }

    public ISearchCriteria GetSearchCriteria()
    {
        Console.WriteLine("[NpaCondition] Building SearchCriteria for 'NpaAllowed == true'...");

        var sc = new SearchCriteria();
        sc.And(new FilterDefinition("NpaAllowed", FilterOperator.Equal, true));

        Console.WriteLine("[NpaCondition] SearchCriteria built => must have NpaAllowed == true.");
        return sc;
    }

    public bool StopAfterMatch => false;
}