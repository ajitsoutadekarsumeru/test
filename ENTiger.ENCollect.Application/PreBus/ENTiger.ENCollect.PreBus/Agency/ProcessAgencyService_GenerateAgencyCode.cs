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
        public async Task<GenerateAgencyCodeDto> GenerateAgencyCode(GenerateAgencyCodeParams @params)
        {
            return await _flexHost.GetFlexiQuery<GenerateAgencyCode>().AssignParameters(@params).Fetch();
        }
    }
}