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
        public async Task<IEnumerable<GetAgencyTypesDto>> GetAgencyTypes(GetAgencyTypesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetAgencyTypes>().AssignParameters(@params).Fetch();
        }
    }
}