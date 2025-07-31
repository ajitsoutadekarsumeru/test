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
        public virtual async Task<GetSegmentAccountsDto> GetSegmentAccounts(GetSegmentAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSegmentAccounts>().AssignParameters(@params).Fetch();
        }
    }
}