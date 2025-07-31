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
        public async Task<IEnumerable<GetPayInSlipsDto>> GetPayInSlips(GetPayInSlipsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPayInSlips>().AssignParameters(@params).Fetch();
        }
    }
}