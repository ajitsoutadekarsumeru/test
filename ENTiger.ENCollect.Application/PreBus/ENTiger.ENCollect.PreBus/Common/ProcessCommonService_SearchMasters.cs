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
        public async Task<SearchMastersDto> SearchMasters(SearchMastersParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchMasters>().AssignParameters(@params).Fetch();
        }
    }
}