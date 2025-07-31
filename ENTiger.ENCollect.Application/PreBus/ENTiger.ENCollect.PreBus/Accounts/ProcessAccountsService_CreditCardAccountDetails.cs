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
        public async Task<CreditCardAccountDetailsDto> CreditCardAccountDetails(CreditCardAccountDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<CreditCardAccountDetails>().AssignParameters(@params).Fetch();
        }
    }
}