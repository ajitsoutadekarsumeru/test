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
        public async Task<FlexiPagedList<SearchMyDepositSlipsDto>> SearchMyDepositSlips(SearchMyDepositSlipsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchMyDepositSlips>().AssignParameters(@params).Fetch();
        }
    }
}