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
        public virtual async Task<IEnumerable<GetExecutionHistoryDetailsDto>> GetExecutionHistoryDetails(GetExecutionHistoryDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetExecutionHistoryDetails>().AssignParameters(@params).Fetch();
        }
    }
}