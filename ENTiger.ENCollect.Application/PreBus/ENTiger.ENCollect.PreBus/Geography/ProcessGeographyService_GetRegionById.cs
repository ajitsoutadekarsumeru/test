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
        public async Task<GetRegionByIdDto> GetRegionById(GetRegionByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetRegionById>().AssignParameters(@params).Fetch();
        }
    }
}