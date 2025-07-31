using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// return the paginated list of settlement-eligible accounts
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<FlexiPagedList<GetSettlementEligibleAccountsDto>> GetSettlementEligibleAccounts(GetSettlementEligibleAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSettlementEligibleAccounts>().AssignParameters(@params).Fetch();
        }
    }
}
