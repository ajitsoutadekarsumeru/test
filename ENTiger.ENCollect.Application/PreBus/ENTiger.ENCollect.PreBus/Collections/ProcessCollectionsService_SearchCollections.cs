using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<SearchCollectionsDto>> SearchCollections(SearchCollectionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCollections>().AssignParameters(@params).Fetch();
        }
    }
}