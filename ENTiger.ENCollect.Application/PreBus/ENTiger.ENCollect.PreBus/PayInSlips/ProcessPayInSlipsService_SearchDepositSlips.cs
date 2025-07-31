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
        public async Task<FlexiPagedList<SearchDepositSlipsDto>> SearchDepositSlips(SearchDepositSlipsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchDepositSlips>().AssignParameters(@params).Fetch();
        }
    }
}