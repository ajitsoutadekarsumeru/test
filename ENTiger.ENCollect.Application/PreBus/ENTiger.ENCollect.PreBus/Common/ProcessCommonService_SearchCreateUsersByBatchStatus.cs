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
        public virtual async Task<IEnumerable<SearchCreateUsersByBatchStatusDto>> SearchCreateUsersByBatchStatus(SearchCreateUsersByBatchStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCreateUsersByBatchStatus>().AssignParameters(@params).Fetch();
        }
    }
}