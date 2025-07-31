
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
        public virtual GenerateTrailGapFileDto GenerateTrailGapFile(GenerateTrailGapFileParams @params)
        {
            return _flexHost.GetFlexiQuery<GenerateTrailGapFile>().AssignParameters(@params).Fetch();
        }
    }
}
