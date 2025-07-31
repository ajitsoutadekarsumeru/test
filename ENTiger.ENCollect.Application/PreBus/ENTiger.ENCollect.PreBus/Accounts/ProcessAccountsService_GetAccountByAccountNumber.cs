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
        public async Task<GetAccountByAccountNumberDto> GetAccountByAccountNumber(GetAccountByAccountNumberParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountByAccountNumber>().AssignParameters(@params).Fetch();
        }
    }
}