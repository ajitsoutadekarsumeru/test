
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
        public virtual async Task<GetPermissionSchemeByIdDto> GetPermissionSchemeById(GetPermissionSchemeByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPermissionSchemeById>().AssignParameters(@params).Fetch();
        }
    }
}
