namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessGeographyService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetStateListDto>> GetStateList(GetStateListParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetStateList>().AssignParameters(@params).Fetch();
        }
    }
}