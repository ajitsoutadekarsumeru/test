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
        public async Task<IEnumerable<agencylistDto>> agencylist(agencylistParams @params)
        {
            return await _flexHost.GetFlexiQuery<agencylist>().AssignParameters(@params).Fetch();
        }
    }
}