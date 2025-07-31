namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {

        public async virtual Task<GetCustomerConsentByIdDto> GetCustomerConsentByIdAsync(GetCustomerConsentByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCustomerConsentById>().AssignParameters(@params).Fetch();
        }
    }
}
