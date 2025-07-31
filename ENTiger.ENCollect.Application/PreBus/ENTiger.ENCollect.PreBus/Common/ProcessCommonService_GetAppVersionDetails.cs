namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetAppVersionDetailsDto> GetAppVersionDetails(GetAppVersionDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAppVersionDetails>().AssignParameters(@params).Fetch();
        }
    }
}