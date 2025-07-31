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
        public async Task<GetBirdEyeViewDto> GetBirdEyeView(GetBirdEyeViewParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBirdEyeView>().AssignParameters(@params).Fetch();
        }
    }
}