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
        public async Task<IEnumerable<GetUsersByTypeDto>> GetUsersByType(GetUsersByTypeParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUsersByType>().AssignParameters(@params).Fetch();
        }
    }
}