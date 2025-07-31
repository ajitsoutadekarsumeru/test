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
        public async Task<IEnumerable<SearchCountryDto>> SearchCountry(SearchCountryParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCountry>().AssignParameters(@params).Fetch();
        }
    }
}