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
        public async Task<IEnumerable<GetCollectionsDto>> GetCollections(GetCollectionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollections>().AssignParameters(@params).Fetch();
        }
    }
}