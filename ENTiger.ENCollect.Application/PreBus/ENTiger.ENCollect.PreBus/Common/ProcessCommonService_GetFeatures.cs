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
        public async Task<IEnumerable<GetFeaturesDto>> GetFeatures(GetFeaturesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetFeatures>().AssignParameters(@params).Fetch();
        }
    }
}