
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
        public virtual GetSettlementByIdDto GetSettlementById(GetSettlementByIdParams @params)
        {
            return _flexHost.GetFlexiQuery<GetSettlementById>().AssignParameters(@params).Fetch();
        }
    }
}
