namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetStatusListDto>> GetStatusList(GetStatusListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetStatusList>().AssignParameters(@params).Fetch();
        }
    }
}