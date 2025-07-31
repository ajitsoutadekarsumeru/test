using System.Collections.Generic;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetTrailGapSummaryDto>> GetTrailGapSummary(GetTrailGapSummaryParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTrailGapSummary>().AssignParameters(@params).Fetch();
        }
    }
}
