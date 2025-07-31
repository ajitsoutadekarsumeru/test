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
        public async Task<IEnumerable<BaseBranchListDto>> BaseBranchList(BaseBranchListParams @params)
        {
            return await _flexHost.GetFlexiQuery<BaseBranchList>().AssignParameters(@params).Fetch();
        }
    }
}