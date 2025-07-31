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
        public async Task<GetTodaysViewDto> GetTodaysView(GetTodaysViewParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTodaysView>().AssignParameters(@params).Fetch();
        }
    }
}