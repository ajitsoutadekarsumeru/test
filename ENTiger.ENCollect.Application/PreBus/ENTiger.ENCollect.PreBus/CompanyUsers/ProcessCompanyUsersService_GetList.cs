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
        public async Task<IEnumerable<GetListsDto>> GetList(GetListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetList>().AssignParameters(@params).Fetch();
        }
    }
}