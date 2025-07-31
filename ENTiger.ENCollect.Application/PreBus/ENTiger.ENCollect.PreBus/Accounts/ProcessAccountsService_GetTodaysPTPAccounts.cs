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
        public async Task<IEnumerable<GetTodaysPTPAccountsDto>> GetTodaysPTPAccounts(GetTodaysPTPAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTodaysPTPAccounts>().AssignParameters(@params).Fetch();
        }
    }
}