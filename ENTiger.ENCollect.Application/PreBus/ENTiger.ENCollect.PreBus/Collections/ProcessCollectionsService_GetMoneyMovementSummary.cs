using System.Collections.Generic;

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
        public async Task<IEnumerable<GetMoneyMovementSummaryDto>> GetMoneyMovementSummary(GetMoneyMovementSummaryParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMoneyMovementSummary>().AssignParameters(@params).Fetch();
        }
    }
}
