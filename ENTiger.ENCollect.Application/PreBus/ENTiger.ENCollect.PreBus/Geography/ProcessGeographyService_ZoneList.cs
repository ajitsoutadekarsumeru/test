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
        public async Task<IEnumerable<ZoneListDto>> ZoneList(ZoneListParams @params)
        {
            return await _flexHost.GetFlexiQuery<ZoneList>().AssignParameters(@params).Fetch();
        }
    }
}