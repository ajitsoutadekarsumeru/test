using System.Collections.Generic;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessPermissionSchemesService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetPermissionSchemesDto>> GetPermissionSchemes(GetPermissionSchemesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPermissionSchemes>().AssignParameters(@params).Fetch();
        }
    }
}
