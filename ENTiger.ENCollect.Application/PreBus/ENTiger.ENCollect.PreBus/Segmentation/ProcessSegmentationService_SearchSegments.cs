using Sumeru.Flex;

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
        public virtual async Task<FlexiPagedList<SearchSegmentsDto>> SearchSegments(SearchSegmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchSegments>().AssignParameters(@params).Fetch();
        }
    }
}