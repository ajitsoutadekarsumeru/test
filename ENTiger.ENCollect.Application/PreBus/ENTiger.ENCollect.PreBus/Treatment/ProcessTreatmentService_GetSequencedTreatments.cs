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
        public virtual async Task<IEnumerable<GetSequencedTreatmentsDto>> GetSequencedTreatments(GetSequencedTreatmentsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetSequencedTreatments>().AssignParameters(@params).Fetch();
        }
    }
}