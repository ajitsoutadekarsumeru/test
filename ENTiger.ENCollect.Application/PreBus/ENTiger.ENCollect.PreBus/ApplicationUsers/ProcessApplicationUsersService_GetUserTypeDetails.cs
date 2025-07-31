namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// Retrieves license limits for the supplied user type
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetUserTypeDetailsDto> GetUserTypeDetails(GetUserTypeDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUserTypeDetails>().AssignParameters(@params).Fetch();
        }
    }
}