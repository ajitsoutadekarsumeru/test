using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetPrimaryAllocationDownloadDto>> GetPrimaryAllocationDownload(GetPrimaryAllocationDownloadParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPrimaryAllocationDownload>().AssignParameters(@params).Fetch();
        }
    }
}