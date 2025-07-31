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
        public async Task<IEnumerable<GetLastFivePTPByAccountNoDto>> GetLastFivePTPByAccountNo(GetLastFivePTPByAccountNoParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetLastFivePTPByAccountNo>().AssignParameters(@params).Fetch();
        }
    }
}