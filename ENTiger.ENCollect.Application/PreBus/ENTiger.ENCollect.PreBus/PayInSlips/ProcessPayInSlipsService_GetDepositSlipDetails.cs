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
        public async Task<GetDepositSlipDetailsDto> GetDepositSlipDetails(GetDepositSlipDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDepositSlipDetails>().AssignParameters(@params).Fetch();
        }
    }
}