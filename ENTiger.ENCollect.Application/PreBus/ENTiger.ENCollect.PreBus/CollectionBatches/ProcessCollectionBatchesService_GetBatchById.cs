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
        public async Task<GetBatchByIdDto> GetBatchById(GetBatchByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBatchById>().AssignParameters(@params).Fetch();
        }
    }
}