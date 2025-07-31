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
        public async Task<IEnumerable<SearchAccountImportFileStatusDto>> SearchAccountImportFileStatus(SearchAccountImportFileStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchAccountImportFileStatus>().AssignParameters(@params).Fetch();
        }
    }
}