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
        public async Task<IEnumerable<GetBanksByNameDto>> GetBanksByName(GetBanksByNameParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetBanksByName>().AssignParameters(@params).Fetch();
        }
    }
}