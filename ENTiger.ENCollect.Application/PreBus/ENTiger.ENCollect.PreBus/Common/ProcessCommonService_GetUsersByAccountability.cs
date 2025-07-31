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
        public async Task<IEnumerable<GetUsersByAccountabilityDto>> GetUsersByAccountability(GetUsersByAccountabilityParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUsersByAccountability>().AssignParameters(@params).Fetch();
        }
    }
}