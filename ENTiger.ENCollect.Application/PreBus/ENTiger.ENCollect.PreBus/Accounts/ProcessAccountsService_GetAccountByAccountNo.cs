namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetAccountByAccountNoDto> GetAccountByAccountNo(GetAccountByAccountNoParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountByAccountNo>().AssignParameters(@params).Fetch();
        }
    }
}