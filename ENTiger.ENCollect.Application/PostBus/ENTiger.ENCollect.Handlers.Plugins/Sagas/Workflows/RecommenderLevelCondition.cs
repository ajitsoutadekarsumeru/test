using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;


namespace ENTiger.ENCollect
{
    /// <summary>
    /// For Recommender1 Level2 the Recommender2 should be of Level3   
    /// </summary>
    public class RecommenderLevelCondition : IDynaCondition<SettlementContext>
    {
        private int _recommenderLevel;
        public bool StopAfterMatch => false; // let aggregator evaluate fully

        public bool IsMatch(SettlementContext ctx)
        {
            _recommenderLevel = ctx.RequestorLevel + 2;
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