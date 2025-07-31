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
        public async Task<GetCreditCardAccountDetailsDto> GetCreditCardAccountDetails(GetCreditCardAccountDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCreditCardAccountDetails>().AssignParameters(@params).Fetch();
        }
    }
}