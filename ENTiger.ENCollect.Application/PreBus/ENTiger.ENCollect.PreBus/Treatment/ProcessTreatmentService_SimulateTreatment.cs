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
        public virtual async Task<IEnumerable<SimulateTreatmentDto>> SimulateTreatment(SimulateTreatmentParams @params)
        {
            return await _flexHost.GetFlexiQuery<SimulateTreatment>().AssignParameters(@params).Fetch();
        }
    }
}