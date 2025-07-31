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
        public virtual async Task<GetNonSegmentedAndTreatmentedDto> GetNonSegmentedAndTreatmented(GetNonSegmentedAndTreatmentedParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetNonSegmentedAndTreatmented>().AssignParameters(@params).Fetch();
        }
    }
}