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
        public virtual async Task<GetInstallmentsByIdDto> GetInstallmentsById(GetInstallmentsByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetInstallmentsById>().AssignParameters(@params).Fetch();
        }
    }
}
