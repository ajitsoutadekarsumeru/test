using Sumeru.Flex;

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
        public virtual async Task<FlexiPagedList<SearchPermissionsDto>> SearchPermissions(SearchPermissionsParams @params)
        {
            return await  _flexHost.GetFlexiQuery<SearchPermissions>().AssignParameters(@params).Fetch();
        }
    }
}
