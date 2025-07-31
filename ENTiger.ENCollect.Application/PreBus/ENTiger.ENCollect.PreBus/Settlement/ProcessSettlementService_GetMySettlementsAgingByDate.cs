
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
        public virtual async Task<IEnumerable<GetMySettlementsAgingByDateDto>> GetMySettlementsAgingByDate(GetMySettlementsAgingByDateParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMySettlementsAgingByDate>().AssignParameters(@params).Fetch();
        }
    }
}
