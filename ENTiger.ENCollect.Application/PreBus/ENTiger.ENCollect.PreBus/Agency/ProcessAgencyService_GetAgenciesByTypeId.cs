namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetAgenciesByTypeIdDto>> GetAgenciesByTypeId(GetAgenciesByTypeIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgenciesByTypeId>().AssignParameters(@params).Fetch();
        }
    }
}