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
        public async Task<IEnumerable<GetUsersByBaseBranchIdDto>> GetUsersByBaseBranchId(GetUsersByBaseBranchIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUsersByBaseBranchId>().AssignParameters(@params).Fetch();
        }
    }
}