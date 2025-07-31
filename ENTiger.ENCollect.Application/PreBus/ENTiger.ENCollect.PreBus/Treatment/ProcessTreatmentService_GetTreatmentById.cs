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
        public virtual async Task<GetTreatmentByIdDto> GetTreatmentById(GetTreatmentByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTreatmentById>().AssignParameters(@params).Fetch();
        }
    }
}