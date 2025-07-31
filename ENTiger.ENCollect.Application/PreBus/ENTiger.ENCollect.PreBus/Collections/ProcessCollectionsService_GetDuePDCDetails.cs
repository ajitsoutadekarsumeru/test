namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetDuePDCDetailsDto>> GetDuePDCDetails(GetDuePDCDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetDuePDCDetails>().AssignParameters(@params).Fetch();
        }
    }
}