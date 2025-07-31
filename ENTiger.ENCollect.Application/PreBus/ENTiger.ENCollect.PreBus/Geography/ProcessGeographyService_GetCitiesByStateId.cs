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
        public async Task<IEnumerable<GetCitiesByStateIdDto>> GetCitiesByStateId(GetCitiesByStateIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCitiesByStateId>().AssignParameters(@params).Fetch();
        }
    }
}