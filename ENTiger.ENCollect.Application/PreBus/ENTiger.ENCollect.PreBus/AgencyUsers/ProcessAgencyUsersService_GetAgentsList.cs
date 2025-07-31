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
        public async virtual Task<IEnumerable<GetAgentsListDto>> GetAgentsList(GetAgentsListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgentsList>().AssignParameters(@params).Fetch();
        }
    }
}