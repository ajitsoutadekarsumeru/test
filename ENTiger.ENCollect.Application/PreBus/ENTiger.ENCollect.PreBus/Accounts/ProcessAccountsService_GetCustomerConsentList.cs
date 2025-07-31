namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {

        public async virtual Task<IEnumerable<GetCustomerConsentListDto>> GetCustomerConsentListAsync(GetCustomerConsentListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCustomerConsentList>().AssignParameters(@params).Fetch();
        }
    }
}
