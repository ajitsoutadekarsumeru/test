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
        public async Task<IEnumerable<GetAgentsByAgencyIdDto>> GetAgentsByAgencyId(GetAgentsByAgencyIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgentsByAgencyId>().AssignParameters(@params).Fetch();
        }
    }
}