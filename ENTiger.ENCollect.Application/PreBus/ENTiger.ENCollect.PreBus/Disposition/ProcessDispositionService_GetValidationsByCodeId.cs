namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessDispositionService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetValidationsByCodeIdDto>> GetValidationsByCodeId(GetValidationsByCodeIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetValidationsByCodeId>().AssignParameters(@params).Fetch();
        }
    }
}