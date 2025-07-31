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
        public virtual async Task<IEnumerable<GetUnSequencedTreatmentsDto>> GetUnSequencedTreatments(GetUnSequencedTreatmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUnSequencedTreatments>().AssignParameters(@params).Fetch();
        }
     }
}