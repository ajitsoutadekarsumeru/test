namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetAgentsByRegionDto>> GetAgentsByRegion(GetAgentsByRegionParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgentsByRegion>().AssignParameters(@params).Fetch();
        }
    }
}