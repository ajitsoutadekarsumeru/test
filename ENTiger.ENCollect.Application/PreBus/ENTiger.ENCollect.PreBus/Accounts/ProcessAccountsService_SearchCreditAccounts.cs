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
        public async Task<IEnumerable<SearchCreditAccountsDto>> SearchCreditAccounts(SearchCreditAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCreditAccounts>().AssignParameters(@params).Fetch();
        }
    }
}