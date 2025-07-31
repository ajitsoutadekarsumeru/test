namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessSegmentationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetUnsequencedAutoSegmentsDto>> GetUnsequencedAutoSegments(GetUnsequencedAutoSegmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUnsequencedAutoSegments>().AssignParameters(@params).Fetch();
        }
    }
}