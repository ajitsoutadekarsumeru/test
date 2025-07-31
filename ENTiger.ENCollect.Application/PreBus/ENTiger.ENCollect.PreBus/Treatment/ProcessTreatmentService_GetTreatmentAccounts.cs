namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<GetTreatmentAccountsDto> GetTreatmentAccounts(GetTreatmentAccountsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTreatmentAccounts>().AssignParameters(@params).Fetch();
        }
    }
}