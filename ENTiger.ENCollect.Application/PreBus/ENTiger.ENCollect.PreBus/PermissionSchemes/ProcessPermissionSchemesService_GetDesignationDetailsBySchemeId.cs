
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
        public virtual async Task<GetDesignationDetailsBySchemeIdDto> GetDesignationDetailsBySchemeId(GetDesignationDetailsBySchemeIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDesignationDetailsBySchemeId>().AssignParameters(@params).Fetch();
        }
    }
}
