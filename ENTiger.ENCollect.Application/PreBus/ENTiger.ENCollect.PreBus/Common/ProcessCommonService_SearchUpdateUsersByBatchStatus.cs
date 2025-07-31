namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchUpdateUsersByBatchStatusDto>> SearchUpdateUsersByBatchStatus(SearchUpdateUsersByBatchStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchUpdateUsersByBatchStatus>().AssignParameters(@params).Fetch();
        }
    }
}