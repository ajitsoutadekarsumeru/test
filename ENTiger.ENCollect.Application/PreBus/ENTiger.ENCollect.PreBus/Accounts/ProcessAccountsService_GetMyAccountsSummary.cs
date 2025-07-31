
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
        public async Task<GetMyAccountsSummaryDto> GetMyAccountsSummary(GetMyAccountsSummaryParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMyAccountsSummary>().AssignParameters(@params).Fetch();
        }
    }
}
