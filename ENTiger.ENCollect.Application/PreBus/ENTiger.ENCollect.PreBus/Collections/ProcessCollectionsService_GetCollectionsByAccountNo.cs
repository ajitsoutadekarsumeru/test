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
        public async Task<IEnumerable<GetCollectionsByAccountNoDto>> GetCollectionsByAccountNo(GetCollectionsByAccountNoParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollectionsByAccountNo>().AssignParameters(@params).Fetch();
        }
    }
}