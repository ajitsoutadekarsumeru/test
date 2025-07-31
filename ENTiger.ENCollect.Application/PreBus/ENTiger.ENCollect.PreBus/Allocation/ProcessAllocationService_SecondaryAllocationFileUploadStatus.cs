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
        public async Task<FlexiPagedList<SecondaryAllocationFileUploadStatusDto>> SecondaryAllocationFileUploadStatus(SecondaryAllocationFileUploadStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<SecondaryAllocationFileUploadStatus>().AssignParameters(@params).Fetch();
        }
    }
}