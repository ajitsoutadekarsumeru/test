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
        public virtual IEnumerable<GetTriggerDetailsByTriggerIdsDto> GetTriggerDetailsByTriggerIds(GetTriggerDetailsByTriggerIdsParams @params)
        {
            return _flexHost.GetFlexiQuery<GetTriggerDetailsByTriggerIds>().AssignParameters(@params).Fetch();
        }
    }
}
