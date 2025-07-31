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
        public async Task<IEnumerable<GetCountryListDto>> GetCountryList(GetCountryListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCountryList>().AssignParameters(@params).Fetch();
        }
    }
}