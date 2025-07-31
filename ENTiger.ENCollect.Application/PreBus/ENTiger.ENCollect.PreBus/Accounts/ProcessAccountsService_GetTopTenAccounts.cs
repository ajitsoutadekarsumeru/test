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
        public async Task<IEnumerable<GetTopTenAccountsDto>> GetTopTenAccounts(GetTopTenAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTopTenAccounts>().AssignParameters(@params).Fetch();
        }
    }
}