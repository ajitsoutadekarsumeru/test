using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<GetMoneyMovementDetailsDto>> GetMoneyMovementDetails(GetMoneyMovementDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMoneyMovementDetails>().AssignParameters(@params).Fetch();
        }
    }
}
