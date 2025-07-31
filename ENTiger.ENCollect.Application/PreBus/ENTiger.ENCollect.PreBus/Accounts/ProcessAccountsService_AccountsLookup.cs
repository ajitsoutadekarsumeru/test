using Sumeru.Flex;

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
        public async Task<FlexiPagedList<AccountsLookupDto>> AccountsLookup(AccountsLookupParams @params)
        {
            return await _flexHost.GetFlexiQuery<AccountsLookup>().AssignParameters(@params).Fetch();
        }
    }
}