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
        public async Task<BaseBranchByIdDto> BaseBranchById(BaseBranchByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<BaseBranchById>().AssignParameters(@params).Fetch();
        }
    }
}