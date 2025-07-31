using Sumeru.Flex;

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
        public async Task<FlexiPagedList<SearchCompanyUserDto>> SearchCompanyUser(SearchCompanyUserParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCompanyUser>().AssignParameters(@params).Fetch();
        }
    }
}