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
        public async Task<FlexiPagedList<MySettlementQueueDetailsDto>> MySettlementQueueDetails(MySettlementQueueDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<MySettlementQueueDetails>().AssignParameters(@params).Fetch();
        }
    }
}