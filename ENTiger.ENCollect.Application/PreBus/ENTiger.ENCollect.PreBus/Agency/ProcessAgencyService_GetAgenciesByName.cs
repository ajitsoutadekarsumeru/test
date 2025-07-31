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
        public async Task<IEnumerable<GetAgenciesByNameDto>> GetAgenciesByName(GetAgenciesByNameParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgenciesByName>().AssignParameters(@params).Fetch();
        }
    }
}