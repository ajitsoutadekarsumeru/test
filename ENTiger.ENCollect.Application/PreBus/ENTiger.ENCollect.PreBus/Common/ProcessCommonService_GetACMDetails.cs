namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetACMDetailsDto>> GetACMDetails(GetACMDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetACMDetails>().AssignParameters(@params).Fetch();
        }
    }
}