using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// return the paginated list of accounts available for settlement
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<FlexiPagedList<GetAccountsForSettlementDto>> GetAccountsForSettlement(GetAccountsForSettlementParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountsForSettlement>().AssignParameters(@params).Fetch();
        }
    }
}
