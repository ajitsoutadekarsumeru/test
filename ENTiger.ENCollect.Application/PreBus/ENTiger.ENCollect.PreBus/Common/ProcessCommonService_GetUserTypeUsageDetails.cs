
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
        public async Task<GetUserTypeUsageDetailsDto> GetUserTypeUsageDetails(GetUserTypeUsageDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUserTypeUsageDetails>().AssignParameters(@params).Fetch();
        }
    }
}
