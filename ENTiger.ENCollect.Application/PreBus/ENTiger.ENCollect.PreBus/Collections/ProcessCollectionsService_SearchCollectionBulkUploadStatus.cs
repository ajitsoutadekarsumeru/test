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
        public virtual async Task<FlexiPagedList<SearchCollectionBulkUploadStatusDto>> SearchCollectionBulkUploadStatus(SearchCollectionBulkUploadStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCollectionBulkUploadStatus>().AssignParameters(@params).Fetch();
        }
    }
}
