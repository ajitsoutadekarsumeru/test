using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// Fetch SearchMyAccounts
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<SearchMyAccountsDto>> SearchMyAccounts(SearchMyAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchMyAccounts>().AssignParameters(@params).Fetch();
        }
    }
}