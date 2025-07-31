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
        public async Task<IEnumerable<BaseBranchByUserIdDto>> BaseBranchByUserId(BaseBranchByUserIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<BaseBranchByUserId>().AssignParameters(@params).Fetch();
        }
    }
}