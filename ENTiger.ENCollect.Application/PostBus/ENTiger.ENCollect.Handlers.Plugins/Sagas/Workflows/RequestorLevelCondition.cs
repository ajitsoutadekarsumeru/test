using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;


namespace ENTiger.ENCollect
{
    /// <summary>
    /// For Requestor Level1 the Actor(s) should be of Level2    
    /// </summary>
    public class RequestorLevelCondition : IDynaCondition<SettlementContext>
    {
        private int _recommenderLevel;
        public bool StopAfterMatch => false; // let aggregator evaluate fully

        public bool IsMatch(SettlementContext ctx)
        {
            _recommenderLevel = ctx.RequestorLevel + 1;
            return true;
        }

        public ISearchCriteria GetSearchCriteria()
        {
            var sc = new SearchCriteria();
            sc.And(new FilterDefinition("Level", FilterOperator.Equal, _recommenderLevel));

            return sc;
        }
    }
}