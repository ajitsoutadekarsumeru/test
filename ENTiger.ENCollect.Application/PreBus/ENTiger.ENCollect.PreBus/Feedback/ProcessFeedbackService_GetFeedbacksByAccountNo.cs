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
        public async Task<FlexiPagedList<GetFeedbacksByAccountNoDto>> GetFeedbacksByAccountNo(GetFeedbacksByAccountNoParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetFeedbacksByAccountNo>().AssignParameters(@params).Fetch();
        }
    }
}