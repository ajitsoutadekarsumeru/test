using System.Collections;
using System.Collections.Generic;

namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessPermissionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetPermissionsDto>> GetPermissions(GetPermissionsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPermissions>().AssignParameters(@params).Fetch();
        }
    }
}
