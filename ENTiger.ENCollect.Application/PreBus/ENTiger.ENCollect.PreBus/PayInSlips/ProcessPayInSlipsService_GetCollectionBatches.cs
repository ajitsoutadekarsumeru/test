namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetCollectionBatchesDto>> GetCollectionBatches(GetCollectionBatchesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollectionBatches>().AssignParameters(@params).Fetch();
        }
    }
}