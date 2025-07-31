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
        public virtual async Task<IEnumerable<GetTrailIntensityDownloadDetailsDto>> GetTrailIntensityDownloadDetails(GetTrailIntensityDownloadDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTrailIntensityDownloadDetails>().AssignParameters(@params).Fetch();
        }
    }
}