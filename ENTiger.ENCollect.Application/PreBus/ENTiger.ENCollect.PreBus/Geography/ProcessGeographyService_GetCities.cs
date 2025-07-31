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
        public async Task<IEnumerable<GetCitieDto>> GetCities(GetCitiesParam @params)
        {
            return await _flexHost.GetFlexiQuery<GetCities>().AssignParameters(@params).Fetch();
        }
    }
}