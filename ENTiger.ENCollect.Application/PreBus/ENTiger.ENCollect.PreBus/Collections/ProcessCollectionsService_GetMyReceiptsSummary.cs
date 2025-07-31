
namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<GetMyReceiptsSummaryDto> GetMyReceiptsSummary(GetMyReceiptsSummaryParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMyReceiptsSummary>().AssignParameters(@params).Fetch();
        }
    }
}
