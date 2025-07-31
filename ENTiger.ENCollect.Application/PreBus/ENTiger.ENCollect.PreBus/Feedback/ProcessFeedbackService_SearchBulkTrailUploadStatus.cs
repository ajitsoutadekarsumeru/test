using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<SearchBulkTrailUploadStatusDto>> SearchBulkTrailUploadStatus(SearchBulkTrailUploadStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchBulkTrailUploadStatus>().AssignParameters(@params).Fetch();
        }
    }
}