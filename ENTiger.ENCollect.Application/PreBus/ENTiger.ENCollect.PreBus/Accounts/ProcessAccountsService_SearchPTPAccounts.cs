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
        public async Task<IEnumerable<SearchPTPAccountsDto>> SearchPTPAccounts(SearchPTPAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchPTPAccounts>().AssignParameters(@params).Fetch();
        }
    }
}