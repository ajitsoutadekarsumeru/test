namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeographyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetStatesByRegionIdDto>> GetStatesByRegionId(GetStatesByRegionIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetStatesByRegionId>().AssignParameters(@params).Fetch();
        }
    }
}