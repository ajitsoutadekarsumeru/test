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
        public async Task<FlexiPagedList<SearchAccountsByFilterDto>> SearchAccountsByFilter(SearchAccountsByFilterParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAccountsByFilter>().AssignParameters(@params).Fetch();
        }
    }
}