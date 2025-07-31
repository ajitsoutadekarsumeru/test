namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeoMasterService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchBaseBranchesDto>> SearchBaseBranches(SearchBaseBranchesParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchBaseBranches>().AssignParameters(@params).Fetch();
        }
    }
}