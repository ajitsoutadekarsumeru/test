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
        public virtual async Task<IEnumerable<GetAutoSegmentsDto>> GetAutoSegments(GetAutoSegmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAutoSegments>().AssignParameters(@params).Fetch();
        }
    }
}