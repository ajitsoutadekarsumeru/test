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
        public virtual async Task<SearchTreatmentsDto> SearchTreatments(SearchTreatmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchTreatments>().AssignParameters(@params).Fetch();
        }
    }
}