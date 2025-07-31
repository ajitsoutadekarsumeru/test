using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;


namespace ENTiger.ENCollect
{
    /// <summary>
    /// For Row 1 => (IsNeeded => principal>0)
    /// sub-rules => 
    ///  - principal <= 50k => L1
    ///  - principal > 50k => L2
    /// possibly both if there's overlap (theoretically).
    /// </summary>
    public class ApproverLevelCondition : IDynaCondition<SettlementContext>
    {
       
        private int approverLevel = 0;

        public bool StopAfterMatch => false; // let aggregator evaluate fully

        public bool IsMatch(SettlementContext ctx)
        {
           
            if (ctx.PrincipalWaiverPercentage == 0)
            {
                if (ctx.InterestAndChargesWaiver <= 500000)
                    approverLevel = 4;

                if (ctx.InterestAndChargesWaiver > 500000)
                    approverLevel = 5;
            }
            else
            {
                if (ctx.PrincipalWaiverPercentage > 0 && ctx.PrincipalWaiverPercentage <= 10)
                    approverLevel = 4;

                if (ctx.PrincipalWaiverPercentage > 10 && ctx.PrincipalWaiverPercentage <= 15)
                    approverLevel = 5;

                if (ctx.PrincipalWaiverPercentage > 15 && ctx.PrincipalWaiverPercentage <= 30)
                    approverLevel = 6;

                if (ctx.PrincipalWaiverPercentage > 30)
                    approverLevel = 7;
            }

            // We do return true => aggregator merges our sub-rules
            return true;
        }

        public ISearchCriteria GetSearchCriteria()
        {
            var sc = new SearchCriteria();
            bool first = true;
            var def = new FilterDefinition("Level", FilterOperator.Equal, approverLevel);
            sc.AddOrCondition(def, ref first);

            // If neither matched => aggregator yields no user => optional approach:
            // if (first) sc.And(new FilterDefinition("UserId", FilterOperator.Equal, "NO_SUCH_USER"));

            return sc;
        }
    }
}