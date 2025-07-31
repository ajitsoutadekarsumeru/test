namespace ENCollect.Dyna.Seed;

public class InterestCondition : IDynaCondition<SettlementWorkflowContext>
{
    public bool IsMatch(SettlementWorkflowContext settlementWorkflowContext)
    {
        // interest logic relevant if principal == 0 and interest > 0
        bool matched = (settlementWorkflowContext.InterestWaiver > 0);
        Console.WriteLine(
            $"[InterestCondition] IsMatch => domainContext.PrincipalWaiver= {settlementWorkflowContext.PrincipalWaiver}, domainContext.InterestWaiver= {settlementWorkflowContext.InterestWaiver}, matched= {matched}");
        return matched;
    }

    public ISearchCriteria GetSearchCriteria()
    {
        Console.WriteLine("[InterestCondition] Building SearchCriteria for interest waiver range...");

        var sc = new SearchCriteria();
        sc.And(new FilterDefinition("MinInterestWaiver", FilterOperator.LessOrEqual, "$InterestWaiver"));
        sc.And(new FilterDefinition("MaxInterestWaiver", FilterOperator.GreaterOrEqual, "$InterestWaiver"));

        Console.WriteLine(
            "[InterestCondition] SearchCriteria built for 'MinInterestWaiver <= $InterestWaiver <= MaxInterestWaiver'.");
        return sc;
    }

    public bool StopAfterMatch => false;
}