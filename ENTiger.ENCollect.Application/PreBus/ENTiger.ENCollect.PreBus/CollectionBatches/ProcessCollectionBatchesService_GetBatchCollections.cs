using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCollectionBatchesService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetBatchCollectionsDto>> GetBatchCollections(GetBatchCollectionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBatchCollections>().AssignParameters(@params).Fetch();
        }
    }
}