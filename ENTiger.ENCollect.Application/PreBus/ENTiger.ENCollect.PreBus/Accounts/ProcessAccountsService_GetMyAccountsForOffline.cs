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
        public async Task<IEnumerable<GetMyAccountsForOfflineDto>> GetMyAccountsForOffline(GetMyAccountsForOfflineParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMyAccountsForOffline>().AssignParameters(@params).Fetch();
        }
    }
}