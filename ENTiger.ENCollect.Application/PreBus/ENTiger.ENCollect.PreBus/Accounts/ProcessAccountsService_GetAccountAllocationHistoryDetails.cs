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
        public async Task<IEnumerable<GetAccountAllocationHistoryDetailsDto>> GetAccountAllocationHistoryDetails(GetAccountAllocationHistoryDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountAllocationHistoryDetails>().AssignParameters(@params).Fetch();
        }
    }
}