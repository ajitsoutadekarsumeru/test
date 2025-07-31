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
    public class CustomerAcceptanceCondition : IDynaCondition<SettlementContext>
    {
        private string _requestorId = string.Empty;

        public bool StopAfterMatch => false;

        public bool IsMatch(SettlementContext ctx)
        {
            _requestorId = ctx.RequestorId;
            return true; 
        }

        public ISearchCriteria GetSearchCriteria()
        {
            var sc = new SearchCriteria();
            bool first = true;

            
        var def = new FilterDefinition("ApplicationUserId", FilterOperator.Equal, _requestorId);
        sc.AddOrCondition(def, ref first);
          

            return sc;
        }
    }
}