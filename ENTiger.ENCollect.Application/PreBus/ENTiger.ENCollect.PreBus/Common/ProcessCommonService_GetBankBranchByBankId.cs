namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetBankBranchByBankIdDto>> GetBankBranchByBankId(GetBankBranchByBankIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBankBranchByBankId>().AssignParameters(@params).Fetch();
        }
    }
}