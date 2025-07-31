namespace ENCollect.Dyna.Seed;
public class PrincipalCondition : IDynaCondition<SettlementWorkflowContext>
{
    public bool IsMatch(SettlementWorkflowContext settlementWorkflowContext)
    {
        // condition relevant if principal waiver > 0
        bool matched = (settlementWorkflowContext.PrincipalWaiver > 0);
        Console.WriteLine(
            $"[PrincipalCondition] IsMatch => domainContext.PrincipalWaiver= {settlementWorkflowContext.PrincipalWaiver}, matched= {matched}");
        return matched;
    }

    public ISearchCriteria GetSearchCriteria()
    {
        Console.WriteLine("[PrincipalCondition] Building SearchCriteria for principal waiver range...");

        var sc = new SearchCriteria();
        sc.And(new FilterDefinition("MinPrincipalWaiver", FilterOperator.LessOrEqual, "$PrincipalWaiver"));
        sc.And(new FilterDefinition("MaxPrincipalWaiver", FilterOperator.GreaterOrEqual, "$PrincipalWaiver"));

        Console.WriteLine(
            "[PrincipalCondition] SearchCriteria built => 'MinPrincipalWaiver <= $PrincipalWaiver <= MaxPrincipalWaiver'.");
        return sc;
    }

    public bool StopAfterMatch => true;
}