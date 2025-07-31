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
        public async Task<IEnumerable<GetBatchesToPrintDto>> GetBatchesToPrint(GetBatchesToPrintParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBatchesToPrint>().AssignParameters(@params).Fetch();
        }
    }
}