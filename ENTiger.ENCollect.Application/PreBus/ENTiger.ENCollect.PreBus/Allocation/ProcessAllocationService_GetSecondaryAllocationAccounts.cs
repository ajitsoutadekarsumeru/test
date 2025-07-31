namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetSecondaryAllocationAccountsDto>> GetSecondaryAllocationAccounts(GetSecondaryAllocationAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSecondaryAllocationAccounts>().AssignParameters(@params).Fetch();
        }
    }
}