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
        public async Task<IEnumerable<SearchCitiesDto>> SearchCities(SearchCitiesParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCities>().AssignParameters(@params).Fetch();
        }
    }
}