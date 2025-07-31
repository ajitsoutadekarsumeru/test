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
        public async Task<GetCollectionByIdDto> GetCollectionById(GetCollectionByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollectionById>().AssignParameters(@params).Fetch();
        }
    }
}