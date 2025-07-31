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
        public async Task<IEnumerable<BankAccountTypesDto>> BankAccountTypes(BankAccountTypesParams @params)
        {
            return await _flexHost.GetFlexiQuery<BankAccountTypes>().AssignParameters(@params).Fetch();
        }
    }
}