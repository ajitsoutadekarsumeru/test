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
        public async Task<IEnumerable<GetAccountContactDetailsDto>> GetAccountContactDetails(GetAccountContactDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountContactDetails>().AssignParameters(@params).Fetch();
        }
    }
}