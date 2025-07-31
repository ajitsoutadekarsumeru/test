using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<FlexiPagedList<SearchTriggersDto>> SearchTriggers(SearchTriggersParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchTriggers>().AssignParameters(@params).Fetch();
        }
    }
    
}
