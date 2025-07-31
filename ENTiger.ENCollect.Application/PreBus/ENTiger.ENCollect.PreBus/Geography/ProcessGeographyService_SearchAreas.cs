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
        public async Task<IEnumerable<SearchAreaDto>> SearchAreas(SearchAreasParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAreas>().AssignParameters(@params).Fetch();
        }
    }
}