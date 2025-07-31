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
        public async Task<IEnumerable<CompanyUserDepartmentsDto>> CompanyUserDepartments(CompanyUserDepartmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<CompanyUserDepartments>().AssignParameters(@params).Fetch();
        }
    }
}