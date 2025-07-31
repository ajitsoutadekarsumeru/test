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
        public async Task<IEnumerable<GetAccountLabelsDto>> GetAccountLabels(GetAccountLabelsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountLabels>().AssignParameters(@params).Fetch();
        }
    }
}