using System.Collections.Generic;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetMyOpenSettlementsDto>> GetMyOpenSettlements(GetMyOpenSettlementsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMyOpenSettlements>().AssignParameters(@params).Fetch();
        }
    }
}
