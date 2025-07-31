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
        public virtual async Task<FlexiPagedList<GetMySettlementDetailsByAgingDto>> GetMySettlementDetailsByAging(GetMySettlementDetailsByAgingParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMySettlementDetailsByAging>().AssignParameters(@params).Fetch();
        }
    }
}
