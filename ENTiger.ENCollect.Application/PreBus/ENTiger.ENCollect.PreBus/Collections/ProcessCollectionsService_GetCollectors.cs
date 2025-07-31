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
        public async Task<IEnumerable<GetCollectorsDto>> GetCollectors(GetCollectorsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetCollectors>().AssignParameters(@params).Fetch();
        }
    }
}