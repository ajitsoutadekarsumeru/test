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
        public async Task<AgencyByCodeDto> AgencyByCode(AgencyByCodeParams @params)
        {
            return await _flexHost.GetFlexiQuery<AgencyByCode>().AssignParameters(@params).Fetch();
        }
    }
}