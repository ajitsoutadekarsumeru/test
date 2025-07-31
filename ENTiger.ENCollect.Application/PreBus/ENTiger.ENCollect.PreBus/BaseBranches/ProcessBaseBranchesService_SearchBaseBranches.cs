namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessBaseBranchesService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchBaseBranchDto>> SearchBaseBranches(SearchBaseBranchParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchBaseBranches>().AssignParameters(@params).Fetch();
        }
    }
}