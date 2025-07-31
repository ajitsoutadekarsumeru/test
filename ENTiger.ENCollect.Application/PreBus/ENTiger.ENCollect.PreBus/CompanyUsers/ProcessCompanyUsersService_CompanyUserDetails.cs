namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<CompanyUserDetailsDto> CompanyUserDetails(CompanyUserDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<CompanyUserDetails>().AssignParameters(@params).Fetch();
        }
    }
}