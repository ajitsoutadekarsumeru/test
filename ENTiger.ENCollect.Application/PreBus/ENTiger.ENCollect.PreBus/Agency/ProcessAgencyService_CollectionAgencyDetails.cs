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
        public async Task<CollectionAgencyDetailsDto> CollectionAgencyDetails(CollectionAgencyDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<CollectionAgencyDetails>().AssignParameters(@params).Fetch();
        }
    }
}