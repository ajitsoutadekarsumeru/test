namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetTravelReportDetailsDto>> GetTravelReportDetails(GetTravelReportDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTravelReportDetails>().AssignParameters(@params).Fetch();
        }
    }
}