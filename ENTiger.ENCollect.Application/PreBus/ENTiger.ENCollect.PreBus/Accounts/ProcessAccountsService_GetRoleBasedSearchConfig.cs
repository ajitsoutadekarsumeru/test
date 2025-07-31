using System.Collections.Generic;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<GetRoleBasedSearchConfigDto>> GetRoleBasedSearchConfig(GetRoleBasedSearchConfigParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetRoleBasedSearchConfig>().AssignParameters(@params).Fetch();
        }
    }
}
