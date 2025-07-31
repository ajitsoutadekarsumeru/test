
using Sumeru.Flex;

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
        public virtual async Task<IEnumerable<GetMySettlementsAgingByStatusDto>> GetMySettlementsAgingByStatus(GetMySettlementsAgingByStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMySettlementsAgingByStatus>().AssignParameters(@params).Fetch();
        }
    }
}
