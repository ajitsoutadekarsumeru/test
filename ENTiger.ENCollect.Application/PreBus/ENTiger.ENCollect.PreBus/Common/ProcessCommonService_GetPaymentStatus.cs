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
        public async Task<IEnumerable<GetPaymentStatusDto>> GetPaymentStatus(GetPaymentStatusParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPaymentStatus>().AssignParameters(@params).Fetch();
        }
    }
}