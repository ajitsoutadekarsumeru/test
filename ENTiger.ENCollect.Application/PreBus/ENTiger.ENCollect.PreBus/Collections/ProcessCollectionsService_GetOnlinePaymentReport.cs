namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetOnlinePaymentReportDto>> GetOnlinePaymentReport(GetOnlinePaymentReportParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetOnlinePaymentReport>().AssignParameters(@params).Fetch();
        }
    }
}