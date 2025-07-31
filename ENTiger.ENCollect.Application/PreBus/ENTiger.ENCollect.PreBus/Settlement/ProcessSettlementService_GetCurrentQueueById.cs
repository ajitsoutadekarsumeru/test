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
        public virtual FlexiPagedList<GetCurrentQueueByIdDto> GetCurrentQueueById(GetCurrentQueueByIdParams @params)
        {
            return _flexHost.GetFlexiQuery<GetCurrentQueueById>().AssignParameters(@params).Fetch();
        }
    }
}
