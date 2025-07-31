using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;


namespace ENTiger.ENCollect
{
    /// <summary>
    /// For Row 2 => (IsNeeded => principal=0 && interest>0)
    /// sub-rules => 
    ///  - interest >50k => L5
    ///  - interest <=50k => L4
    /// Overlap => if interest=20k => both => 'UserLevel==L3 OR L2'
    /// </summary>
    public class Row2InterestSubRulesCondition : IDynaCondition<SettlementContext>
    {
        private bool _ruleL4 = false;
        private bool _ruleL5 = false;
        private int approverLevel = 0;
        public bool StopAfterMatch => false;

        public bool IsMatch(SettlementContext ctx)
        {
            // row-level logic is in .IsNeeded => aggregator is only called if
            // principal=0 && interest>0. 
            // sub-rules => we see if interest <=25k => L3, interest <=50k => L2
            if (ctx.InterestAndChargesWaiver <= 500000)
                approverLevel = 4;

            if (ctx.InterestAndChargesWaiver > 500000)
                approverLevel = 5;

            return true;
        }

        public ISearchCriteria GetSearchCriteria()
        {
            var sc = new SearchCriteria();
            bool first = true;

            var def = new FilterDefinition("Level", FilterOperator.Equal, approverLevel);
            sc.AddOrCondition(def, ref first);

            return sc;
        }
    }
}