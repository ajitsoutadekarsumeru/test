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
        public async Task<FlexiPagedList<PrimaryUnAllocationBatchStatusDto>> PrimaryUnAllocationBatchStatus(PrimaryUnAllocationBatchStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<PrimaryUnAllocationBatchStatus>().AssignParameters(@params).Fetch();
        }
    }
}