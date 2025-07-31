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
        public virtual async Task<IEnumerable<GetAccountsByIdsDto>> GetAccountsByIds(GetAccountsByIdsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountsByIds>().AssignParameters(@params).Fetch();
        }
    }
}
