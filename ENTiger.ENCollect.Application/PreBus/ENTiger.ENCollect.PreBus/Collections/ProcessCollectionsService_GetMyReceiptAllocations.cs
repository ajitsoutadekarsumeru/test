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
        public async Task<IEnumerable<GetMyReceiptAllocationsDto>> GetMyReceiptAllocations(GetMyReceiptAllocationsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMyReceiptAllocations>().AssignParameters(@params).Fetch();
        }
    }
}