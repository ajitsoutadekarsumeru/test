namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetAckBatchesDto>> GetAckBatches(GetAckBatchesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAckBatches>().AssignParameters(@params).Fetch();
        }
    }
}