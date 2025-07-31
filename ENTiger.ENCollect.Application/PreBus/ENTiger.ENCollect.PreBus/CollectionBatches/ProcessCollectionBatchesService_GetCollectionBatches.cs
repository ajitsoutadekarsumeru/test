namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCollectionBatchesService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetCollectionBatchDto> GetCollectionBatches(GetCollectionBatchParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollectionBatches>().AssignParameters(@params).Fetch();
        }
    }
}