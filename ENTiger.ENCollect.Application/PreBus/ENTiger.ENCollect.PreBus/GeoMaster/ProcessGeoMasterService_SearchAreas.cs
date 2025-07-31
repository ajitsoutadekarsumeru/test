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
        public async Task<IEnumerable<SearchAreasDto>> SearchAreas(SearchAreasParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAreas>().AssignParameters(@params).Fetch();
        }
    }
}