using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// Fetch GetPaidMyAccounts
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetPaidMyAccountsDto>> GetPaidMyAccounts(GetPaidMyAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPaidMyAccounts>().AssignParameters(@params).Fetch();
        }
    }
}