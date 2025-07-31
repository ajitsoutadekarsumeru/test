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
        public async Task<IEnumerable<GetCountriesDto>> GetCountries(GetCountriesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCountries>().AssignParameters(@params).Fetch();
        }
    }
}