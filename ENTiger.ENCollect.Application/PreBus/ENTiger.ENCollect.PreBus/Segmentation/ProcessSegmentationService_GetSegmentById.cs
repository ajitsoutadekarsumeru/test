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
        public virtual async Task<GetSegmentByIdDto> GetSegmentById(GetSegmentByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSegmentById>().AssignParameters(@params).Fetch();
        }
    }
}