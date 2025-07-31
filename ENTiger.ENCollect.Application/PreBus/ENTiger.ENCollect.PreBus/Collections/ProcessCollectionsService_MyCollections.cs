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
        public async Task<MyCollectionsDto> MyCollections(MyCollectionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<MyCollections>().AssignParameters(@params).Fetch();
        }
    }
}