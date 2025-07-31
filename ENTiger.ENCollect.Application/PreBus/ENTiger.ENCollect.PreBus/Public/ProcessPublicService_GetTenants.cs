namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessPublicService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetTenantsDto>> GetTenants(GetTenantsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTenants>().AssignParameters(@params).Fetch();
        }
    }
}