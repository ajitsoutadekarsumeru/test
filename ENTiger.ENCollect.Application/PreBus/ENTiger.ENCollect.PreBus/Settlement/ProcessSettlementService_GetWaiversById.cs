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
        public virtual async Task<GetWaiversByIdDto> GetWaiversById(GetWaiversByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetWaiversById>().AssignParameters(@params).Fetch();
        }
    }
}
