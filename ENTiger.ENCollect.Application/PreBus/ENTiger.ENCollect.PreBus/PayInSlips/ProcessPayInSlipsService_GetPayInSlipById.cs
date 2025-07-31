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
        public async Task<GetPayInSlipByIdDto> GetPayInSlipById(GetPayInSlipByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPayInSlipById>().AssignParameters(@params).Fetch();
        }
    }
}