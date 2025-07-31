namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async virtual Task<GetCustomerConsentByTokenDto> GetCustomerConsentByTokenAsync(GetCustomerConsentByTokenParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCustomerConsentByToken>().AssignParameters(@params).Fetch();
        }
    }
}
