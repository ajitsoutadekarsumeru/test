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
        public async Task<IEnumerable<CaseGroupSummaryDto>> MySettlementQueue(MySettlementQueueParams @params)
        {
            return await _flexHost.GetFlexiQuery<MySettlementQueue>().AssignParameters(@params).Fetch();
        }
    }
}