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
        public async Task<IEnumerable<BankListDto>> BankList(BankListParams @params)
        {
            return await _flexHost.GetFlexiQuery<BankList>().AssignParameters(@params).Fetch();
        }
    }
}