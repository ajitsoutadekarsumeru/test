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
        public async Task<IEnumerable<GetAgenciesDto>> GetAgencies(GetAgenciesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgencies>().AssignParameters(@params).Fetch();
        }
    }
}