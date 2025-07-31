using System.Collections.Generic;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessHierarchyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetMastersByParentIdsDto>> GetMastersByParentIds(GetMastersByParentIdsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetMastersByParentIds>().AssignParameters(@params).Fetch();
        }
    }
}
