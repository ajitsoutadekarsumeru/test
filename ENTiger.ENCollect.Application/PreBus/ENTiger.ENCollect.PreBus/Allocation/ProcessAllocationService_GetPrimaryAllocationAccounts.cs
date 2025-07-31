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
        public async Task<IEnumerable<GetPrimaryAllocationAccountsDto>> GetPrimaryAllocationAccounts(GetPrimaryAllocationAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPrimaryAllocationAccounts>().AssignParameters(@params).Fetch();
        }
    }
}