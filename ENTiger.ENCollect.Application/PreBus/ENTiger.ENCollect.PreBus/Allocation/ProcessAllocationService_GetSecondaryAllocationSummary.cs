using System.Collections.Generic;

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
        public async Task<IEnumerable<GetSecondaryAllocationSummaryDto>> GetSecondaryAllocationSummary(GetSecondaryAllocationSummaryParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSecondaryAllocationSummary>().AssignParameters(@params).Fetch();
        }
    }
}
