namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetTCAgenciesDto>> GetTCAgencies(GetTCAgenciesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTCAgencies>().AssignParameters(@params).Fetch();
        }
    }
}