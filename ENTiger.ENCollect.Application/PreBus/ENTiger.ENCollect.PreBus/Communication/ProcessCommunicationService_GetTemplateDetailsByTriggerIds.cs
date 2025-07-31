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
        public virtual IEnumerable<GetTemplateDetailsByTemplateIdsDto> GetTemplateDetailsByTemplateIds(GetTemplateDetailsByTemplateIdsParams @params)
        {
            return _flexHost.GetFlexiQuery<GetTemplateDetailsByTemplateIds>().AssignParameters(@params).Fetch();
        }
    }
}
