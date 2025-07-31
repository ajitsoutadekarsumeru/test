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
        public async Task<IEnumerable<GetRegionListDto>> GetRegionList(GetRegionListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetRegionList>().AssignParameters(@params).Fetch();
        }
    }
}