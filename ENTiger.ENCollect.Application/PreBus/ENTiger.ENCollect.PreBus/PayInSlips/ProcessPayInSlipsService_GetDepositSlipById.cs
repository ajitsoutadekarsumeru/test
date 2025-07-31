namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetDepositSlipByIdDto> GetDepositSlipById(GetDepositSlipByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDepositSlipById>().AssignParameters(@params).Fetch();
        }
    }
}