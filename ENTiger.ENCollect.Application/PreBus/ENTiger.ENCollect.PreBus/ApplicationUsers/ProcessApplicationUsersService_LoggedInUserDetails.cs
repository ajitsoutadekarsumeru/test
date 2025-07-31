namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<LoggedInUserDetailsDto> LoggedInUserDetails(LoggedInUserDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<LoggedInUserDetails>().AssignParameters(@params).Fetch();
        }
    }
}