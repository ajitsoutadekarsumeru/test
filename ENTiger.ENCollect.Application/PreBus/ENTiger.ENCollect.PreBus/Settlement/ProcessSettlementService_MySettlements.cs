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
        public async Task<MySettlementsSummaryDto> MySettlements(MySettlementsSummaryParams @params)
        {
            return await _flexHost.GetFlexiQuery<MySettlements>().AssignParameters(@params).Fetch();
        }
    }
}