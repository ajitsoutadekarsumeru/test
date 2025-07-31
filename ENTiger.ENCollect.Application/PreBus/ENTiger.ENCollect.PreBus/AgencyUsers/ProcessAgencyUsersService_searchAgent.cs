using Sumeru.Flex;

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
        public async Task<FlexiPagedList<searchAgentDto>> searchAgent(searchAgentParams @params)
        {
            return await _flexHost.GetFlexiQuery<searchAgent>().AssignParameters(@params).Fetch();
        }
    }
}