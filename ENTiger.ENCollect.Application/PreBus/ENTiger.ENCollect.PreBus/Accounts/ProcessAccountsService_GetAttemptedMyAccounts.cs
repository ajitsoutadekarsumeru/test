using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// Fetch GetAttemptedMyAccounts
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetAttemptedMyAccountsDto>> GetAttemptedMyAccounts(GetAttemptedMyAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAttemptedMyAccounts>().AssignParameters(@params).Fetch();
        }
    }
}