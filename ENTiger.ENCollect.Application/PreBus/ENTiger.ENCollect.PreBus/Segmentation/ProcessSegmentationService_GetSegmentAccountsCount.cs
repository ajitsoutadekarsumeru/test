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
        public virtual async Task<GetSegmentAccountsCountDto> GetSegmentAccountsCount(GetSegmentAccountsCountParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSegmentAccountsCount>().AssignParameters(@params).Fetch();
        }
    }
}