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
        public async Task<FlexiPagedList<SearchAckedCollectionsDto>> SearchAckedCollections(SearchAckedCollectionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAckedCollections>().AssignParameters(@params).Fetch();
        }
    }
}