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
        public async Task<IEnumerable<SearchAreaPinCodesDto>> SearchAreaPinCodes(SearchAreaPinCodesParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAreaPinCodes>().AssignParameters(@params).Fetch();
        }
    }
}