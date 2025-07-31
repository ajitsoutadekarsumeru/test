namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeographyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchStateDto>> SearchState(SearchStateParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchState>().AssignParameters(@params).Fetch();
        }
    }
}