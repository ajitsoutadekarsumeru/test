using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// Fetch GetMyAccounts
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetMyAccountsDto>> GetMyAccounts(GetMyAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMyAccounts>().AssignParameters(@params).Fetch();
        }
    }
}