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
        public async Task<IEnumerable<GetUserPerformanceBandsDto>> GetUserPerformanceBands(GetUserPerformanceBandsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUserPerformanceBands>().AssignParameters(@params).Fetch();
        }
    }
}