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
        public async Task<IEnumerable<GetDepositBankListDto>> GetDepositBankList(GetDepositBankListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDepositBankList>().AssignParameters(@params).Fetch();
        }
    }
}