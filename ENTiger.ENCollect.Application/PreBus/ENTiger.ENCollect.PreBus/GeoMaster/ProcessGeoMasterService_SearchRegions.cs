namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeoMasterService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchRegionsDto>> SearchRegions(SearchRegionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchRegions>().AssignParameters(@params).Fetch();
        }
    }
}