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
        public virtual async Task<GetHistoryByIdDto> GetHistoryById(GetHistoryByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetHistoryById>().AssignParameters(@params).Fetch();
        }
    }
}
