namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<IEnumerable<GetUserByNameDto>> GetUsersByName(GetUsersByNameParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUsersByName>().AssignParameters(@params).Fetch();
        }
    }
}