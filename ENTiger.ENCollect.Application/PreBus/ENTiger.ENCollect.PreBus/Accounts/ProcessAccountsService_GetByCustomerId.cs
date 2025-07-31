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
        public async Task<IEnumerable<GetByCustomerIdDto>> GetByCustomerId(GetByCustomerIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetByCustomerId>().AssignParameters(@params).Fetch();
        }
    }
}