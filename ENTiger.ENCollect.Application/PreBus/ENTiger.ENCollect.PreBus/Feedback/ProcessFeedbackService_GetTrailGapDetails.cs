using ENTiger.ENCollect.AllocationModule;
using Sumeru.Flex;

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
        public async Task<FlexiPagedList<GetTrailGapDetailsDto>> GetTrailGapDetails(GetTrailGapDetailsParams @params)

        {
            return await _flexHost.GetFlexiQuery<GetTrailGapDetails>().AssignParameters(@params).Fetch();
        }
    }
}