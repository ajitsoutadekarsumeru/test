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
        public async Task<GetAccountabilityDto> GetAccountability(GetAccountabilityParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAccountability>().AssignParameters(@params).Fetch();
        }
    }
}