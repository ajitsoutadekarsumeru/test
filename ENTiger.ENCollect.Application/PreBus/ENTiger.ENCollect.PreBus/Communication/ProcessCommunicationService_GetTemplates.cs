using System.Collections.Generic;

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
        public async Task<IEnumerable<GetTemplatesDto>> GetTemplates(GetTemplatesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTemplates>().AssignParameters(@params).Fetch();
        }
    }
}
