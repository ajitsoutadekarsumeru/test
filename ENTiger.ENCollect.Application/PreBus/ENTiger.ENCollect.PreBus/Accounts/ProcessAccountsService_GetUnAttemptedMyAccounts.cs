using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetUnAttemptedMyAccountsDto>> GetUnAttemptedMyAccounts(GetUnAttemptedMyAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUnAttemptedMyAccounts>().AssignParameters(@params).Fetch();
        }
    }
}