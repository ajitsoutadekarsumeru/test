namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetTeleCallerAccountDetailsDto> GetTeleCallerAccountDetails(GetTeleCallerAccountDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTeleCallerAccountDetails>().AssignParameters(@params).Fetch();
        }
    }
}