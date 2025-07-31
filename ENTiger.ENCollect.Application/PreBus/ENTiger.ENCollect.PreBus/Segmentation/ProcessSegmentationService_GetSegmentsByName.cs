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
        public virtual async Task<IEnumerable<GetSegmentsByNameDto>> GetSegmentsByName(GetSegmentsByNameParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSegmentsByName>().AssignParameters(@params).Fetch();
        }
    }
}