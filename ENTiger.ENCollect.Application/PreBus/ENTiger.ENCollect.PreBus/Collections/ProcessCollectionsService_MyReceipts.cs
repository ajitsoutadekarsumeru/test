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
        public async Task<FlexiPagedList<MyReceiptsDto>> MyReceipts(MyReceiptsParams @params)
        {
            return await _flexHost.GetFlexiQuery<MyReceipts>().AssignParameters(@params).Fetch();
        }
    }
}