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
        public async Task<FlexiPagedList<GetCollectionsForCancellationDto>> GetCollectionsForCancellation(GetCollectionsForCancellationParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollectionsForCancellation>().AssignParameters(@params).Fetch();
        }
    }
}