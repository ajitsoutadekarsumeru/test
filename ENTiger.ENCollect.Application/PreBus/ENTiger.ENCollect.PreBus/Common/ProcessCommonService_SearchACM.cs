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
        public async Task<IEnumerable<SearchACMDto>> SearchACM(SearchACMParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchACM>().AssignParameters(@params).Fetch();
        }
    }
}