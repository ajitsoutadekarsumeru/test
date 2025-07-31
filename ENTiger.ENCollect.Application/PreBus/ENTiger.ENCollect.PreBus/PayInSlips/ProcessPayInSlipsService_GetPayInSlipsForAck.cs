using Sumeru.Flex;

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
        public async Task<FlexiPagedList<GetPayInSlipsForAckDto>> GetPayInSlipsForAck(GetPayInSlipsForAckParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPayInSlipsForAck>().AssignParameters(@params).Fetch();
        }
    }
}